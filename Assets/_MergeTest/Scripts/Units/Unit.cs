using System.Collections.Generic;
using MergeTest.Core;
using UnityEngine;
using System;

namespace MergeTest.Units
{
	public class Unit : MonoBehaviour, IUnit
	{
		[SerializeField] private List<GameObject> _bodies = new ();

		private int _currentLevel;
		
		public string Id { get; private set; }
		
		public event Action<Unit> OnMerged;

		public void Initialize()
		{
			Id = Guid.NewGuid().ToString();
			
			_currentLevel = 0;
			
			for (int i = 0; i < _bodies.Count; i++)
			{
				if (i == _currentLevel)
					_bodies[i].TryEnable();
				else
					_bodies[i].TryDisable();
			}
		}

		public void Take()
		{
		}

		public void Release()
		{
		}
	}
}