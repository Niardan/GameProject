using System;

namespace Assets.Scripts.Mining.Controllers.GamePole.GameProcess
{
    public delegate void StateHandler();
    public class GamePoleStateControler
    {
        private readonly DestroyControllerBall _destroyControllerBall;
        private readonly FallControllerBall _fallControllerBall;
        private readonly MovedController _movedController;
        private readonly NewGenerateController _newGenerateController;
        private readonly CheckChangedBallController _checkChangedBallController;
	    private readonly HintControler _hintControler;
	    private readonly BallController[,] _balls;

		private StateHandler _state;
        private bool _replay;
	    private bool _endGame;

	    public event Action GameOver;

        public GamePoleStateControler(DestroyControllerBall destroyControllerBall, FallControllerBall fallControllerBall, MovedController movedController, NewGenerateController newGenerateController, CheckChangedBallController checkChangedBallController, BallController[,] balls, HintControler hintControler)
        {
            _destroyControllerBall = destroyControllerBall;
            _fallControllerBall = fallControllerBall;
            _movedController = movedController;
            
            _newGenerateController = newGenerateController;
            _checkChangedBallController = checkChangedBallController;
	        _balls = balls;
	        _hintControler = hintControler;
	        _movedController.EndBallMoved += OnEndBallMoved;
            newGenerateController.SetGenerator(true);
        }

        public void StartGame()
        {
            _state = GenerateState;
            NextState();
        }

        private void OnEndBallMoved(bool changed)
        {
            if (changed)
            {
				_hintControler.ResetTimer();
                _state = StateDestroy;
                NextState();
            }
            else
            {
                _movedController.SetAllowMove(true);
            }
        }

        private void NextState()
        {
            _state();
        }

        private void StateWait()
        {
	        if (!_checkChangedBallController.CheckAllBall(_balls))
	        {
		        foreach (var ball in _balls)
		        {
			        ball.IsDestroed = true;
		        }
		        _movedController.SetAllowMove(false);
				_state = StateDestroy;
		        _endGame = true;
				NextState();
			}
            _newGenerateController.SetGenerator(false);
            _movedController.SetAllowMove(true);
        }

        private void StateCheckChanged()
        {
            _checkChangedBallController.CheckChangedComplete += OnCheckChangedComplete;
            _checkChangedBallController.CheckMovedController();
        }

        private void OnCheckChangedComplete(bool changed)
        {
            _checkChangedBallController.CheckChangedComplete -= OnCheckChangedComplete;
            if (changed)
            {
                _state = StateDestroy;
               
            }
            else
            {
                _state = StateWait;
            }
            NextState();
        }

        private void StateDestroy()
        {
            _destroyControllerBall.DestroyComplete += OnDestroyComplete;
            _destroyControllerBall.DestroyAllBall();
        }

        private void FallState()
        {
            _fallControllerBall.FallComplete += OnFallComplete;
            _fallControllerBall.StartFall();
        }

        private void GenerateState()
        {
            _newGenerateController.BallGenerateComplete += OnBallGenerateComplete;
            _newGenerateController.Generate();
        }

        private void OnBallGenerateComplete(bool changed)
        {
            _newGenerateController.BallGenerateComplete -= OnBallGenerateComplete;
            if (changed)
            {
                _state = FallState;
            }
            else if (!_replay)
            {
                _replay = true;
                _state = GenerateState;
            }
            else
            {
                _state = StateCheckChanged;
            }
            NextState();
        }

        private void OnFallComplete(bool changed)
        {
            _fallControllerBall.FallComplete -= OnFallComplete;
            if (changed)
            {
                _state = GenerateState;
            }
            else if(!_replay)
            {
                _replay = true;
                _state = GenerateState;
            }
            else
            {
                _state = StateCheckChanged;
            }
            NextState();
        }

        private void OnDestroyComplete(bool changed)
        {
            _destroyControllerBall.DestroyComplete -= OnDestroyComplete;
	        if (_endGame)
	        {
		        CallGameOver();
	        }
	        else
	        {
				if (changed)
				{
					_state = FallState;
					_replay = false;
					NextState();
				}
			}
        }

	    private void CallGameOver()
	    {
		    if (GameOver != null)
		    {
			    GameOver();
		    }
	    }
    }
}