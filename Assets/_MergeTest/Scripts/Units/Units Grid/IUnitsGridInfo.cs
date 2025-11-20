namespace MergeTest.Units.Grid
{
	public interface IUnitsGridInfo
	{
		public bool IsHasEmptyTile { get; }

		public bool TryGetEmptyCell(out IUnitsGridCell cell);
	}
}