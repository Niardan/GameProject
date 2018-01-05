using UnityEngine;

namespace Assets.Scripts.Mining.Controllers.GamePole
{
    public class MoveReverce
    {
        private readonly BallController _ball;
        private readonly Vector2 _ballkoord;
        private readonly BallPoint _ballPoint;
        
        public MoveReverce(BallController ball)
        {
            _ball = ball;
            _ballkoord = ball.View.Ball.anchoredPosition;
            _ballPoint = ball.Positinon;
        }

        public BallController Ball
        {
            get { return _ball; }
        }

        public Vector2 Ballkoord
        {
            get { return _ballkoord; }
        }

        public BallPoint Point
        {
            get { return _ballPoint; }
        }
    }
}