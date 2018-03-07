using Assets.Scripts.Mining.Controllers.GamePole;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Mining.Views
{
    public delegate void BallMoveSideHandler(MoveSide side);
    public class SidePointerObject : UiPointerObject
    {
        public event BallMoveSideHandler BallMoveSide;
        private Vector2 _clickPosition;
        public override void OnPointerDown(PointerEventData eventData)
        {
            _clickPosition = eventData.position;
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            var startX = _clickPosition.x;
            var startY = _clickPosition.y;
            var endX = eventData.position.x;
            var endY = eventData.position.y;

            var changeX = startX - endX;
            var changeY = startY - endY;

            if (Mathf.Abs(changeY) < 40)
            {
                if (changeX > 15)
                {
                    CallBallMoveSide(MoveSide.Left);
                }
                else if (changeX < -15)
                {
                    CallBallMoveSide(MoveSide.Right);
                }
            }
            else if (Mathf.Abs(changeX) < 40)
            {
                if (changeY < -15)
                {
                    CallBallMoveSide(MoveSide.Up);
                }
                else if (changeY > 15)
                {
                    CallBallMoveSide(MoveSide.Down);
                }
            }

        }

        private void CallBallMoveSide(MoveSide side)
        {
            if (BallMoveSide != null)
            {
                BallMoveSide(side);
            }
        }
    }
}