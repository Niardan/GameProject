using System;
using System.Collections;
using Assets.Scripts.Mining.GamePole;
using Assets.Scripts.Mining.Views;
using UnityEngine;

namespace Assets.Scripts.Mining.Controllers.GamePole
{
	public delegate void BallMovedHandler(BallController ball, MoveSide side);
	public delegate void BallAnimationEndHandler(BallController ball);
	public class BallController
	{
		private readonly BallView _ballView;
		private readonly MoveAnimator _moveAnimator;
		public event BallMovedHandler BallMoved;
		public event BallAnimationEndHandler BallAnimationEnd;
		public event BallAnimationEndHandler BallDestroedEnd;

		private int _xPositinon;
		private int _yPosition;
		private readonly TypeBall _typeBall;
		private readonly ResourcesCount _typeResources;
		private bool _isChanged;
		private bool _isDestroed;

		public BallController(BallView ballView, BallPoint position, TypeBall typeBall, ResourcesCount typeResources) : this(ballView, position.X, position.Y, typeBall, typeResources)
		{
		}
		public BallController(BallView ballView, int xPositinon, int yPosition, TypeBall typeBall, ResourcesCount typeResources)
		{
			_ballView = ballView;
			_xPositinon = xPositinon;
			_yPosition = yPosition;
			_typeBall = typeBall;
			_typeResources = typeResources;
			_moveAnimator = _ballView.MoveAnimator;
			_ballView.PointerObject.BallMoveSide += PointerObject_BallMoveSide;
			_ballView.MoveAnimator.EndAnimation += OnEndAnimation;
			_isChanged = true;
		}

		private void OnEndAnimation()
		{
			CallEndAnimation();
		}

		public void SetSprite(Sprite sprite, Color color)
		{
			_ballView.Image.sprite = sprite;
			var main = _ballView.ParticleSystem.main;
			main.startColor = color;
		}

		public void BallSetPosition(Vector2 koord, BallPoint position)
		{
			_ballView.Ball.anchoredPosition = koord;
			_xPositinon = position.X;
			_yPosition = position.Y;
			_isChanged = true;
		}

		public void BallSetPosition(BallPoint position)
		{
			_xPositinon = position.X;
			_yPosition = position.Y;
			_isChanged = true;
		}

		public void BallMove(Vector2 koord, int xPosition, int yPosition)
		{
			_xPositinon = xPosition;
			_yPosition = yPosition;
			_moveAnimator.MoveTo(koord);
			_isChanged = true;
		}

		public BallPoint Positinon
		{
			get { return new BallPoint(_xPositinon, _yPosition); }
			set
			{
				_xPositinon = value.X;
				_yPosition = value.Y;
			}
		}

		public TypeBall TypeBall
		{
			get { return _typeBall; }
		}

		public ResourcesCount TypeResources
		{
			get { return _typeResources; }
		}

		public Vector2 Koordinates { get { return _ballView.Ball.anchoredPosition; } }

		public BallView View
		{
			get { return _ballView; }
		}

		public bool IsChanged
		{
			get { return _isChanged; }
			set { _isChanged = value; }
		}

		public bool IsDestroed
		{
			get { return _isDestroed; }
			set { _isDestroed = value; }
		}

		private void PointerObject_BallMoveSide(MoveSide side)
		{
			CallBallMoved(side);
		}

		private void CallBallMoved(MoveSide side)
		{
			if (BallMoved != null)
			{
				BallMoved(this, side);
			}
		}

		private void CallEndAnimation()
		{
			if (BallAnimationEnd != null)
			{
				BallAnimationEnd(this);
			}
		}

		private void CallBallDestroedEnd()
		{
			if (BallDestroedEnd != null)
			{
				BallDestroedEnd(this);
			}
		}

		public void Destroy()
		{
			_ballView.Image.enabled = false;
			_ballView.ParticleSystem.gameObject.SetActive(true);
			_ballView.ParticleSystem.Play();
			_ballView.StartCoroutine(OnDestroed());
		}

		private IEnumerator OnDestroed()
		{

			yield return new WaitForSeconds(_ballView.DestroyTime);
			_ballView.ParticleSystem.Stop();
			_ballView.ParticleSystem.gameObject.SetActive(false);
			CallBallDestroedEnd();
		}
	}

}

