using System.Collections.Generic;
using MergeTest.Characters.Grid;
using UnityEngine;
using VInspector;
using Zenject;

namespace MergeTest.Characters
{
	public class CharactersInstaller : MonoInstaller
	{
		[SerializeField] private CharactersSpawner _charactersSpawner;
		[SerializeField] private CharactersGrid _grid;
		[SerializeField] private List<CharactersPool> _pools = new ();

		public override void InstallBindings()
		{
			Container.Bind<IReadOnlyList<CharactersPool>>().FromInstance(_pools)
				.WhenInjectedIntoInstance(_charactersSpawner);

			Container.Bind<ICharacterGridInfo>().FromInstance(_grid)
				.WhenInjectedIntoInstance(_charactersSpawner);
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_pools.Clear();
			_pools.AddRange(FindObjectsByType<CharactersPool>(FindObjectsInactive.Include, FindObjectsSortMode.None));
			
			_charactersSpawner = FindFirstObjectByType<CharactersSpawner>(FindObjectsInactive.Include);
			_grid = FindFirstObjectByType<CharactersGrid>(FindObjectsInactive.Include);
		}
#endif
	}
}