using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Domain {
    [Serializable]
    [DataContract]
    public partial class CreditScore {

        public CreditScore() {
        }

        [DataMember]
        public string RecordID { get; set; }

        [DataMember]
        public string UserID { get; set; }

        [DataMember]
        public string TrainingID { get; set; }

        [DataMember]
        public float Grade { get; set; }

        [DataMember]
        public float RealGrade { get; set; }
    }
}
