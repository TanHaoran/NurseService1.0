/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     YanMing
 *       日期：     2017/7/28 15:00:45
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
    /// userpracticecertificate 
    /// </summary>
    [Serializable]
    [DataContract]
    public partial class Userpracticecertificate
	{
		public Userpracticecertificate()
		{
			
		}
		private string _registerid;
		private string _certificateid;
        private string _name;
		private DateTime _birthdate;
		private string _sex;
		private string _country;
		private string _practiceaddress;
		private string _idcard;
		private DateTime _firstregisterdate;
		private DateTime _lastregisterdate;
		private DateTime _registertodate;
		private string _registerauthority;
		private string _certificateauthority;
		private DateTime _certificatedate;
		private string _picture1;
		private string _picture2;
		private string _picture3;
		private string _picture4;
		private int _verifystatus;
		private string _verifyview;
        private DateTime _firstjobtime;

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
			get{return _certificateid; }
			set{ _certificateid = value;}
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
        public DateTime BirthDate
		{
			get{return _birthdate;}
			set{_birthdate = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Sex
		{
			get{return _sex;}
			set{_sex = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Country
		{
			get{return _country;}
			set{_country = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string PracticeAddress
		{
			get{return _practiceaddress;}
			set{_practiceaddress = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string IDCard
		{
			get{return _idcard;}
			set{_idcard = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
        public DateTime FirstRegisterDate
		{
			get{return _firstregisterdate;}
			set{_firstregisterdate = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
        public DateTime LastRegisterDate
		{
			get{return _lastregisterdate;}
			set{_lastregisterdate = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
        public DateTime RegisterToDate
		{
			get{return _registertodate;}
			set{_registertodate = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string RegisterAuthority
		{
			get{return _registerauthority;}
			set{_registerauthority = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string CertificateAuthority
		{
			get{return _certificateauthority;}
			set{_certificateauthority = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
        public DateTime CertificateDate
		{
			get{return _certificatedate;}
			set{_certificatedate = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Picture1
		{
			get{return _picture1;}
			set{_picture1 = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Picture2
		{
			get{return _picture2;}
			set{_picture2 = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Picture3
		{
			get{return _picture3;}
			set{_picture3 = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Picture4
		{
			get{return _picture4;}
			set{_picture4 = value;}
		}
		
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public DateTime FirstJobTime
        {
            get { return _firstjobtime; }
            set { _firstjobtime = value; }
        }



       // 从审核表中取数据
        ///<sumary>
		/// 
		///</sumary>
        [DataMember]
        public int VerifyStatus
        {
            get { return _verifystatus; }
            set { _verifystatus = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string VerifyView
        {
            get { return _verifyview; }
            set { _verifyview = value; }
        }

    }
}
