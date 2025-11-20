using System.Collections.Generic;

namespace MergeTest.Units
{
	public interface IReadOnlyUnitsHolder
	{
		public IReadOnlyDictionary<string, Unit> Units { get; }
	}
}