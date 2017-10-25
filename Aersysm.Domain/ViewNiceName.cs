using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Domain
{
   
    [DataContract]
    public partial class ViewNiceName
    {

        [DataMember]
        public String RegisterId { get; set; }

        [DataMember]
        public String NickName { get; set; }
    }
}
