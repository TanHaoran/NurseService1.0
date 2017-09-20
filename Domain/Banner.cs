/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     YanMing
 *       日期：     2017/8/5 15:20:38
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
	/// banner 
	/// </summary>
	[Serializable]
    [DataContract]
	public partial class Banner
	{
		public Banner()
		{
			
		}
		private string _bannerid;
		private string _bannerurl;
		private int _displayorder;
		private int _isflag;
		private int _type;
		private DateTime _bannertime;
		private string _issuer;
		private string _bannertourl;
		private string _hospitalid;
		private string _departmentid;
        private string _agency;
        private string _title;
        private int _isdelete;
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
		public string BannerId
		{
			get{return _bannerid;}
			set{_bannerid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string BannerUrl
		{
			get{return _bannerurl;}
			set{_bannerurl = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public int DisplayOrder
		{
			get{return _displayorder;}
			set{_displayorder = value;}
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
		public DateTime BannerTime
		{
			get{return _bannertime;}
			set{_bannertime = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Issuer
		{
			get{return _issuer;}
			set{_issuer = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string BannerToUrl
		{
			get{return _bannertourl;}
			set{_bannertourl = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string HospitalId
		{
			get{return _hospitalid;}
			set{_hospitalid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string DepartmentId
		{
			get{return _departmentid;}
			set{_departmentid = value;}
		}
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string Agency
        {
            get { return _agency; }
            set { _agency = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public int  IsDelete
        {
            get { return _isdelete; }
            set { _isdelete = value; }
        }
    }
}
