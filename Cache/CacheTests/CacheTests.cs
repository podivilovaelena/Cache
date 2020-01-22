using Cache;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CacheTests
{
    [TestClass]
    public class CacheTests
    {
        
        [TestMethod]
        public void NwayCacheTest1()
        {
            NWayCache<int, string> cache = new NWayCache<int, string>(new LruCacheAlgorithm<int>(), 8, 2);
            cache.Put(16, "first");
            cache.Put(32, "second");
            cache.Put(48, "third");
            Assert.AreEqual(cache.Get(16), null);
            Assert.AreEqual(cache.Get(32), "second");
            Assert.AreEqual(cache.Get(48), "third");
        }
        

        [TestMethod]
        public void NwayCacheTest2()
        {
            NWayCache<int, string> cache = new NWayCache<int, string>(new LruCacheAlgorithm<int>(), 8, 2);
            cache.Put(7, "first");
            cache.Put(23, "second");
            cache.Put(39, "third");
            Assert.AreEqual(cache.Get(7), null);
            Assert.AreEqual(cache.Get(23), "second");
            Assert.AreEqual(cache.Get(39), "third");
        }
        
        [TestMethod]
        public void NwayCacheTest3()
        {
            NWayCache<int, string> cache = new NWayCache<int, string>(new LruCacheAlgorithm<int>(), 8, 2);
            cache.Put(15, "first");
            cache.Put(31, "second");
            cache.Put(47, "third");
            Assert.AreEqual(cache.Get(15), null);
            Assert.AreEqual(cache.Get(31), "second");
            Assert.AreEqual(cache.Get(47), "third");
        }

        [TestMethod]
        public void NwayCacheTest4()
        {
            NWayCache<int, string> cache = new NWayCache<int, string>(new MruCacheAlgorithm<int>(), 8, 2);
            cache.Put(16, "first");
            cache.Put(32, "second");
            cache.Put(48, "third");
            Assert.AreEqual(cache.Get(16), "first");
            Assert.AreEqual(cache.Get(32), null);
            Assert.AreEqual(cache.Get(48), "third");
        }

    }
}
