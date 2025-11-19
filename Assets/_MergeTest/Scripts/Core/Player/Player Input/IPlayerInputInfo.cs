using UnityEngine;
using System;

namespace MergeTest.Core
{
	public interface IPlayerInputInfo
	{
		public bool IsInputProcess { get; }
		
		public event Action<RaycastHit> OnSelected;
		public event Action OnInputFinished;
		public event Action OnInputStarted;
	}
}