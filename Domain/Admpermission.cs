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
    public partial  class Admpermission
    {
        [DataMember]
        public string   Id { get; set; }

        [DataMember]
        public string  AdmId { get; set; }

        [DataMember]
        public string  PermissionId { get; set; }

        [DataMember]
        public string PermissionName { get; set; }
    }
}
