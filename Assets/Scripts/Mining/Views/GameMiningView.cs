using System.Collections.Generic;
using Assets.Scripts.Basic;
using Assets.Scripts.Mining.Controllers.GamePole;
using Assets.Scripts.Mining.GamePole;
using Assets.Scripts.ViewDescription;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mining.Views
{
    public class GameMiningView : MonoBehaviour, ISceneView
    {
        [SerializeField] private GamePoleView _gamePole;
        [SerializeField] private BallView _ball;
        [SerializeField] private Dictionary<string, Sprite> _sprites;
        [SerializeField] private BallSpritesViewDescription _ballSpritesViewDescription;
	    [SerializeField] private Button _endGame;
	    [SerializeField] private GameObject _gameOver;
	    [SerializeField] private HintView _hintAnimation;
		
        public GamePoleView GamePoleView { get { return _gamePole; } }
        public BallView Ball { get { return _ball; } }
        public BallSpritesViewDescription BallSpritesViewDescription { get { return _ballSpritesViewDescription; } }

	    public Button EndGame
	    {
		    get { return _endGame; }
	    }

	    public GameObject GameOver
	    {
		    get { return _gameOver; }
	    }

	    public HintView HintAnimation
	    {
		    get { return _hintAnimation; }
	    }
    }
}