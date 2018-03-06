using System.Collections.Generic;
using Assets.Scripts.ViewDescription;
using UnityEngine;

namespace Assets.Scripts.Bloks.Controllers
{
    public class BlockGameGenerator
    {
        private readonly BlockController[,] _blocks;
        private readonly int _borderGamePole;
        private readonly int _weightGamePole;
        private readonly int _heightGamePole;
        private readonly BlocksGenerator _blocksGenerator;
        private BlockSpritesViewDescription _spritesViewDescription;
        private IDictionary<Side, Sprite> _sideSprites;
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

        private void GenBlock(int x, int y)
        {
            var block = _blocksGenerator.GetBlock();
            block.SetPosition(new BlockPoint(x, y));
            IsMoved(block);
            var side = GetSide(x, y);
            block.Block.Initiation(side);
            block.View.Arrow.sprite = _sideSprites[side];
            _blocks[x, y] = block;
        }

        public void GenerateStateBorder()
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

            if (y >= _weightGamePole - _heightGamePole)
            {
                return Side.Up;
            }

            return Side.Null;
        }

        private void IsMoved(BlockController block)
        {
            var x = block.Position.X;
            var y = block.Position.Y;
            if (x == _borderGamePole - 1 || y == _borderGamePole - 1 || x == _weightGamePole - _borderGamePole || y == _heightGamePole - _borderGamePole)
            {
                block.Block.Moved = false;
            }
            else
            {
                block.Block.Moved = true;
            }
        }

    }
}