using System;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Helpers;
using Senparc.CO2NET.Helpers.Serializers;

namespace Senparc.CO2NET.Tests.Helpers
{
    public partial class SerializerHelperTests
    {
        #region ΢��JSON�ĺ���null���ض�ֵֵ���ض����Բ���
        [TestMethod()]
        public void GetJsonStringTest_Null()
        {
            var obj =
                new
                {
                    X =
                        new RootClass()
                        {
                            A = "Jeffrey",
                            B = 31,
                            C = null,
                            ElementClassA = new ElementClass() { A = "Jeffrey", B = null },
                            ElementClassB = null
                        },
                    Y = new
                    {
                        O = "0",
                        Z = (string)null
                    }
                };


            DateTime dt1 = DateTime.Now;

            {
                //�������κ����ԣ�����Ѿ��������Ե����Գ��⣩
                var json = SerializerHelper.GetJsonString(obj, new JsonSetting(false));
                Console.WriteLine(json);
                var exceptedJson = "{\"X\":{\"A\":\"Jeffrey\",\"B\":31,\"ElementClassA\":{\"A\":\"Jeffrey\",\"B\":null,\"RootClass\":null},\"ElementClassB\":null,\"ElementClass2\":null},\"Y\":{\"O\":\"0\",\"Z\":null}}";
                Assert.AreEqual(exceptedJson, json);
            }

            {
                //��������Ϊnull������
                var json = SerializerHelper.GetJsonString(obj, new JsonSetting(true));
                Console.WriteLine(json);
                var exceptedJson = "{\"X\":{\"A\":\"Jeffrey\",\"B\":31,\"ElementClassA\":{\"A\":\"Jeffrey\"}},\"Y\":{\"O\":\"0\"}}";
                Assert.AreEqual(exceptedJson, json);


                var obj2 = new RootClass()
                {
                    A = "Jeffrey",
                    B = 31,
                    C = null,
                    ElementClassA = new ElementClass() { A = "Jeffrey", B = null },
                    ElementClassB = null
                };

                var json2 = SerializerHelper.GetJsonString(obj2,
                                new JsonSetting(true, new List<string>(new[] { "B" })));

                var exceptedJson2 = "{\"A\":\"Jeffrey\",\"B\":31,\"ElementClassA\":{\"A\":\"Jeffrey\"}}";
                Console.WriteLine(json2);
                Assert.AreEqual(exceptedJson2, json2);

            }

            {
                //�����ض�Ϊnull������
                var json = SerializerHelper.GetJsonString(obj,
                                new JsonSetting(false, new List<string>(new[] { "Z" })));//Z���Իᱻ����
                Console.WriteLine(json);
                var exceptedJson = "{\"X\":{\"A\":\"Jeffrey\",\"B\":31,\"ElementClassA\":{\"A\":\"Jeffrey\",\"B\":null,\"RootClass\":null},\"ElementClassB\":null,\"ElementClass2\":null},\"Y\":{\"O\":\"0\"}}";
                Assert.AreEqual(exceptedJson, json);
            }

            {
                //�����ض�ֵ���ԣ����ԣ�
                var obj4 = new RootClass()
                {
                    A = "IGNORE",//�ᱻ����
                    B = 31,
                    C = null,
                    ElementClassA = null,
                    ElementClassB = null,
                    ElementClass2 = null
                };
                var json = SerializerHelper.GetJsonString(obj4, new JsonSetting(true));//Z���Իᱻ����
                Console.WriteLine(json);
                var exceptedJson = "{\"B\":31}";
                Assert.AreEqual(exceptedJson, json);
            }

            {
                //�����ض�ֵ���ԣ������ԣ�
                var obj4 = new RootClass()
                {
                    A = "DO NET IGNORE",//���ᱻ����
                    B = 31,
                    C = null,
                    ElementClassA = null,
                    ElementClassB = null,
                    ElementClass2 = null
                };
                var json = SerializerHelper.GetJsonString(obj4, new JsonSetting(true));//Z���Իᱻ����
                Console.WriteLine(json);
                var exceptedJson = "{\"A\":\"DO NET IGNORE\",\"B\":31}";
                Assert.AreEqual(exceptedJson, json);
            }

            {
                //�����ض�����Ϊnull������
                var obj3 = new RootClass()
                {
                    A = "Jeffrey",
                    B = 31,
                    C = null,
                    ElementClassA = new ElementClass() { A = "Jeffrey", B = null },
                    ElementClassB = null,
                    ElementClass2 = null//���ᱻ����
                };

                var json3 = SerializerHelper.GetJsonString(obj3,
                             new JsonSetting(false, null, new List<Type>(new[] { typeof(ElementClass), typeof(ElementClass2) })));
                Console.WriteLine(json3);
                var exceptedJson3 = "{\"A\":\"Jeffrey\",\"B\":31,\"ElementClassA\":{\"A\":\"Jeffrey\",\"B\":null,\"RootClass\":null}}";
                Assert.AreEqual(exceptedJson3, json3);

            }



            Console.WriteLine((DateTime.Now - dt1).TotalMilliseconds);
        }

        public class RootClass /*: JsonIgnoreNull, IJsonIgnoreNull*/
        {
            private string _private { get; set; }
            [JsonSetting.IgnoreValue("IGNORE")]
            public string A { get; set; }
            public int B { get; set; }
            [JsonSetting.IgnoreNull]
            public int? C { get; set; }
            public ElementClass ElementClassA { get; set; }
            public ElementClass ElementClassB { get; set; }
            public ElementClass2 ElementClass2 { get; set; }

            public RootClass()
            {
                _private = "Private";
            }
        }

        public class ElementClass /*: JsonIgnoreNull, IJsonIgnoreNull*/
        {
            public string A { get; set; }
            public string B { get; set; }
            public RootClass RootClass { get; set; }
        }

        public class ElementClass2
        {
            public string A { get; set; }
            public string B { get; set; }
            public RootClass RootClass { get; set; }
        }
        #endregion


        #region ExpandoObject����ת������

        [TestMethod()]
        public void GetJsonStringTest_Expando()
        {
            dynamic test = new ExpandoObject();
            test.x = "Senparc.Weixin SDK";
            test.y = DateTime.Now;

            DateTime dt1 = DateTime.Now;

            var json = SerializerHelper.GetJsonString(test);
            Console.WriteLine(json);

            Console.WriteLine((DateTime.Now - dt1).TotalMilliseconds);

        }

        #endregion


        #region �������л��������л�

        [Serializable]
        public class Data
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [TestMethod()]
        public void GetJsonStringTest()
        {
            var data = new Data()
            {
                Id = 1,
                Name = "Senparc"
            };
            string json = SerializerHelper.GetJsonString(data);
            Assert.AreEqual("{\"Id\":1,\"Name\":\"Senparc\"}", json);
            Console.WriteLine(json);
        }


        #endregion

        [TestMethod()]
        public void GetObjectTest()
        {
            string json = "{\"Id\":1,\"Name\":\"Senparc\"}";
            Data data = SerializerHelper.GetObject<Data>(json);

            Assert.AreEqual(1, data.Id);
            Assert.AreEqual("Senparc", data.Name);
        }

        #region JsonSetting ����

        [Serializable]
        public class WeixinData
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string Note { get; set; }
            public string Sign { get; set; }
            public Sex Sex { get; set; }
        }


        [TestMethod]
        public void JsonSettingTest()
        {
            var weixinData = new WeixinData()
            {
                Id = 1,
                UserName = "JeffreySu",
                Note = null,
                Sign = null,
                Sex = Sex.��
            };

            //string json = js.GetJsonString(weixinData);
            //Console.WriteLine(json);

            //JsonSetting jsonSetting = new JsonSetting(true);
            //string json2 = js.GetJsonString(weixinData, jsonSetting);
            //Console.WriteLine(json2);

            JsonSetting jsonSetting3 = new JsonSetting(true, new List<string>() { "Note" });
            string json3 = SerializerHelper.GetJsonString(weixinData, jsonSetting3);
            Console.WriteLine(json3);
        }

        #endregion

    }
}
