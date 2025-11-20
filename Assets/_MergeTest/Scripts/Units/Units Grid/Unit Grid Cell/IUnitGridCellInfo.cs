using System;

namespace MergeTest.Units.Grid
{
	public interface IUnitGridCellInfo
	{
		public event Action<IUnitsGridCell> OnEmpty;
		public event Action<IUnitsGridCell> OnFull;
	}
}