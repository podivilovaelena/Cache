using System;
using System.Collections.Generic;
using System.Text;

namespace Cache
{
    public class NWayCache<K,V>
    {
        private readonly int _setCount;
        private readonly  int _blockInSetCount;
        private ICacheAlgorithm<K> _cacheAlgorithm;
        
        private Dictionary<int, Dictionary<K, V>> _cachedItems=new Dictionary<int, Dictionary<K, V>>();  
        private Dictionary<int, List<K>> _orders=new Dictionary<int, List<K>>();

        public NWayCache(ICacheAlgorithm<K> cacheAlgorithm, int setCount, int blockInSetCount)
        {
            _blockInSetCount = blockInSetCount;
            _setCount = setCount;
            _cacheAlgorithm = cacheAlgorithm;
            for (int i = 0; i < _setCount; i++)
            {
                _cachedItems.Add(i,new Dictionary<K, V>());
                _orders.Add(i,new List<K>());
            }
        }

        public V Get(K key)
        {
            int index = GetIndex(key);
            int setNumber = GetSetNumber(index);
            var setCachedItems = _cachedItems[setNumber];
            var setOrders = _orders[setNumber];
            if (!setCachedItems.ContainsKey(key))
            {
                return default(V);
            }

            setOrders=_cacheAlgorithm.ChangeOrders(setOrders,key,key);
            _orders[setNumber] = setOrders;
            return setCachedItems[key];
        }

        public void Put(K key, V value)
        {
            int index = GetIndex(key);
            int setNumber = GetSetNumber(index);
            var setCachedItems = _cachedItems[setNumber];
            var setOrders = _orders[setNumber];
            if (setCachedItems.ContainsKey(key))
            {
                setCachedItems[key] = value;
                setOrders = _cacheAlgorithm.ChangeOrders(setOrders, key, key);
            }
            else
            {
                if (setCachedItems.Count < _blockInSetCount)
                {
                    setCachedItems.Add(key, value);
                    setOrders=_cacheAlgorithm.AddKeyToOrders(setOrders,key);
                }
                else
                {
                    K indexToDelete = _cacheAlgorithm.GetIndexToDelete(setOrders);
                    setCachedItems.Remove(indexToDelete);
                    setCachedItems.Add(key, value);
                    setOrders = _cacheAlgorithm.ChangeOrders(setOrders, indexToDelete, key);
                }
            }

            _cachedItems[setNumber] = setCachedItems;
            _orders[setNumber] = setOrders;
        }
        
        private int GetIndex(K key)
        {
            int hashValue = key.GetHashCode();
            return hashValue % (_setCount*_blockInSetCount);
        }

        private int GetSetNumber(int index)
        {
            return index/_blockInSetCount;
        }
    }
}
