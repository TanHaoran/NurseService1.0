/**
  * Name:Train.Models-aers_tbl_registereduser
  * Author: banshine
  * Description: aers_tbl_registereduser 实体层 
  * Date: 2015-6-8 10:57:02
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Aersysm.Domain
{
    [DataContract]
    public partial class aers_tbl_registereduser
    {
        
            #region ReguserId
            
            [DataMember]
            public String ReguserId { get;set; }
            
            #endregion
        
        
            #region LoginName
            
            [DataMember]
            public String LoginName { get;set; }
            
            #endregion
        
        
            #region Password
            
            [DataMember]
            public String Password { get;set; }
            
            #endregion
        
        
            #region IsFlag
            
            [DataMember]
            public Int32 IsFlag { get;set; }
            
            #endregion
        
        
            #region Remarks
            
            [DataMember]
            public String Remarks { get;set; }
            
            #endregion
        
        
            #region OperatorId
            
            [DataMember]
            public String OperatorId { get;set; }
            
            #endregion
        
        
            #region OperatorDate
            
            [DataMember]
            public DateTime OperatorDate { get;set; }

        #endregion

    
        

        [DataMember]
        public String StaffId { get; set; }
        [DataMember]
        public String DepId { get; set; }
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public String RoleState { get; set; }
        [DataMember]
        public String Sex { get; set; }
        [DataMember]
        public String Position { get; set; }
        [DataMember]
        public String Phone { get; set; }
        [DataMember]
        public String Address { get; set; }
        [DataMember]
        public String IDNumber { get; set; }
        [DataMember]
        public String StaffRemarks { get; set; }

        [DataMember]
        public String HospId { get; set; }
        [DataMember]
        public String HospName { get; set; }
        [DataMember]
        public String hospitalAddress { get; set; }
        [DataMember]
        public String hospitalPhone { get; set; }
        [DataMember]
        public String Contact { get; set; }
        [DataMember]
        public String Grade { get; set; }
        [DataMember]
        public DateTime Validitytime { get; set; }
        [DataMember]

        public String HospdepName { get; set; }

    }

}

