using System.Collections.Generic;
using UnityEngine;
using VInspector;
using Zenject;

namespace MergeTest.Units.Grid
{
	public class UnitsGridInstaller : MonoInstaller
	{
		[SerializeField] private UnitsGrid _unitsGrid;
		[SerializeField] private List<UnitsGridCell> _cells = new ();

		public override void InstallBindings()
		{
			Container.Bind<IReadOnlyList<UnitsGridCell>>().FromInstance(_cells).WhenInjectedIntoInstance(_unitsGrid);
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_cells.Clear();
			_cells.AddRange(GetComponentsInChildren<UnitsGridCell>(true));

			_unitsGrid = FindFirstObjectByType<UnitsGrid>(FindObjectsInactive.Include);
		}
#endif
	}
}