using UnityEngine;

namespace Assets.Scripts.Views
{
    public class SimpleView : MonoBehaviour
    {
        [Header("SimpleView")]
        [SerializeField]
        protected GameObject _gameObject;

        public virtual void SetActive(bool active)
        {
            if (CachedGameObject.activeSelf != active)
            {
                CachedGameObject.SetActive(active);
            }
        }

        public virtual void Dispose()
        {
            Destroy(CachedGameObject);
        }

        public GameObject CachedGameObject
        {
            get
            {
                if (_gameObject == null)
                {
                    _gameObject = gameObject;
                }

                return _gameObject;
            }
        }

        public bool IsActive
        {
            get { return CachedGameObject.activeInHierarchy; }
        }
    }
}
