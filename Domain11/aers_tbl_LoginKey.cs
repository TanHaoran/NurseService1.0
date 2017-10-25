using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Aersysm.Domain
{
    [DataContract]
   public partial   class aers_tbl_LoginKey
    {
        [DataMember]
        public string LKId { get; set; }

        [DataMember]
        public string  ReguserId { get; set; }

        [DataMember]
        public string  LoginKey { get; set; }

        [DataMember]
        public DateTime  LoginTime { get; set; }

        [DataMember]
        public string  LoginIP { get; set; }

        [DataMember]
        public int  LoginStatus { get; set; }
    }
}
