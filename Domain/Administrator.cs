using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Domain
{
    [Serializable]
    [DataContract]
    public partial  class Administrator
    {
        [DataMember]
        public string AdmId { get; set; }

        [DataMember]
        public string  UserId { get; set; }

        [DataMember]
        public string  Password { get; set; }

        [DataMember]
        public string  Name { get; set; }

        [DataMember]
        public DateTime OperatorTime { get; set; }

        [DataMember]
        public string  OperatorId { get; set; }

        [DataMember]
        public string  HospitalId { get; set; }

        [DataMember]
        public int Role { get; set; }

        [DataMember]
        public string  RegisterId { get; set; }

        [DataMember]
        public List<Admdepartment> Admdepartmentlist { get; set; }

        [DataMember]
        public List<Admpermission> Admpermissionlist { get; set; }


        [DataMember]
        public string  HospitalName { get; set; }

        [DataMember] 
        public int Status { get; set; }
    }
}
