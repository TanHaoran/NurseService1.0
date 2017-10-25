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
    public class ViewUserInfo
    {
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string RegisterId { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public string NickName { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Sex { get; set; }

        [DataMember]
        public DateTime  Birthday { get; set; }

        [DataMember]
        public string  PCertificateId { get; set; }

        [DataMember]
        public int PVerifyStatus { get; set; }
        

        [DataMember]
        public DateTime  FirstJobTime { get; set; }

        [DataMember]
        public string QCertificateId { get; set; }

        [DataMember]
        public int QVerifyStatus { get; set; }

        [DataMember]
        public string HospitalName { get; set; }

        [DataMember]
        public string HospitalId { get; set; }

        [DataMember]
        public string DepartmentName { get; set; }

        [DataMember]
        public string DepartmentId { get; set; }

        [DataMember]
        public string EmployeeId { get; set; }

        //9.7所在科室人数
        [DataMember]
        public int DepartmentUserCount { get; set; }        
    }
}
