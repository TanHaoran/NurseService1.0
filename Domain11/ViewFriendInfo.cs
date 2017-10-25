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
    public class ViewFriendInfo
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string NickName { get; set; }

        [DataMember]
        public string Sex { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string DepartmentName { get; set; }

        [DataMember]
        public string   Role { get; set; }  //职务

        [DataMember]
        public string Avatar { get; set; }  //职务

        [DataMember]
        public string Remark { get; set; }  //我对好友的备注

        [DataMember]
        public bool  IsFriend { get; set; }  //是否是好友   1是，0不是

        [DataMember]
        public string  HospitalName { get; set; }

        [DataMember]
        public bool IsInternalHospital { get; set; }
        //[DataMember]
        //public string RegisterId { get; set; }

        //读好友加了两个
        [DataMember]
        public string MyId { get; set; }

        [DataMember]
        public string FriendId { get; set; }
    }
}
