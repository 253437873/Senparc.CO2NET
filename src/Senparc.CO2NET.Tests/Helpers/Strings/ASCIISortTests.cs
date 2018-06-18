using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Helpers;

namespace Senparc.CO2NET.Tests.Helpers
{
    [TestClass]
    public class ASCIISortTests
    {
        [TestMethod]
        public void ASCIISortTest()
        {
            string[] scoure = new[] { "1", "2", "A", "a", "B", "b" };
            scoure.ToList().OrderBy(x => x); //��� 1 2 a A b B
            Console.WriteLine("ʹ��OrderBy����");
            foreach (var item in scoure)
            {
                Console.WriteLine(item);
            }
            Assert.AreEqual("1", scoure[0]);
            Assert.AreEqual("2", scoure[1]);
            Assert.AreEqual("A", scoure[2]);
            Assert.AreEqual("a", scoure[3]);//ע�����ﲻͬ
            Assert.AreEqual("B", scoure[4]);//ע�����ﲻͬ
            Assert.AreEqual("b", scoure[5]);

            ArrayList arrSource = new ArrayList(new[] { "1", "2", "A", "a", "B", "b" });
            arrSource.Sort(ASCIISort.Create());
            Console.WriteLine("ʹ��ASCIISort����");
            foreach (var item in arrSource)
            {
                Console.WriteLine(item);
            }
            Assert.AreEqual("1", arrSource[0]);
            Assert.AreEqual("2", arrSource[1]);
            Assert.AreEqual("A", arrSource[2]);
            Assert.AreEqual("B", arrSource[3]);//ע�����ﲻͬ
            Assert.AreEqual("a", arrSource[4]);//ע�����ﲻͬ
            Assert.AreEqual("b", arrSource[5]);
        }

    }
}
