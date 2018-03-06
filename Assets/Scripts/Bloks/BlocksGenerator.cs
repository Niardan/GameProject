using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Bloks.Controllers;
using Assets.Scripts.Bloks.Views;
using Assets.Scripts.ViewDescription;

namespace Assets.Scripts.Bloks
{
    public class BlocksGenerator
    {
        private readonly BlockPoolView _blockPool;
        private readonly BlockSpritesViewDescription _spritesViewDescription;
        private readonly Random _rand = new Random();
        private readonly Stack<BlockController> _blocks = new Stack<BlockController>();

        public BlocksGenerator(BlockPoolView blockPool, BlockSpritesViewDescription spritesViewDescription)
        {
            _blockPool = blockPool;
            _spritesViewDescription = spritesViewDescription;
            _blocks.Push(GenBlock());
        }

        public BlockController GetBlock()
        { 
            var block = _blocks.Pop();
            if (_blocks.Count <= 0)
            {
                _blocks.Push(GenBlock());
            }

            return block;
        }

        public BlockController CurrentBlock { get { return _blocks.Peek(); } }

        private BlockController GenBlock()
        {
            string typeBlock = GetRandomTypeBlock();
            Block block = new Block(typeBlock);
            var blockView = _blockPool.FreeBlock;
            blockView.Image.sprite = _spritesViewDescription.GetSprite(typeBlock);

            return new BlockController(blockView, block);
        }

        private string GetRandomTypeBlock()
        {
            var index = _rand.Next(_spritesViewDescription.Count);
            return _spritesViewDescription.GetType(index);
        }

    }
}