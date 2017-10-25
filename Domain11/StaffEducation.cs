using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Domain {
    public partial class StaffEducation {

        public StaffEducation() {

        }

        [DataMember]
        public string UserID { get; set; }

        [DataMember]
        public string Journal { get; set; }

        [DataMember]
        public int TypeID { get; set; }

        [DataMember]
        public float Education { get; set; }

        [DataMember]
        public float Grade { get; set; }

        [DataMember]
        public float Fraction { get; set; }
    }
}
