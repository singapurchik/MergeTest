using UnityEngine;

namespace MergeTest.Units
{
	public sealed class SelectedUnitGroundMover
	{
		private readonly SelectedUnitState _selectionState;
		private readonly float _moveSpeed;

		public SelectedUnitGroundMover(SelectedUnitState selectionState, float moveSpeed)
		{
			_selectionState = selectionState;
			_moveSpeed = moveSpeed;
		}

		public void MoveSelectedUnit(Vector3 point, float deltaTime)
		{
			if (!_selectionState.HasSelection)
				return;

			var unit = _selectionState.Unit;
			var current = unit.transform.position;

			var target = point;
			target.y = current.y;

			unit.transform.position = Vector3.Lerp(current, target, _moveSpeed * deltaTime);
		}
	}
}