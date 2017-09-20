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
	/// userregisterSqlMapDao
	/// </summary>
	public class userregisterSqlMapDao : BaseSqlMapDao
	{
		public userregisterSqlMapDao ()
		{
			//
			// TODO: 此处添加userregisterSqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<userregister> GetuserregisterList()
		{
			return ExecuteQueryForList<userregister>("Selectuserregister",null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Adduserregister(userregister userregister)
		{

            //  userregister.RegisterId = new aers_sys_seedSqlMapDao().GetMaxID("userregister");
                         //  Insertuserregister
            ExecuteInsert("Insertuserregister",userregister);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updateuserregister(userregister userregister)
		{
			ExecuteUpdate("Updateuserregister",userregister);
		}
		
		/// <summary>
		/// 得到明细
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public userregister GetuserregisterDetail(System.String RegisterId)
		{
			return ExecuteQueryForObject<userregister>("Selectuserregister",RegisterId);
		}

        /// <summary>
		/// 得到明细根据手机号
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public userregister GetuserregisterDetailByPhone(System.String Phone)
        {
            return ExecuteQueryForObject<userregister>("SelectuserregisterByPhone", Phone);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void Deleteuserregister(System.String RegisterId)
		{
			ExecuteDelete("Deleteuserregister",RegisterId);
		}






        public int   DeleteAll()
        {
                                        //sms           注册                   执业证                          资格证                              基本信息                   组织关系                    授权                  qq
          return   ExecSQL("DELETE from sms;DELETE from userregister;DELETE from userquacertificate;DELETE from userpracticecertificate;DELETE from userbasicinfo;DELETE from userrelrecord;DELETE from userauths;DELETE from qq;");
        }

        public int UpdateSeed()
        {                                                            //sms                                                           注册                                                      执业证                                                          资格证                                                                         基本信息                                                       组织关系                                                        授权
            return ExecSQL("update aers_sys_seed set MaxNo=0 where TableName=sms;update aers_sys_seed set MaxNo=0 where TableName=userregister;update aers_sys_seed set MaxNo=0 where TableName=userquacertificate;update aers_sys_seed set MaxNo=0 where TableName=userpracticecertificate;update aers_sys_seed set MaxNo=0 where TableName=userbasicinfo;update aers_sys_seed set MaxNo=0 where TableName=userrelrecord;update aers_sys_seed set MaxNo=0 where TableName=userauths;update aers_sys_seed set MaxNo=0 where TableName=qq;");
        }
    }
}
