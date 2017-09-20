/***************************************************************************
 * 
 *       功能：     持久层基类
 *       作者：     YanMing
 *       日期：     2017/7/29 20:36:14
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
	/// feedbackSqlMapDao
	/// </summary>
	public partial class FeedbackSqlMapDao : BaseSqlMapDao
	{
		public FeedbackSqlMapDao ()
		{
			//
			// TODO: 此处添加feedbackSqlMapDao的构造函数
			//
		}

		/// <summary>
		/// 得到列表
		/// </summary>
		public IList<Feedback> GetfeedbackList()
		{
			return ExecuteQueryForList<Feedback>("Selectfeedback",null);
		}

		/// <summary>
		/// 新建
		/// </summary>
		public void Addfeedback(Feedback feedback)
		{
            feedback .FeedbackId= new aers_sys_seedSqlMapDao().GetMaxID("Feedback");
            feedback.FeedbackTime = DateTime.Now;
            ExecuteInsert("Insertfeedback",feedback);
		}
		/// <summary>
		/// 修改
		/// </summary>
		public void Updatefeedback(Feedback feedback)
		{
			ExecuteUpdate("Updatefeedback",feedback);
		}
		


	}
}
