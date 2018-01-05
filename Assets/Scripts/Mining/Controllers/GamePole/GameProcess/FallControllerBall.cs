namespace Assets.Scripts.Mining.Controllers.GamePole.GameProcess
{
    public delegate void CompleteOperationHandler(bool changed);
    public class FallControllerBall
    {
        private readonly BallController[,] _ballPole;
        private readonly MovedController _movedController;
        private bool _fallChanged;
        private int _movedBall;

        public event CompleteOperationHandler FallComplete;

        public FallControllerBall(BallController[,] ballPole, MovedController movedController)
        {
            _ballPole = ballPole;
            _movedController = movedController;
        }

        public void StartFall()
        {
            FallAll();
        }

        private void FallAll()
        {
            _fallChanged = false;
            for (int y = _ballPole.GetLength(1) - 1; y >= 0; y--)
            {
                for (int x = 0; x < _ballPole.GetLength(0); x++)
                {
                    var ball = _ballPole[x, y];
                    if (ball != null)
                    {
                        if (FallBall(ball))
                        {
                            _fallChanged = true;
                        }
                    }
                }
            }
            if (!_fallChanged)
            {
                CallFallComplete();
            }
        }

        private bool FallBall(BallController ball)
        {
            int x = ball.Positinon.X;
            int y = ball.Positinon.Y;
            int newY = y + 1;

            if (newY < _ballPole.GetLength(1))
            {
                var newball = _ballPole[x, newY];
                if (newball == null)
                {
                    _ballPole[x, y] = null;
                    _ballPole[x, newY] = ball;
                    ball.BallAnimationEnd += OnBallAnimationEndFailedComplete;
                    _movedBall++;
                    _movedController.Move(ball, x, newY);
                    return true;
                }
            }
            return false;
        }

        private void OnBallAnimationEndFailedComplete(BallController ball)
        {
            ball.BallAnimationEnd -= OnBallAnimationEndFailedComplete;
            _movedBall--;
            if (_movedBall == 0)
            {
                CallFallComplete();
            }
        }

        private void CallFallComplete()
        {
            if (FallComplete != null)
            {
                FallComplete(_fallChanged);
            }
        }
    }
}