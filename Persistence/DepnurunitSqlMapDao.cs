/***************************************************************************
 * 
 *       功能：     持久层基类
 *       作者：     ym
 *       日期：     2017/7/21
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
	/// depnurunitSqlMapDao
	/// </summary>
	public class depnurunitSqlMapDao : BaseSqlMapDao
	{
		public depnurunitSqlMapDao ()
		{
			//
			// TODO: 此处添加depnurunitSqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<depnurunit> GetdepnurunitList()
		{
			return ExecuteQueryForList<depnurunit>("Selectdepnurunit",null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Adddepnurunit(depnurunit depnurunit)
		{
            depnurunit.DepNurUnitId = new aers_sys_seedSqlMapDao().GetMaxID("depnurunit");
            ExecuteInsert("Insertdepnurunit",depnurunit);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updatedepnurunit(depnurunit depnurunit)
		{
			ExecuteUpdate("Updatedepnurunit",depnurunit);
		}
		


	}
}
