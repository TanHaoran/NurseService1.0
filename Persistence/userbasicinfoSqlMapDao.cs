/***************************************************************************
 * 
 *       功能：     持久层基类
 *       作者：     BuzzlySoft
 *       日期：     2017-07-26
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
	/// userbasicinfoSqlMapDao
	/// </summary>
	public class userbasicinfoSqlMapDao : BaseSqlMapDao
	{
		public userbasicinfoSqlMapDao ()
		{
			//
			// TODO: 此处添加userbasicinfoSqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<UserBasicInfo> GetuserbasicinfoList()
		{
			return ExecuteQueryForList<UserBasicInfo>("Selectuserbasicinfo",null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Adduserbasicinfo(UserBasicInfo userbasicinfo)
		{
            //if (userbasicinfo.Birthday == null || userbasicinfo.Birthday == Convert.ToDateTime("01/01/0001"))
            //{
            //    userbasicinfo.Birthday = Convert.ToDateTime("01/01/1900");
            //}
            // userbasicinfo.RegisterId = new aers_sys_seedSqlMapDao().GetMaxID("Message");
            ExecuteInsert("Insertuserbasicinfo",userbasicinfo);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updateuserbasicinfo(UserBasicInfo userbasicinfo)  //Convert.ToDateTime("01/01/1900");
        {
            //if (userbasicinfo.Birthday ==null||userbasicinfo.Birthday==Convert.ToDateTime ("01/01/0001"))
            //{
            //    userbasicinfo .Birthday = Convert.ToDateTime("01/01/1900");
            //}
			ExecuteUpdate("Updateuserbasicinfo",userbasicinfo);
		}
		
		/// <summary>
		/// 得到明细
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public UserBasicInfo GetuserbasicinfoDetail(System.String RegisterId)
		{
			return ExecuteQueryForObject<UserBasicInfo>("Selectuserbasicinfo", RegisterId);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		public void Deleteuserbasicinfo(System.String RegisterId)
		{
			ExecuteDelete("Deleteuserbasicinfo", RegisterId);
		}

        public int DeleteAll()
        {
            return ExecSQL("DELETE from userbasicinfo");
        }
    }
}
