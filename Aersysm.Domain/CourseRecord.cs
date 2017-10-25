using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Aersysm.Domain
{
    [DataContract]
    public partial  class CourseRecord
    {

        

              [DataMember]
        public Course Course { get; set; }

        #region CouRecID
        [DataMember]
        public String CouRecID { get; set; }
      
        #endregion

        #region CourseID
        [DataMember]
        public String CourseID { get; set; }


        #endregion

        #region StaffId
        [DataMember]
        public String StaffId { get; set; }
       

        #endregion

        #region StudyDateCount
        [DataMember]
        public Int32 StudyDateCount { get; set; }
       

        #endregion

        #region LastPlayChapterID
        [DataMember]
        public String LastPlayChapterID { get; set; }
       

        #endregion

        #region LastPlayDate
        [DataMember]
        public DateTime   LastPlayDate { get; set; }


        #endregion

        #region ModifyDate
        [DataMember]
        public DateTime ModifyDate { get; set; }
       

        #endregion

        #region PlayTime 
        [DataMember]
        public String PlayTime { get; set; }
        #endregion
    }
}
