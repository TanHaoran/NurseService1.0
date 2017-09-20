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
	/// smsSqlMapDao
	/// </summary>
	public class smsSqlMapDao : BaseSqlMapDao
	{
		public smsSqlMapDao ()
		{
			//
			// TODO: 此处添加smsSqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<sms> GetsmsList()
		{
			return ExecuteQueryForList<sms>("Selectsms",null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Addsms(sms sms)
		{
            //sms.SMSId= new aers_sys_seedSqlMapDao().GetMaxID("sms");
            ExecuteInsert("Insertsms",sms);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updatesms(sms sms)
		{
			ExecuteUpdate("Updatesms",sms);
		}

        public int DeleteAll()
        {
            return ExecSQL("DELETE from sms");
        }

    }
}
