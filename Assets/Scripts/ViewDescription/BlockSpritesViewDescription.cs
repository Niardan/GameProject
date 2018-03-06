using System;
using System.Collections.Generic;
using Assets.Scripts.Mining.Controllers.GamePole;
using UnityEngine;

namespace Assets.Scripts.ViewDescription
{
    [CreateAssetMenu(fileName = "BlockSpritesViewDescription", menuName = "ScriptableObject/BlockSpritesViewDescription")]
    public class BlockSpritesViewDescription : ViewDescriptionListCollection<BlockSprite>
    {
        private IDictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
        private IDictionary<string, Color> _colors = new Dictionary<string, Color>();
        private List<string> _types = new List<string>();
        protected override void DoAfterSerialize()
        {
            _types.Clear();
            foreach (var sprite in this)
            {
                _sprites[sprite.TypeBlock] = sprite.Sprite;
                _colors[sprite.TypeBlock] = sprite.Color;
                _types.Add(sprite.TypeBlock);
            }
        }

        public ICollection<string> GetTypes()
        {
            return _sprites.Keys;
        }

        public int CountTypes { get { return _types.Count; } }

        public string GetType(int index)
        {
            return _types[index];
        }

        public Color GetColor(string typeBall)
        {
            return _colors[typeBall];
        }

        public Sprite GetSprite(string typeBall)
        {
            return _sprites[typeBall];
        }
    }


    [Serializable]
    public class BlockSprite
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private string _typeBlock;
        [SerializeField] private Color _color;

        public Sprite Sprite
        {
            get { return _sprite; }
        }


        public string TypeBlock
        {
            get { return _typeBlock; }
        }

        public Color Color
        {
            get { return _color; }
        }
    }
}