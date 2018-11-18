using System;
using System.IO;
using System.Threading;
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
        public static string LogFilePath => Path.Combine(UnitTestHelper.RootPath, "App_Data", "SenparcTraceLog", $"SenparcTrace-{SystemTime.Now.ToString("yyyyMMdd")}.log");


        public SenparcTraceTests()
        {
            //ע��
            var mockEnv = new Mock<IHostingEnvironment>();
            mockEnv.Setup(z => z.ContentRootPath).Returns(() => UnitTestHelper.RootPath);
            RegisterService.Start(mockEnv.Object, new SenparcSetting() { IsDebug = true });

            //ɾ����־�ļ�
            //File.Delete(_logFilePath);
        }


        [TestMethod]
        public void LogTest()
        {
            //ֱ�ӵ��ô˷��������¼��log�ļ��У����������ϵͳ��־��
            var keyword = Guid.NewGuid().ToString();//����ַ���
            SenparcTrace.Log($"���Log��{keyword}");
            Thread.Sleep(600); //�ȴ�����ִ��
            //Assert.IsTrue(UnitTestHelper.CheckKeywordsExist(_logFilePath,keyword));
        }


        [TestMethod]
        public void SendCustomLogTest()
        {
            var keyword = Guid.NewGuid().ToString();//����ַ���
            SenparcTrace.SendCustomLog("����", $"���Log��{keyword}");
            Thread.Sleep(600); //�ȴ�����ִ��
            Assert.IsTrue(UnitTestHelper.CheckKeywordsExist(LogFilePath, "����", keyword));
        }


        [TestMethod]
        public void SendApiLogTest()
        {
            var url = "http://www.senparc.com";
            var result = Guid.NewGuid().ToString();//����ַ���
            SenparcTrace.SendApiLog(url, result);
            Thread.Sleep(1000); //�ȴ�����ִ��
            Assert.IsTrue(UnitTestHelper.CheckKeywordsExist(LogFilePath, url, result));
        }


        [TestMethod]
        public void SendApiPostDataLogTest()
        {
            var url = "http://www.senparc.com";
            var data = Guid.NewGuid().ToString();//����ַ���
            SenparcTrace.SendApiLog(url, data);
            Thread.Sleep(600); //�ȴ�����ִ��
            Assert.IsTrue(UnitTestHelper.CheckKeywordsExist(LogFilePath, url, data));
        }



        [TestMethod]
        public void BaseExceptionLogTest()
        {
            var keyword = Guid.NewGuid().ToString();//����ַ���
            var ex = new BaseException("�����쳣��" + keyword);
            //Log���¼���Σ���һ������BaseException��ʼ����ʱ�����ô˷���
            SenparcTrace.BaseExceptionLog(ex);
            Thread.Sleep(600); //�ȴ�����ִ��
            Assert.IsTrue(UnitTestHelper.CheckKeywordsExist(LogFilePath, "�����쳣", keyword));
        }

        [TestMethod]
        public void OnLogFuncTest()
        {
            var onlogCount = 0;
            SenparcTrace.OnLogFunc = () => onlogCount++;

            var keyword = Guid.NewGuid().ToString();//����ַ���
            SenparcTrace.SendCustomLog("����OnLogFuncTest", keyword);
            Thread.Sleep(600); //�ȴ�����ִ��
            Assert.IsTrue(UnitTestHelper.CheckKeywordsExist(LogFilePath, keyword));
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
            Thread.Sleep(600); //�ȴ�����ִ��
            Assert.IsTrue(UnitTestHelper.CheckKeywordsExist(LogFilePath, keyword));
            Assert.AreEqual(2, onlogCount);
        }

    }
}
