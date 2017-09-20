/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     YanMing
 *       日期：     2017/7/29 22:08:00
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace Aersysm.Domain
{
	/// <summary>
	/// releaseversion 
	/// </summary>
	[Serializable]
    [DataContract]
	public partial class Releaseversion
	{
		public Releaseversion()
		{
			
		}
		private string _versionid;
		private float  _version;
		private string _releaseurl;
		private DateTime _releasetime;
        private string _updatecontent;
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
		public string VersionId
		{
			get{return _versionid;}
			set{_versionid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public float Version
		{
			get{return _version;}
			set{_version = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string ReleaseUrl
		{
			get{return _releaseurl;}
			set{_releaseurl = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime ReleaseTime
		{
			get{return _releasetime;}
			set{_releasetime = value;}
		}

        ///<sumary>
		/// 
		///</sumary>
        [DataMember]
        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public string  UpdateContent
        {
            get { return _updatecontent; }
            set { _updatecontent = value; }
        }
        
    }
}
