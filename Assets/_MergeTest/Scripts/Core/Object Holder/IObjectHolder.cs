namespace MergeTest.Core
{
	public interface IObjectHolder
	{
		public void Take(IHoldableObject holdableObject);
		
		public void Release();
	}
}