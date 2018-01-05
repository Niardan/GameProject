using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ViewDescription
{
    public class ViewDescriptionListCollection<T> : ScriptableObject, ISerializationCallbackReceiver, IEnumerable<T> 
    {

        [SerializeField] private List<T> _itemsList;

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize(){
			DoAfterSerialize();
		}
	    protected virtual void DoAfterSerialize()
	    {

	    }

		public T this[int index]
        {
            get
            {
                return _itemsList[index];
            }
        }

        public int Count { get { return _itemsList.Count; } }

        public IEnumerator<T> GetEnumerator()
        {
            return _itemsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}