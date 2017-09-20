using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Aersysm.Domain
{
    [DataContract]
    public partial  class SMSMessage
    {
        [DataMember]
        public String SMSID { get; set; }

        [DataMember]
        public String ReguserID { get; set; }

        [DataMember]
        public String PhoneNumber { get; set; }

        [DataMember]
        public String VerificationCode { get; set; }

        [DataMember]
        public DateTime  SMSSendTime { get; set; }

        [DataMember]
        public int  SendStatus { get; set; }
    }
}
