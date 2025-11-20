using UnityEngine;

namespace MergeTest.UI
{
	[RequireComponent(typeof(CanvasGroup))]
	public abstract class UIScreen : MonoBehaviour
	{
		protected CanvasGroup CanvasGroup;

		protected virtual void Awake() => CanvasGroup = GetComponent<CanvasGroup>();

		public virtual void Show() => CanvasGroup.alpha = 1;

		public virtual void Hide() => CanvasGroup.alpha = 0;
	}
}