using Assets.Scripts.Mining.Views;
using Assets.Scripts.Views.UI.Common;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mining.GamePole
{
    public class BallView : MonoBehaviour
    {
        [SerializeField] private RectTransform _ball;
        [SerializeField] private SidePointerObject _uiPointerObject;
        [SerializeField] private MoveAnimator _moveAnimator;
        [SerializeField] private Image _image;
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private float _destroyTime;
        [SerializeField] private ParticleSystem _particleSystem;

        public RectTransform Ball { get { return _ball; } }
        public SidePointerObject PointerObject { get { return _uiPointerObject; } }
        public MoveAnimator MoveAnimator { get { return _moveAnimator; } }
        public Image Image { get { return _image; } }
        public ParticleSystem ParticleSystem { get { return _particleSystem; } }

        public float DestroyTime
        {
            get { return _destroyTime; }
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