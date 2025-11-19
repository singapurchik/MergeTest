using MergeTest.Core;

namespace MergeTest.Characters
{
	public class CharactersPool : ObjectPool<Character>
	{
		protected override void InitializeObject(Character character)
		{
			character.OnMerged += ReturnToPool;
		}

		protected override void CleanupObject(Character character)
		{
			character.OnMerged -= ReturnToPool;
		}
	}
}