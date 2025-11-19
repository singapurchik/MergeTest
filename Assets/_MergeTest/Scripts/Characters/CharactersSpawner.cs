using System.Collections.Generic;
using MergeTest.Characters.Grid;
using UnityEngine;
using VInspector;
using Zenject;

namespace MergeTest.Characters
{
	public class CharactersSpawner : MonoBehaviour
	{
		[SerializeField] private float _startDelay = 1;
		[SerializeField] private float _spawnInterval = 3;

		[Inject] private IReadOnlyList<CharactersPool> _charactersPools;
		[Inject] private ICharacterGridInfo _gridInfo;

		private float _nextSpawnTime;
		private float _activateTime;
		private bool _isActive;

		[Button]
		private void TryActivate()
		{
			_activateTime = Time.timeSinceLevelLoad + _startDelay;
		}

		private void Spawn(ICharacterGridTile tile)
		{
			var character = _charactersPools[Random.Range(0, _charactersPools.Count)].Get();
			character.Initialize(tile.SpawnPoint);
			tile.SetCharacter(character);
		}
		
		private void Update()
		{
			if (_isActive)
			{
				if (Time.timeSinceLevelLoad > _nextSpawnTime && _gridInfo.TryGetEmptyTile(out var tile))
				{
					Spawn(tile);
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