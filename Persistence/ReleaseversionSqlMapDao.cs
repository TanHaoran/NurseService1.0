/***************************************************************************
 * 
 *       功能：     持久层基类
 *       作者：     YanMing
 *       日期：     2017/7/29 22:13:34
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
	/// releaseversionSqlMapDao
	/// </summary>
	public partial class ReleaseversionSqlMapDao : BaseSqlMapDao
	{
		public ReleaseversionSqlMapDao ()
		{
			//
			// TODO: 此处添加releaseversionSqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<Releaseversion> GetReleaseversionList()
		{
			return ExecuteQueryForList<Releaseversion>("Selectreleaseversion",null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Addreleaseversion(Releaseversion releaseversion)
		{
			//int id = GetId("releaseversion");
			//releaseversion.VersionId = id;
			
			ExecuteInsert("Insertreleaseversion",releaseversion);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updatereleaseversion(Releaseversion releaseversion)
		{
			ExecuteUpdate("Updatereleaseversion",releaseversion);
		}
		
		/// <summary>
		/// 得到明细
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Releaseversion GetreleaseversionDetail(System.String VersionId)
		{
			return ExecuteQueryForObject<Releaseversion>("Selectreleaseversion",VersionId);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		public void Deletereleaseversion(System.String VersionId)
		{
			ExecuteDelete("Deletereleaseversion",VersionId);
		}
		

	}
}
