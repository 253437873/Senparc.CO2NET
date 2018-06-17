using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Exceptions;
using Senparc.CO2NET.Tests.TestEntities;
using System;

namespace Senparc.CO2NET.Tests.Exceptions
{
    [TestClass]
    public class BaseExceptionTests
    {
        [TestMethod]
        public void BaseExceptionTest()
        {
            try
            {
                throw new TestException("�쳣����", new Exception("�ڲ��쳣"));
            }
            catch (TestException ex)
            {
                Assert.AreEqual("�쳣����", ex.Message);
                Assert.AreEqual("�ڲ��쳣", ex.InnerException.Message);

                //TODO�������ռ�¼
            }
            catch (BaseException ex)
            {
                Assert.Fail();
            }

        }
    }
}
