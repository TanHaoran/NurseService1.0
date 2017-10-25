/**
  * Name:Train.Models-plancontents
  * Author: banshine
  * Description: plancontents 实体层 
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
    public partial class plancontents
    {

        [DataMember]
        public String PCID { get; set; }




        [DataMember]
        public String PlanID{ get;set;}

        [DataMember]
        public String CourseID {get;set;}

        [DataMember]
        public Int32 IndexNo { get;set; }

        [DataMember]
        public Decimal Credit{get;set;}


        [DataMember]
        public String OperatorID {get;set;}



        [DataMember]
        public DateTime ModifyDate { get;set; }



        [DataMember]
        public String HID { get; set; }


        [DataMember]
        public Int32 Status { get; set; }

        [DataMember]
        public Course Course { get; set; }


    }

}

