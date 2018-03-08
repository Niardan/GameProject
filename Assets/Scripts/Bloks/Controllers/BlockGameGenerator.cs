using System.Collections.Generic;
using Assets.Scripts.ViewDescription;
using UnityEngine;

namespace Assets.Scripts.Bloks.Controllers
{
    public delegate void BlockHandler(BlockController block);
    public class BlockGameGenerator
    {
        private readonly BlockController[,] _blocks;
        private readonly int _borderGamePole;
        private readonly int _weightGamePole;
        private readonly int _heightGamePole;
        private readonly BlocksGenerator _blocksGenerator;
        private BlockSpritesViewDescription _spritesViewDescription;
        private IDictionary<Side, Sprite> _sideSprites;

        public event BlockHandler AddBlock;

        public BlockGameGenerator(BlockController[,] blocks, int borderGamePole, int weightGamePole, int heightGamePole, BlocksGenerator blocksGenerator, BlockSpritesViewDescription spritesViewDescription, IDictionary<Side, Sprite> sideSprites)
        {
            _blocks = blocks;
            _borderGamePole = borderGamePole;
            _weightGamePole = weightGamePole;
            _heightGamePole = heightGamePole;
            _blocksGenerator = blocksGenerator;
            _spritesViewDescription = spritesViewDescription;
            _sideSprites = sideSprites;
        }

        private void GenBlock(int x, int y, bool started = true)
        {
            if (_blocks[x, y] == null)
            {
                var block = _blocksGenerator.GetBlock();
                block.SetPosition(new BlockPoint(x, y));
                IsMoved(block);
                var side = GetSide(x, y);
                block.Initiation(side);
                if (side != Side.Null)
                {
                    block.View.Arrow.enabled = true;
                    block.View.Arrow.sprite = _sideSprites[side];
                }
                else
                {
                    block.View.Arrow.enabled = false;
                }

                _blocks[x, y] = block;
                block.IsStarted = started;
                CallAddBlock(block);
            }
        }

        public void GenerateStartBorder()
        {
            for (int j = _borderGamePole; j < _heightGamePole - _borderGamePole; j++)
            {
                for (int i = 0; i < _borderGamePole; i++)
                {
                    GenBlock(i, j);
                }
                for (int i = _weightGamePole - _borderGamePole; i < _weightGamePole; i++)
                {
                    GenBlock(i, j);
                }
            }

            for (int i = _borderGamePole; i < _weightGamePole - _borderGamePole; i++)
            {
                for (int j = 0; j < _borderGamePole; j++)
                {
                    GenBlock(i, j);
                }

                for (int j = _heightGamePole - _borderGamePole; j < _heightGamePole; j++)
                {
                    GenBlock(i, j);
                }
            }

            GenBlock(_weightGamePole / 2, _heightGamePole / 2, false);
        }

        public void GenerateStateBorder()
        {
            for (int j = _borderGamePole; j < _heightGamePole - _borderGamePole; j++)
            {
                GenBlock(0, j);
                GenBlock(_weightGamePole - 1, j);
            }

            for (int i = _borderGamePole; i < _weightGamePole - _borderGamePole; i++)
            {
                GenBlock(i, 0);
                GenBlock(i, _heightGamePole - 1);
            }
        }

        private Side GetSide(int x, int y)
        {
            if (x < _borderGamePole)
            {
                return Side.Right;
            }

            if (x >= _weightGamePole - _borderGamePole)
            {
                return Side.Left;
            }
            if (y < _borderGamePole)
            {
                return Side.Down;
            }

            if (y >= _heightGamePole - _borderGamePole)
            {
                return Side.Up;
            }

            return Side.Null;
        }

        public void IsMoved(BlockController block)
        {
            var x = block.Position.X;
            var y = block.Position.Y;
            if (x == _borderGamePole - 1 || y == _borderGamePole - 1 || x == _weightGamePole - _borderGamePole || y == _heightGamePole - _borderGamePole)
            {
                block.Moved = false;
            }
            else
            {
                block.Moved = true;
            }
        }

        private void CallAddBlock(BlockController block)
        {
            if (AddBlock != null)
            {
                AddBlock(block);
            }
        }

    }
}