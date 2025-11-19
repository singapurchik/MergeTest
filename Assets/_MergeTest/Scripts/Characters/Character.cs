using System.Collections.Generic;
using MergeTest.Core;
using UnityEngine;
using System;

namespace MergeTest.Characters
{
	public class Character : MonoBehaviour, IHoldableObject
	{
		[SerializeField] private List<GameObject> _bodies = new ();

		private int _currentLevel;
		
		public event Action<Character> OnMerged;

		public void Initialize(Transform parent)
		{
			_currentLevel = 0;
			
			for (int i = 0; i < _bodies.Count; i++)
			{
				if (i == _currentLevel)
					_bodies[i].TryEnable();
				else
					_bodies[i].TryDisable();
			}

			transform.SetParent(parent,false);
		}

		public void Take()
		{
		}

		public void Release()
		{
		}
	}
}