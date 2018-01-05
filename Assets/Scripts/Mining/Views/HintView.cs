using System.Collections;
using Assets.Scripts.Mining.Controllers.GamePole;
using Assets.Scripts.Views;
using UnityEngine;

namespace Assets
{
	public class HintView : SimpleView
	{
		[SerializeField] private RectTransform _hint;
		[SerializeField] private RectTransform _arrow;
		[SerializeField] private float _timeHint;

		private Coroutine _moveArrow;

		public float TimeHint
		{
			get { return _timeHint; }
		}

		public void StartAnimation()
		{
			if (_moveArrow != null)
			{
				StopCoroutine(_moveArrow);
				_moveArrow = null;
			};
			_moveArrow = StartCoroutine(MoveArrow());
		}

		public void StopAnimation()
		{
			if (_moveArrow != null)
			{
				StopCoroutine(_moveArrow);
				_moveArrow = null;
			};
		}

		public void SetHintPosition(Vector2 point)
		{

			_hint.anchoredPosition = new Vector2(point.x, point.y + 40);
		}

		private IEnumerator MoveArrow()
		{
			int y = 0;
			int step = 1;
			while (true)
			{
				y += step;
				_arrow.anchoredPosition = new Vector2(0, y);
				if (y >= 10)
				{
					step = -1;
				}
				if (y <= -10)
				{
					step = 1;
				}
				yield return new WaitForSeconds(0.01F);
			}
		}
	}
}
