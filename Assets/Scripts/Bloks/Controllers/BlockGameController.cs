using System.Collections.Generic;
using Assets.Scripts.Bloks.Views;
using Assets.Scripts.ViewDescription;
using UnityEngine;

namespace Assets.Scripts.Bloks.Controllers
{
    public class BlockGameController
    {
        private BlockPoolView _blockPool;
        private readonly BlockController[,] _blocks;
        private IDictionary<Side, Sprite> _sideSprites;
        private BlocksGenerator _blocksGenerator;
        private BlockSpritesViewDescription _spritesViewDescription;
        private BlockStateController _blockStateController;
        private BlockGameGenerator _blockGameGenerator;
        private readonly DestroyBlock _destroyBlock;
        private readonly int _borderGamePole = 3;
        private readonly int _weightGamePole = 15;
        private readonly int _heightGamePole = 15;
        private readonly BlockView _previewBlock;

        private BlockGamePoleController _blockGamePoleController;
        public BlockGameController(BlockPoolView blockPool, IDictionary<Side, Sprite> sideSprites, BlockSpritesViewDescription spritesViewDescription, BlockView previewBlock)
        {
            _blockPool = blockPool;
            _sideSprites = sideSprites;
            _spritesViewDescription = spritesViewDescription;
            _previewBlock = previewBlock;
            _blocksGenerator = new BlocksGenerator(_blockPool, _spritesViewDescription);
            _blocksGenerator.ChangeBlockPrewiew += _blocksGenerator_ChangeBlockPrewiew;
            _blocks = new BlockController[_weightGamePole, _heightGamePole];
            _blockGameGenerator = new BlockGameGenerator(_blocks, _borderGamePole, _weightGamePole, _heightGamePole, _blocksGenerator, _spritesViewDescription, _sideSprites);
            _blockGamePoleController = new BlockGamePoleController(_blocks, _weightGamePole, _heightGamePole, _borderGamePole, _blockGameGenerator);
            _destroyBlock = new DestroyBlock(_blocksGenerator, _blocks, _blockGameGenerator);
            _blockStateController = new BlockStateController(_blockGameGenerator, _blockGamePoleController, _destroyBlock);
          
        }

        private void _blocksGenerator_ChangeBlockPrewiew(BlockView blockView)
        {
            _previewBlock.Image.sprite = blockView.Image.sprite;
        }

        public void UpdateGame()
        {
            _blockStateController.UpdateGame();
        }


    }
}