/***************************************************************************
 * 
 *       功能：     持久层基类
 *       作者：     YanMing
 *       日期：     2017/8/10 15:44:20
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
	/// groupuserSqlMapDao
	/// </summary>
	public partial class GroupuserSqlMapDao : BaseSqlMapDao
	{
		public GroupuserSqlMapDao ()
		{
			//
			// TODO: 此处添加groupuserSqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<Groupuser> GetGroupuserList()
		{
			return ExecuteQueryForList<Groupuser>("SelectGroupuser",null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Addgroupuser(Groupuser groupuser)
		{
			//int id = GetId("groupuser");
			//groupuser.GroupUserId = id;
			
			ExecuteInsert("InsertGroupuser",groupuser);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updategroupuser(Groupuser groupuser)
		{
			ExecuteUpdate("UpdateGroupuser",groupuser);
		}
		
		/// <summary>
		/// 得到明细
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Groupuser GetGroupuserDetail(System.String GroupUserId)
		{
			return ExecuteQueryForObject<Groupuser>("SelectGroupuser",GroupUserId);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		public void Deletegroupuser(System.String GroupUserId)
		{
			ExecuteDelete("DeleteGroupuser",GroupUserId);
		}
		

	}
}
