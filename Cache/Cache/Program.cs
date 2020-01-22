using System;

namespace Cache
{
    class Program
    {
        static void Main(string[] args)
        {
          NWayCache<int,int> myCache=new NWayCache<int, int>(new LruCacheAlgorithm<int>(), 4,2);
          myCache.Put(1,1);
        }
    }
}
