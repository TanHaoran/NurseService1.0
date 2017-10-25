using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Aersysm.Domain
{
    [DataContract]
    public partial class view_tbl_eventsresume
   {
        #region 事件编号
        [DataMember]
        public string EventId { get; set; }
        #endregion

        #region 事件类型
        [DataMember]
        public string Event { get; set; }
        #endregion

        #region 事件等级
        [DataMember]
        public string EventLevel { get; set; }
        #endregion

        #region 发生时间
        [DataMember]
        public string HappenedDate { get; set; }
        #endregion

        [DataMember]
        public DateTime HappenedDateTime { get; set; }

        #region 上报时间
        [DataMember]
        public string SendtoDate { get; set; }
        #endregion

        #region 上报人所在科室名称
        [DataMember]
        public string Dep { get; set; }
        #endregion

        #region 是否反馈
        [DataMember]
        public string FeedbackState { get; set; }
        #endregion

        #region 上报状态
        [DataMember]
        public string ExamineState { get; set; }
       #endregion 

   }
}
