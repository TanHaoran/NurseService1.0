using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Aersysm.Domain
{
    [DataContract]
    public partial class view_Login
    {

        [DataMember]
        public String Name { get; set; }


        [DataMember]
        public String Pwd { get; set; }

        [DataMember]
        public String Ruid { get; set; }

    }
}
