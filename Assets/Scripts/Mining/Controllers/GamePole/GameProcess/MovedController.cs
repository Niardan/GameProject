using System.Collections.Generic;

namespace Assets.Scripts.Mining.Controllers.GamePole.GameProcess
{
    public delegate void StartMoveBallHandler();
    public delegate void EndMoveBallHandler(bool changed);
    public class MovedController
    {
        private readonly BallController[,] _ballPole;
        private readonly GamePoleView _gamePole;
        private readonly CheckBall _checkBall;


        public event StartMoveBallHandler StartBallMoved;
        public event EndMoveBallHandler EndBallMoved;

        private readonly IDictionary<BallController, MoveReverce> _reverces = new Dictionary<BallController, MoveReverce>(2);
        private int _movedBall;
        private bool _changed;

        private bool _allowMove;

        public MovedController(BallController[,] ballPole, GamePoleView gamePole, CheckBall checkBall)
        {
            _ballPole = ballPole;
            _gamePole = gamePole;
            _checkBall = checkBall;
        }

        public void Move(BallController ball, int x, int y)
        {
            var koord = _gamePole.GetCoordPoints(x, y);
            ball.BallMove(koord, x, y);
        }

        public void OnBallMoved(BallController ball, MoveSide side)
        {
            if (_allowMove)
            {
                int x = ball.Positinon.X;
                int y = ball.Positinon.Y;
                int newX = x;
                int newY = y;
                bool moved = false;
                switch (side)
                {
                    case MoveSide.Down:
                        newY++;
                        if (newY < _gamePole.CountVertCell)
                        {
                            moved = true;
                        }
                        break;
                    case MoveSide.Left:
                        newX--;
                        if (newX >= 0)
                        {
                            moved = true;
                        }
                        break;
                    case MoveSide.Right:
                        newX++;
                        if (newX < _gamePole.CountHorCell)
                        {
                            moved = true;
                        }
                        break;
                    case MoveSide.Up:
                        newY--;
                        if (newY >= 0)
                        {
                            moved = true;
                        }
                        break;

                }
                if (moved)
                {
                    _allowMove = false;
                    CallStartBallMoved();
                    var newBall = _ballPole[newX, newY];
                    _ballPole[newX, newY] = ball;
                    _ballPole[x, y] = newBall;
                    if (newBall != null)
                    {
                        var ballReverce = new MoveReverce(ball);
                        var newBallReverce = new MoveReverce(newBall);
                        Move(ball, newX, newY);
                        Move(newBall, x, y);
                        _movedBall += 2;
                        if (_checkBall.CheckChangedBall(ball, _ballPole, true) | _checkBall.CheckChangedBall(newBall, _ballPole, true))
                        {
                            _changed = true;
                            ball.BallAnimationEnd += OnBallAnimationComplete;
                            newBall.BallAnimationEnd += OnBallAnimationComplete;
                        }
                        else
                        {
							_reverces.Clear();
                            _reverces[ball] = ballReverce;
                            _reverces[newBall] =  newBallReverce;
                            ball.BallAnimationEnd += OnBallReverce;
                            newBall.BallAnimationEnd += OnBallReverce;
                        }
                    }
                }
            }
        }

        private void OnBallAnimationComplete(BallController ball)
        {
            ball.BallAnimationEnd -= OnBallAnimationComplete;
            _movedBall--;
            if (_movedBall == 0)
            {
                CallEndBallMoved(_changed);
            }
        }

        private void OnBallReverce(BallController ball)
        {
            ball.BallAnimationEnd -= OnBallReverce;
            _movedBall--;
            if (_movedBall == 0)
            {
                _changed = false;
               ReverceMove();
            }
        }

        private void ReverceMove()
        {
            foreach (var ball in _reverces.Values)
            {
                _movedBall++;
                ball.Ball.BallAnimationEnd += OnBallAnimationComplete;
                _ballPole[ball.Point.X, ball.Point.Y] = ball.Ball;
                Move(ball.Ball, ball.Point.X, ball.Point.Y);
            }
        }

        private void CallStartBallMoved()
        {
            if (StartBallMoved != null)
            {
                StartBallMoved();
            }
        }

        public void SetAllowMove(bool move)
        {
            _allowMove = move;
        }

        private void CallEndBallMoved(bool changed)
        {
            if (EndBallMoved != null)
            {
                EndBallMoved(changed);
            }
        }
    }
}