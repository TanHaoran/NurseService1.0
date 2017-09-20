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
	/// countrySqlMapDao
	/// </summary>
	public class countrySqlMapDao : BaseSqlMapDao
	{
		public countrySqlMapDao ()
		{
			//
			// TODO: 此处添加countrySqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<country> GetcountryList()
		{
			return ExecuteQueryForList<country>("Selectcountry",null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Addcountry(country country)
		{
            country.CountryId = new aers_sys_seedSqlMapDao().GetMaxID("CertificateAudit");
            ExecuteInsert("Insertcountry",country);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updatecountry(country country)
		{
			ExecuteUpdate("Updatecountry",country);
		}
		
		/// <summary>
		/// 得到明细
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public country GetcountryDetail(System.String CountryId)
		{
			return ExecuteQueryForObject<country>("Selectcountry",CountryId);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		public void Deletecountry(System.String CountryId)
		{
			ExecuteDelete("Deletecountry",CountryId);
		}
		

	}
}
