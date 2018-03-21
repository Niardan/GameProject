namespace Assets.Scripts.Bloks.Controllers
{
    public class ClickMoveController
    {
        private readonly int _borderGamePole;
        private readonly int _weightGamePole;
        private readonly int _heightGamePole;

        private readonly BlockController[,] _blocks;


        private bool OnClickMove(BlockController sender, BlockPoint newPoint)
        {
            if (_blocks[newPoint.X, newPoint.Y] == null && AllowClickMoved(sender))
            {
                sender.Moved = true;
                sender.IsStarted = false;
                sender.AcceptMove();
                return true;
            }
            return false;
        }
        
        private bool AllowClickMoved(BlockController block)
        {
            int x = block.Position.X;
            int y = block.Position.Y;
            if (block.Side == Side.Left || block.Side == Side.Right)
            {
                for (int i = _borderGamePole; i < _weightGamePole - _borderGamePole; i++)
                {
                    if (_blocks[i, y] != null)
                    {
                        return true;
                    }
                }
            }
            if (block.Side == Side.Up || block.Side == Side.Down)
            {
                for (int i = _borderGamePole; i < _heightGamePole - _borderGamePole; i++)
                {
                    if (_blocks[x, i] != null)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}