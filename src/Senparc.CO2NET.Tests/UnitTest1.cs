using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Senparc.CO2NET.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            #region �����첽����
            Task.Run(async () =>
            {

                Console.WriteLine("=== �첽��� ===");
            }).GetAwaiter().GetResult();
            #endregion
        }
    }
}
