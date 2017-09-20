using System;
using System.Collections;
using System.Collections.Generic;
using IBatisNet.Common;
using IBatisNet.DataMapper;
using IBatisNet.Common.Exceptions;
using Aersysm.Domain;

namespace Aersysm.Persistence
{
   public  class UserLoginStatusSqlMapDao : BaseSqlMapDao
    {
        public UserLoginStatusSqlMapDao()
        {

        }

        /// <summary>
		/// 得到列表
		/// </summary>
		public IList<UserLoginStatus> GetUserLoginStatusList()
        {
            return ExecuteQueryForList<UserLoginStatus>("SelectUserLoginStatus", null);
        }

        /// <summary>
        /// 新建
        /// </summary>
        public void AddUserLoginStatus(UserLoginStatus userloginstatus)
        {
            //int id = GetId("userauths");
            //userauths.AuthsId = id;

            ExecuteInsert("InsertUserLoginStatus", userloginstatus);
        }
        /// <summary>
        /// 修改
        /// </summary>
        public void UpdateUserLoginStatus(UserLoginStatus userloginstatus)
        {
            ExecuteUpdate("UpdateUserLoginStatus", userloginstatus);
        }

        /// <summary>
        /// 得到明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UserLoginStatus GetUserLoginStatusDetail(System.String RegisterId)
        {
            return ExecuteQueryForObject<UserLoginStatus>("SelectUserLoginStatus", RegisterId);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void DeleteUserLoginStatus(System.String RegisterId)
        {
            ExecuteDelete("DeleteUserLoginStatus", RegisterId);
        }
        // DeleteUserauths
    }
}
