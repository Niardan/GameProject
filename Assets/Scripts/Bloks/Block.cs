namespace Assets.Scripts.Bloks
{
    public delegate void MoveBlockHandler(Block sender, Side side, BlockPoint newPoint);
    public class Block
    {
        private BlockPoint _point;
        private readonly Side _side;
        private readonly BlockPoint _sidePoint;
        private bool _moved;

        public event MoveBlockHandler TryMove;

        public event MoveBlockHandler Move;
        public Block(BlockPoint point, Side side)
        {
            _point = point;
            _side = side;
            switch (side)
            {
                case Side.Down:
                    _sidePoint = new BlockPoint(0, 1);
                    break;
                case Side.Up:
                    _sidePoint = new BlockPoint(0, -1);
                    break;
                case Side.Left:
                    _sidePoint = new BlockPoint(-1, 0);
                    break;
                case Side.Right:
                    _sidePoint = new BlockPoint(1, 0);
                    break;
                default:
                    _sidePoint = new BlockPoint(0, 0);
                    break;
            }
        }

        public bool Moved
        {
            get { return _moved; }
            set { _moved = value; }
        }

        public void Update()
        {
            if (_moved)
            {
                CallTryMove();
            }
        }

        public void AcceptMove()
        {
            _point = GetNewPoint();
            CallMove();
        }

        protected virtual void CallTryMove()
        {
            if (TryMove != null)
            {
                TryMove(this, _side, GetNewPoint());
            }
        }

        protected virtual void CallMove()
        {
            if (Move != null)
            {
                Move(this, _side, _point);
            }
        }

        private BlockPoint GetNewPoint()
        {
            return _point.Add(_sidePoint);
        }
    }
}