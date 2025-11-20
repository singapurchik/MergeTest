using UnityEngine;

namespace MergeTest.Units.Grid
{
	public interface IUnitsGridInfo
	{
		public bool IsHasEmptyTile { get; }

		public bool TryGetEmptyTilePoint(out Transform tilePoint);
	}
}