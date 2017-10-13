/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     YanMing
 *       日期：     2017/7/29 19:17:38
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
    /// department 
    /// </summary>
    [Serializable]
    [DataContract]
    public partial class Department
	{
		public Department()
		{
			
		}
		private string _departmentid;
		private string _hospitalid;
		private string _name;
		private string _parentid;
		private string _contact;
		private string  _phone;
		private string _spellcode;
		private string _logo;
		private int _isflag;
		private string _introduction;
		private int _displayorder;
		private string _address;

        private string _hospitalname;
		
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
		public string HospitalId
		{
			get{return _hospitalid;}
			set{_hospitalid = value;}
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
		public string ParentId
		{
			get{return _parentid;}
			set{_parentid = value;}
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
        public string HospitalName
        {
            get { return _hospitalname; }
            set { _hospitalname = value; }
        }


        [DataMember]
        public string OperatorId { get; set; }
    }
}
