using System.Collections.Generic;
using Assets.Scripts.Bloks.Views;
using SyntaxTree.VisualStudio.Unity.Bridge.Configuration;

namespace Assets.Scripts.Bloks.Controllers
{
    public class ReverseMoveController
    {
        private BlocksGenerator _blocksGenerator;
        private readonly BlockController[,] _blocks;
        private BlockGameGenerator _blockGameGenerator;

        private Dictionary<BlockController, BlockPoint> _changedBlock = new Dictionary<BlockController, BlockPoint>();

        private readonly int _weightGamePole;
        private readonly int _heightGamePole;

        public ReverseMoveController(BlockController[,] blocks, BlocksGenerator blocksGenerator, BlockGameGenerator blockGameGenerator)
        {
            _blocks = blocks;
            _blocksGenerator = blocksGenerator;
            _blockGameGenerator = blockGameGenerator;
        }

        private bool ReverceCheck(BlockController sender, BlockPoint newPoint)
        {
            int x = newPoint.X;
            int y = newPoint.Y;

            var nextBlock = _blocks[x, y];
            if (nextBlock != null && nextBlock.IsStarted&&nextBlock.Side!=sender.Side)
            {
                nextBlock.Reverce();
                _changedBlock.Add(sender, newPoint);
                sender.AcceptMove();
                return true;
            }
            return false;
        }

        private bool ReverceMove(BlockController sender, BlockPoint newPoint)
        {
            int x = newPoint.X;
            int y = newPoint.Y;

            if (x < 0 || x >= _weightGamePole || y < 0 || y >= _heightGamePole)
            {
                _blocksGenerator.SetBlock(sender);
                MoveBall();
                return false;
            }

            var nextBlock = _blocks[x, y];
            if (nextBlock != null && nextBlock.IsStarted)
            {
                nextBlock.Reverce();
                _changedBlock.Add(sender, newPoint);
                sender.AcceptMove();
                return true;
            }
            return false;
        }

        private void MoveBall()
        {
            foreach (var block in _changedBlock)
            {
                _blocks[block.Value.X, block.Value.Y] = block.Key;
            }
        }
    }
}