/**
  * Name:Aersysm.Models-aers_tbl_events_ddzc
  * Author: banshine
  * Description: aers_tbl_events_ddzc 实体层 
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
    public partial class aers_tbl_events_ddzc
    {

        [DataMember]
        public string LoginKey { get; set; }

        [DataMember]
        public Int32 Age { get; set; }


        [DataMember]
        public String EveddzcId { get; set; }



        [DataMember]
        public String EveresId { get; set; }



        [DataMember]
        public String DdzcType { get; set; }



        [DataMember]
        public String HospId { get; set; }



        [DataMember]
        public String HospDepId { get; set; }



        [DataMember]
        public DateTime HappenedDate { get; set; }



        [DataMember]
        public DateTime SendtoDate { get; set; }



        [DataMember]
        public String FillStaff { get; set; }



        [DataMember]
        public String HospNumber { get; set; }



        [DataMember]
        public String PatientSex { get; set; }



        [DataMember]
        public String PatientAge { get; set; }


        [DataMember]
        public String PatientAgeUnit { get; set; }


        [DataMember]
        public String NursLevel { get; set; }



        [DataMember]
        public String Diagnosis { get; set; }



        [DataMember]
        public String IsEvaluate { get; set; }



        [DataMember]
        public String EvaluateScore { get; set; }



        [DataMember]
        public String EvaluateDate { get; set; }



        [DataMember]
        public String IsTakePrevent { get; set; }



        [DataMember]
        public String DzPlace { get; set; }



        [DataMember]
        public String DzState { get; set; }



        [DataMember]
        public String MentalState { get; set; }



        [DataMember]
        public String SelfcareState { get; set; }



        [DataMember]
        public String DzHazards { get; set; }



        [DataMember]
        public String DzHazardsQt { get; set; }



        [DataMember]
        public String DzHistory { get; set; }



        [DataMember]
        public String UseDrug { get; set; }



        [DataMember]
        public String GroundState { get; set; }



        [DataMember]
        public String IndoorState { get; set; }



        [DataMember]
        public String EscortState { get; set; }



        [DataMember]
        public String DzHurtState { get; set; }



        [DataMember]
        public String DzHurtPosition { get; set; }



        [DataMember]
        public String DzHurtArea { get; set; }



        [DataMember]
        public String DzHandle { get; set; }



        [DataMember]
        public String DzHandleQt { get; set; }



        [DataMember]
        public String EventDescription { get; set; }



        [DataMember]
        public String EventLevel { get; set; }



        [DataMember]
        public String StaffType { get; set; }



        [DataMember]
        public String StaffTypeQt { get; set; }



        [DataMember]
        public String StaffPosition { get; set; }



        [DataMember]
        public String StaffEducation { get; set; }



        [DataMember]
        public String StaffEduQt { get; set; }



        [DataMember]
        public String StaffWorkyears { get; set; }



        [DataMember]
        public String AuditFeedback { get; set; }



        [DataMember]
        public String AuditStatus { get; set; }



        [DataMember]
        public String Remark { get; set; }



        [DataMember]
        public String IsFlag { get; set; }



        [DataMember]
        public DateTime OperatorDate { get; set; }



        [DataMember]
        public String OperatorID { get; set; }



        [DataMember]
        public String Degreeinjury { get; set; }




        [DataMember]
        public String Restrain { get; set; }

        [DataMember]
        public String BedColumnUse { get; set; }


        [DataMember]
        public IList<aers_tbl_events_yc_parts> Parts { get; set; }


        [DataMember]
        public String DzPlaceQt { get; set; }


        [DataMember]
        public String DzStateQt { get; set; }


        [DataMember]
        public String MentalStateQt { get; set; }

        [DataMember]
        public String UseDrugQt { get; set; }



        [DataMember]
        public String Address { get; set; }



        [DataMember]
        public String ExamineState { get; set; }


        [DataMember]
        public String FileName { get; set; }

    }

}

