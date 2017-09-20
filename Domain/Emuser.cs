/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     YanMing
 *       日期：     2017/8/9 15:13:40
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
	/// emuser 
	/// </summary>
	[Serializable]
    [DataContract]
	public partial class Emuser
	{
		public Emuser()
		{
			
		}
		private string _emuserid;
		private string _registerid;
		private string _emregisterid;
		private string _emnickname;
		private int _isonline;
		private string _empassword;
		
       
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string EmUserId
		{
			get{return _emuserid;}
			set{_emuserid = value;}
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
		public string EmRegisterId
		{
			get{return _emregisterid;}
			set{_emregisterid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string EmNickName
		{
			get{return _emnickname;}
			set{_emnickname = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public int IsOnline
		{
			get{return _isonline;}
			set{_isonline = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string EmPassword
		{
			get{return _empassword;}
			set{_empassword = value;}
		}
	}
}
