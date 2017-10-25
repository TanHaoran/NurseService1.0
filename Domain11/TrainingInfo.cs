using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Domain {
    public partial class TrainingInfo {

        public TrainingInfo() {

        }

        [DataMember]
        public string TrainingID { get; set; }

        [DataMember]
        public string TrainingName { get; set; }

        [DataMember]
        public DateTime StartTime { get; set; }

        [DataMember]
        public string Level { get; set; }

        [DataMember]
        public string LinkPhone { get; set; }

        [DataMember]
        public string CourseID { get; set; }

        [DataMember]
        public DateTime ModifyDate { get; set; }

        [DataMember]
        public float Credit { get; set; }

        [DataMember]
        public int TrainingNumber { get; set; }

        [DataMember]
        public string TrainType { get; set; }
    }
}
