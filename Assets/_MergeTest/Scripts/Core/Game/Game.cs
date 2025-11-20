using System;
using UnityEngine;

namespace MergeTest.Core
{
	public class Game : MonoBehaviour, IGameInitializeInfo
	{
		public event Action OnInitialized;

		private void Start()
		{
			OnInitialized?.Invoke();
		}
	}
}