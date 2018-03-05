using System.Collections.Generic;

namespace Assets.Scripts.Bloks
{
    public class BlockGamePoleController
    {
        private readonly Block[,] _blocks;

        public BlockGamePoleController(int heightGamePole, int weightGamePole, int borderGamePole)
        {
            _blocks = new Block[weightGamePole, heightGamePole];
        }

        private void AddBlock(Block block)
        {
            block.ClickTryMove += OnClickTryMove;
            block.Move += OnMove;
            block.TryMove += OnBlockTryMove;
        }

        private void OnBlockTryMove(Block sender, Side side, BlockPoint newPoint)
        {
            throw new System.NotImplementedException();
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