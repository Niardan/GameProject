using Assets.Scripts.Bloks.Views;
using UnityEngine;

namespace Assets.Scripts.Bloks.Controllers
{
    public delegate void MoveBlockHandler(BlockController sender, Side side, BlockPoint newPoint);

    public class BlockController
    {
        private readonly BlockView _blockView;
        private BlockPoint _position;
        private BlockPoint _oldPosition;
        private Side _side;
        private BlockPoint _sidePoint;
        private readonly string _type;
        private bool _moved;
        private bool _allowMove;

        private bool _isStarted = true;

        public event MoveBlockHandler ClickMove;
        public event MoveBlockHandler TryMove;
        public event MoveBlockHandler Move;
        public event BlockHandler Destroyed;

        public BlockController(BlockView blockView, string type)
        {
            _blockView = blockView;
            _type = type;
            _blockView.PointerObject.EventPointerDown += OnEventPointerDown;
        }

        private void OnEventPointerDown(UnityEngine.EventSystems.PointerEventData obj)
        {
            OnClickMove();
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

        public void Destroy()
        {
            Debug.Log("Destroy");
            CallDestroyed();
        }

        public BlockPoint Position
        {
            get { return _position; }
            set { _position = value; }

        }

        public BlockView View
        {
            get { return _blockView; }
        }

        public void SetPosition(BlockPoint position)
        {
            _oldPosition = position;
            _position = position;
            _blockView.SetPosition(position);
        }

        public bool Moved
        {
            get { return _moved; }
            set { _moved = value; }
        }

        public Side Side
        {
            get { return _side; }
        }

        public bool IsStarted
        {
            get { return _isStarted; }
            set { _isStarted = value; }
        }

        public string Type
        {
            get { return _type; }
        }

        public void Update()
        {
            if (_side != Side.Null && _moved)
            {
                CallTryMove();
            }
        }

        public void OnClickMove()
        {
            if (!_moved)
            {
                CallClickMove();
            }
        }

        public void AcceptMove()
        {
            _allowMove = true;
        }

        public void UpdateMove()
        {
            if (_allowMove)
            {
                _allowMove = false;
                _oldPosition = _position;
                _position = GetNewPoint();
                _blockView.MoveTo(_position);
                CallMove();
            }
           
        }

        private void CallTryMove()
        {
            if (TryMove != null)
            {
                TryMove(this, _side, GetNewPoint());
            }
        }

        private void CallClickMove()
        {
            if (ClickMove != null)
            {
                ClickMove(this, _side, GetNewPoint());
            }
        }

        private void CallMove()
        {
            if (Move != null)
            {
                Move(this, _side, _oldPosition);
            }
        }

        private void CallDestroyed()
        {
            if (Destroyed != null)
            {
                Destroyed(this);
            }
        }

        private BlockPoint GetNewPoint()
        {
            return _position.Add(_sidePoint);
        }

    }
}