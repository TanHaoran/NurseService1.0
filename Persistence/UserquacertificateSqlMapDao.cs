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
	/// userquacertificateSqlMapDao
	/// </summary>
	public class UserquacertificateSqlMapDao : BaseSqlMapDao
	{
		public UserquacertificateSqlMapDao ()
		{
			//
			// TODO: 此处添加userquacertificateSqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<Userquacertificate> GetuserquacertificateList()
		{
			return ExecuteQueryForList<Userquacertificate>("Selectuserquacertificate",null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Adduserquacertificate(Userquacertificate userquacertificate)
		{
            //int id = GetId("userquacertificate");
            //userquacertificate.RegisterId = id;
            //userquacertificate.RegisterId=
            ExecuteInsert("Insertuserquacertificate",userquacertificate);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updateuserquacertificate(Userquacertificate userquacertificate)
		{
			ExecuteUpdate("Updateuserquacertificate",userquacertificate);
		}
		
		/// <summary>
		/// 得到明细
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Userquacertificate GetuserquacertificateDetail(System.String RegisterId)
		{
			return ExecuteQueryForObject<Userquacertificate>("Selectuserquacertificate",RegisterId);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		public void Deleteuserquacertificate(System.String RegisterId)
		{
			ExecuteDelete("Deleteuserquacertificate",RegisterId);
		}

        public int DeleteAll()
        {
            return ExecSQL("DELETE from userquacertificate");
        }
    }
}
