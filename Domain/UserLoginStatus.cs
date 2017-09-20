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
    public partial class UserLoginStatus
    {
        [DataMember]
        public string RegisterId { get; set; }

        [DataMember]
        public int  LoginType { get; set; }

        [DataMember]
        public DateTime  FirstLoginTime { get; set; }

        [DataMember]
        public DateTime  LastLoginTime { get; set; }
    }
}
