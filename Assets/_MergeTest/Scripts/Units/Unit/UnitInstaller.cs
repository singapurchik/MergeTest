using UnityEngine;
using VInspector;
using Zenject;

namespace MergeTest.Units
{
	public class UnitInstaller : MonoInstaller
	{
		[SerializeField] private Unit _unit;

		public override void InstallBindings()
		{
			var unitAnimator = new UnitAnimator();
			Container.QueueForInject(unitAnimator);
			
			Container.BindInstance(unitAnimator).WhenInjectedIntoInstance(_unit);
		}

#if UNITY_EDITOR
		[Button]
		private void FindDependencies()
		{
			_unit = GetComponentInChildren<Unit>(true);
		}
#endif
	}
}