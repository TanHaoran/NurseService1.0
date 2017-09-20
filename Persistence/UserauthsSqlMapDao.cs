/***************************************************************************
 * 
 *       功能：     持久层基类
 *       作者：     YanMing
 *       日期：     2017/8/2 14:57:01
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
	/// userauthsSqlMapDao
	/// </summary>
	public partial class UserauthsSqlMapDao : BaseSqlMapDao
	{
		 public UserauthsSqlMapDao ()
		{
			//
			// TODO: 此处添加userauthsSqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<Userauths> GetUserauthsList()
		{
			return ExecuteQueryForList<Userauths>("SelectUserauths",null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Adduserauths(Userauths userauths)
		{
			//int id = GetId("userauths");
			//userauths.AuthsId = id;
			
			ExecuteInsert("InsertUserauths",userauths);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void  Updateuserauths(Userauths userauths)
		{
			ExecuteUpdate("UpdateUserauths",userauths);
		}
		
		/// <summary>
		/// 得到明细
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Userauths GetUserauthsDetail(System.String AuthsId)
		{
			return ExecuteQueryForObject<Userauths>("SelectUserauths",AuthsId);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		public void Deleteuserauths(System.String AuthsId)
		{
			ExecuteDelete("DeleteUserauths",AuthsId);
		}
                          // DeleteUserauths

        public int DeleteAll()
        {
            return ExecSQL("DELETE from Userauths");
        }
    }
}
