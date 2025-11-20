using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

namespace MergeTest.Units
{
	public class Unit : MonoBehaviour
	{
		[SerializeField] private UnitType _type;
		[SerializeField] private List<UnitCostume> _costumes = new ();
		[SerializeField] private float _takeScaleMultiplier = 1.5f;
		[Space(5)]
		[SerializeField] private UnitVisualEffectsPlayer _visualEffectsPlayer;
		
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
			_visualEffectsPlayer.Initialize();
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
					_animator.SetAnimator(costume.Animator);
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
			_visualEffectsPlayer.PlayLevelUp(transform.position + Vector3.up);
			UpdateCostume();
		}
		
		public void Select()
		{
			_animator.PlayMoveAnim();
			transform.localScale *= _takeScaleMultiplier;
		}

		public void UnSelect()
		{
			_animator.StopMoveAnim();
			transform.localScale = _defaultScale;
		}
		
		public void DestroyUnit()
		{
			UnSelect();
			OnDestroyed?.Invoke(this);
		}
	}
}