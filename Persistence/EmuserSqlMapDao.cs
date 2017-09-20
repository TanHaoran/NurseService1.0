/***************************************************************************
 * 
 *       功能：     持久层基类
 *       作者：     YanMing
 *       日期：     2017/8/9 15:13:40
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
	/// emuserSqlMapDao
	/// </summary>
	public partial class EmuserSqlMapDao : BaseSqlMapDao
	{
		public EmuserSqlMapDao ()
		{
			//
			// TODO: 此处添加emuserSqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<Emuser> GetEmuserList()
		{
			return ExecuteQueryForList<Emuser>("SelectEmuser",null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Addemuser(Emuser emuser)
		{
			//int id = GetId("emuser");
			//emuser.EmUserId = id;
			
			ExecuteInsert("InsertEmuser",emuser);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updateemuser(Emuser emuser)
		{
			ExecuteUpdate("UpdateEmuser",emuser);
		}
		
		/// <summary>
		/// 得到明细
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Emuser GetEmuserDetail(System.String EmUserId)
		{
			return ExecuteQueryForObject<Emuser>("SelectEmuser",EmUserId);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		public void Deleteemuser(System.String EmUserId)
		{
			ExecuteDelete("DeleteEmuser",EmUserId);
		}
		

	}
}
