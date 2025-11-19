using MergeTest.Core;
using UnityEngine;

namespace MergeTest.Characters.Grid
{
	public interface ICharacterGridTile
	{
		public Transform SpawnPoint { get; }

		public void SetCharacter(IHoldableObject holdableObject);
	}
}