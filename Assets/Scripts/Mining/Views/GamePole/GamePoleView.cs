using Assets.Scripts.Mining.Controllers.GamePole;
using Assets.Scripts.Mining.GamePole;
using UnityEngine;

public class GamePoleView : MonoBehaviour
{
    [SerializeField] private int _countHorCell;
    [SerializeField] private int _countVertCell;
    [SerializeField] private RectTransform _rectDesck;

    private Vector2 _sizeCell;

    public void Activate()
    {
        _sizeCell = new Vector2(_rectDesck.rect.width / _countHorCell, _rectDesck.rect.height / _countVertCell);
    }

    public int CountHorCell { get { return _countHorCell; } }
    public int CountVertCell { get { return _countVertCell; } }

    public void AddBall(BallView ball)
    {
        ball.transform.SetParent(_rectDesck);
    }

    public Vector2 GetCoordPoints(int x, int y)
    {
        float xCoord = x * _sizeCell.x + _sizeCell.x / 2;
        float yCoord = y * _sizeCell.y + _sizeCell.y / 2;
        return new Vector2(xCoord, -yCoord);
    }

    public Vector2 GetCoordPoints(BallPoint position)
    {
        return GetCoordPoints(position.X, (int)position.Y);
    }
    public float StepHor { get { return _sizeCell.x; } }
    public float StepVert { get { return _sizeCell.y; } }
}
