/***************************************************************************
 * 
 *       功能：     持久层基类
 *       作者：     YanMing
 *       日期：     2017/8/10 15:44:19
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
	/// groupinfoSqlMapDao
	/// </summary>
	public partial class GroupinfoSqlMapDao : BaseSqlMapDao
	{
		public GroupinfoSqlMapDao ()
		{
			//
			// TODO: 此处添加groupinfoSqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<Groupinfo> GetGroupinfoList()
		{
			return ExecuteQueryForList<Groupinfo>("SelectGroupinfo",null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Addgroupinfo(Groupinfo groupinfo)
		{
            //int id = GetId("groupinfo");
            //groupinfo.GroupId = id;

            ExecuteInsert("InsertGroupinfo",groupinfo);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updategroupinfo(Groupinfo groupinfo)
		{
			ExecuteUpdate("UpdateGroupinfo",groupinfo);
		}
		
		/// <summary>
		/// 得到明细
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Groupinfo GetGroupinfoDetail(System.String GroupId)
		{
			return ExecuteQueryForObject<Groupinfo>("SelectGroupinfo",GroupId);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		public void Deletegroupinfo(System.String GroupId)
		{
			ExecuteDelete("DeleteGroupinfo",GroupId);
		}
		

	}
}
