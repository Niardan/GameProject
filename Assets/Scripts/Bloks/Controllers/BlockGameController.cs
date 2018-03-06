using System.Collections.Generic;
using Assets.Scripts.Bloks.Views;
using Assets.Scripts.ViewDescription;
using UnityEngine;

namespace Assets.Scripts.Bloks.Controllers
{
    public class BlockGameController
    {
        private BlockPoolView _blockPool;
        private IDictionary<Side, Sprite> _sideSprites;
        private BlocksGenerator _blocksGenerator;
        private BlockSpritesViewDescription _spritesViewDescription;

        private BlockGamePoleController _blockGamePoleController;
        public BlockGameController(BlockPoolView blockPool, IDictionary<Side, Sprite> sideSprites, BlockSpritesViewDescription spritesViewDescription)
        {
            _blockPool = blockPool;
            _sideSprites = sideSprites;
            _spritesViewDescription = spritesViewDescription;
            _blocksGenerator = new BlocksGenerator(_blockPool, _spritesViewDescription);
            _blockGamePoleController = new BlockGamePoleController(15, 15, 3, _blocksGenerator, _spritesViewDescription, _sideSprites);
        }


    }
}