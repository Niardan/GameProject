using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Mining.Views
{
    public class MoveAnimator : MonoBehaviour
    {
        [SerializeField] private RectTransform _object;
        [SerializeField] private float _speed;
        private Vector2 _endPoint;
        private Coroutine _moved;

        public event Action EndAnimation;
        public void MoveTo(Vector2 endPoint)
        {
            _endPoint = endPoint;
            if (_moved != null)
            {
                StopCoroutine(_moved);
            }

                _moved = StartCoroutine(Moved(endPoint));
        }

        private IEnumerator Moved(Vector2 endPoint)
        {
            Vector2 startPosition = _object.anchoredPosition;
            float time = 0;
            while (true)
            {
                _object.anchoredPosition = Vector2.Lerp(startPosition, endPoint, time*10);
                time += (Time.deltaTime * _speed)/10;
              
                yield return new WaitForEndOfFrame();
                if (_object.anchoredPosition == endPoint)
                {
                    if (EndAnimation != null)
                    {
                        _moved=null;
                        EndAnimation();
                    }

                    break;
                }
            }

        }
    }
}