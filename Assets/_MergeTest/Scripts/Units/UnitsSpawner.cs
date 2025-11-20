using System.Collections.Generic;
using MergeTest.Units.Grid;
using UnityEngine;
using VInspector;
using Zenject;

namespace MergeTest.Units
{
	public class UnitsSpawner : MonoBehaviour
	{
		[SerializeField] private float _startDelay = 1;
		[SerializeField] private float _spawnInterval = 3;

		[Inject] private IReadOnlyList<UnitsPool> _charactersPools;
		[Inject] private IUnitsGridRegister _gridRegister;
		[Inject] private IUnitsGridInfo _gridInfo;

		private float _nextSpawnTime;
		private float _activateTime;
		private bool _isActive;

		[Button]
		private void TryActivate()
		{
			_activateTime = Time.timeSinceLevelLoad + _startDelay;
		}

		private void Spawn(IUnitsGridCell cell)
		{
			var unit = _charactersPools[Random.Range(0, _charactersPools.Count)].Get();
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
			else if (Time.timeSinceLevelLoad > _activateTime)
			{
				_nextSpawnTime = 0;
				_isActive = true;
			}
		}
	}
}