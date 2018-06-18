using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;

namespace Senparc.CO2NET.Tests.Helpers
{
    [TestClass]
    public partial class SerializerHelperTests
    {
        [TestMethod]
        public void EncodeUnicodeTest()
        {
            var input = "ʢ������";
            var result = SerializerHelper.EncodeUnicode(input);
            Console.WriteLine(result);
            Assert.IsNotNull(result);
            Assert.IsTrue(result != null && result.Contains("\\u"));
        }

        [TestMethod]
        public void DecodeUnicodeTest()
        {
            var input = "\\u76DB\\u6D3E\\u7F51\\u7EDC";
            var result = SerializerHelper.DecodeUnicode(input);
            Console.WriteLine(result);
            Assert.AreEqual("ʢ������", result);
            
            //TODO:����д������Ҫ����
        }
    }
}
