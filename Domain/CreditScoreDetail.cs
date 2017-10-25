using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain {
    public partial class CreditScoreDetail {

        public CreditScoreDetail() {

        }

        [DataMember]
        public string Type { get; set; }


        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public float score { get; set; }

        [DataMember]
        public DateTime Time { get; set; }
    }
}
