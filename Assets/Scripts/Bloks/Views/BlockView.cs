using Assets.Scripts.Mining.Views;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Bloks.Views
{
    public class BlockView : MonoBehaviour
    {
        [SerializeField] private RectTransform _block;
        [SerializeField] private UiPointerObject _uiPointerObject;
        [SerializeField] private MoveAnimator _moveAnimator;
        [SerializeField] private Image _image;
        [SerializeField] private Image _arrow;
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private float _destroyTime;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private float _sizeCell;

        public RectTransform Block { get { return _block; } }
        public UiPointerObject PointerObject { get { return _uiPointerObject; } }
        public MoveAnimator MoveAnimator { get { return _moveAnimator; } }
        public Image Image { get { return _image; } }
        public Image Arrow { get { return _arrow; } }
        public ParticleSystem ParticleSystem { get { return _particleSystem; } }

        public float DestroyTime
        {
            get { return _destroyTime; }
        }

        public void SetPosition(BlockPoint point)
        {
            var x = point.X * _sizeCell;
            var y = -point.Y * _sizeCell;
            _block.anchoredPosition = new Vector2(x, y);
        }

        public void MoveTo(BlockPoint point)
        {
            var x = point.X * _sizeCell;
            var y = -point.Y * _sizeCell;
            MoveAnimator.MoveTo(new Vector2(x, y));
        }

        public void SetActive(bool active)
        {
            Image.enabled = active;
            if (_gameObject.activeSelf != active)
            {
                _gameObject.SetActive(active);
            }
        }
    }
}