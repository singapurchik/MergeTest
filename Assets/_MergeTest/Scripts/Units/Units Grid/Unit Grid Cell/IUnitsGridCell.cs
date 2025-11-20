using UnityEngine;

namespace MergeTest.Units.Grid
{
	public interface IUnitsGridCell : IUnitGridCellInfo
	{
		public Transform SpawnPoint { get; }
		
		public bool IsEmpty { get; }

		public void SetFull();

		public void SetEmpty();
	}
}