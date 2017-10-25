using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Aersysm.Domain
{
    [DataContract]
    public partial class view_tabl_onekey
    {
        #region 上报人
        [DataMember]
        public string FillStaff { get; set; }
        #endregion

        #region 科室名称
        [DataMember]
        public string HospdepName { get; set; }
        #endregion


        #region EveresId

        [DataMember]
        public String EveresId { get; set; }

        #endregion


        #region EveresName

        [DataMember]
        public String EveresName { get; set; }

        #endregion




        #region FeedbackState
        [DataMember]
        public Int32 FeedbackState { get; set; }

        #endregion


        #region ExamineState
        [DataMember]
        public Int32 ExamineState { get; set; }

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
        public string HospId { get; set; }
        #endregion

        #region IsFlag

        public Int32 IsFlag { get; set; }

        #endregion

        #region OperatorID
        public String OperatorID { get; set; }

        #endregion
    }
}
