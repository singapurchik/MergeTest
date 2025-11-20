using System.Collections.Generic;
using MergeTest.Core;
using System.Linq;
using UnityEngine;
using Zenject;

namespace MergeTest.Units.Grid
{
	public class UnitsGrid : MonoBehaviour, IUnitsGridInfo, IUnitsGridRegister
	{
		[Inject] private IReadOnlyList<UnitsGridCell> _cells;
		[Inject] private IPlayerInputInfo _inputInfo;
		
		private readonly Dictionary<IUnitsGridCell, string> _units = new (9);
		
		public bool IsHasEmptyTile => _emptyCells.Count > 0;

		private UnitsGridCell _selectedCell;

		private HashSet<IUnitsGridCell> _emptyCells;

		private void Awake()
		{
			_emptyCells = new HashSet<IUnitsGridCell>(_cells.Count);

			for (int i = 0; i < _cells.Count; i++)
			{
				var cell = _cells[i];
				AddEmptyCell(cell);
				cell.OnFull += RemoveEmptyCell;
				cell.OnEmpty += AddEmptyCell;
			}
		}

		private void OnDestroy()
		{
			for (int i = 0; i < _cells.Count; i++)
			{
				var cell = _cells[i];
				cell.OnFull -= RemoveEmptyCell;
				cell.OnEmpty -= AddEmptyCell;
			}
		}

		public bool TryGetUnitIdFromCell(IUnitsGridCell cell, out string unitId) => _units.TryGetValue(cell, out unitId);

		public void AddUnitToCell(IUnitsGridCell cell, string unitId) => _units.Add(cell, unitId);
		
		public bool TryRemoveUnitFromCell(IUnitsGridCell cell) => _units.Remove(cell);
		
		private void RemoveEmptyCell(IUnitsGridCell cell) => _emptyCells.Remove(cell);

		private void AddEmptyCell(IUnitsGridCell cell) => _emptyCells.Add(cell);
		
		public bool TryGetEmptyCell(out IUnitsGridCell cell)
		{
			if (_emptyCells.Count > 0)
			{
				cell = _emptyCells.First();
				_emptyCells.Remove(cell);
				return true;
			}
			
			cell = null;
			return false;
		}
	}
}