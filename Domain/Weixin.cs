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
    public partial   class Weixin
    {
        [DataMember]
        public string  Id { get; set; }

        [DataMember]
        public string  OpenId { get; set; }

        [DataMember]
        public string  NickName { get; set; }

        [DataMember]
        public int   Sex { get; set; }

        [DataMember]
        public string  Language { get; set; }

        [DataMember]
        public string  City { get; set; }

        [DataMember]
        public string  Province { get; set; }

        [DataMember]
        public string  Country { get; set; }

        [DataMember]
        public string  HeadImgurl { get; set; }

        [DataMember]
        public string DeviceRegId { get; set; }

        [DataMember]
        public string RegisterId { get; set; }

        [DataMember]
        public string DeviceId { get; set; }
    }
}
