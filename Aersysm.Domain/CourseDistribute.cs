using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Aersysm.Domain
{
    [DataContract]
  public partial  class CourseDistribute
    {
        [DataMember]
        public String DistributeID { get; set; }

        [DataMember]
        public String CourseID { get; set; }

        [DataMember]
        public int  DistributeType { get; set; }

        [DataMember]
        public String ReceiverID { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public DateTime EndTime { get; set; }

        [DataMember]
        public String DistributorID { get; set; }

        [DataMember]
        public DateTime  DistributeTime { get; set; }
    }
}
