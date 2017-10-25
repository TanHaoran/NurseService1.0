/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     YanMing
 *       日期：     2017/7/29 20:36:13
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
namespace Aersysm.Domain
{
    using System;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Runtime.Serialization;

    /// <summary>
    /// feedback 
    /// </summary>
    [Serializable]
    [DataContract]
    public partial class Feedback
	{
		public Feedback()
		{
			
		}
		private string _feedbackid;
		private string _registerid;
		private string _content;
		private DateTime _feedbacktime;
		private DateTime _servicetime;
		private string _serviceid;
		
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string FeedbackId
		{
			get{return _feedbackid;}
			set{_feedbackid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string RegisterId
		{
			get{return _registerid;}
			set{_registerid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Content
		{
			get{return _content;}
			set{_content = value;}
		}
        ///<sumary>
        /// 
        ///</sumary>

        [DataMember]
      //  [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime FeedbackTime
		{
			get{return _feedbacktime;}
			set{_feedbacktime = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
       // [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime ServiceTime
		{
			get{return _servicetime;}
			set{_servicetime = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string ServiceId
		{
			get{return _serviceid;}
			set{_serviceid = value;}
		}

       
	}
}
