using System;
using System.Collections.Generic;
using Assets.Scripts.Mining.Views;
using Assets.Scripts.ViewDescription;
using UnityEngine;


namespace Assets.Scripts.Mining.Controllers.GamePole
{
	public class GenerateBalls
	{
		private TypeBall _typeBallResource;
		private int _countColor;
		private float _percentageResources;
		private ResourcesCount _resources;
		private BallPoint _sizeGamePole;
		private BallController[,] _balls;
		private BallPoint _generatePosition;
		private readonly BallPool _ballPool;
		private readonly System.Random _rand = new System.Random();
		private readonly MiningSceneViewDescription _miningViewDescription;

		public GenerateBalls(BallPoint sizePole, MiningSceneViewDescription miningViewDescription, BallPool ballPool)
		{
			_sizeGamePole = sizePole;
			_typeBallResource = miningViewDescription.TypeBallResource;
			_countColor = miningViewDescription.Balls.Count;
			_percentageResources = miningViewDescription.PercentageResources;
			_resources = new ResourcesCount(miningViewDescription.TypeResources, miningViewDescription.CountResource);
			_miningViewDescription = miningViewDescription;
			_ballPool = ballPool;
		}
		public BallController[,] GenerateAll()
		{
			_balls = new BallController[_sizeGamePole.X, _sizeGamePole.Y];
			_generatePosition = new BallPoint(0, 0);

			do
			{
				_balls[_generatePosition.X, _generatePosition.Y] = NextBall();

			} while (NextPosition());
			return _balls;
		}

		private BallController NextBall()
		{
			var listBall = GetTypesBall();
			while (true)
			{
				var ball = GenerateBall(listBall);
				if (CheckGenerateBall(ball))
				{
					return ball;
				}
				_ballPool.FreeBall = ball.View;
			}
		}

		private List<TypeBall> GetTypesBall()
		{
			List<TypeBall> ballsTypes = new List<TypeBall>();
			foreach (var ball in _miningViewDescription.Balls)
			{
				ballsTypes.Add(ball);
			}
			return ballsTypes;
		}

		private bool NextPosition()
		{
			int x = _generatePosition.X + 1;
			if (x < _sizeGamePole.X)
			{
				_generatePosition = new BallPoint(x, _generatePosition.Y);
				return true;
			}
			else
			{
				int y = _generatePosition.Y + 1;
				if (y < _sizeGamePole.Y)
				{
					x = 0;
					_generatePosition = new BallPoint(x, y);
					return true;
				}
			}
			return false;
		}

		public BallController GenerateBall(List<TypeBall> ballsTypes)
		{
			var indexColor = _rand.Next(0, ballsTypes.Count);
			TypeBall typeBall = ballsTypes[indexColor];
			ballsTypes.Remove(typeBall);
			ResourcesCount resources = null;
			if (typeBall == _typeBallResource)
			{
				float chance = _rand.Next(0, 100) / 100F;
				if (chance < _percentageResources)
				{
					resources = _resources;
				}
			}
			return new BallController(_ballPool.FreeBall, _generatePosition, typeBall, resources);
		}

		public BallController GenerateRandomBall(BallPoint position)
		{
			List<TypeBall> ballsTypes = GetTypesBall();
			var indexColor = _rand.Next(0, ballsTypes.Count);
			TypeBall typeBall = ballsTypes[indexColor];
			ResourcesCount resources = null;
			if (typeBall == _typeBallResource)
			{
				float chance = _rand.Next(0, 100) / 100F;
				if (chance < _percentageResources)
				{
					resources = _resources;
				}
			}
			return new BallController(_ballPool.FreeBall, position, typeBall, resources);
		}

		private bool CheckGenerateBall(BallController ball)
		{
			if (CountLeftBall(ball, 1) > 2 || CountDownBall(ball, 1) > 2)
			{
				return false;
			}
			return true;
		}

		private int CountLeftBall(BallController ball, int count)
		{
			if (ball.Positinon.X > 0)
			{
				var newBall = _balls[ball.Positinon.X - 1, ball.Positinon.Y];
				if (ball.TypeBall == newBall.TypeBall)
				{
					return CountLeftBall(newBall, count + 1);
				}

			}
			return count;
		}

		private int CountDownBall(BallController ball, int count)
		{

			if (ball.Positinon.Y > 0)
			{
				var newBall = _balls[ball.Positinon.X, ball.Positinon.Y - 1];
				if (ball.TypeBall == newBall.TypeBall)
				{
					return CountDownBall(newBall, count + 1);
				}
			}
			return count;
		}


	}
}