/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     ym
 *       日期：     2017/7/21
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
namespace Aersysm.Domain
{
	using System;
	
	/// <summary>
	/// sms 
	/// </summary>
	[Serializable]
	public class sms
	{
		public sms()
		{
			
		}
		
		private string _smsid;
		private string  _phone;
		private string _code;
		private DateTime _sendtime;
		private int _status;
        private int _type;

		///<sumary>
		/// 
		///</sumary>
		public string SMSId
		{
			get{return _smsid;}
			set{_smsid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
		public string  Phone
		{
			get{return _phone;}
			set{_phone = value;}
		}
		///<sumary>
		/// 
		///</sumary>
		public string Code
		{
			get{return _code;}
			set{_code = value;}
		}
		///<sumary>
		/// 
		///</sumary>
		public DateTime SendTime
		{
			get{return _sendtime;}
			set{_sendtime = value;}
		}
		///<sumary>
		/// 
		///</sumary>
		public int Status
		{
			get{return _status;}
			set{_status = value;}
		}
        ///<sumary>
		/// 
		///</sumary>
		public int Type
        {
            get { return _type; }
            set { _type = value; }
        }
    }
}
