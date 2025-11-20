using System.Collections.Generic;
using MergeTest.Core;
using UnityEngine;
using Zenject;

namespace MergeTest.Units.Grid
{
	public class UnitsGrid : MonoBehaviour, IUnitsGridInfo
	{
		[Inject] private IReadOnlyList<UnitsGridCell> _tiles;
		[Inject] private IPlayerInputInfo _inputInfo;
		
		public bool IsHasEmptyTile => _emptyTiles.Count > 0;
		
		private UnitsGridCell _selectedCell;

		private Queue<UnitsGridCell> _emptyTiles;

		private void Awake()
		{
			_emptyTiles = new Queue<UnitsGridCell>(_tiles.Count);
			
			for (int i = 0; i < _tiles.Count; i++)
				_emptyTiles.Enqueue(_tiles[i]);
		}
		
		public bool TryGetEmptyTilePoint(out Transform tilePoint)
		{
			if (_emptyTiles.Count > 0)
			{
				var tile = _emptyTiles.Dequeue();
				tilePoint = tile.SpawnPoint;
				return true;
			}
			
			tilePoint = null;
			return false;
		}
	}
}