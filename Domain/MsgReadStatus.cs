using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Aersysm.Domain
{
    [DataContract]
    public partial class MsgReadStatus
    {
        [DataMember]
        public string  ID { get; set; }

        public string  UserID { get; set; }

        public string MessageID { get; set; }

        public int  ReadStatus { get; set; }
    }
}
