using UnityEngine;

namespace MergeTest.Units.Grid
{
	public class UnitsGridCell : MonoBehaviour, IUnitsGridCell
	{
		[SerializeField] private Transform _spawnPoint;
		
		public Transform SpawnPoint => _spawnPoint;

		public bool IsEmpty { get; private set; }
		
		public string UnitId { get; private set; }

		private void Awake() => enabled = false;
		
		public void AddUnit(string unitId)
		{
			IsEmpty = false;
			UnitId = unitId;
		}

		public void RemoveUnit()
		{
			UnitId = null;
			IsEmpty = true;
		}
	}
}