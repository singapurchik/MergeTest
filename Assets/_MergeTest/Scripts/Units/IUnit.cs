using MergeTest.Core;

namespace MergeTest.Units
{
	public interface IUnit : IHoldable
	{
		public string Id { get; }
	}
}