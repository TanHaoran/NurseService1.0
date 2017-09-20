/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     YanMing
 *       日期：     2017/7/28 15:00:46
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
	/// userquacertificate 
	/// </summary>
	[Serializable]
    [DataContract]
    public partial class Userquacertificate
	{
		public Userquacertificate()
		{
			
		}
		private string _registerid;
		private string _certificateid;
		private string _manageid;
		private string _name;
		private string _sex;
		private DateTime _datebirth;
		private string _majorname;
		private string _qualevel;
		private string _category;
		private DateTime _approvedate;
		private string _issuingagency;
		private DateTime _issuingdate;
		private string _picture1;
		private string _picture2;
		private int _verifystatus;
		private string _verifyview;
		
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
		public string ManageId
		{
			get{return _manageid;}
			set{_manageid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Name
		{
			get{return _name;}
			set{_name = value;}
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
        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime DateBirth
		{
			get{return _datebirth;}
			set{_datebirth = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string MajorName
		{
			get{return _majorname;}
			set{_majorname = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string QuaLevel
		{
			get{return _qualevel;}
			set{_qualevel = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Category
		{
			get{return _category;}
			set{_category = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime ApproveDate
		{
			get{return _approvedate;}
			set{_approvedate = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string IssuingAgency
		{
			get{return _issuingagency;}
			set{_issuingagency = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
        [JsonConverter(typeof(JavaScriptDateTimeConverter))]
        public DateTime IssuingDate
		{
			get{return _issuingdate;}
			set{_issuingdate = value;}
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



        //从审核表中取数据
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
		public string VerifyView
		{
			get{return _verifyview;}
			set{_verifyview = value;}
		}

       
	}
}
