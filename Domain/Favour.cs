using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Aersysm.Domain
{
    [DataContract]
     public partial  class Favour
    {
        [DataMember]
        public String ID { get; set; }

        [DataMember]
        public int  TypeID { get; set; }

        [DataMember]
        public String UserID { get; set; }

        [DataMember]
        public String MainID { get; set; }

        [DataMember]
        public DateTime OperatorDate { get; set; }

        [DataMember]
        public String res { get; set; }
    }
}
