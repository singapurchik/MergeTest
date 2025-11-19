using UnityEngine;
using VInspector;
using Zenject;

namespace MergeTest.Core
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private PlayerInput _input;

		public override void InstallBindings()
		{
			Container.Bind<IPlayerInputInfo>().FromInstance(_input).AsSingle();
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_input = FindFirstObjectByType<PlayerInput>(FindObjectsInactive.Include);
		}
#endif
	}
}