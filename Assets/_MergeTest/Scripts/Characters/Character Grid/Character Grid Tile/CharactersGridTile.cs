using MergeTest.Core;
using UnityEngine;

namespace MergeTest.Characters.Grid
{
	public class CharactersGridTile : MonoBehaviour, ICharacterGridTile
	{
		[SerializeField] private Transform _spawnPoint;
		
		public Transform SpawnPoint => _spawnPoint;

		public IHoldableObject CurrentHoldableObject { get; private set; }
		
		public bool IsHoldingObject => CurrentHoldableObject != null;
		
		public void SetCharacter(IHoldableObject holdableObject) => CurrentHoldableObject = holdableObject;

		public void TakeCharacter() => CurrentHoldableObject.Take();
	}
}