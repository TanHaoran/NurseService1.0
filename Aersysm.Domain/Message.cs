using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Aersysm.Domain
{
    [DataContract]
    public partial  class Message
    {
        [DataMember]
        public String MessageID { get; set; }

        [DataMember]
        public int  MessageType { get; set; }

        [DataMember]
        public String ReceiverID { get; set; }

        [DataMember]
        public String MessageTitle { get; set; }

        [DataMember]
        public String MessageContent { get; set; }

        [DataMember]
        public DateTime  MessageDate { get; set; }

        [DataMember]
        public String MessageSenderID { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime  EndTime { get; set; }

        [DataMember]
        public int IsRead { get; set; }
        [DataMember]
        public string ddddd  { get; set; }
    }
}
