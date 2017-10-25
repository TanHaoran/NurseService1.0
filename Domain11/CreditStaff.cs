using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Domain {

    [Serializable]
    [DataContract]
    public partial class CreditStaff {

        public CreditStaff() {
        }

        [DataMember]
        public string staffId { get; set; }

        [DataMember]
        public string loginName { get; set; }

        [DataMember]
        public string password { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string inpatientAreaId { get; set; }

        [DataMember]
        public string inpatientAreaName { get; set; }

        [DataMember]
        public string hospitalId { get; set; }

        [DataMember]
        public string hospitalName { get; set; }
    }
}
