using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.MessageQueue;
using Senparc.CO2NET.Threads;

namespace Senparc.CO2NET.Tests.Threads
{
    [TestClass]
    public class SenparcMessageQueueThreadUtilityTests
    {
        [TestMethod]
        public void SenparcMessageQueueThreadUtilityTest()
        {
            var smq = new SenparcMessageQueue();
            var key = "SenparcMessageQueueThreadUtilityTest";
            smq.Add(key, () =>
            {
                Console.WriteLine("ִ��SenparcMessageQueue");
            });

            Console.WriteLine($"SenparcMessageQueue.Count��{smq.GetCount()}");

            var senparcMessageQueue = new SenparcMessageQueueThreadUtility();

            Task.Factory.StartNew(() =>
            {
                senparcMessageQueue.Run();
            });//�첽ִ��

            //
            while (smq.GetCount() > 0)
            {
                //ִ�ж���
            }

            Console.WriteLine($"SenparcMessageQueue���д�����ϣ���ǰ��Ŀ��{smq.GetCount()}");

        }
    }
}
