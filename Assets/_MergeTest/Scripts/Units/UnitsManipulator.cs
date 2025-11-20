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
		
		[Inject] private UnitsGridRegister _unitsGridRegister;
		[Inject] private IReadOnlyUnitsHolder _unitsHolder;
		[Inject] private IPlayerInputInfo _inputInfo;
		[Inject] private Camera _mainCamera;
		
		private Unit _selectedUnit;

		private const float MAX_RAY_DISTANCE = 200f;

		private void OnEnable()
		{
			_inputInfo.OnInputStarted += TrySelectUnit;
		}

		private void OnDisable()
		{
			_inputInfo.OnInputStarted -= TrySelectUnit;
		}

		private void TrySelectUnit()
		{
			var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
				
			if (Physics.Raycast(ray, out var hit) && hit.transform.TryGetComponent(out IUnitsGridCell cell)
			    && !cell.IsEmpty && _unitsGridRegister.TryGetUnit(cell, out var unitId))
					_selectedUnit = _unitsHolder.Units[unitId];
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