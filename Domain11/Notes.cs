/**
  * Name:Train.Models-Notes
  * Author: banshine
  * Description: Notes 实体层 
  * Date: 2015-3-26 10:16:26
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Aersysm.Domain
{
    [DataContract]
    public partial class Notes
    {

        [DataMember]
        public String NoteID { get; set; }


        [DataMember]
        public String CourseID { get; set; }

        [DataMember]
        public String Content { get; set; }

        [DataMember]
        public String UserID { get; set; }

        [DataMember]
        public DateTime ModifyDate { get; set; }

        [DataMember]
        public Int32 IsShare { get; set; }

        [DataMember]
        public Int32 FavorCount { get; set; }

        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public Course Course { get; set; }
    }

}

