/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     YanMing
 *       日期：     2017/7/29 19:17:39
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

    /// <summary>
    /// hospital 
    /// </summary>
    [Serializable]
    [DataContract]
    public partial class Hospital
	{
		public Hospital()
		{
			
		}
		private string _hospitalid;
		private string _areacode;
		private string _province;
		private string _city;
		private string _region;
		private string _name;
		private string _grade;
		private string _contact;
		private string _phone;
		private string _spellcode;
		private string _logo;
		private int _isflag;
		private string _introduction;
		private int _displayorder;
		private string _address;
        private string _operatorid;
        private DateTime  _operatortime;
        private int _isopenblsj;
        private int _isopenpb;
        private int _isopenxf;
        private string  _latitude;
        private string  _longitude;
        private int _isdelete;

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
		public string AreaCode
		{
			get{return _areacode;}
			set{_areacode = value;}
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
		public string Region
		{
			get{return _region;}
			set{_region = value;}
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
		public string Grade
		{
			get{return _grade;}
			set{_grade = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Contact
		{
			get{return _contact;}
			set{_contact = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Phone
		{
			get{return _phone;}
			set{_phone = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string SpellCode
		{
			get{return _spellcode;}
			set{_spellcode = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Logo
		{
			get{return _logo;}
			set{_logo = value;}
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
		public string Introduction
		{
			get{return _introduction;}
			set{_introduction = value;}
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
		public string Address
		{
			get{return _address;}
			set{_address = value;}
		}

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string OperatorId
        {
            get { return _operatorid; }
            set { _operatorid = value; }
        }

        ///<sumary>
		/// 
		///</sumary>
        [DataMember]
        public DateTime OperatorTime
        {
            get { return _operatortime; }
            set { _operatortime = value; }
        }

        ///<sumary>
		/// 
		///</sumary>
        [DataMember]
        public int IsOpenBLSJ
        {
            get { return _isopenblsj; }
            set { _isopenblsj = value; }
        }

        ///<sumary>
		/// 
		///</sumary>
        [DataMember]
        public int IsOpenPB
        {
            get { return _isopenpb; }
            set { _isopenpb = value; }
        }

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public int IsOpenXF
        {
            get { return _isopenxf; }
            set { _isopenxf = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string   Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string  Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public int IsDelete
        {
            get { return _isdelete; }
            set { _isdelete = value; }
        }
    }
}
