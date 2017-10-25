/***************************************************************************
 * 
 *       功能：     实体类
 *       作者：     YanMing
 *       日期：     2017/7/31 17:22:22
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
	/// userrelrecord 
	/// </summary>
	[Serializable]
    [DataContract]
	public partial class Userrelrecord
	{
		public Userrelrecord()
		{
			
		}
		private string _registerid;
		private string _hospitalid;
		private string _hospitalname;
		private string _departmentid;
		private string _departmentname;
		private string _nursingunitid;
		private string _nursingunitname;
		private string _employeeid;
		private string  _role;
		
       
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
		public string HospitalId
		{
			get{return _hospitalid;}
			set{_hospitalid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string HospitalName
		{
			get{return _hospitalname;}
			set{_hospitalname = value;}
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
		public string DepartmentName
		{
			get{return _departmentname;}
			set{_departmentname = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string NursingUnitId
		{
			get{return _nursingunitid;}
			set{_nursingunitid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string NursingUnitName
		{
			get{return _nursingunitname;}
			set{_nursingunitname = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string EmployeeId
		{
			get{return _employeeid;}
			set{_employeeid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
        [DataMember]
		public string  Role
		{
			get{return _role;}
			set{_role = value;}
		}
	}
}
