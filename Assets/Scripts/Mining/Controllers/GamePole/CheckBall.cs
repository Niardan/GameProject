using System.Collections.Generic;

namespace Assets.Scripts.Mining.Controllers.GamePole
{
    public class CheckBall
    {
        private readonly BallPoint _sizeGamePole;
        public CheckBall(BallPoint sizeGamePole)
        {
            _sizeGamePole = sizeGamePole;
        }

        public bool CheckChangedBall(BallController ball, BallController[,] newGamePole, bool destoed)
        {
            bool change = false;
            ICollection<BallController> balls = new List<BallController> { ball };
            CountLeftBall(ball, balls, newGamePole);
            CountRightBall(ball, balls, newGamePole);

            if (balls.Count > 2)
            {
                change = true;
	            if (destoed)
	            {
					foreach (var destroedBall in balls)
					{
						destroedBall.IsDestroed = true;
					}
				}
            }
            balls = new List<BallController> { ball };

            CountDownBall(ball, balls, newGamePole);
            CountUpBall(ball, balls, newGamePole);
            if (balls.Count > 2)
            {
                change = true;
				if (destoed)
				{
					foreach (var destroedBall in balls)
					{
						destroedBall.IsDestroed = true;
					}
				}

			}
            balls.Clear();
            return change;
        }

        private ICollection<BallController> CountLeftBall(BallController ball, ICollection<BallController> balls, BallController[,] newGamePole)
        {
            if (ball.Positinon.X > 0)
            {
                var newBall = newGamePole[ball.Positinon.X - 1, ball.Positinon.Y];
                if (newBall != null && ball.TypeBall == newBall.TypeBall)
                {
                    balls.Add(newBall);
                    return CountLeftBall(newBall, balls, newGamePole);
                }
            }
            return balls;
        }

        private ICollection<BallController> CountRightBall(BallController ball, ICollection<BallController> balls, BallController[,] newGamePole)
        {
            if (ball.Positinon.X < _sizeGamePole.X - 1)
            {
                var newBall = newGamePole[ball.Positinon.X + 1, ball.Positinon.Y];
                if (newBall != null && ball.TypeBall == newBall.TypeBall)
                {
                    balls.Add(newBall);
                    return CountRightBall(newBall, balls, newGamePole);
                }
            }
            return balls;
        }

        private ICollection<BallController> CountDownBall(BallController ball, ICollection<BallController> balls, BallController[,] newGamePole)
        {
            if (ball.Positinon.Y > 0)
            {
                var newBall = newGamePole[ball.Positinon.X, ball.Positinon.Y - 1];
                if (newBall != null && ball.TypeBall == newBall.TypeBall)
                {
                    balls.Add(newBall);
                    return CountDownBall(newBall, balls, newGamePole);
                }
            }
            return balls;
        }

        private ICollection<BallController> CountUpBall(BallController ball, ICollection<BallController> balls, BallController[,] newGamePole)
        {
            if (ball.Positinon.Y < _sizeGamePole.Y - 1)
            {
                var newBall = newGamePole[ball.Positinon.X, ball.Positinon.Y + 1];
                if (newBall != null && ball.TypeBall == newBall.TypeBall)
                {
                    balls.Add(newBall);
                    return CountUpBall(newBall, balls, newGamePole);
                }
            }
            return balls;
        }
    }
}