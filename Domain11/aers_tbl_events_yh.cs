/**
  * Name:Aersysm.Models-aers_tbl_events_yh
  * Author: banshine
  * Description: aers_tbl_events_yh 实体层 
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
    public partial class aers_tbl_events_yh
    {
        [DataMember]
        public string LoginKey { get; set; }

        [DataMember]
        public Int32 Age { get; set; }

        #region EveYhId

        [DataMember]
            public String EveYhId { get;set; }
            
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
        
        
            #region PatientSex
            
            [DataMember]
            public Int32 PatientSex { get;set; }
            
            #endregion
        
        
            #region PatientAge
            
            [DataMember]
            public Int32 PatientAge { get;set; }

        #endregion

             #region PatientAgeUnit
        [DataMember]
        public String PatientAgeUnit { get; set; }
        #endregion PatientAgeUnit
    

            #region NursLevel

        [DataMember]
            public String NursLevel { get;set; }
            
            #endregion
        
        
            #region Diagnosis
            
            [DataMember]
            public String Diagnosis { get;set; }
            
            #endregion
        
        
            #region EventDescription
            
            [DataMember]
            public String EventDescription { get;set; }
            
            #endregion
        
        
            #region EventLevel
            
            [DataMember]
            public String EventLevel { get;set; }
            
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
        
        
            #region BedUtilization
            
            [DataMember]
            public String BedUtilization { get;set; }
            
            #endregion
        
        
            #region BedNurseRatio
            
            [DataMember]
            public String BedNurseRatio { get;set; }
            
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


        [DataMember]
        public String ExamineState { get; set; }
        [DataMember]
        public String FileName { get; set; }
    }
    
}

