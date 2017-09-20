/***************************************************************************
 * 
 *       功能：     持久层基类
 *       作者：     YanMing
 *       日期：     2017/8/3 18:20:36
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
	/// qqSqlMapDao
	/// </summary>
	public class QqSqlMapDao : BaseSqlMapDao
    {
		public QqSqlMapDao ()
		{
			//
			// TODO: 此处添加qqSqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<Qq> GetQqList()
		{
			return ExecuteQueryForList<Qq>("SelectQq",null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Addqq(Qq qq)
		{
			
			ExecuteInsert("InsertQq",qq);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updateqq(Qq qq)
		{
			ExecuteUpdate("UpdateQq",qq);
		}

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void Deleteqq(System.String Id)
        {
            ExecuteDelete("DeleteQq",Id);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void DeleteqqByOpenId(System.String OpenId)
        {
            ExecuteDelete("DeleteQqByOpenId", OpenId);
        }

    }
}
