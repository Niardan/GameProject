using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Bloks.Views
{
    public class BlockPoolView : MonoBehaviour
    {
        [SerializeField] private int _poolSize;
        [SerializeField] private BlockView _prefab;
        [SerializeField] private Transform _parrent;

        private readonly Queue<BlockView> _freeBallPool;

        public BlockPoolView()
        {
            _freeBallPool = new Queue<BlockView>(_poolSize);
            for (int i = 0; i < _poolSize; i++)
            {
                _freeBallPool.Enqueue(Object.Instantiate(_prefab, _parrent));
            }

        }

        public BlockView FreeBlock
        {
            get
            {
                var block = _freeBallPool.Count > 0 ? _freeBallPool.Dequeue() : Object.Instantiate(_prefab, _parrent);
                return block;
            }
            set
            {
                value.Block.anchoredPosition = new Vector2(1000, 1000);
                value.SetActive(false);
                value.Arrow.enabled = false;
                _freeBallPool.Enqueue(value);
            }
        }
    }
}