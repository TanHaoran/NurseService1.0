using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Aersysm.Domain
{
    [DataContract]
    public partial class CourseUser
    {
        [DataMember]
        public String ID { get; set; }

        [DataMember]
        public String CourseID { get; set; }

        [DataMember]
        public String UserID { get; set; }
        

    }
}
