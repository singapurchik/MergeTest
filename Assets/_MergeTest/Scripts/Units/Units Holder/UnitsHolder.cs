using System.Collections.Generic;

namespace MergeTest.Units
{
	public class UnitsHolder : IReadOnlyUnitsHolder
	{
		private readonly Dictionary<string, Unit> _units = new (9);
		
		public IReadOnlyDictionary<string, Unit> Units => _units;
		
		public void Remove(Unit unit) => _units.Remove(unit.Id);
		
		public void Add(Unit unit) => _units.Add(unit.Id, unit);
	}
}