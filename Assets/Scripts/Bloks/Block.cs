namespace Assets.Scripts.Bloks
{
    public delegate void MoveBlockHandler(Block sender, Side side, BlockPoint newPoint);
    public class Block
    {
        private BlockPoint _point;
        private Side _side;
        private BlockPoint _sidePoint;
        private readonly string _type;
        private bool _moved;

        public event MoveBlockHandler ClickTryMove;
        public event MoveBlockHandler TryMove;
        public event MoveBlockHandler Move;
        public Block(string type)
        {
            _type = type;
        }

        public void Initiation(Side side)
        {
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

        public BlockPoint Position
        {
            get { return _point; }
            set { _point = value; }
        }

        public void Update()
        {
            if (_side != Side.Null && _moved)
            {
                CallTryMove();
            }
        }

        public void ClickMove()
        {
            if (!_moved)
            {
                CallClickTryMove();
            }
        }

        public void AcceptMove()
        {
            _point = GetNewPoint();
            CallMove();
        }

        private void CallTryMove()
        {
            if (TryMove != null)
            {
                TryMove(this, _side, GetNewPoint());
            }
        }

        private void CallClickTryMove()
        {
            if (ClickTryMove != null)
            {
                ClickTryMove(this, _side, GetNewPoint());
            }
        }

        private void CallMove()
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