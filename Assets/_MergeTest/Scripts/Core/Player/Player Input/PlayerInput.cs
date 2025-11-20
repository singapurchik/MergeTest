using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace MergeTest.Core
{
	public class PlayerInput : MonoBehaviour, IPlayerInputInfo
	{
		public bool IsInputProcess { get; private set; }
		public Vector2 PointerScreenPosition { get; private set; }

		public event Action OnInputFinished;
		public event Action OnInputStarted;

		private void Update()
		{
			GetPointerInput(out var position, out var button);

			if (button == null)
				return;

			PointerScreenPosition = position;

			if (button.wasPressedThisFrame)
			{
				IsInputProcess = true;
				OnInputStarted?.Invoke();
			}

			if (button.wasReleasedThisFrame)
			{
				IsInputProcess = false;
				OnInputFinished?.Invoke();
			}
		}

		private void GetPointerInput(out Vector2 position, out ButtonControl button)
		{
#if UNITY_EDITOR
			button = Mouse.current.leftButton;
			position = Mouse.current.position.ReadValue();
#elif UNITY_ANDROID ||  UNITY_IOS
				var touch = Touchscreen.current.primaryTouch;
				button = touch.press;
				position = touch.position.ReadValue();
#endif
		}
	}
}