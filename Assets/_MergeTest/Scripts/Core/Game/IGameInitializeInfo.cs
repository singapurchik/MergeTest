using System;

namespace MergeTest.Core
{
	public interface IGameInitializeInfo
	{
		public event Action OnInitialized;
	}
}