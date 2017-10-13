/***************************************************************************
 * 
 *       功能：     持久层基类
 *       作者：     YanMing
 *       日期：     2017/7/29 19:17:38
 * 
 *       修改日期： 
 *       修改人：
 *       修改内容：
 * 
 * *************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using IBatisNet.Common;
using IBatisNet.DataMapper;
using IBatisNet.Common.Exceptions;
using Aersysm.Domain;

namespace Aersysm.Persistence
{
	/// <summary>
	/// departmentSqlMapDao
	/// </summary>
	public partial class DepartmentSqlMapDao : BaseSqlMapDao
	{
		public DepartmentSqlMapDao ()
		{
			//
			// TODO: 此处添加departmentSqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<Department> GetdepartmentList()
		{
            return ExecuteQueryForList<Department>("Selectdepartment", null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Adddepartment(Department department)
		{
			//int id = GetId("department");
			//department.DepartmentId = id;
			
			ExecuteInsert("Insertdepartment",department);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updatedepartment(Department department)
		{
			ExecuteUpdate("Updatedepartment",department);
		}
		
		/// <summary>
		/// 得到明细
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Department GetdepartmentDetail(System.String DepartmentId)
		{
			return ExecuteQueryForObject<Department>("Selectdepartment",DepartmentId);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		public void Deletedepartment(System.String DepartmentId)
		{
			ExecuteDelete("Deletedepartment",DepartmentId);
		}
		

	}
}
