using UnityEngine;
using System;

namespace MergeTest.Core
{
	public interface IPlayerInputInfo
	{
		public Vector2 PointerScreenPosition { get; }
		
		public bool IsInputProcess { get; }
		
		public event Action OnInputFinished;
		public event Action OnInputStarted;
	}
}