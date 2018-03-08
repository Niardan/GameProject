using System.Collections.Generic;
using Assets.Scripts.Bloks.Views;
using Assets.Scripts.Mining.Views;

namespace Assets.Scripts.Bloks.Controllers
{
    public class DestroyBlock
    {
        private BlocksGenerator _blocksGenerator;
        private readonly BlockController[,] _blocks;

        private HashSet<BlockController> _changedBlock = new HashSet<BlockController>();
        private BlockPoolView _blockPool;

        public DestroyBlock(BlocksGenerator blocksGenerator, BlockController[,] blocks, BlockGameGenerator blockGameGenerator, BlockPoolView blockPool)
        {
            _blocksGenerator = blocksGenerator;
            _blocks = blocks;
            _blockPool = blockPool;
            blockGameGenerator.AddBlock += _blockGameGenerator_AddBlock;
        }

        private void _blockGameGenerator_AddBlock(BlockController block)
        {
            block.Move += Block_Move;
            block.Destroyed += Block_Destroyed;
        }

        private void Block_Destroyed(BlockController block)
        {
            block.Move -= Block_Move;
            block.Destroyed -= Block_Destroyed;
        }

        private void Block_Move(BlockController sender, Side side, BlockPoint newPoint)
        {
            _changedBlock.Add(sender);
        }

        public void UpdateDestroy()
        {
            foreach (var block in _changedBlock)
            {
                if (!block.IsStarted)
                {
                    CheckBlock(block);
                }
            }
            _changedBlock.Clear();
        }

        private void CheckBlock(BlockController block)
        {
            var blocks = new List<BlockController>();
            int count = CheckRecursion(block, blocks, 1);
            if (count >= 3)
            {
                foreach (var dblock in blocks)
                {
                    dblock.Destroy();
                    _blocks[dblock.Position.X, dblock.Position.Y] = null;
                    _blockPool.FreeBlock = dblock.View;
                }
            }
        }

        private int CheckRecursion(BlockController block, List<BlockController> blocks, int count)
        {
            blocks.Add(block);
            int x = block.Position.X;
            int y = block.Position.Y;
            var newBlock = _blocks[x, y - 1];

            if (newBlock != null && newBlock.Type == block.Type && !blocks.Contains(newBlock) && !newBlock.IsStarted)
            {
                count++;
                count = CheckRecursion(newBlock, blocks, count);
            }
            newBlock = _blocks[x, y + 1];
            if (newBlock != null && newBlock.Type == block.Type && !blocks.Contains(newBlock) && !newBlock.IsStarted)
            {
                count++;
                count = CheckRecursion(newBlock, blocks, count);
            }
            newBlock = _blocks[x - 1, y];
            if (newBlock != null && newBlock.Type == block.Type && !blocks.Contains(newBlock) && !newBlock.IsStarted)
            {
                count++;
                count = CheckRecursion(newBlock, blocks, count);
            }
            newBlock = _blocks[x + 1, y];
            if (newBlock != null && newBlock.Type == block.Type && !blocks.Contains(newBlock) && !newBlock.IsStarted)
            {
                count++;
                count = CheckRecursion(newBlock, blocks, count);
            }
            return count;
        }
    }
}