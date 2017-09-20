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
    using System.Runtime.Serialization;
    using System.Web;

    /// <summary>
    /// userregister 
    /// </summary>
    [Serializable]
    [DataContract]
	public class userregister
	{
		public userregister()
		{
			
		}

        //public string RegisterId { get; set; }

        //public string NickName { get; set; }

        //public string Avatar { get; set; }

        //public string Name { get; set; }

        //public string Phone { get; set; }

        //public string Password { get; set; }

        private string _registerid;
        private string _nickname;
        private string _avatar;
        private string _name;
        private string _phone;   //院内账号登陆时为院内账号
        private string _password;

        private string _countrycode;
        private string _deviceId;
        private string _deviceregId;

        private int _logintype; //院内账号登陆时4不良事件 5学分，6排班
        private string _hospitalId;
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string RegisterId
        {
            get { return _registerid; }
            set { _registerid = value; }
        }
      
        ///<sumary>
        ///// System.Web.HttpUtility.UrlEncode(model.QQData.NickName, System.Text.Encoding.UTF8);  //解码
        ///</sumary>
       // string st = HttpUtility.UrlDecode(_nickname, System.Text.Encoding.UTF8);
        [DataMember]
        public string NickName
        {
            
            get { return _nickname; }
            set { _nickname = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
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
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string CountryCode
        {
            get { return _countrycode; }
            set { _countrycode = value; }
        }

        [DataMember]
        public string DeviceId
        {
            get { return _deviceId; }
            set { _deviceId = value; }
        }

        [DataMember]
        public string DeviceRegId
        {
            get { return _deviceregId; }
            set { _deviceregId = value; }
        }

        //院内账号登陆时加type 0 不良事件   1  学分   2  考试
        [DataMember]
        public int  LoginType
        {
            get { return _logintype; }
            set { _logintype = value; }
        }

        [DataMember]
        public string  HospitalId
        {
            get { return _hospitalId; }
            set { _hospitalId = value; }
        }
    }
}
