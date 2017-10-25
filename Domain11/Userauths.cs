/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     YanMing
 *       日期：     2017/8/2 14:57:01
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
namespace Aersysm.Domain {
    /// <summary>
    /// userauths 
    /// </summary>
    [Serializable]
    [DataContract]
    public partial class Userauths {
        public Userauths() {

        }
        private string _authsid;
        private string _registerid;
        private int _logintype;
        private string _loginnumber;
        private DateTime _loginlasttime;
        private string _imei;
        private int _verified;
        private string _password;
        private string _reguserid;
        // ReguserId

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string AuthsId {
            get { return _authsid; }
            set { _authsid = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string RegisterId {
            get { return _registerid; }
            set { _registerid = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public int LoginType {
            get { return _logintype; }
            set { _logintype = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string LoginNumber {
            get { return _loginnumber; }
            set { _loginnumber = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public DateTime LoginLastTime {
            get { return _loginlasttime; }
            set { _loginlasttime = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string IMEI {
            get { return _imei; }
            set { _imei = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public int Verified {
            get { return _verified; }
            set { _verified = value; }
        }
        ///<sumary>
		/// 
		///</sumary>
        [DataMember]
        public string Password {
            get { return _password; }
            set { _password = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string ReguserId {
            get { return _reguserid; }
            set { _reguserid = value; }
        }

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string XFuserId { get; set; }

    }
}
