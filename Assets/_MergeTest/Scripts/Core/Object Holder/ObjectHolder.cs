using UnityEngine;

namespace MergeTest.Core
{
	public class ObjectHolder : MonoBehaviour, IObjectHolder
	{
		private IHoldableObject _currentHoldable;
		
		public void Take(IHoldableObject holdableObject)
		{
			_currentHoldable = holdableObject;
		}

		public void Release()
		{
			throw new System.NotImplementedException();
		}
	}
}