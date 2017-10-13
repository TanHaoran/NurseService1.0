using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Domain {
    public partial class NoticeDepartment {
        public NoticeDepartment() {

        }

        [DataMember]
        public string id { get; set;}


        [DataMember]
        public string noticeId { get; set; }

        [DataMember]
        public string departmentId { get; set; }
        
    }
}
