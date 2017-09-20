/***************************************************************************
 * 
 *       功能：     持久层基类
 *       作者：     YanMing
 *       日期：     2017/7/31 17:22:22
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
	/// userrelrecordSqlMapDao
	/// </summary>
	public partial class UserrelrecordSqlMapDao : BaseSqlMapDao
	{
		public UserrelrecordSqlMapDao ()
		{
			//
			// TODO: 此处添加userrelrecordSqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<Userrelrecord> GetUserrelrecordList()
		{
			return ExecuteQueryForList<Userrelrecord>("Selectuserrelrecord",null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Adduserrelrecord(Userrelrecord userrelrecord)
		{
			//int id = GetId("userrelrecord");
			//userrelrecord.RegisterId = id;
			
			ExecuteInsert("Insertuserrelrecord",userrelrecord);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updateuserrelrecord(Userrelrecord userrelrecord)
		{
			ExecuteUpdate("Updateuserrelrecord",userrelrecord);
		}
		
		/// <summary>
		/// 得到明细
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Userrelrecord GetuserrelrecordDetail(System.String RegisterId)
		{
			return ExecuteQueryForObject<Userrelrecord>("Selectuserrelrecord",RegisterId);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		public void Deleteuserrelrecord(System.String RegisterId)
		{
			ExecuteDelete("Deleteuserrelrecord",RegisterId);
		}

        public int DeleteAll()
        {
            return ExecSQL("DELETE from userrelrecord");
        }

    }
}
