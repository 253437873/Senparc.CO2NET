using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.MessageQueue;
using Senparc.CO2NET.Threads;

namespace Senparc.CO2NET.Tests.Threads
{
    [TestClass]
    public class ThreadUtilityTests
    {
        [TestMethod]
        public void ThreadUtilityTest()
        {
            ThreadUtility.Register();
            ThreadUtility.Register();//���ע��Ȼֻ��¼һ�Σ������һ�Σ�
            ThreadUtility.Register();

            Assert.AreEqual(1, ThreadUtility.AsynThreadCollection.Count);

            var smq = new SenparcMessageQueue();
            var key = "ThreadUtilityTests";
            smq.Add(key, () =>
            {
                Console.WriteLine("����ִ��SenparcMessageQueue");
            });

            //������Ҫ���� SenparcMessageQueueThreadUtility.Run()�������Ѿ����Զ�����

            while (smq.GetCount() > 0)
            {
                //ִ�ж���
            }

            Console.WriteLine($"SenparcMessageQueue���д�����ϣ���ǰ��Ŀ��{smq.GetCount()}");
        }
    }
}
