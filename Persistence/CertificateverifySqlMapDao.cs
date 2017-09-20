/***************************************************************************
 * 
 *       功能：     持久层基类
 *       作者：     YanMing
 *       日期：     2017/7/28 12:00:47
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
	/// certificateverifySqlMapDao
	/// </summary>
	public partial class CertificateverifySqlMapDao : BaseSqlMapDao
	{
		public CertificateverifySqlMapDao ()
		{
			//
			// TODO: 此处添加certificateverifySqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<Certificateverify> GetcertificateverifyList()
		{
			return ExecuteQueryForList<Certificateverify>("Selectcertificateverify",null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Addcertificateverify(Certificateverify certificateverify)
		{
			//int id = GetId("certificateverify");
			//certificateverify.VerifyId = id;
			
			ExecuteInsert("Insertcertificateverify",certificateverify);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updatecertificateverify(Certificateverify certificateverify)
		{
			ExecuteUpdate("Updatecertificateverify",certificateverify);
		}
		
		/// <summary>
		/// 得到明细
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Certificateverify GetcertificateverifyDetail(System.String VerifyId)
		{
			return ExecuteQueryForObject<Certificateverify>("Selectcertificateverify",VerifyId);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		public void Deletecertificateverify(System.String VerifyId)
		{
			ExecuteDelete("Deletecertificateverify",VerifyId);
		}
		

	}
}
