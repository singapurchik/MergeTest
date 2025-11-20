using UnityEngine;
using VInspector;
using Zenject;

namespace MergeTest.Core
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private PlayerInput _input;
		[SerializeField] private Game _game;

		public override void InstallBindings()
		{
			Container.Bind<IPlayerInputInfo>().FromInstance(_input).AsSingle();
			Container.Bind<IGameInitializeInfo>().FromInstance(_game).AsSingle();
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_input = FindFirstObjectByType<PlayerInput>(FindObjectsInactive.Include);
			_game = FindFirstObjectByType<Game>(FindObjectsInactive.Include);
		}
#endif
	}
}