using UnityEngine;
using Zenject;

namespace MergeTest.Units
{
	public class UnitAnimator
	{
		[Inject] private Animator _animator;
		
		private readonly int _isIdleAnimBool = Animator.StringToHash("isIdle");
		
		public void PlayIdleAnim() => _animator.SetBool(_isIdleAnimBool, true);

		public void StopIdleAnim() => _animator.SetBool(_isIdleAnimBool, false);

		public void ChangeAvatar(Avatar avatar)
		{
			_animator.avatar = avatar;
			// _animator.Rebind();
			// _animator.Update(0f);
		}
	}
}