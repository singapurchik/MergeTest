using System.Collections.Generic;
using MergeTest.Core;
using UnityEngine;
using System;

namespace MergeTest.Units
{
	public class Unit : MonoBehaviour, IUnit
	{
		[SerializeField] private UnitType _type;
		[SerializeField] private List<GameObject> _skins = new ();

		public UnitType Type => _type;
		
		public string Id { get; private set; }

		public int Level { get; private set; } = 1;
		public int MaxLevel => _skins.Count;

		public event Action<Unit> OnDestroyed;

		public void Initialize()
		{
			Id = Guid.NewGuid().ToString();
			Level = 1;
			UpdateSkin();
		}

		private void UpdateSkin()
		{
			for (int i = 0; i < _skins.Count; i++)
			{
				if (i == Level - 1)
					_skins[i].TryEnable();
				else
					_skins[i].TryDisable();
			}
		}
		
		public void SetParent(Transform parent)
		{
			transform.SetParent(parent, false);
			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
		}

		public void LevelUp()
		{
			Level++;
			UpdateSkin();
		}

		public void DestroyUnit() => OnDestroyed?.Invoke(this);

		public void Take()
		{
		}

		public void Release()
		{
		}
	}
}