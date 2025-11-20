using UnityEngine;

namespace MergeTest.Units.Grid
{
	public sealed class UnitsGridSelection
	{
		private readonly IUnitsGridRegister _unitsGridRegister;
		private readonly IReadOnlyUnitsHolder _unitsHolder;
		private readonly SelectedUnitState _selectionState;

		public UnitsGridSelection(IUnitsGridRegister unitsGridRegister, IReadOnlyUnitsHolder unitsHolder,
			SelectedUnitState selectionState)
		{
			_unitsGridRegister = unitsGridRegister;
			_unitsHolder = unitsHolder;
			_selectionState = selectionState;
		}

		public void TrySelectUnit(RaycastHit hit)
		{
			if (!hit.transform.TryGetComponent(out IUnitsGridCell cell))
				return;

			if (cell.IsEmpty)
				return;

			if (!_unitsGridRegister.TryGetUnitIdFromCell(cell, out var unitId))
				return;

			if (!_unitsHolder.Units.TryGetValue(unitId, out var unit))
				return;

			_selectionState.Set(unit, cell);
			unit.Select();
		}
	}
}