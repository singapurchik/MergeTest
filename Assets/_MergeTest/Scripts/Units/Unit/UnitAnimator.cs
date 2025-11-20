using UnityEngine;

namespace MergeTest.Units
{
	public class UnitAnimator
	{
		private Animator _animator;
		
		private readonly int _isIdleAnimBool = Animator.StringToHash("isIdle");
		
		public void SetAnimator(Animator animator) => _animator = animator;
		
		public void PlayIdleAnim() => _animator.SetBool(_isIdleAnimBool, true);

		public void StopIdleAnim() => _animator.SetBool(_isIdleAnimBool, false);
	}
}