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
    public  class ViewJsonCommon
    {
        [DataMember]
        public String Name { get; set; }
    }
}
