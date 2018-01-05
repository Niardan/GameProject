using System.Collections;
using Assets.Scripts.Mining.Controllers.GamePole;
using Assets.Scripts.Mining.Controllers.GamePole.GameProcess;
using UnityEngine;

namespace Assets.Scripts.Mining.Controllers
{
	public class HintControler
	{
		private readonly HintView _hintView;
		private Coroutine _timer;
		private readonly CheckChangedBallController _changedBallController;
		private BallController _ball;
		private readonly MonoBehaviour _monoBehaviour;


		public HintControler(HintView hintView, CheckChangedBallController changedBallController, MonoBehaviour monoBehaviour)
		{
			_hintView = hintView;
			_changedBallController = changedBallController;
			_monoBehaviour = monoBehaviour;
			_changedBallController.SuccessBall += OnSuccessBall;
		}

		private void OnSuccessBall(GamePole.BallController ball)
		{
			_ball = ball;
		}

		public void ResetTimer()
		{
			if (_timer != null)
			{
				_monoBehaviour.StopCoroutine(_timer);
				_hintView.StopAllCoroutines();
				_hintView.SetActive(false);
				_timer = null;
			}
			_timer = _monoBehaviour.StartCoroutine(WaitHint());
		}

		public IEnumerator WaitHint()
		{
			yield return new WaitForSeconds(_hintView.TimeHint);
			_hintView.SetHintPosition(_ball.Koordinates);
			_hintView.SetActive(true);
			_hintView.StartAnimation();
		}

	}
}