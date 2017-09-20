/**
  * Name:Train.Models-aers_tbl_eventsresume
  * Author: banshine
  * Description: aers_tbl_eventsresume 实体层 
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
    public partial class aers_tbl_eventsresume
    {

        #region EveresId
        [DataMember]
        public String EveresId { get; set; }

        #endregion


        #region EveresName

        [DataMember]
        public String EveresName { get; set; }

        #endregion


        #region SpellNo

        [DataMember]
        public String SpellNo { get; set; }

        #endregion


        #region EveresLevel

        [DataMember]
        public String EveresLevel { get; set; }

        #endregion


        #region FeedbackState

        [DataMember]
        public Int32 FeedbackState { get; set; }

        #endregion


        #region ExamineState

        [DataMember]
        public String ExamineState { get; set; }

        #endregion


        #region HappenedDate

        [DataMember]
        public DateTime HappenedDate { get; set; }

        #endregion


        #region SendtoDate

        [DataMember]
        public DateTime SendtoDate { get; set; }

        #endregion

        #region HospDepId
        [DataMember]
        public string HospDepId { get; set; }
        #endregion

        #region HospId
        [DataMember]
        public string HospId { get; set; }
        #endregion


        #region Remark

        public String Remark { get; set; }

        #endregion


        #region IsFlag

        public Int32 IsFlag { get; set; }

        #endregion


        #region OperatorDate

        public DateTime OperatorDate { get; set; }

        #endregion


        #region OperatorID
        [DataMember]
        public String OperatorID { get; set; }


        [DataMember]
        public String Address { get; set; }

        #endregion


        [DataMember]
        public String FileName { get; set; }


        [DataMember]
        public String  Grade { get; set; }


        [DataMember]
        public String OperatorName { get; set; }
    }

}

