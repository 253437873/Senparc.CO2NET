using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Cache;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Senparc.CO2NET.Tests
{
    [TestClass]
    public class CacheSerializeExtensionTest
    {
        public class TestClass
        {
            public string ID { get; set; }
            public long Star { get; set; }
            public DateTime AddTime { get; set; }
        }

        [TestMethod]
        public void CacheWrapperTest()
        {
            var testClass = new TestClass() {
                ID = Guid.NewGuid().ToString(),
                Star = DateTime.Now.Ticks,
                AddTime = DateTime.Now
            };

            var json = testClass.SerializeToCache();
            Console.WriteLine(json);

            var obj = json.DeserializeFromCache<TestClass>();
            Assert.AreEqual(obj.ID, testClass.ID);
        }

        [TestMethod]
        public void CacheWrapperEfficiencyTest()
        {
            Console.WriteLine("��ʼCacheWrapper�첽����");
            var threadCount = 10;
            var finishCount = 0;
            List<Thread> threadList = new List<Thread>();
            for (int i = 0; i < threadCount; i++)
            {
                var thread = new Thread(() => {

                    var testClass = new TestClass()
                    {
                        ID = Guid.NewGuid().ToString(),
                        Star = DateTime.Now.Ticks,
                        AddTime = DateTime.Now
                    };

                    var dtx = DateTime.Now;
                    var json = testClass.SerializeToCache();
                    //Console.WriteLine(json);
                    Console.WriteLine($"testClass.SerializeToCache ��ʱ��{(DateTime.Now - dtx).TotalMilliseconds}ms");


                    dtx = DateTime.Now;
                    var obj = json.DeserializeFromCache<TestClass>();
                    Console.WriteLine($"json.DeserializeFromCache<TestClass> ��ʱ��{(DateTime.Now - dtx).TotalMilliseconds}ms");
                    Assert.AreEqual(obj.ID, testClass.ID);
                    Assert.AreEqual(obj.Star, testClass.Star);
                    Assert.AreEqual(obj.AddTime, testClass.AddTime);

                    Console.WriteLine("");

                    finishCount++;
                });
                threadList.Add(thread);
            }

            threadList.ForEach(z => z.Start());

            while (finishCount < threadCount)
            {
                //�ȴ�
            }

        }

    }
}
