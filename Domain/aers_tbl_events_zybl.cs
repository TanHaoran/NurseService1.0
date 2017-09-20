/**
  * Name:Aersysm.Models-aers_tbl_events_zybl
  * Author: banshine
  * Description: aers_tbl_events_zybl 实体层 
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
    public partial class aers_tbl_events_zybl
    {
        [DataMember]
        public string LoginKey { get; set; }

        [DataMember]
        public Int32 Age { get; set; }

        #region EveZyblId

        [DataMember]
            public String EveZyblId { get;set; }
            
            #endregion
        
        
            #region EveresId
            
            [DataMember]
            public String EveresId { get;set; }
            
            #endregion
        
        
            #region HospId
            
            [DataMember]
            public String HospId { get;set; }
            
            #endregion
        
        
            #region HospDepId
            
            [DataMember]
            public String HospDepId { get;set; }
            
            #endregion
        
        
            #region HappenedDate
            
            [DataMember]
            public DateTime HappenedDate { get;set; }
            
            #endregion
        
        
            #region SendtoDate
            
            [DataMember]
            public DateTime SendtoDate { get;set; }
            
            #endregion
        
        
            #region FillStaff
            
            [DataMember]
            public String FillStaff { get;set; }
            
            #endregion
        
        
            #region HospNumber
            
            [DataMember]
            public String HospNumber { get;set; }
            
            #endregion
        
        
            #region PatientExpose
            
            [DataMember]
            public String PatientExpose { get;set; }
            
            #endregion
        
        
            #region ExposeTypeOne
            
            [DataMember]
            public String ExposeTypeOne { get;set; }
            
            #endregion
        
        
            #region ExposeTypeTwo
            
            [DataMember]
            public String ExposeTypeTwo { get;set; }
            
            #endregion
        
        
            #region HarmDegree
            
            [DataMember]
            public String HarmDegree { get;set; }
            
            #endregion
        
        
            #region FomesFrom
            
            [DataMember]
            public String FomesFrom { get;set; }
            
            #endregion
        
        
            #region PharmacyState
            
            [DataMember]
            public String PharmacyState { get;set; }
            
            #endregion
        
        
            #region UseDrug
            
            [DataMember]
            public String UseDrug { get;set; }
            
            #endregion
        
        
            #region UseDrugTime
            
            [DataMember]
            public String UseDrugTime { get; set; }
            
            #endregion
        
        
            #region StaffType
            
            [DataMember]
            public String StaffType { get;set; }
            
            #endregion
        
        
            #region StaffTypeQt
            
            [DataMember]
            public String StaffTypeQt { get;set; }
            
            #endregion
        
        
            #region StaffPosition
            
            [DataMember]
            public String StaffPosition { get;set; }
            
            #endregion
        
        
            #region StaffEducation
            
            [DataMember]
            public String StaffEducation { get;set; }
            
            #endregion
        
        
            #region StaffEduQt
            
            [DataMember]
            public String StaffEduQt { get;set; }
            
            #endregion
        
        
            #region StaffWorkyears
            
            [DataMember]
            public String StaffWorkyears { get;set; }
            
            #endregion
        
        
            #region AuditFeedback
            
            [DataMember]
            public String AuditFeedback { get;set; }
            
            #endregion
        
        
            #region AuditStatus
            
            [DataMember]
            public Int32 AuditStatus { get;set; }
            
            #endregion
        
        
            #region Remark
            
            [DataMember]
            public String Remark { get;set; }
            
            #endregion
        
        
            #region IsFlag
            
            [DataMember]
            public Int32 IsFlag { get;set; }
            
            #endregion
        
        
            #region OperatorDate
            
            [DataMember]
            public DateTime OperatorDate { get;set; }
            
            #endregion
        
        
            #region OperatorID
            
            [DataMember]
            public String OperatorID { get;set; }

        #endregion


        #region Other

        [DataMember]
        public String Other { get; set; }

        #endregion

        #region Other

        [DataMember]
        public String EventLevel { get; set; }

        #endregion

        #region Other

        [DataMember]
        public String TouchPatient { get; set; }

        #endregion


        [DataMember]
        public String EventDescription { get; set; }

        [DataMember]
        public String DegreeDamage { get; set; }


        [DataMember]
        public String PatientExposeQt { get; set; }

        [DataMember]
        public String ExposeTypeOneQt { get; set; }


        [DataMember]
        public String UseDrugQt { get; set; }


        [DataMember]
        public String ExamineState { get; set; }


        [DataMember]
        public String FileName { get; set; }

        [DataMember]
        public String Address { get; set; }

        
    }
    
}

