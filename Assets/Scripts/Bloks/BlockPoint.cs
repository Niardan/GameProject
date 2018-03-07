namespace Assets.Scripts.Bloks
{
    public struct BlockPoint
    {
        private int _x;
        private int _y;

        public BlockPoint(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public int X
        {
            get { return _x; }
        }

        public int Y
        {
            get { return _y; }
        }

        public BlockPoint Add(BlockPoint point)
        {
            return new BlockPoint(_x + point.X, _y + point.Y);
        }
        public BlockPoint Rem(BlockPoint point)
        {
            return new BlockPoint(_x - point.X, _y - point.Y);
        }
    }
}