using System.Collections.Generic;
using MergeTest.Core;
using UnityEngine;
using VInspector;
using Zenject;

namespace MergeTest.Characters.Grid
{
	public class CharactersGridInstaller : MonoInstaller
	{
		[SerializeField] private CharactersGrid _charactersGrid;
		[SerializeField] private List<CharactersGridTile> _tiles = new ();

		public override void InstallBindings()
		{
			Container.Bind<IReadOnlyList<CharactersGridTile>>().FromInstance(_tiles).WhenInjectedIntoInstance(_charactersGrid);
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_tiles.Clear();
			_tiles.AddRange(GetComponentsInChildren<CharactersGridTile>(true));
		}
#endif
	}
}