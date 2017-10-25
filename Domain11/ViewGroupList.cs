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
   public   class ViewGroupList
    {
        [DataMember]
        public string  HXGroupId { get; set; }

        [DataMember]
        public string RegisterId { get; set; }

        [DataMember]
        public string GroupId { get; set; }

        [DataMember]
        public int GroupUserCount { get; set; }

        [DataMember]
        public string HXNickName { get; set; }

        [DataMember]
        public DateTime CreateTime { get; set; }

        [DataMember]
        public List<ViewFriendInfo> groupMemberList { get; set; }
    }
}
