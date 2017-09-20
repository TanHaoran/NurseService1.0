using Aersysm;
using Aersysm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Services
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IEMService”。
    [ServiceContract]
    public interface IEMService
    {
       // [OperationContract]
       // [WebGet(UriTemplate = "GetFriendsList?RegisterId={RegisterId}&KeyWord={KeyWord}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
       // RsList<userregister> GetFriendsList(string RegisterId, string KeyWord);


       // [OperationContract]
       // [WebInvoke(Method = "POST", UriTemplate = "login", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
       //string  UserLogin1();

        //获取全部国家编码
        [OperationContract]
        [WebGet(UriTemplate = "GetCountryCodeAll", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<country> GetCountryCodeAll();
    }
}
