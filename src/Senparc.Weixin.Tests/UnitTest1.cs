using Microsoft.VisualStudio.TestTools.UnitTesting;
using Senparc.CO2NET.Extensions;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.Card;
using Senparc.Weixin.MP.Containers;
using System;

namespace Senparc.Weixin.Tests
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// �������� https://github.com/JeffreySu/WeiXinMPSDK/issues/1305
        /// </summary>
        [TestMethod]
        public void Test()
        {
            try
            {
                var appId = "wxe273c3a02e09ff8c";
                var appSecret = "631f30445f640e1a870f1ef79aa543bd";
                var accessToken = AccessTokenContainer.TryGetAccessToken(appId, appSecret);

                Card_GrouponData data1 = new Card_GrouponData()
                {
                    base_info = _BaseInfo,
                    deal_detail = "����"
                };

                //���λ�ñ���**
                var result1 = CardApi.CreateCard(accessToken, data1);

                var data = new Card_MemberCardData()
                {
                    base_info = _BaseInfo,
                    supply_bonus = true,
                    supply_balance = false,
                    prerogative = "123123",
                    bind_old_card_url = "www.daidu.com",
                    wx_activate = true
                };

                var result = CardApi.CreateCard(accessToken, data);
                System.Console.WriteLine(result.ToJson());

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        protected Card_BaseInfoBase _BaseInfo = new Card_BaseInfoBase()
        {
            logo_url = "http:\\www.supadmin.cn/uploads/allimg/120216/1_120216214725_1.jpg",
            brand_name = "������",
            code_type = Card_CodeType.CODE_TYPE_TEXT,
            title = "132 Ԫ˫�˻���ײ�",
            sub_title = "��ĩ�񻶱ر�",
            color = "Color010",
            notice = "ʹ��ʱ�����Ա��ʾ��ȯ",
            service_phone = "020-88888888",
            description = @"�����������Ż�ͬ��\n �����Ź�ȯ��Ʊ����������ʱ���̻����\n ���ھ���
ʹ�ã�������ʳ\n ��ǰ���ɴ�����ͺ�δ���꣬�ɴ��\n ���Ź�ȯ��������������2 ��ʹ�ã�����������
�������ս��Ϸ�5 Ԫ/λ\n ����л���Դ���ˮ����",
            date_info = new Card_BaseInfo_DateInfo()
            {
                type = Card_DateInfo_Type.DATE_TYPE_PERMANENT.ToString(),
            },
            sku = new Card_BaseInfo_Sku()
            {
                quantity = 5
            },
            use_limit = 1,
            get_limit = 3,
            use_custom_code = false,
            bind_openid = false,
            can_share = true,
            can_give_friend = true,
            url_name_type = Card_UrlNameType.URL_NAME_TYPE_RESERVATION,
            custom_url = "http://www.weiweihi.com",
            source = "���ڵ���",
            custom_url_name = "����ʹ��",
            custom_url_sub_title = "6������tips",
            promotion_url_name = "�����Ż�",
            promotion_url = "http://www.qq.com",
        };
    }
}
