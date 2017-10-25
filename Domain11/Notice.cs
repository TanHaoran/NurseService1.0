/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     YanMing
 *       日期：     2017/8/5 12:57:44
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
using System.Collections.Generic;

namespace Aersysm.Domain
{
	/// <summary>
	/// notice 
	/// </summary>
	[Serializable]
    [DataContract]
	public partial class Notice
	{
		public Notice()
		{
			
		}
		private string _noticeid;
		private string _title;
		private string _content;
		private DateTime _noticetime;
		private int _isflag;
		private int _displayorder;
		private int _type;
		private int _isvital;
		private string _hospitalid;
		private string _departmentid;
		private string _issuer;
        private string _agency;

        private string _attachment;
        private int _isdelete;
        private string _operatorid;
        private DateTime _operatortime;

        private string _hospitalname;
        private string _departmentname;
      //  private string _departmentname;
       
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string NoticeId
		{
			get{return _noticeid;}
			set{_noticeid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Title
		{
			get{return _title;}
			set{_title = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string Content
		{
			get{return _content;}
			set{_content = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public DateTime NoticeTime
		{
			get{return _noticetime;}
			set{_noticetime = value;}
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
		public int DisplayOrder
		{
			get{return _displayorder;}
			set{_displayorder = value;}
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
		public int IsVital
		{
			get{return _isvital;}
			set{_isvital = value;}
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
		public string Issuer
		{
			get{return _issuer;}
			set{_issuer = value;}
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
        public string Attachment
        {
            get { return _attachment; }
            set { _attachment = value; }
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
        public string  HospitalName
        {
            get { return _hospitalname; }
            set { _hospitalname = value; }
        }
        ///<sumary>
        /// 
        ///</sumary>
        [DataMember]
        public string  DepartmentName
        {
            get { return _departmentname; }
            set { _departmentname = value; }
        }


        [DataMember]
        public List<Department> departmentlist { get; set; }
    }
}
