/**
  * Name:Aersysm.Models-aers_tbl_events_yc
  * Author: banshine
  * Description: aers_tbl_events_yc 实体层 
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
    public partial class aers_tbl_events_yc
    {

        [DataMember]
        public string LoginKey { get; set; }


        [DataMember]
        public Int32 Age { get; set; }


        [DataMember]
        public String EveycId { get; set; }



        [DataMember]
        public String EveresId { get; set; }



        [DataMember]
        public String HospId { get; set; }



        [DataMember]
        public String HospDepId { get; set; }



        [DataMember]
        public DateTime HappenedDate { get; set; }



        [DataMember]
        public String HappenedType { get; set; }



        [DataMember]
        public DateTime SendtoDate { get; set; }


        [DataMember]
        public String FillStaff { get; set; }


        [DataMember]
        public String HospNumber { get; set; }



        [DataMember]
        public String PatientSex { get; set; }



        [DataMember]
        public Int32 PatientAge { get; set; }


        [DataMember]
        public String PatientAgeUnit { get; set; }


        [DataMember]
        public String NursLevel { get; set; }



        [DataMember]
        public String Diagnosis { get; set; }



        [DataMember]
        public String PatientHeight { get; set; }



        [DataMember]
        public String PatientWeight { get; set; }



        [DataMember]
        public String HemoglobinNum { get; set; }



        [DataMember]
        public String AlbuminNum { get; set; }



        [DataMember]
        public String IsEdema { get; set; }



        [DataMember]
        public String EvaluateStateQt { get; set; }


        [DataMember]
        public String EvaluateScore { get; set; }



        [DataMember]
        public DateTime EvaluateDate { get; set; }



        [DataMember]
        public String EvaluateState { get; set; }



        [DataMember]
        public String HighRiskLevel { get; set; }



        [DataMember]
        public String IsTakePrevent { get; set; }



        [DataMember]
        public String TakePreventQt { get; set; }



        [DataMember]
        public String OutcomeState { get; set; }



        [DataMember]
        public String OutcomeQt { get; set; }



        [DataMember]
        public String AuditFeedback { get; set; }



        [DataMember]
        public Int32 AuditStatus { get; set; }



        [DataMember]
        public String Remark { get; set; }



        public Int32 IsFlag { get; set; }


        [DataMember]
        public IList<aers_tbl_events_yc_parts> Parts { get; set; }

        [DataMember]
        public DateTime OperatorDate { get; set; }


        [DataMember]
        public String OperatorID { get; set; }


        [DataMember]
        public String TakePreventOther { get; set; }


        [DataMember]
        public String Occurrence { get; set; }


        [DataMember]
        public String Assessment { get; set; }




        [DataMember]
        public String AssessmentOther { get; set; }




        [DataMember]
        public String Highriskgrade { get; set; }


        [DataMember]
        public String ExamineState { get; set; }


        [DataMember]
        public String FileName { get; set; }

        [DataMember]
        public String Address { get; set; }
        
    }

}

