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
	
	/// <summary>
	/// depnurunit 
	/// </summary>
	[Serializable]
	public class depnurunit
	{
		public depnurunit()
		{
			
		}
		
		private string _depnurunitid;
		private string _departmentid;
		private string _nursingunitid;
		
		///<sumary>
		/// 
		///</sumary>
		public string DepNurUnitId
		{
			get{return _depnurunitid;}
			set{_depnurunitid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
		public string DepartmentId
		{
			get{return _departmentid;}
			set{_departmentid = value;}
		}
		///<sumary>
		/// 
		///</sumary>
		public string NursingUnitId
		{
			get{return _nursingunitid;}
			set{_nursingunitid = value;}
		}
	}
}
