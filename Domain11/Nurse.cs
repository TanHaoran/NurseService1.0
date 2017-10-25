using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Domain {


    [Serializable]
    [DataContract]
    public partial class Nurse {

        [DataMember]
        public string adminId { get; set; }

        [DataMember]
        public string registerId { get; set; }

        [DataMember]
        public string cellphone { get; set; }

        [DataMember]
        public string password { get; set; }

        [DataMember]
        public string name { get; set; }


        [DataMember]
        public string sex { get; set; }


        [DataMember]
        public string departmentId { get; set; }


        [DataMember]
        public string departmentName { get; set; }


        [DataMember]
        public string hospitalId { get; set; }


        [DataMember]
        public string hospitalName { get; set; }


        [DataMember]
        public string positions { get; set; }

        [DataMember]
        public ViewUserBindInfo bindInfo { get; set; }










    }
}
