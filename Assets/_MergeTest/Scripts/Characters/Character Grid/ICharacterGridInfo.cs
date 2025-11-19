namespace MergeTest.Characters.Grid
{
	public interface ICharacterGridInfo
	{
		public bool IsHasEmptyTile { get; }

		public bool TryGetEmptyTile(out ICharacterGridTile tile);
	}
}