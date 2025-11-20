using UnityEngine;

namespace MergeTest.Units.Grid
{
public sealed class UnitsGridPlacement
{
	private readonly IUnitsGridRegister _unitsGridRegister;
	private readonly IReadOnlyUnitsHolder _unitsHolder;
	private readonly SelectedUnitState _selectionState;

	public UnitsGridPlacement(
		IUnitsGridRegister unitsGridRegister,
		IReadOnlyUnitsHolder unitsHolder,
		SelectedUnitState selectionState)
	{
		_unitsGridRegister = unitsGridRegister;
		_unitsHolder = unitsHolder;
		_selectionState = selectionState;
	}

	public void TryPlaceSelectedUnit(IUnitsGridCell targetCell)
	{
		if (!_selectionState.HasSelection)
			return;

		var selectedUnit = _selectionState.Unit;
		var fromCell = _selectionState.Cell;

		if (targetCell == null)
		{
			ReturnUnitToParentCell(selectedUnit);
			selectedUnit.UnSelect();
			_selectionState.Clear();
			return;
		}

		if (targetCell.IsEmpty)
		{
			MoveUnitToNewCell(selectedUnit, fromCell, targetCell);
			selectedUnit.UnSelect();
			_selectionState.Clear();
			return;
		}

		if (targetCell == fromCell)
		{
			ReturnUnitToParentCell(selectedUnit);
			selectedUnit.UnSelect();
			_selectionState.Clear();
			return;
		}

		if (!_unitsGridRegister.TryGetUnitIdFromCell(targetCell, out var unitId) ||
		    !_unitsHolder.Units.TryGetValue(unitId, out var targetUnit))
		{
			ReturnUnitToParentCell(selectedUnit);
			selectedUnit.UnSelect();
			_selectionState.Clear();
			return;
		}

		var canMerge =
			targetUnit.Type == selectedUnit.Type &&
			targetUnit.Level == selectedUnit.Level &&
			targetUnit.Level < targetUnit.MaxLevel;

		if (canMerge)
		{
			DestroySelectedUnit(selectedUnit, fromCell);
			targetUnit.LevelUp();
		}
		else
		{
			ReturnUnitToParentCell(selectedUnit);
			selectedUnit.UnSelect();
		}

		_selectionState.Clear();
	}

	private void DestroySelectedUnit(Unit selectedUnit, IUnitsGridCell fromCell)
	{
		selectedUnit.DestroyUnit();
		_unitsGridRegister.RemoveUnitFromCell(fromCell);
		fromCell.SetEmpty();
	}

	private void MoveUnitToNewCell(Unit selectedUnit, IUnitsGridCell fromCell, IUnitsGridCell toCell)
	{
		fromCell.SetEmpty();
		_unitsGridRegister.RemoveUnitFromCell(fromCell);

		toCell.SetFull();
		_unitsGridRegister.AddUnitToCell(toCell, selectedUnit.Id);
		selectedUnit.SetParent(toCell.SpawnPoint);
	}

	private static void ReturnUnitToParentCell(Unit selectedUnit)
	{
		selectedUnit.transform.localPosition = Vector3.zero;
	}
}
}
