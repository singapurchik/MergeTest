using MergeTest.Core;
using UnityEngine;
using System;

namespace MergeTest.Units
{
	[Serializable]
	public struct UnitCostume
	{
		[SerializeField] private GameObject _skin;
		[SerializeField] private Animator _animator;
			
		public Animator Animator => _animator;

		public bool TryDisable() => _skin.TryDisable();
			
		public bool TryEnable() => _skin.TryEnable();
	}
}