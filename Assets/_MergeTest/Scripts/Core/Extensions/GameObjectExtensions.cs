using UnityEngine;

namespace MergeTest.Core
{
	public static class GameObjectExtensions
	{
		public static bool TryEnable(this GameObject gameObject)
		{
			if (gameObject.activeSelf)
				return false;

			gameObject.SetActive(true);
			return true;
		}

		public static bool TryDisable(this GameObject gameObject)
		{
			if (!gameObject.activeSelf)
				return false;

			gameObject.SetActive(false);
			return true;
		}
	}	
}