
namespace MergeTest.Units.Grid
{
	public interface IUnitsGridRegister
	{
		public bool TryGetUnitIdFromCell(IUnitsGridCell cell, out string unitId);

		public void AddUnitToCell(IUnitsGridCell cell, string unitId);
		
		public void RemoveUnitFromCell(IUnitsGridCell cell);
	}
}