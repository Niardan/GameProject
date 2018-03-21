using System;

namespace Assets.Scripts.Bloks.Controllers
{
    public class BlockMoveController
    {
        private BlocksGenerator _blocksGenerator;
        private readonly BlockController[,] _blocks;
        private BlockGameGenerator _blockGameGenerator;


        public event Action EndMove;
        public event Action EndNotMove;
        public event Action MoveClick;

        private readonly int _borderGamePole;
        private readonly int _weightGamePole;
        private readonly int _heightGamePole;

        private int _moveCount = 0;

        public BlockMoveController(BlockController[,] blocks, BlocksGenerator blocksGenerator, BlockGameGenerator blockGameGenerator)
        {
            _blocks = blocks;
            _blocksGenerator = blocksGenerator;
            _blockGameGenerator = blockGameGenerator;
            _blockGameGenerator.AddBlock += _blockGameGenerator_AddBlock;
        }

        private void _blockGameGenerator_AddBlock(BlockController block)
        {
            block.Destroyed += OnBlockDestroyed;
            block.Move += OnBlockMove;
            block.ReverceMove += OnBlockReverceMove;
            block.TryMove += Block_TryMove;
            block.ClickMove += BlockOnClickMove;
            block.View.MoveAnimator.EndAnimation += MoveAnimatorOnEndAnimation;
        }

        private void MoveAnimatorOnEndAnimation()
        {
            _moveCount--;
            if (_moveCount == 0)
            {
                CallEndMove();
            }
        }

        private void BlockOnClickMove(BlockController sender, Side side, BlockPoint newPoint)
        {
            throw new NotImplementedException();
        }

        private void Block_TryMove(BlockController sender, Side side, BlockPoint newPoint)
        {
            var nextBlock = _blocks[newPoint.X, newPoint.Y];
            if (nextBlock != null && nextBlock.IsStarted)
            {
                nextBlock.Reverce();
                sender.AcceptMove();
            }
        }

        private void OnBlockReverceMove(BlockController sender, Side side, BlockPoint newPoint)
        {

        }

        private void OnBlockMove(BlockController sender, Side side, BlockPoint newPoint)
        {

        }

        private void OnBlockDestroyed(BlockController block)
        {
            block.Destroyed -= OnBlockDestroyed;
            block.Move -= OnBlockMove;
            block.ReverceMove -= OnBlockReverceMove;
            block.TryMove -= Block_TryMove;
            block.ClickMove -= BlockOnClickMove;
            block.View.MoveAnimator.EndAnimation -= MoveAnimatorOnEndAnimation;
        }

        private void CallEndMove()
        {
            if (EndMove != null)
            {
                EndMove();
            }
        }

        private void CallEndNotUpdate()
        {
            if (EndNotMove != null)
            {
                EndNotMove();
            }
        }

        private void CallMoveClick()
        {
            if (MoveClick != null)
            {
                MoveClick();
            }
        }
    }
}