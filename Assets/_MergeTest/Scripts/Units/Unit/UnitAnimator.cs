using UnityEngine;

namespace MergeTest.Units
{
	public class UnitAnimator
	{
		private Animator _animator;
		
		private readonly int _isMovingAnimBool = Animator.StringToHash("isMoving");
		
		public void SetAnimator(Animator animator) => _animator = animator;
		
		public void PlayMoveAnim() => _animator.SetBool(_isMovingAnimBool, true);

		public void StopMoveAnim() => _animator.SetBool(_isMovingAnimBool, false);
	}
}