using UnityEngine;

namespace MergeTest.Units.Grid
{
	public sealed class UnitsGridPlacement
	{
		private readonly IUnitsGridRegister _unitsGridRegister;
		private readonly IReadOnlyUnitsHolder _unitsHolder;
		private readonly SelectedUnitState _selectionState;

		public UnitsGridPlacement(IUnitsGridRegister unitsGridRegister, IReadOnlyUnitsHolder unitsHolder,
			SelectedUnitState selectionState)
		{
			_unitsGridRegister = unitsGridRegister;
			_selectionState = selectionState;
			_unitsHolder = unitsHolder;
		}

		public void TryPlaceSelectedUnit(IUnitsGridCell targetCell)
		{
			if (_selectionState.HasSelection)
			{
				var selectedUnit = _selectionState.Unit;
				var fromCell = _selectionState.Cell;

				bool shouldUnselect = true;

				if (targetCell == null)
				{
					ReturnUnitToParentCell(selectedUnit);
				}
				else if (targetCell.IsEmpty)
				{
					MoveUnitToNewCell(selectedUnit, fromCell, targetCell);
				}
				else if (targetCell == fromCell)
				{
					ReturnUnitToParentCell(selectedUnit);
				}
				else
				{
					if (_unitsGridRegister.TryGetUnitIdFromCell(targetCell, out var unitId) &&
					    _unitsHolder.Units.TryGetValue(unitId, out var targetUnit))
					{
						bool canMerge =
							targetUnit.Type == selectedUnit.Type &&
							targetUnit.Level == selectedUnit.Level &&
							targetUnit.Level < targetUnit.MaxLevel;

						if (canMerge)
						{
							DestroySelectedUnit(selectedUnit, fromCell);
							targetUnit.LevelUp();
							shouldUnselect = false;
						}
						else
						{
							ReturnUnitToParentCell(selectedUnit);
						}
					}
					else
					{
						ReturnUnitToParentCell(selectedUnit);
					}
				}

				if (shouldUnselect)
					selectedUnit.UnSelect();

				_selectionState.Clear();
			}
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