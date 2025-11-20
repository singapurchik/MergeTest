using UnityEngine;
using System;

namespace MergeTest.Units
{
	[Serializable]
	public struct UnitVisualEffectsPlayer
	{
		[SerializeField] private ParticleSystem _levelUpEffect;

		public void Initialize()
		{
			_levelUpEffect.transform.SetParent(null);
		}

		public void PlayLevelUp(Vector3 position)
		{
			_levelUpEffect.transform.position = position;
			_levelUpEffect.Play();
		}
	}
}