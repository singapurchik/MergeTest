using System;

namespace MergeTest.Core
{
	public interface IGameInitializeInfo
	{
		public bool IsInitialized { get; }
		
		public event Action OnInitialized;
	}
}