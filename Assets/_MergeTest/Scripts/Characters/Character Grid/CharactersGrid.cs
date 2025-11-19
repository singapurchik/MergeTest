using System.Collections.Generic;
using MergeTest.Core;
using UnityEngine;
using Zenject;

namespace MergeTest.Characters.Grid
{
	public class CharactersGrid : MonoBehaviour, ICharacterGridInfo
	{
		[Inject] private IReadOnlyList<CharactersGridTile> _tiles;
		[Inject] private IPlayerInputInfo _inputInfo;
		[Inject] private IObjectHolder _objectHolder;
		
		public bool IsHasEmptyTile => _emptyTiles.Count > 0;
		
		private CharactersGridTile _selectedTile;

		private Queue<CharactersGridTile> _emptyTiles;

		private void Awake()
		{
			_emptyTiles = new Queue<CharactersGridTile>(_tiles.Count);
			
			for (int i = 0; i < _tiles.Count; i++)
				_emptyTiles.Enqueue(_tiles[i]);
		}

		private void OnEnable()
		{
			_inputInfo.OnSelected += OnSelected;
		}

		private void OnSelected(RaycastHit hit)
		{
			if (hit.transform.TryGetComponent(out CharactersGridTile tile) && tile.IsHoldingObject)
			{
				_selectedTile = tile;
				_objectHolder.Take(_selectedTile.CurrentHoldableObject);
			}
		}
		
		public bool TryGetEmptyTile(out ICharacterGridTile tile)
		{
			if (_emptyTiles.Count > 0)
			{
				tile = _emptyTiles.Dequeue();
				return true;
			}
			
			tile = null;
			return false;
		}
	}
}