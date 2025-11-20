using MergeTest.Units.Grid;
using MergeTest.Core;
using UnityEngine;
using Zenject;

namespace MergeTest.Units
{
	public class UnitsManipulator : MonoBehaviour
	{
		[SerializeField] private LayerMask _groundLayer;
		[SerializeField] private float _unitMoveSpeed = 20f;
		
		[Inject] private IUnitsGridRegister _unitsGridRegister;
		[Inject] private IReadOnlyUnitsHolder _unitsHolder;
		[Inject] private IPlayerInputInfo _inputInfo;
		[Inject] private Camera _mainCamera;
		
		private IUnitsGridCell _selectedCell;
		private Unit _selectedUnit;

		private const float MAX_RAY_DISTANCE = 200f;

		private void OnEnable()
		{
			_inputInfo.OnInputFinished += TryReleaseUnit;
			_inputInfo.OnInputStarted += TryTakeUnit;
		}

		private void OnDisable()
		{
			_inputInfo.OnInputFinished -= TryReleaseUnit;
			_inputInfo.OnInputStarted -= TryTakeUnit;
		}

		private void TryTakeUnit()
		{
			var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out var hit)
			    && hit.transform.TryGetComponent(out IUnitsGridCell cell)
			    && !cell.IsEmpty && _unitsGridRegister.TryGetUnitIdFromCell(cell, out var unitId))
			{
				_selectedCell = cell;
				_selectedUnit = _unitsHolder.Units[unitId];
			}
		}

		private void TryReleaseUnit()
		{
			if (_selectedUnit != null)
			{
				var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

				if (Physics.Raycast(ray, out var hit) && hit.transform.TryGetComponent(out IUnitsGridCell cell))
				{
					if (cell.IsEmpty)
					{
						SetUnitToNewCell(cell);
					}
					else if (cell == _selectedCell)
					{
						ReturnUnitToParentCell();
					}
					else
					{
						_unitsGridRegister.TryGetUnitIdFromCell(cell, out var unitId);
						var newCellUnit = _unitsHolder.Units[unitId];

						if (newCellUnit.Type == _selectedUnit.Type
						    && newCellUnit.Level == _selectedUnit.Level && newCellUnit.Level < newCellUnit.MaxLevel)
						{
							DestroyUnit();
							newCellUnit.LevelUp();
						}
						else
						{
							ReturnUnitToParentCell();
						}
					}
				}
				else
				{
					ReturnUnitToParentCell();
				}
			}
		}

		private void DestroyUnit()
		{
			_selectedUnit.DestroyUnit();
			_selectedUnit = null;
			_unitsGridRegister.TryRemoveUnitFromCell(_selectedCell);
			_selectedCell.SetEmpty();
			_selectedCell = null;
		}

		private void SetUnitToNewCell(IUnitsGridCell cell)
		{
			_selectedCell.SetEmpty();
			_unitsGridRegister.TryRemoveUnitFromCell(_selectedCell);
			_selectedCell = null;
						
			cell.SetFull();
			_unitsGridRegister.AddUnitToCell(cell, _selectedUnit.Id);
			_selectedUnit.SetParent(cell.SpawnPoint);
			_selectedUnit = null;
		}

		private void ReturnUnitToParentCell()
		{
			_selectedUnit.transform.localPosition = Vector3.zero;
			_selectedUnit = null;
			_selectedCell = null;
		}
		
		private void MoveSelectedUnitToMouse()
		{
			var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
			
			if (Physics.Raycast(ray, out var hit, MAX_RAY_DISTANCE, _groundLayer))
			{
				var target = hit.point;

				target.y = _selectedUnit.transform.position.y;

				_selectedUnit.transform.position = Vector3.Lerp(
					_selectedUnit.transform.position, target, _unitMoveSpeed * Time.deltaTime);
			}
		}

		private void Update()
		{
			if (_inputInfo.IsInputProcess && _selectedUnit != null)
				MoveSelectedUnitToMouse();
		}
	}
}