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
     public  class ViewHospitalUser
    {
        [DataMember]
        public string LoginName { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
