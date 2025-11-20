using System.Collections.Generic;
using MergeTest.Core;
using UnityEngine;
using Zenject;
using System;

namespace MergeTest.Units
{
	public class Unit : MonoBehaviour
	{
		[Serializable]
		private struct Costume
		{
			[SerializeField] private GameObject _skin;
			[SerializeField] private Avatar _avatar;
			
			public Avatar Avatar => _avatar;

			public bool TryDisable() => _skin.TryDisable();
			
			public bool TryEnable() => _skin.TryEnable();
		}
		
		[SerializeField] private UnitType _type;
		[SerializeField] private List<Costume> _costumes = new ();
		[SerializeField] private float _takeScaleMultiplier = 1.5f;

		[Inject] private UnitAnimator _animator;
		
		public UnitType Type => _type;

		private Vector3 _defaultScale;
		
		public string Id { get; private set; }

		public int Level { get; private set; } = 1;
		public int MaxLevel => _costumes.Count;

		public event Action<Unit> OnDestroyed;

		private void Awake()
		{
			_defaultScale = transform.localScale;
		}

		public void Initialize()
		{
			Id = Guid.NewGuid().ToString();
			Level = 1;
			UpdateCostume();
		}

		private void UpdateCostume()
		{
			for (int i = 0; i < _costumes.Count; i++)
			{
				var costume = _costumes[i];
				
				if (i == Level - 1)
				{
					costume.TryEnable();
					_animator.ChangeAvatar(costume.Avatar);
				}
				else
				{
					costume.TryDisable();
				}
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
			UpdateCostume();
		}
		
		public void Take()
		{
			_animator.PlayIdleAnim();
			transform.localScale *= _takeScaleMultiplier;
		}

		public void Release()
		{
			_animator.StopIdleAnim();
			transform.localScale = _defaultScale;
		}
		
		public void DestroyUnit() => OnDestroyed?.Invoke(this);
	}
}