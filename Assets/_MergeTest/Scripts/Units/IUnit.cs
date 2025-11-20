using MergeTest.Core;
using UnityEngine;

namespace MergeTest.Units
{
	public interface IUnit : IHoldable
	{
		public UnitType Type { get; }
		
		public string Id { get; }
		
		public int MaxLevel {get;}
		public int Level { get; }
		
		public void SetParent(Transform parent);
	}
}