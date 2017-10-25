/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     YanMing
 *       日期：     2017/8/10 15:44:19
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
	/// groupuser 
	/// </summary>
	[Serializable]
    [DataContract]
	public partial class Groupuser
	{
		public Groupuser()
		{
			
		}
		private string _groupuserid;
		private string _registerid;
		private string _nickname;
		private DateTime _jointime;
		private int _ismaster;
		private int _isflag;
        private string _hxgroupId;
        private string _groupId;

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
		public string GroupUserId
		{
			get{return _groupuserid;}
			set{_groupuserid = value;}
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
		public string NickName
		{
			get{return _nickname;}
			set{_nickname = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public DateTime JoinTime
		{
			get{return _jointime;}
			set{_jointime = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public int IsMaster
		{
			get{return _ismaster;}
			set{_ismaster = value;}
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

        [DataMember]
        public string  HXGroupId
        {
            get { return _hxgroupId; }
            set { _hxgroupId = value; }
        }

        [DataMember]
        public string GroupId
        {
            get { return _groupId; }
            set { _groupId = value; }
        }
    }
}
