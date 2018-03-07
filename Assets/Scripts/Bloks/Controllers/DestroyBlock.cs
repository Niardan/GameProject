using System.Collections.Generic;

namespace Assets.Scripts.Bloks.Controllers
{
    public class DestroyBlock
    {
        private BlocksGenerator _blocksGenerator;
        private readonly BlockController[,] _blocks;

        private HashSet<BlockController> _changedBlock = new HashSet<BlockController>();

        public DestroyBlock(BlocksGenerator blocksGenerator, BlockController[,] blocks, BlockGameGenerator blockGameGenerator)
        {
            _blocksGenerator = blocksGenerator;
            _blocks = blocks;
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
                CheckBlock(block);
            }
            _changedBlock.Clear();
        }

        private void CheckBlock(BlockController block)
        {
            var blocks = new List<BlockController>();
            if (CheckRecursion(block, blocks, 0) > 3)
            {
                foreach (var dblock in blocks)
                {
                    dblock.Destroy();
                }
            }
        }

        private int CheckRecursion(BlockController block, List<BlockController> blocks, int count)
        {
            blocks.Add(block);
            int x = block.Position.X;
            int y = block.Position.Y;
            var newBlock = _blocks[x, y - 1];

            if (newBlock != null && !newBlock.IsStarted)
            {
                count = CheckRecursion(newBlock, blocks, count);
            }
            newBlock = _blocks[x, y + 1];
            if (newBlock != null && !newBlock.IsStarted)
            {
                count = CheckRecursion(newBlock, blocks, count);
            }
            newBlock = _blocks[x - 1, y];
            if (newBlock != null && !newBlock.IsStarted)
            {
                count = CheckRecursion(newBlock, blocks, count);
            }
            newBlock = _blocks[x + 1, y];
            if (newBlock != null && !newBlock.IsStarted)
            {
                count = CheckRecursion(newBlock, blocks, count);
            }

            count++;
            return count;
        }
    }
}