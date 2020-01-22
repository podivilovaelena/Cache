using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cache
{
    public class MruCacheAlgorithm<K>:ICacheAlgorithm<K>
    {
        public K GetIndexToDelete(List<K> orders)
        {
            return orders.Last();
        }

        public List<K> ChangeOrders(List<K> orders, K oldKey, K newKey)
        {
            orders.Remove(oldKey);
            orders.Add(newKey);
            return orders;
        }

        public List<K> AddKeyToOrders(List<K> orders, K key)
        {
            orders.Add(key);
            return orders;
        }
    }
}
