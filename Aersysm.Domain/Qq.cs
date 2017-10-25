/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     YanMing
 *       日期：     2017/8/2 14:29:49
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
	/// qq 
	/// </summary>
	[Serializable]
    [DataContract]
	public partial class Qq
	{
		public Qq()
		{
			
		}
		private string _id;
		private string  _openid;
		private string _figureurl;
		private string _nickname;
		private string _province;
		private string _city;
		private string _gender;
		private string _accesstoken;
		private string _expires;
        private string _deviceId;
        private string _registerId;
        private string _phone;
        private string _reviceregId;
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
		public string Id
		{
			get{return _id;}
			set{_id = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string OpenId
        {
			get{return _openid;}
			set{_openid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string FigureUrl
        {
			get{return _figureurl;}
			set{_figureurl = value;}
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
		public string Province
        {
			get{return _province;}
			set{_province = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string City
        {
			get{return _city;}
			set{_city = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Gender
		{
			get{return _gender;}
			set{_gender = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string AccessToken
        {
			get{return _accesstoken;}
			set{_accesstoken = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Expires
        {
			get{return _expires;}
			set{_expires = value;}
		}

        ///<sumary>
		/// 
		///</sumary>
        [DataMember]
        public string DeviceId
        {
            get { return _deviceId; }
            set { _deviceId = value; }
        }

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string RegisterId
        {
            get { return _registerId; }
            set { _registerId = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        [DataMember]
        public string DeviceRegId
        {
            get { return _reviceregId; }
            set { _reviceregId = value; }
        }
    }
}
