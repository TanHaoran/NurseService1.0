/**
  * Name:Train.Models-Answer
  * Author: banshine
  * Description: Answer 实体层 
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
    public partial class Answer
    {


        [DataMember]

        public String AID { get; set; }

        [DataMember]

        public String QID { get; set; }

        [DataMember]

        public String Results { get; set; }

        [DataMember]
        public String Rightkey  { get; set; }

       [DataMember]

        public String UserID { get; set; }

        [DataMember]
        public DateTime ModifyDate { get; set; }


        [DataMember]
        public String CourseID { get; set; }

    }

}

