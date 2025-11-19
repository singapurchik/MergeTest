using UnityEngine;
using VInspector;
using Zenject;

namespace MergeTest.Core
{
	public class ProjectInstaller : MonoInstaller
	{
		[SerializeField] private Camera _mainCamera;

		public override void InstallBindings()
		{
			Container.BindInstance(_mainCamera).AsSingle();
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_mainCamera = GetComponentInChildren<Camera>(true);
		}
#endif
	}
}