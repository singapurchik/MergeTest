using System.Collections.Generic;

namespace MergeTest.Units.Grid
{
	public class UnitsGridRegister
	{
		private readonly Dictionary<IUnitsGridCell, string> _units = new (9);
		
		public bool TryGetUnit(IUnitsGridCell cell, out string unitId) => _units.TryGetValue(cell, out unitId);

		public bool TryRemoveUnit(IUnitsGridCell cell, out string unitId) => _units.Remove(cell, out unitId);
		
		public void AddUnit(IUnitsGridCell cell, string unitId) => _units.Add(cell, unitId);
	}
}