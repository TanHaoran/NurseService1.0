using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Domain
{
    [Serializable]
    [DataContract]
    public  class ViewBind
    {
        [DataMember]
        public string RegisterId { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public Qq   QQData { get; set; }


        [DataMember]
        public Weixin  WXData { get; set; }

        [DataMember]
        public Weibo  WBData { get; set; }

        [DataMember]
        public string LoginName { get; set; }  //院内账号登陆名

        [DataMember]
        public string Password { get; set; }  //院内账号密码

        [DataMember]
        public int LoginType { get; set; }  //绑定解绑类型 0手机号、1qq、2微信、3微博、4不良事件、5学分、6排班

        [DataMember]
        public string  HospitalId { get; set; }  //院内账号绑定时学分、考试，需要传入医院Id
    }
}
