/**
  * Name:Train.Models-Course
  * Author: banshine
  * Description: Course 实体层 
  * Date: 2015-3-26 10:16:26
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Aersysm.Domain
{
    [DataContract]
    public partial class Course
    {

        [DataMember]

        public String CourseID
        {
            get;
            set;
        }






        [DataMember]
        public String CourseType
        {
            get;
            set;
        }
        [DataMember]

        public String CourseName
        {
            get;
            set;
        }

        [DataMember]
        public String CourseTeacher
        {
            get;
            set;
        }

        [DataMember]
        public String CourseIntro
        {
            get;
            set;
        }

        [DataMember]
        public Int32 CourseDuration
        {
            get;
            set;
        }

        [DataMember]
        public Int32 CourseLevel
        {
            get;
            set;
        }

        [DataMember]
        public String CourseTag
        {
            get;
            set;
        }



        [DataMember]
        public String CourseThumb
        {
            get;
            set;
        }



        [DataMember]
        public String SuitableDuty
        {
            get;
            set;
        }

        [DataMember]
        public Int32 SuitableYearUp
        {
            get;
            set;
        }


        [DataMember]
        public Int32 SuitableYearDown
        {
            get;
            set;
        }



        [DataMember]
        public Int32 CourseHot
        {
            get;
            set;
        }



        [DataMember]
        public Int32 CourseSort
        {
            get;
            set;
        }

        [DataMember]
        public String AuthorID
        {
            get;
            set;
        }



        [DataMember]
        public String OperatorID
        {
            get;
            set;
        }



        [DataMember]
        public DateTime OperateTime
        {
            get;
            set;
        }

        [DataMember]
        public Int32 Recommend
        {
            get;
            set;
        }


        [DataMember]
        public String TeacherIntroduction
        {
            get;

            set;
        }



        [DataMember]
        public String ChapterPoints
        {
            get;

            set;
        }




        [DataMember]
        public String HospId
        {
            get;
            set;
        }


        [DataMember]
        public String HospdepId
        {
            get;

            set;
        }




    }

}

