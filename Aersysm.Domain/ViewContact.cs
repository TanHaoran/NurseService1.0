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
    public  class ViewContact
    {
        [DataMember]
        public string RegisterId { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public string NickName { get; set; }

        [DataMember]
        public int status  { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string   Name { get; set; }
    }
}
