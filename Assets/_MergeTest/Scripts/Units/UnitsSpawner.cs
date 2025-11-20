using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using MergeTest.Core;
using MergeTest.Units.Grid;
using UnityEngine;
using Zenject;

namespace MergeTest.Units
{
	public class UnitsSpawner : MonoBehaviour
	{
		[SerializeField] private float _startDelay = 1;
		[SerializeField] private float _spawnInterval = 3;

		[Inject] private IGameInitializeInfo _gameInitializeInfo;
		[Inject] private IReadOnlyList<UnitsPool> _unitsPools;
		[Inject] private IUnitsGridRegister _gridRegister;
		[Inject] private IUnitsGridInfo _gridInfo;

		private float _nextSpawnTime;
		private float _activateTime;
		private bool _isActive;

		private void OnEnable()
		{
			_gameInitializeInfo.OnInitialized += Initialize;
		}

		private void OnDisable()
		{
			_gameInitializeInfo.OnInitialized -= Initialize;
		}

		private void Initialize()
		{
			_activateTime = Time.timeSinceLevelLoad + _startDelay;
		}

		private void Spawn(IUnitsGridCell cell)
		{
			var unit = _unitsPools[Random.Range(0, _unitsPools.Count)].Get();
			unit.SetParent(cell.SpawnPoint);
			_gridRegister.AddUnitToCell(cell, unit.Id);
			cell.SetFull();
		}
		
		private void Update()
		{
			if (_isActive)
			{
				if (Time.timeSinceLevelLoad > _nextSpawnTime && _gridInfo.TryGetEmptyCell(out var cell))
				{
					Spawn(cell);
					_nextSpawnTime = Time.timeSinceLevelLoad + _spawnInterval;
				}
			}
			else if (_activateTime > 0 && Time.timeSinceLevelLoad > _activateTime)
			{
				_nextSpawnTime = 0;
				_isActive = true;
			}
		}
	}
}