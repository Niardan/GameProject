using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Mining.Controllers.GamePole.GameProcess;
using Assets.Scripts.Mining.Views;
using Assets.Scripts.ViewDescription;
using UnityEngine;

namespace Assets.Scripts.Mining.Controllers.GamePole
{

	public class GamePoleController
	{

		private readonly BallController[,] _balls;

		private GamePoleStateControler _gamePoleStateControler;
		public event Action GameOver;

		public GamePoleController(HintView hintView, GamePoleView gamePole, BallPool ballPool, BallSpritesViewDescription spritesViewDescription, MiningSceneViewDescription miningViewDescription, MonoBehaviour monoBehaviour)
		{
			_balls = new BallController[gamePole.CountHorCell, gamePole.CountVertCell];
			var checkBall = new CheckBall(new BallPoint(gamePole.CountHorCell, gamePole.CountVertCell));
			var movedController = new MovedController(_balls, gamePole, checkBall);
			var destroyControler = new DestroyControllerBall(_balls, movedController, ballPool);
			var checkChangedBallController = new CheckChangedBallController(_balls, checkBall);
			var fallBallContorller = new FallControllerBall(_balls, movedController);
			var generateBalls = new GenerateBalls(new BallPoint(gamePole.CountHorCell, gamePole.CountVertCell), miningViewDescription, ballPool);
			var newGenerateController = new NewGenerateController(generateBalls, gamePole, _balls, spritesViewDescription, movedController);
			var hintController = new HintControler(hintView, checkChangedBallController, monoBehaviour);
			_gamePoleStateControler = new GamePoleStateControler(destroyControler, fallBallContorller, movedController, newGenerateController, checkChangedBallController, _balls, hintController);
			_gamePoleStateControler.GameOver += CallGameOver;

			gamePole.Activate();
			_gamePoleStateControler.StartGame();
		}

		public void CallGameOver()
		{
			if (GameOver != null)
			{
				GameOver();
			}
		}
	}
}