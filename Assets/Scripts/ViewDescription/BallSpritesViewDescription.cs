using System;
using System.Collections.Generic;
using Assets.Scripts.Mining.Controllers.GamePole;
using UnityEngine;

namespace Assets.Scripts.ViewDescription
{
    [CreateAssetMenu(fileName = "BallSpritesViewDescription", menuName = "ScriptableObject/BallSpritesViewDescription")]
    public class BallSpritesViewDescription : ViewDescriptionListCollection<BallSprite>
    {
        private IDictionary<TypeBall, Sprite> _spritesWithoutOre = new Dictionary<TypeBall, Sprite>();
        private IDictionary<TypeBall, Sprite> _spritesOre = new Dictionary<TypeBall, Sprite>();
        private IDictionary<TypeBall, Color> _colors = new Dictionary<TypeBall, Color>();
        protected override void DoAfterSerialize()
        {
            foreach (var sprite in this)
            {
                _spritesWithoutOre[sprite.Ball] = sprite.Sprite;
                _spritesOre[sprite.Ball] = sprite.SpriteOre;
                _colors[sprite.Ball] = sprite.Color;
            }
        }

        public Color GetColor(TypeBall typeBall)
        {
            return _colors[typeBall];
        }

        public Sprite GetSprite(TypeBall typeBall, bool isOre)
        {
            if (isOre)
            {
                return _spritesOre[typeBall];
            }
            else
            {
                return _spritesWithoutOre[typeBall];
            }
        }
    }


    [Serializable]
    public class BallSprite
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Sprite _spriteOre;
        [SerializeField] private TypeBall _typeBall;
        [SerializeField] private Color _color;

        public Sprite Sprite
        {
            get { return _sprite; }
        }

        public Sprite SpriteOre
        {
            get { return _spriteOre; }
        }

        public TypeBall Ball
        {
            get { return _typeBall; }
        }

        public Color Color
        {
            get { return _color; }
        }
    }
}