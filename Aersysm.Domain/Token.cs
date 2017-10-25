using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Domain
{
    [DataContract]
    public partial  class Token
    {
        [DataMember]
        public string RegisterId { get ; set; }

        [DataMember]
        public string  TokenCode { get; set; }
    }
}
