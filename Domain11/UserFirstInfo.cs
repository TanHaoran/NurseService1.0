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
    public  class UserFirstInfo
    {
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string RegisterId { get; set; }

        [DataMember]
        public string  HospitalId { get; set; }

        [DataMember]
        public string HospitalName { get; set; }

        [DataMember]
        public string  DepartmentId { get; set; }

        [DataMember]
        public string  DepartmentName { get; set; }

        [DataMember]
        public int  PStatus { get; set; }

        [DataMember]
        public int  QStatus { get; set; }

        [DataMember]
        public string Avatar  { get; set; }

        [DataMember]
        public string  NickName { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ReguserId { get; set; }  //不良事件

        [DataMember]
        public string XFId { get; set; }  //学分

        [DataMember]
        public string PBId { get; set; }  //排班

        //9.7所在科室人数
        [DataMember]
        public int DepartmentUserCount { get; set; }
    }
}
