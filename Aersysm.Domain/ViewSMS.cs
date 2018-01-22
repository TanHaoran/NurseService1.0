using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Domain
{
    [DataContract]
    public partial class ViewSMS
    {

        [DataMember]
        public String Phone { get; set; }


        [DataMember]
        public String Code { get; set; }

        [DataMember]
        public String RegisterId { get; set; }

        [DataMember]
        public int  Type { get; set; }

        [DataMember]
        public string DeviceRegId { get; set; }   //registration


        [DataMember]
        public int temp { get; set; } 


    }
}
