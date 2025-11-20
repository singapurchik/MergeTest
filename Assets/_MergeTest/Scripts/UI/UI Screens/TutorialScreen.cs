using MergeTest.Core;
using DG.Tweening;
using UnityEngine;
using Zenject;
using TMPro;

namespace MergeTest.UI
{
	public class TutorialScreen : UIScreen
	{
		[SerializeField] private TextMeshProUGUI _tutorialText;

		[Header("Animation settings")]
		[SerializeField] private float _scaleMultiplier = 1.08f;
		[SerializeField] private float _scaleDuration = 0.5f;

		[Inject] private IPlayerInputInfo _inputInfo;

		private Tween _tutorialTween;

		private void OnEnable()
		{
			_inputInfo.OnInputStarted += Hide;
			PlayTutorialAnim();
		}

		private void OnDisable()
		{
			_inputInfo.OnInputStarted -= Hide;
			KillTutorialAnim();
		}

		public override void Hide()
		{
			base.Hide();
			KillTutorialAnim();
			enabled = false;
		}

		private void PlayTutorialAnim()
		{
			KillTutorialAnim();

			var rect = _tutorialText.rectTransform;
			rect.localScale = Vector3.one;

			_tutorialTween = rect
				.DOScale(_scaleMultiplier, _scaleDuration)
				.SetEase(Ease.InOutSine)
				.SetLoops(-1, LoopType.Yoyo);
		}

		private void KillTutorialAnim()
		{
			if (_tutorialTween != null && _tutorialTween.IsActive())
			{
				_tutorialTween.Kill();
				_tutorialTween = null;
			}

			if (_tutorialText != null)
				_tutorialText.rectTransform.localScale = Vector3.one;
		}
	}
}