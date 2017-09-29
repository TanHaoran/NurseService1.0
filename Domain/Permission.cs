using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Domain 
{
    public partial class Permission
    {
        [DataMember]
        public String permissionId { get; set; }

        [DataMember]
        public String name { get; set; }

        [DataMember]
        public int flag { get; set; }

    }
}
