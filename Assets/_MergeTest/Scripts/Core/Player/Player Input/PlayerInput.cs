using UnityEngine.InputSystem;
using UnityEngine;
using System;

namespace MergeTest.Core
{
	public class PlayerInput : MonoBehaviour, IPlayerInputInfo
	{
		[SerializeField] private InputActionReference _clickAction;

		public bool IsInputProcess { get; private set; }
		public Vector2 PointerScreenPosition { get; private set; }

		public event Action OnInputFinished;
		public event Action OnInputStarted;

		private InputAction ClickAction => _clickAction;

		private void OnEnable()
		{
			ClickAction.started += OnPressStarted;
			ClickAction.canceled += OnPressCanceled;
			ClickAction.Enable();
		}

		private void OnDisable()
		{
			ClickAction.started -= OnPressStarted;
			ClickAction.canceled -= OnPressCanceled;
			ClickAction.Disable();
		}

		private void OnPressStarted(InputAction.CallbackContext context)
		{
			IsInputProcess = true;
			UpdatePointerPosition();
			OnInputStarted?.Invoke();
		}

		private void OnPressCanceled(InputAction.CallbackContext context)
		{
			IsInputProcess = false;
			UpdatePointerPosition();
			OnInputFinished?.Invoke();
		}

		private void UpdatePointerPosition() => PointerScreenPosition = Pointer.current.position.ReadValue();
		
		private void Update()
		{
			UpdatePointerPosition();
		}
	}
}