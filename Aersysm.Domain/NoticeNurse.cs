using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Domain {
    public partial class NoticeNurse {

        public NoticeNurse() {

        }

        [DataMember]
        public string id { get; set; }

        [DataMember]
        public string noticeId { get; set; }

        [DataMember]
        public string nurseId { get; set; }
    }
}
