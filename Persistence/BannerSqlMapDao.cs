/***************************************************************************
 * 
 *       功能：     持久层基类
 *       作者：     YanMing
 *       日期：     2017/8/5 15:20:40
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
	/// bannerSqlMapDao
	/// </summary>
	public partial class BannerSqlMapDao : BaseSqlMapDao
	{
		public BannerSqlMapDao ()
		{
			//
			// TODO: 此处添加bannerSqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<Banner> GetBannerList()
		{
			return ExecuteQueryForList<Banner>("SelectBanner",null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Addbanner(Banner banner)
		{
			//int id = GetId("banner");
			//banner.BannerId = id;
			
			ExecuteInsert("InsertBanner",banner);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updatebanner(Banner banner)
		{
			ExecuteUpdate("UpdateBanner",banner);
		}

        /// <summary>
		/// 逻辑删除
		/// </summary>
		public void UpdateDelbanner(System .String BannerId)
        {
            ExecuteUpdate("UpdateDelBanner", BannerId);
        }

        /// <summary>
        /// 得到明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Banner GetBannerDetail(System.String BannerId)
		{
			return ExecuteQueryForObject<Banner>("SelectBanner",BannerId);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		public void Deletebanner(System.String BannerId)
		{
			ExecuteDelete("DeleteBanner",BannerId);
		}
		

	}
}
