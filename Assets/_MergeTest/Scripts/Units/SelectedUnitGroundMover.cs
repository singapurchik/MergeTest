using UnityEngine;

namespace MergeTest.Units
{
	public sealed class SelectedUnitGroundMover
	{
		private readonly SelectedUnitState _selectionState;
		private readonly float _moveSpeed;

		private const float HORIZONTAL_OFFSET = 0.5f;

		public SelectedUnitGroundMover(SelectedUnitState selectionState, float moveSpeed)
		{
			_selectionState = selectionState;
			_moveSpeed = moveSpeed;
		}

		public void MoveSelectedUnit(Vector3 point, float deltaTime)
		{
			if (_selectionState.HasSelection)
			{
				var unit = _selectionState.Unit;
				var current = unit.transform.position;

				var target = point;
				target.y = _selectionState.Cell.SpawnPoint.position.y + HORIZONTAL_OFFSET;

				unit.transform.position = Vector3.Lerp(current, target, _moveSpeed * deltaTime);	
			}
		}
	}
}