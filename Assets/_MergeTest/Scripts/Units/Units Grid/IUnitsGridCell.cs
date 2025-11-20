namespace MergeTest.Units.Grid
{
	public interface IUnitsGridCell
	{
		public bool IsEmpty { get; }
		
		public void AddUnit(string unitId);
	}
}