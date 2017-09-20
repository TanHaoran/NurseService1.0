/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     YanMing
 *       日期：     2017/7/28 12:00:47
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
using System;
using System.Runtime.Serialization;

namespace Aersysm.Domain
{


    /// <summary>
    /// certificateverify 
    /// </summary>
    [Serializable]
    [DataContract]
    public partial class Certificateverify
	{
		public Certificateverify()
		{
			
		}
		private string _verifyid;
		private string _registerid;
		private string _certificateid;
		private DateTime _submittime;
		private int _type;
		private string _serviceid;
		private DateTime _dealtime;
		private int _verifystatus;
        private string _verifyview;

		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string VerifyId
		{
			get{return _verifyid;}
			set{_verifyid = value;}
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
		public string CertificateId
		{
			get{return _certificateid;}
			set{_certificateid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public DateTime SubmitTime
		{
			get{return _submittime;}
			set{_submittime = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public int Type
		{
			get{return _type;}
			set{_type = value;}
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
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public DateTime DealTime
		{
			get{return _dealtime;}
			set{_dealtime = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public int VerifyStatus
		{
			get{return _verifystatus;}
			set{_verifystatus = value;}
		}
        ///<sumary>
		/// 
		///</sumary>
        [DataMember]
        public string  VerifyView
        {
            get { return _verifyview; }
            set { _verifyview = value; }
        }
        
    }
}
