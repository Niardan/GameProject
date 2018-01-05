namespace Assets.Scripts.Mining.Controllers.GamePole.GameProcess
{
	public delegate void CheckChangedCompleteHandler(bool changed);

	public delegate void SuccessBallHandler(BallController ball);

	public class CheckChangedBallController
	{
		private readonly BallController[,] _ballPole;
		private readonly CheckBall _checkBall;

		private bool _changed;

		public event CheckChangedCompleteHandler CheckChangedComplete;
		public event SuccessBallHandler SuccessBall;
		public CheckChangedBallController(BallController[,] ballPole, CheckBall checkBall)
		{
			_ballPole = ballPole;
			_checkBall = checkBall;
		}

		public bool CheckAllBall(BallController[,] newGamePole)
		{
			for (int y = 0; y < _ballPole.GetLength(0) - 1; y++)
			{
				for (int x = 0; x < _ballPole.GetLength(0) - 1; x++)
				{
					ReverceBall(x, y, x + 1, y);
					if (_checkBall.CheckChangedBall(_ballPole[x, y], newGamePole, false))
					{
						ReverceBall(x, y, x + 1, y);
						CallSuccessBall(_ballPole[x+1, y]);
						return true;
					}
					if (_checkBall.CheckChangedBall(_ballPole[x + 1, y], newGamePole, false))
					{
						ReverceBall(x, y, x + 1, y);
						CallSuccessBall(_ballPole[x, y]);
						return true;
					}



					ReverceBall(x, y, x + 1, y);

					ReverceBall(x, y, x, y + 1);

					if (_checkBall.CheckChangedBall(_ballPole[x, y], newGamePole, false))
					{
						ReverceBall(x, y, x, y + 1);
						CallSuccessBall(_ballPole[x, y+1]);
						return true;
					}

					if (_checkBall.CheckChangedBall(_ballPole[x, y + 1], newGamePole, false))
					{
						ReverceBall(x, y, x, y + 1);
						CallSuccessBall(_ballPole[x, y]);
						return true;
					}

					ReverceBall(x, y, x, y + 1);
				}
			}
			foreach (var ball in newGamePole)
			{

				if (_checkBall.CheckChangedBall(ball, newGamePole, false))
				{
					return true;
				}
			}
			return false;
		}
		private void ReverceBall(int x, int y, int x1, int y1)
		{
			var ball1 = _ballPole[x, y];
			ball1.BallSetPosition(new BallPoint(x1, y1));
			var ball2 = _ballPole[x1, y1];
			ball2.BallSetPosition(new BallPoint(x, y));
			_ballPole[x, y] = ball2;
			_ballPole[x1, y1] = ball1;
		}

		public void CheckMovedController()
		{
			_changed = false;
			foreach (var ball in _ballPole)
			{
				if (ball != null && ball.IsChanged)
				{
					if (!_checkBall.CheckChangedBall(ball, _ballPole, true))
					{
						ball.IsChanged = false;
					}
					else
					{
						_changed = true;
					}
				}
			}

			CallCheckChangedComplete();
		}

		private void CallCheckChangedComplete()
		{
			if (CheckChangedComplete != null)
			{
				CheckChangedComplete(_changed);
			}
		}

		private void CallSuccessBall(BallController ball)
		{
			if (SuccessBall != null)
			{
				SuccessBall(ball);
			}
		}
	}
}