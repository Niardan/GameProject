using System.Collections.Generic;

namespace Assets.Scripts.Bloks.Controllers.Moved
{
    public class TryMoveController
    {
        private BlocksGenerator _blocksGenerator;
        private readonly BlockController[,] _blocks;
        private BlockGameGenerator _blockGameGenerator;

        private Dictionary<BlockController, BlockPoint> _changedBlock = new Dictionary<BlockController, BlockPoint>();

        private readonly int _borderGamePole;
        private readonly int _weightGamePole;
        private readonly int _heightGamePole;

        private bool TryMove(BlockController sender, BlockPoint newPoint)
        {
            if (_blocks[newPoint.X, newPoint.Y] == null)
            {
                _blocks[sender.Position.X, sender.Position.Y] = null;
                _blocks[newPoint.X, newPoint.Y] = sender;
                sender.AcceptMove();
            }
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
    }
}