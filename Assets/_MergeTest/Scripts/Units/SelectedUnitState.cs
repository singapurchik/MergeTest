using MergeTest.Units.Grid;

namespace MergeTest.Units
{
	public sealed class SelectedUnitState
	{
		public Unit Unit { get; private set; }
		public IUnitsGridCell Cell { get; private set; }

		public bool HasSelection => Unit != null;

		public void Set(Unit unit, IUnitsGridCell cell)
		{
			Unit = unit;
			Cell = cell;
		}

		public void Clear()
		{
			Unit = null;
			Cell = null;
		}
	}
}