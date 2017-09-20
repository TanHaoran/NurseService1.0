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
	/// userpracticecertificateSqlMapDao
	/// </summary>
	public class UserpracticecertificateSqlMapDao : BaseSqlMapDao
	{
		public UserpracticecertificateSqlMapDao ()
		{
			//
			// TODO: 此处添加userpracticecertificateSqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<Userpracticecertificate> GetuserpracticecertificateList()
		{
			return ExecuteQueryForList<Userpracticecertificate>("Selectuserpracticecertificate",null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Adduserpracticecertificate(Userpracticecertificate userpracticecertificate)
		{
            //int id = GetId("userpracticecertificate");
            //userpracticecertificate.RegisterId = id;
            //if (userpracticecertificate.BirthDate==null)
            //{

            //}
			ExecuteInsert("Insertuserpracticecertificate",userpracticecertificate);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updateuserpracticecertificate(Userpracticecertificate userpracticecertificate)
		{
			ExecuteUpdate("Updateuserpracticecertificate",userpracticecertificate);
		}
		
		/// <summary>
		/// 得到明细
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Userpracticecertificate GetuserpracticecertificateDetail(System.String RegisterId)
		{
			return ExecuteQueryForObject<Userpracticecertificate>("Selectuserpracticecertificate",RegisterId);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		public void Deleteuserpracticecertificate(System.String RegisterId)
		{
			ExecuteDelete("Deleteuserpracticecertificate",RegisterId);
		}

        public int DeleteAll()
        {
            return ExecSQL("DELETE from userpracticecertificate");
        }
    }
}
