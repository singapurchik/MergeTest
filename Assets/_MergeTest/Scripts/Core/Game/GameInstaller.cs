using MergeTest.Core.Cameras;
using UnityEngine;
using VInspector;
using Zenject;

namespace MergeTest.Core
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private CameraShaker _cameraShaker;
		[SerializeField] private PlayerInput _input;
		[SerializeField] private Game _game;

		public override void InstallBindings()
		{
			Container.Bind<ICameraShaker>().FromInstance(_cameraShaker).AsSingle();
			Container.Bind<IGameInitializeInfo>().FromInstance(_game).AsSingle();
			Container.Bind<IPlayerInputInfo>().FromInstance(_input).AsSingle();
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_cameraShaker = FindFirstObjectByType<CameraShaker>(FindObjectsInactive.Include);
			_input = FindFirstObjectByType<PlayerInput>(FindObjectsInactive.Include);
			_game = FindFirstObjectByType<Game>(FindObjectsInactive.Include);
		}
#endif
	}
}