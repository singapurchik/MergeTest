using UnityEngine;
using System;
using Zenject;

namespace MergeTest.Core
{
	public class PlayerInput : MonoBehaviour, IPlayerInputInfo
	{
		[Inject] private Camera _mainCamera;

		public bool IsInputProcess { get; private set; }
		
		public event Action<RaycastHit> OnSelected;
		public event Action OnInputFinished;
		public event Action OnInputStarted;

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				IsInputProcess = true;
				OnInputStarted?.Invoke();
				
				var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
				
				if (Physics.Raycast(ray, out var hit))
					OnSelected?.Invoke(hit);
			}

			if (Input.GetMouseButtonUp(0))
			{
				IsInputProcess = false;
				OnInputFinished?.Invoke();
			}
		}
	}
}