using System.Collections.Generic;
using Assets.Scripts.Mining.GamePole;
using UnityEngine;

namespace Assets.Scripts.Mining.Views
{
    public class BallPool
    {

        private readonly List<BallView> _balls;
        private readonly Queue<BallView> _freeBallPool;

        public BallPool(int poolSize, BallView ball)
        {
            _freeBallPool = new Queue<BallView>(poolSize);
            for (int i = 0; i < poolSize; i++)
            {
                _freeBallPool.Enqueue(Object.Instantiate(ball));
            }

        }

        public BallView FreeBall
        {
            get
            {
                var ball = _freeBallPool.Dequeue();
                return ball;
            }
            set
            {
                value.Ball.anchoredPosition = new Vector2(1000,1000);
                value.SetActive(false);
                _freeBallPool.Enqueue(value);
            }
        }
    }
}