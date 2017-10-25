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
    public class ViewUserBindInfo
    {
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string RegisterId { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string WeixinNickName { get; set; }

        [DataMember]
        public string WeixinOpenId { get; set; }


        [DataMember]
        public string QQNickName { get; set; }

        [DataMember]
        public string QQOpenId  { get; set; }

        [DataMember]
        public int BindCount { get; set; }


        [DataMember]
        public string  WeiboOpenId { get; set; }

        [DataMember]
        public string WeiboNickName { get; set; }


        [DataMember]
        public string BLSJOpenId { get; set; }

        [DataMember]
        public string BLSJId { get; set; }


        [DataMember]
        public string XFOpenId { get; set; }

        [DataMember]
        public string XFId { get; set; }


        [DataMember]
        public string PBOpenId { get; set; }

        [DataMember]
        public string PBId { get; set; }
    }
}
