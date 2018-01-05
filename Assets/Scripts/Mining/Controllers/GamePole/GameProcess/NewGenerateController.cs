using Assets.Scripts.ViewDescription;
using UnityEngine;

namespace Assets.Scripts.Mining.Controllers.GamePole.GameProcess
{
    public delegate BallController GetBallHandler(BallPoint position);
    public delegate void BallGenerateCompleteHandler(bool changed);
    public class NewGenerateController
    {
        private readonly GamePoleView _gamePoleView;
        private readonly BallController[,] _ballPole;
        private BallController[,] _generatedBall;
        private readonly GenerateBalls _generateBalls;
        private readonly BallSpritesViewDescription _spritesViewDescription;
        private readonly MovedController _movedController;
        private GetBallHandler _getBall;
        private bool _changed;

        public event BallGenerateCompleteHandler BallGenerateComplete;

        public NewGenerateController(GenerateBalls generateBalls, GamePoleView gamePoleView, BallController[,] ballPole, BallSpritesViewDescription spritesViewDescription, MovedController movedController)
        {
            _generateBalls = generateBalls;
            _gamePoleView = gamePoleView;
            _ballPole = ballPole;
            _spritesViewDescription = spritesViewDescription;
            _movedController = movedController;
        }

        public void Generate()
        {
            GenerateLine();
            CallBallGenerateComplete();
        }

        private void GenerateLine()
        {
            for (int i = 0; i < _ballPole.GetLength(0); i++)
            {
                if (_ballPole[i, 0] == null)
                {
                    _changed = true;
                    _ballPole[i, 0] = _getBall(new BallPoint(i, 0));
                }
            }
        }


        public void SetGenerator(bool newGeneration)
        {
            if (newGeneration)
            {
                _generatedBall = _generateBalls.GenerateAll();
                _getBall = GetGeneratedBall;
            }
            else
            {
                _getBall = GetNewBall;
            }
        }

        private BallController GetGeneratedBall(BallPoint position)
        {
            int x = position.X;
            int y = _generatedBall.GetLength(1) - 1;
            BallController ball;
            do
            {
                ball = _generatedBall[x, y];
                _generatedBall[x, y] = null;
                y--;
            } while (ball == null);
            _gamePoleView.AddBall(ball.View);
            InitBall(ball, position.X, position.Y);
            return ball;
        }

        private BallController GetNewBall(BallPoint position)
        {
            var ball = _generateBalls.GenerateRandomBall(position);
            InitBall(ball, position.X, position.Y);
            return ball;
        }

        private void InitBall(BallController ball, int xPosition, int yPosition)
        {
            ball.Positinon = new BallPoint(xPosition, yPosition);
            var genPosition = _gamePoleView.GetCoordPoints(xPosition, yPosition);

            ball.View.Ball.anchoredPosition = new Vector2(genPosition.x, genPosition.y + _gamePoleView.StepVert);
            ball.SetSprite(_spritesViewDescription.GetSprite(ball.TypeBall, ball.TypeResources != null), _spritesViewDescription.GetColor(ball.TypeBall));
            _ballPole[xPosition, yPosition] = ball;
            ball.View.SetActive(true);
            ball.BallMoved += _movedController.OnBallMoved;
            ball.BallMove(genPosition, xPosition, yPosition);
        }

        private void CallBallGenerateComplete()
        {
            if (BallGenerateComplete!=null)
            {
                BallGenerateComplete(_changed);
            }
        }


    }
}