using Aersysm.Domain;
using Aersysm.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“EMService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 EMService.svc 或 EMService.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class EMService : IEMService
    {
        //public RsList<userregister> GetFriendsList(string RegisterId, string KeyWord)
        //{
        //    RsList<userregister> r = new Services.RsList<userregister>();
        //    if (string.IsNullOrWhiteSpace(KeyWord))
        //    {
        //        r.code = 1;
        //        r.msg = "搜索关键字不能为空";
        //        return r;
        //    }
        //    try
        //    {
        //        userregisterSqlMapDao udao = new userregisterSqlMapDao();
        //        var alllist = udao.GetuserregisterList();
        //        var datalist = alllist.Where(o => o.NickName.IndexOf(KeyWord) > 0||o.Name .IndexOf (KeyWord )>0||o.Phone.IndexOf(KeyWord)>0).ToList();
        //        r.body = datalist;
        //        r.code = 0;
        //        return r;
        //    }
        //    catch (Exception)
        //    {
        //        r.code = 1;
        //        r.msg = "查找好友信息失败";
        //        return r;
        //    }
        //}

        //public string UserLogin1()
        //{
        //    return "1";
        //}

        #region 获取全部国家编码 0
        /// <summary>
        /// 获取全部国家编码
        /// </summary>
        /// <returns></returns>
        public RsList<country> GetCountryCodeAll()
        {
            RsList<country> r = new Services.RsList<country>();
            try
            {
                countrySqlMapDao cdao = new countrySqlMapDao();
                var data = cdao.GetcountryList().ToList();
                r.code = 0;
                r.body = data;
            }
            catch (Exception e)
            {
                r.code = 1;
                r.msg = e.ToString();
            }
            return r;
        }
        #endregion
    }
}
