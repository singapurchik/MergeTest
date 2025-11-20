using System.Collections.Generic;
using MergeTest.Units.Grid;
using UnityEngine;
using VInspector;
using Zenject;

namespace MergeTest.Units
{
	public class UnitsInstaller : MonoInstaller
	{
		[SerializeField] private UnitsManipulator _manipulator;
		[SerializeField] private UnitsSpawner _unitsSpawner;
		[SerializeField] private UnitsGrid _grid;
		[SerializeField] private List<UnitsPool> _pools = new ();

		public override void InstallBindings()
		{
			var register = new UnitsGridRegister();
			var unitsHolder = new UnitsHolder();
			
			Container.Bind<IReadOnlyUnitsHolder>().FromInstance(unitsHolder).AsSingle();
			
			Container.BindInstance(register).WhenInjectedIntoInstance(_manipulator);
			Container.BindInstance(unitsHolder).WhenInjectedInto<UnitsPool>();
			
			Container.Bind<IReadOnlyList<UnitsPool>>().FromInstance(_pools)
				.WhenInjectedIntoInstance(_unitsSpawner);

			Container.Bind<IUnitsGridInfo>().FromInstance(_grid)
				.WhenInjectedIntoInstance(_unitsSpawner);
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_pools.Clear();
			_pools.AddRange(FindObjectsByType<UnitsPool>(FindObjectsInactive.Include, FindObjectsSortMode.None));
			
			_manipulator = FindFirstObjectByType<UnitsManipulator>(FindObjectsInactive.Include);
			_unitsSpawner = FindFirstObjectByType<UnitsSpawner>(FindObjectsInactive.Include);
			_grid = FindFirstObjectByType<UnitsGrid>(FindObjectsInactive.Include);
		}
#endif
	}
}