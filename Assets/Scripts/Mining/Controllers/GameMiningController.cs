using Assets.Scripts.Mining.Controllers.GamePole;
using Assets.Scripts.Mining.GamePole;
using Assets.Scripts.Mining.Views;
using Assets.Scripts.ViewDescription;
using UnityEngine;

namespace Assets.Scripts.Mining.Controllers
{
	public class GameMiningController
	{
		private readonly GameMiningView _view;
		private readonly BallPool _ballPool;
		private readonly GamePoleController _gamePoleController;
		private readonly GameManager _gameManager;


		public GameMiningController(GameMiningView view, MiningSceneViewDescription miningViewDescription, GameManager gameManager)
		{
			_view = view;
			_gameManager = gameManager;
			_ballPool = new BallPool(64, view.Ball);
			_gamePoleController = new GamePoleController(_view.HintAnimation, _view.GamePoleView, _ballPool, _view.BallSpritesViewDescription, miningViewDescription, _view);
			_gamePoleController.GameOver += GameOver;
			_view.EndGame.onClick.AddListener(OnEndGame);
		}

		private void OnEndGame()
		{
			_gameManager.UnloadScene("Mining");
		}

		private void GameOver()
		{
			_view.GameOver.SetActive(true);
		}
	}
}