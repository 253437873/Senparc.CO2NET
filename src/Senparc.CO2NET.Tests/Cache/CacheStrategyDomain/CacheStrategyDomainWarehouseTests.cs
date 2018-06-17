using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Cache;
using Senparc.CO2NET.Tests.TestEntities;
using System;

namespace Senparc.CO2NET.Tests.Cache.CacheStrategyDomain
{


    [TestClass]
    public class CacheStrategyDomainWarehouseTests
    {
        [TestMethod]
        public void RegisterAndGetTest()
        {
            //��ԭĬ�ϻ���״̬
            CacheStrategyFactory.RegisterObjectCacheStrategy(()=>LocalObjectCacheStrategy.Instance);

            //ע��
            CacheStrategyDomainWarehouse.RegisterCacheStrategyDomain(TestExtensionCacheStrategy.Instance);

            //��ȡ

            //��ȡ��ǰ������ԣ�Ĭ��Ϊ�ڴ滺�棩
            var objectCache = CacheStrategyFactory.GetObjectCacheStrategyInstance();
            var testCacheStrategy = CacheStrategyDomainWarehouse
                .GetDomainExtensionCacheStrategy(objectCache, new TestCacheDomain());

            Assert.IsInstanceOfType(testCacheStrategy, typeof(TestExtensionCacheStrategy));

            var baseCache = testCacheStrategy.BaseCacheStrategy();
            Assert.IsInstanceOfType(baseCache, objectCache.GetType());


            //д��
            var testStr = Guid.NewGuid().ToString();
            baseCache.Set("TestCache", testStr);

            //��ȡ
            var result = (testCacheStrategy as TestExtensionCacheStrategy).GetTestCache("TestCache");
            Assert.AreEqual(testStr, result);
        }
    }
}
