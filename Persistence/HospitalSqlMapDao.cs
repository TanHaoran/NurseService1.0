/***************************************************************************
 * 
 *       功能：     持久层基类
 *       作者：     YanMing
 *       日期：     2017/7/29 19:17:40
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
	/// hospitalSqlMapDao
	/// </summary>
	public partial class HospitalSqlMapDao : BaseSqlMapDao
	{
		public HospitalSqlMapDao ()
		{
			//
			// TODO: 此处添加hospitalSqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<Hospital> GethospitalList()
		{
			return ExecuteQueryForList<Hospital>("Selecthospital",null);
		}

        /// <summary>
		/// 得到列表
		/// </summary>
		public IList<Hospital> GethospitalListName()
        {
            return ExecuteQueryForList<Hospital>("SelecthospitalName", null);
        }
        /// <summary>
        /// 新建
        /// </summary>
        public void Addhospital(Hospital hospital)
		{
			//int id = GetId("hospital");
			//hospital.HospitalId = id;
			
			ExecuteInsert("Inserthospital",hospital);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updatehospital(Hospital hospital)
		{
			ExecuteUpdate("Updatehospital",hospital);
		}

        /// <summary>
        /// 逻辑删除
        /// </summary>
        public void UpdateDelhospital(System.String HospitalId)
        {
            ExecuteUpdate("UpdateDelhospital", HospitalId);
        }

        /// <summary>
        /// 得到明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Hospital GethospitalDetail(System.String HospitalId)
		{
			return ExecuteQueryForObject<Hospital>("Selecthospital",HospitalId);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		public void Deletehospital(System.String HospitalId)
		{
			ExecuteDelete("Deletehospital",HospitalId);
		}

        /// <summary>
        /// 得到列表模糊匹配
        /// </summary>
        public IList<Hospital> GethospitalListLike(string Address)
        {
            return ExecuteQueryForList<Hospital>("SelecthospitalLike", Address);
            //string sql= "select * from Table1where concat('name', 'info')like'%xxx%';
        }
    }
}
