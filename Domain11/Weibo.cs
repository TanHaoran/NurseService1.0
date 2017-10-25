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
    public partial  class Weibo
    {
        [DataMember]
        public string Id { get; set; }
        //openid
        [DataMember]
        public string idstr { get; set; }

        //昵称
        [DataMember]
        public string name { get; set; }

        //省市
        [DataMember]
        public string location { get; set; }

        //个人简介
        [DataMember]
        public string description { get; set; }

        //头像
        [DataMember]
        public string profile_image_url { get; set; }

        //性别
        [DataMember]
        public string gender { get; set; }

        [DataMember]
        public string DeviceRegId { get; set; }

        [DataMember]
        public string DeviceId { get; set; }

    }
}
