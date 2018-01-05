using Assets.Scripts.Mining.Views;

namespace Assets.Scripts.Mining.Controllers.GamePole.GameProcess
{
    public delegate void DestroyCompleteHandler(bool changed);
    public class DestroyControllerBall
    {
        private readonly BallController[,] _ballPole;
        private readonly MovedController _movedController;
        private readonly BallPool _ballPool;
        private int _isDestroy;
    
        public DestroyControllerBall(BallController[,] ballPole, MovedController movedController, BallPool ballPool)
        {
            _ballPole = ballPole;
            _movedController = movedController;
            _ballPool = ballPool;
        }

        public event DestroyCompleteHandler DestroyComplete;
        public void DestroyAllBall()
        {
            bool changed = false;
            for (int y = _ballPole.GetLength(1) - 1; y >= 0; y--)
            {
                for (int x = 0; x < _ballPole.GetLength(0); x++)
                {
                    var ball = _ballPole[x, y];
                    if (ball != null && ball.IsDestroed)
                    {
                        changed = true;
                        _isDestroy++;
                        ball.BallDestroedEnd += OnBallDestroedEnd;
                        ball.Destroy();
                    }
                }
            }
            if (!changed)
            {
                CallDestroyComplete(false);
            }
        }

        private void OnBallDestroedEnd(BallController ball)
        {
            ball.BallDestroedEnd -= OnBallDestroedEnd;
            _isDestroy--;
            DestroyBall(ball);
            if (_isDestroy == 0)
            {
                CallDestroyComplete(true);
            }
        }

        private void DestroyBall(BallController ball)
        {
            ball.BallMoved -= _movedController.OnBallMoved;
            _ballPole[ball.Positinon.X, ball.Positinon.Y] = null;
            _ballPool.FreeBall = ball.View;
        }

        private void CallDestroyComplete(bool changed)
        {
            if (DestroyComplete != null)
            {
                DestroyComplete(changed);
            }
        }
    }
}