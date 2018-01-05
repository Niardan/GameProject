namespace Assets.Scripts.Mining.Controllers.GamePole
{
    public struct BallPoint
    {
        private int _x;
        private int _y;
        public BallPoint(int x, int y)
        {
            _x = x;
            _y = y;
        }
        public int X { get { return _x; } }
        public int Y { get { return _y; } }
    }
}