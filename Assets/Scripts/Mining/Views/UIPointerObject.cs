using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Views.UI.Common
{
    public class UIPointerObject : SimpleView, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<PointerEventData> EventPointerClick;
        public event Action<PointerEventData> EventPointerDown;
        public event Action<PointerEventData> EventPointerUp;
        public event Action<PointerEventData> EventPointerEnter;
        public event Action<PointerEventData> EventPointerExit;

        public event Action<PointerEventData, bool> EventHold;

        private const float HOLD_DURATION = 0.25f;

        protected bool _pressed;
        protected bool _holded;
        private IEnumerator _holdWaiterRoutine;

        private IEnumerator WaitToHold(PointerEventData eventData)
        {
            yield return null;
            yield return new WaitForSeconds(HOLD_DURATION);

            _holded = true;
            _holdWaiterRoutine = null;

            CallHold(eventData, true);
        }

        protected void StopHoldWaiter()
        {
            if (_holdWaiterRoutine != null)
            {
                StopCoroutine(_holdWaiterRoutine);
                _holdWaiterRoutine = null;
            }

            if (_holded)
            {
                _holded = false;

                CallHold(null, false);
            }
        }

        protected void CallHold(PointerEventData eventData, bool isHold)
        {
            if (EventHold != null) EventHold(eventData, isHold);
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            _pressed = true;

            StopHoldWaiter();
            _holdWaiterRoutine = WaitToHold(eventData);
            StartCoroutine(_holdWaiterRoutine);

            if (EventPointerDown != null)
            {
                EventPointerDown(eventData);
            }
        }

        public void OnApplicationPause(bool pause)
        {
            if (!pause && _pressed) OnPointerUp(null);
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            _pressed = false;

            StopHoldWaiter();

            if (EventPointerUp != null)
            {
                EventPointerUp(eventData);
            }
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            _pressed = false;

            if (EventPointerClick != null)
            {
                EventPointerClick(eventData);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (EventPointerEnter != null)
            {
                EventPointerEnter(eventData);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (EventPointerExit != null)
            {
                EventPointerExit(eventData);
            }
        }

        private void OnDisable()
        {
            if (_pressed)
            {
                OnPointerUp(new PointerEventData(EventSystem.current));
            }
        }

        public bool Pressed { get { return _pressed; } }
    }
}