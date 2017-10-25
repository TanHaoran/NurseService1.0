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
    public  class ViewGroupCreate
    {
        //[DataMember]
        //public string OwnerId { get; set; }

        //[DataMember]
        //public string GroupNickName { get; set; }

        //[DataMember]
        //public int MaxCount { get; set; }

        //[DataMember]
        //public String Desc { get; set; }

        [DataMember]
        public ViewRegisterId ListMemberId { get; set; }
    }
}
