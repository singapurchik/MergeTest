using MergeTest.Core;
using Zenject;

namespace MergeTest.Units
{
	public class UnitsPool : ObjectPool<Unit>
	{
		[Inject] private UnitsHolder _unitsHolder;
		
		protected override void InitializeObject(Unit unit)
		{
			unit.OnMerged += ReturnToPool;
			unit.Initialize();
			_unitsHolder.Add(unit);
		}

		protected override void CleanupObject(Unit unit)
		{
			_unitsHolder.Remove(unit);
			unit.OnMerged -= ReturnToPool;
		}
	}
}