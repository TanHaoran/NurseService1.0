/***************************************************************************
 * 
 *       功能：     持久层基类
 *       作者：     YanMing
 *       日期：     2017/7/29 19:17:40
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
	/// nursingunitidSqlMapDao
	/// </summary>
	public partial class NursingunitidSqlMapDao : BaseSqlMapDao
	{
		public NursingunitidSqlMapDao ()
		{
			//
			// TODO: 此处添加nursingunitidSqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<Nursingunitid> GetnursingunitidList()
		{
			return ExecuteQueryForList<Nursingunitid>("Selectnursingunitid",null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Addnursingunitid(Nursingunitid nursingunitid)
		{
			
			ExecuteInsert("Insertnursingunitid",nursingunitid);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updatenursingunitid(Nursingunitid nursingunitid)
		{
			ExecuteUpdate("Updatenursingunitid",nursingunitid);
		}
		


	}
}
