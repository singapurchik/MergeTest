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
		[SerializeField] private float _maxRayDistance = 200f;

		[Inject] private SelectedUnitGroundMover _groundMover;
		[Inject] private SelectedUnitState _selectionState;
		[Inject] private UnitsGridSelection _gridSelection;
		[Inject] private UnitsGridPlacement _gridPlacement;
		[Inject] private IPlayerInputInfo _inputInfo;
		[Inject] private Camera _mainCamera;

		private void OnEnable()
		{
			_inputInfo.OnInputStarted += HandlePointerDown;
			_inputInfo.OnInputFinished += HandlePointerUp;
		}

		private void OnDisable()
		{
			_inputInfo.OnInputStarted -= HandlePointerDown;
			_inputInfo.OnInputFinished -= HandlePointerUp;
		}

		private void HandlePointerDown()
		{
			var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out var hit, _maxRayDistance))
				_gridSelection.TrySelectUnit(hit);
		}

		private void HandlePointerUp()
		{
			if (!_selectionState.HasSelection)
				return;

			var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
			IUnitsGridCell targetCell = null;

			if (Physics.Raycast(ray, out var hit, _maxRayDistance) &&
			    hit.transform.TryGetComponent(out IUnitsGridCell cell))
			{
				targetCell = cell;
			}

			_gridPlacement.TryPlaceSelectedUnit(targetCell);
		}

		private void Update()
		{
			if (!_inputInfo.IsInputProcess || !_selectionState.HasSelection)
				return;

			var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

			if (!Physics.Raycast(ray, out var hit, _maxRayDistance, _groundLayer))
				return;

			_groundMover.MoveSelectedUnit(hit.point, Time.deltaTime);
		}
	}
}