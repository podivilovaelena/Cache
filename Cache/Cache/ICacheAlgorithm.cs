using System;
using System.Collections.Generic;
using System.Text;

namespace Cache
{
    public interface ICacheAlgorithm<K>
    {
        K GetIndexToDelete(List<K> orders);

        List<K> ChangeOrders(List<K> orders, K oldKey, K newKey);

        List<K> AddKeyToOrders(List<K> orders, K key);

    }
}
