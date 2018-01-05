using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Basic;
using UnityEngine;

namespace Assets.Scripts.ViewDescription
{
	public class ViewDescriptionsCollection<T> : ScriptableObject, ISerializationCallbackReceiver, IEnumerable<T> where T : IIdentified
	{
		[SerializeField] private List<T> _itemsList;
		private readonly IDictionary<string, T> _items = new Dictionary<string, T>();

		public void OnBeforeSerialize()
		{
		}

		public void OnAfterDeserialize()
		{
			_items.Clear();
			foreach (var item in _itemsList)
			{
				_items[item.Id] = item;
			}

			DoAfterSerialize();
		}

		protected virtual void DoAfterSerialize()
		{

		}

		public T this[string key]
		{
			get
			{
				return _items[key];
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _items.Values.GetEnumerator();
		}

		public bool TryGet(string key, out T value)
		{
			return _items.TryGetValue(key, out value);
		}

		public bool ContainsKey(string key)
		{
			return _items.ContainsKey(key);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
