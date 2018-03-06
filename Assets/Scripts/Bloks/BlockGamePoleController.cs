using System.Collections.Generic;
using Assets.Scripts.Bloks.Controllers;
using Assets.Scripts.ViewDescription;
using UnityEngine;

namespace Assets.Scripts.Bloks
{
    public class BlockGamePoleController
    {
        private readonly BlockController[,] _blocks;
        private readonly int _borderGamePole;
        private readonly int _weightGamePole;
        private readonly int _heightGamePole;
        private readonly BlocksGenerator _blocksGenerator;
        private BlockSpritesViewDescription _spritesViewDescription;
        private IDictionary<Side, Sprite> _sideSprites;

        private BlockGameGenerator _blockGameGenerator;

        public BlockGamePoleController(int heightGamePole, int weightGamePole, int borderGamePole, BlocksGenerator blocksGenerator, BlockSpritesViewDescription spritesViewDescription, IDictionary<Side, Sprite> sideSprites)
        {
            _borderGamePole = borderGamePole;
            _blocksGenerator = blocksGenerator;
            _spritesViewDescription = spritesViewDescription;
            _sideSprites = sideSprites;
            _weightGamePole = weightGamePole;
            _heightGamePole = heightGamePole;
            _blocks = new BlockController[weightGamePole, heightGamePole];
            _blockGameGenerator = new BlockGameGenerator(_blocks, _borderGamePole, _weightGamePole, _heightGamePole, _blocksGenerator, _spritesViewDescription, _sideSprites);
            _blockGameGenerator.GenerateStateBorder();

            foreach (var block in _blocks)
            {
                if (block != null)
                {
                    AddBlock(block);
                }
            }
        }

        private void AddBlock(BlockController block)
        {
            block.Block.ClickTryMove += OnClickTryMove;
            block.Block.Move += OnMove;
            block.Block.TryMove += OnBlockTryMove;
        }

        private void OnBlockTryMove(Block sender, Side side, BlockPoint newPoint)
        {
            int x = 0;
        }

        private void OnMove(Block sender, Side side, BlockPoint newPoint)
        {
            throw new System.NotImplementedException();
        }

        private void OnClickTryMove(Block sender, Side side, BlockPoint newPoint)
        {
            throw new System.NotImplementedException();
        }

        private void NextState()
        {

        }
    }
}