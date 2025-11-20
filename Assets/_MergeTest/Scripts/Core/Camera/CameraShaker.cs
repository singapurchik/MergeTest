using Cinemachine;
using UnityEngine;

namespace MergeTest.Core.Cameras
{
	public class CameraShaker : MonoBehaviour, ICameraShaker
	{
		[SerializeField] private CinemachineImpulseSource _mergeUnitsImpulse;
		[SerializeField] private float _mergeUnitsImpulsePower = 0.5f;
	
		public void PlayerMergeUnitImpulse() => _mergeUnitsImpulse.GenerateImpulse(_mergeUnitsImpulsePower);
		
		private void PlayImpulse(CinemachineImpulseSource impulseSource, float power = 1)
			=> impulseSource.GenerateImpulse(power);
	}
}