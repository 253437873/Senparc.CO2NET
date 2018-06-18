using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.MessageQueue;

namespace Senparc.CO2NET.Tests.MessageQueue
{
    [TestClass]
    public class SenparcMessageQueueTests
    {
        [TestMethod]
        public void SenparcMessageQueueTest()
        {


            var smq = new SenparcMessageQueue();
            var keyPrefix = "TestMQ_";
            var count = smq.GetCount();

            for (int i = 0; i < 3; i++)
            {
                var key = keyPrefix + i;
                //����Add
                smq.Add(key, () =>
                  {
                      Console.WriteLine("ִ�ж��У�" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff"));
                  });


                Console.WriteLine("��Ӷ����" + key);
                Console.WriteLine("��ǰ������" + smq.GetCount());
                Console.WriteLine("CurrentKey��" + smq.GetCurrentKey());
                Assert.AreEqual(count + 1, smq.GetCount());
                count = smq.GetCount();

                //����GetItem
                var item = smq.GetItem(key);
                Console.WriteLine("item.AddTime��" + item.AddTime);
                Assert.AreEqual(key, item.Key);

            }

            //����Remove
            smq.Add("ToRemove", () =>
            {
                Console.WriteLine("���������һ����˵��û������ɹ�");
            });
            smq.Remove("ToRemove");

            //�����߳�
            Threads.ThreadUtility.Register();

            while (smq.GetCount() > 0)
            {
                //�ȴ����д�����
            }

            Console.WriteLine("���д�����ϣ���ǰ����������" + smq.GetCount());
        }
    }
}
