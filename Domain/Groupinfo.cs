/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     YanMing
 *       日期：     2017/8/13 14:26:52
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
	/// groupinfo 
	/// </summary>
	[Serializable]
    [DataContract]
	public partial class Groupinfo
	{
		public Groupinfo()
		{
			
		}
		private string _groupid;
		private string _groupnickname;
		private int _usercount;
		private string _createrid;
		private DateTime _createtime;
		private int _isflag;
		private string _descg;
		private int _maxgroupcount;
        private string  _hxgroupId;
        

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
		public string GroupId
		{
			get{return _groupid;}
			set{_groupid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string GroupNickName
		{
			get{return _groupnickname;}
			set{_groupnickname = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public int UserCount
		{
			get{return _usercount;}
			set{_usercount = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string CreaterId
		{
			get{return _createrid;}
			set{_createrid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public DateTime CreateTime
		{
			get{return _createtime;}
			set{_createtime = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public int IsFlag
		{
			get{return _isflag;}
			set{_isflag = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Descg
		{
			get{return _descg;}
			set{_descg = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public int MaxGroupCount
		{
			get{return _maxgroupcount; }
			set{ _maxgroupcount = value;}
		}

        ///<sumary>
		/// 
		///</sumary>
        [DataMember]
        public string  HXGroupId
        {
            get { return _hxgroupId; }
            set { _hxgroupId = value; }
        }
        
    }
}
