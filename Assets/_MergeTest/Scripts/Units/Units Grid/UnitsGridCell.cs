using System;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace MergeTest.Units.Grid
{
	public class UnitsGridCell : MonoBehaviour, IUnitsGridCell
	{
		[SerializeField] private Transform _spawnPoint;
		
		public Transform SpawnPoint => _spawnPoint;

		public bool IsEmpty { get; private set; } = true;
		
		public event Action<IUnitsGridCell> OnEmpty;
		public event Action<IUnitsGridCell> OnFull;
		
		private void Awake() => enabled = false;
		
		public void SetFull()
		{
			IsEmpty = false;
			OnFull?.Invoke(this);
		}

		public void SetEmpty()
		{
			IsEmpty = true;
			OnEmpty?.Invoke(this);
		}

#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			Gizmos.color = IsEmpty ? Color.green : Color.red;
			Gizmos.DrawCube(transform.position, Vector3.one);
		}
#endif
	}
}