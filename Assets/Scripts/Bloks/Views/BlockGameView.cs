using System.Collections.Generic;
using Assets.Scripts.ViewDescription;
using UnityEngine;

namespace Assets.Scripts.Bloks.Views
{
    public class BlockGameView : MonoBehaviour
    {
        [SerializeField] private Sprite _leftArrowSprite;
        [SerializeField] private Sprite _rightArrowSprite;
        [SerializeField] private Sprite _upArrowSprite;
        [SerializeField] private Sprite _downArrowSprite;
        [SerializeField] private Sprite _nullArrowSprite;

        [SerializeField] private BlockSpritesViewDescription _spritesViewDescription;

        [SerializeField] private BlockView _blockViewPrefab;

        private IDictionary<Side, Sprite> _sideSprites = new Dictionary<Side, Sprite>(5);
        public BlockGameView()
        {
            _sideSprites.Add(Side.Down,_downArrowSprite);
            _sideSprites.Add(Side.Left,_leftArrowSprite);
            _sideSprites.Add(Side.Right, _rightArrowSprite);
            _sideSprites.Add(Side.Up, _upArrowSprite);
            _sideSprites.Add(Side.Null, _nullArrowSprite);
        }
    }
}