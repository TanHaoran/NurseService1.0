/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     YanMing
 *       日期：     2017/8/9 14:53:45
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
	/// emchat 
	/// </summary>
	[Serializable]
    [DataContract]
	public partial class Emchat
	{
		public Emchat()
		{
			
		}
		private string _emchatid;
		private string _myid;
		private string _emmyid;
		private string _friendid;
		private string _emfriendid;
		private string _remark;
		private int _isflag;
		
       
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string EMChatId
		{
			get{return _emchatid;}
			set{_emchatid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string MyId
		{
			get{return _myid;}
			set{_myid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string EMMyId
		{
			get{return _emmyid;}
			set{_emmyid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string FriendId
		{
			get{return _friendid;}
			set{_friendid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string EMFriendId
		{
			get{return _emfriendid;}
			set{_emfriendid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Remark
		{
			get{return _remark;}
			set{_remark = value;}
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
	}
}
