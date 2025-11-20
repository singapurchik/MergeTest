using UnityEngine;
using Zenject;
using System;

namespace MergeTest.Core
{
	public class Game : MonoBehaviour, IGameInitializeInfo
	{
		[Inject] private IPlayerInputInfo _playerInputInfo;
		
		public bool IsInitialized { get; private set; }
		
		public event Action OnInitialized;

		private void OnEnable()
		{
			_playerInputInfo.OnInputStarted += TryInitialize;
		}

		private void OnDisable()
		{
			_playerInputInfo.OnInputStarted -= TryInitialize;
		}

		private void TryInitialize()
		{
			if (!IsInitialized)
			{
				IsInitialized = true;
				OnInitialized?.Invoke();	
			}
		}
	}
}