/***************************************************************************
 * 
 *       功能：     持久层基类
 *       作者：     YanMing
 *       日期：     2017/8/5 12:57:45
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
	/// noticeSqlMapDao
	/// </summary>
	public partial class NoticeSqlMapDao : BaseSqlMapDao
	{
		public NoticeSqlMapDao ()
		{
			//
			// TODO: 此处添加noticeSqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<Notice> GetNoticeList()
		{
			return ExecuteQueryForList<Notice>("SelectNotice",null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Addnotice(Notice notice)
		{
			//int id = GetId("notice");
			//notice.NoticeId = id;
			
			ExecuteInsert("InsertNotice",notice);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updatenotice(Notice notice)
		{
			ExecuteUpdate("UpdateNotice",notice);
		}

        /// <summary>
		/// 逻辑删除
		/// </summary>
		public void UpdateDelNotice(System .String NoticeId)
        {
            ExecuteUpdate("UpdateDelNotice", NoticeId);
        }

        /// <summary>
        /// 得到明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Notice GetNoticeDetail(System.String NoticeId)
		{
			return ExecuteQueryForObject<Notice>("SelectNotice",NoticeId);
		}

		/// <summary>
		/// 删除
		/// </summary>
		/// <param name="id"></param>
		public void Deletenotice(System.String NoticeId)
		{
			ExecuteDelete("DeleteNotice",NoticeId);
		}
		

	}
}
