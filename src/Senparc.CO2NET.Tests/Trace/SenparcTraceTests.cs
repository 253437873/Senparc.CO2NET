using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Senparc.CO2NET.Exceptions;
using Senparc.CO2NET.RegisterServices;
using Senparc.CO2NET.Trace;

namespace Senparc.CO2NET.Tests.Trace
{
    [TestClass]
    public class SenparcTraceTests
    {
        private string _rootPath => Path.GetFullPath("..\\..\\..\\");

        private string _logFilePath => Path.Combine(_rootPath, "App_Data", "SenparcTraceLog", $"SenparcTrace-{DateTime.Now.ToString("yyyyMMdd")}.log");

        private bool CheckLog(params string[] keywords)
        {
            using (var fs = new FileStream(_logFilePath, FileMode.Open))
            {
                using (var sr = new StreamReader(fs))
                {
                    var content = sr.ReadToEnd();
                    foreach (var item in keywords)
                    {
                        if (!content.Contains(item))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }

        public SenparcTraceTests()
        {
            //ע��
            var mockEnv = new Mock<IHostingEnvironment>();
            mockEnv.Setup(z => z.ContentRootPath).Returns(() => Path.GetFullPath("..\\..\\..\\"));
            RegisterService.Start(mockEnv.Object, true);

            //ɾ����־�ļ�
            //File.Delete(_logFilePath);
        }


        [TestMethod]
        public void LogTest()
        {
            //ֱ�ӵ��ô˷��������¼��log�ļ��У����������ϵͳ��־��
            var keyword = Guid.NewGuid().ToString();//����ַ���
            SenparcTrace.Log($"���Log��{keyword}");
            //Assert.IsTrue(CheckLog(keyword));
        }


        [TestMethod]
        public void SendCustomLogTest()
        {
            var keyword = Guid.NewGuid().ToString();//����ַ���
            SenparcTrace.SendCustomLog("����", $"���Log��{keyword}");
            Assert.IsTrue(CheckLog("����", keyword));
        }


        [TestMethod]
        public void SendApiLogTest()
        {
            var url = "http://www.senparc.com";
            var result = Guid.NewGuid().ToString();//����ַ���
            SenparcTrace.SendApiLog(url,result);
            Assert.IsTrue(CheckLog(url,result));
        }


        [TestMethod]
        public void SendApiPostDataLogTest()
        {
            var url = "http://www.senparc.com";
            var data = Guid.NewGuid().ToString();//����ַ���
            SenparcTrace.SendApiLog(url, data);
            Assert.IsTrue(CheckLog(url, data));
        }



        [TestMethod]
        public void BaseExceptionLogTest()
        {
            var keyword = Guid.NewGuid().ToString();//����ַ���
            var ex = new BaseException("�����쳣��"+ keyword);
            //Log���¼���Σ���һ������BaseException��ʼ����ʱ�����ô˷���
            SenparcTrace.BaseExceptionLog(ex);
            Assert.IsTrue(CheckLog("�����쳣", keyword));
        }

        [TestMethod]
        public void OnLogFuncTest()
        {
            var onlogCount = 0;
            SenparcTrace.OnLogFunc = () => onlogCount++;

            var keyword = Guid.NewGuid().ToString();//����ַ���
            SenparcTrace.SendCustomLog("����OnLogFuncTest", keyword);
            Assert.IsTrue(CheckLog(keyword));
            Assert.AreEqual(1, onlogCount);
        }


        [TestMethod]
        public void OnBaseExceptionFuncTest()
        {
            var onlogCount = 0;
            SenparcTrace.OnLogFunc = () => onlogCount++;

            var keyword = Guid.NewGuid().ToString();//����ַ���
            var ex = new BaseException("�����쳣��" + keyword);
            //Log���¼���Σ���һ������BaseException��ʼ����ʱ�����ô˷���
            SenparcTrace.BaseExceptionLog(ex);
            Assert.IsTrue(CheckLog(keyword));
            Assert.AreEqual(2, onlogCount);
        }

    }
}
