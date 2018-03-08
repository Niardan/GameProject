using System;

namespace Assets.Scripts.Bloks.Controllers
{
    public class BlockStateController
    {
        private BlockGameGenerator _blockGameGenerator;
        private BlockGamePoleController _blockGamePole;
        private readonly DestroyBlock _destroyBlock;

        private Action _currentState;

        private bool _stopUpdate;
        public BlockStateController(BlockGameGenerator blockGameGenerator, BlockGamePoleController blockGamePole, DestroyBlock destroyBlock)
        {
            _blockGameGenerator = blockGameGenerator;
            _blockGamePole = blockGamePole;
            _destroyBlock = destroyBlock;
            _blockGameGenerator.GenerateStartBorder();
            CurrentState = StateUpdate;
            _blockGamePole.EndMove += OnEndMove;
            _blockGamePole.MoveClick += OnMoveClick;
            _blockGamePole.EndNotMove += OnEndNotMove;
        }

        private Action CurrentState
        {
            get { return _currentState; }
            set { _currentState = value; }
        }

        private void OnEndNotMove()
        {
            CurrentState = StateDestroy;
        }

        private void OnMoveClick()
        {
            CurrentState = StateUpdate;
        }

        public void UpdateGame()
        {
            NextState();
        }
        
        public void NextState()
        {
            if (CurrentState != null)
            {
                CurrentState();
            }
        }

        private void StateGenerate()
        {
            _blockGameGenerator.GenerateStateBorder();
            CurrentState = StateUpdate;
        }

        private void StateUpdate()
        {
            CurrentState = null;
            _blockGamePole.Update();
        }

        private void StateDestroy()
        {
            _destroyBlock.UpdateDestroy();
            CurrentState = StateUpdate;
        }

        private void OnEndMove()
        {
            CurrentState = StateGenerate;
        }
    }
}