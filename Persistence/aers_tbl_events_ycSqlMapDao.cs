/**
  * Name:Aersysm.SqlMapDao-aers_tbl_events_yc
  * Author: banshine
  * Description: aers_tbl_events_yc Dao层 
  * Date: 2015-6-8 11:13:02
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Domain;
using System.Collections;

namespace Aersysm.Persistence
{
    public partial class aers_tbl_events_ycSqlMapDao : BaseSqlMapDao
    {
        #region 事件上报
        /// <summary>
        /// 事件上报
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Insert(aers_tbl_events_yc model) 
        {
            model.EveycId = new aers_sys_seedSqlMapDao().GetMaxID("eventsyc");
            DateTime dt = DateTime.Now;
            model.OperatorID = model.FillStaff;
            model.OperatorDate = dt;
            model.IsFlag = 0;
            model.AuditFeedback =string.Empty;
            model.AuditStatus = 0;
            String stmtId = "aers_tbl_events_yc_Insert";
            ExecuteInsert(stmtId, model);
            return model.EveycId;
        }
        #endregion



        #region 事件上报
        /// <summary>
        /// 事件上报
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Update(aers_tbl_events_yc model)
        {
            DateTime dt = DateTime.Now;
            model.OperatorID = model.FillStaff;
            model.OperatorDate = dt;
            model.IsFlag = 0;
            model.AuditFeedback = string.Empty;
            model.AuditStatus = 0;
            String stmtId = "aers_tbl_events_yc_Update";
            ExecuteUpdate(stmtId, model);
            return model.EveycId;
        }
        #endregion

        #region 根据事件汇总编码查询
        /// <summary>
        /// 根据事件汇总编码查询
        /// </summary>
        /// <param name="eid">事件汇总表编码</param>
        /// <returns></returns>
        public aers_tbl_events_yc FindByEud(string eid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("EveresId", eid);
            return ExecuteQueryForObject<aers_tbl_events_yc>("aers_tbl_events_yc_FindByEveresId",ht);
        }
        #endregion

        #region 根据压疮事件编码查询
        /// <summary>
        /// 根据压疮事件编码查询
        /// </summary>
        /// <param name="eid">主键,压疮事件编码</param>
        /// <returns></returns>
        public aers_tbl_events_yc Find(string uid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("EveycId", uid);
            return ExecuteQueryForObject<aers_tbl_events_yc>("aers_tbl_events_yc_Find", ht);
        }
        #endregion


        #region 更新状态
        public void UpdateState(string feedback, string examine,string eud)
        {
            String stmtId = "aers_tbl_events_yc_Update_State";
            Hashtable ht = new Hashtable();
            ht.Add("EveresId", eud);
            ht.Add("AuditFeedback", feedback);
            ht.Add("AuditStatus", examine);
            ExecuteUpdate(stmtId, ht);
        }
        #endregion




        public IList<aers_tbl_events_yc> FindListByData(DateTime HappenedDate)
        {
            String stmtId = "aers_tbl_events_yc_FindByData";
            Hashtable ht = new Hashtable();
            ht.Add("HappenedDate", HappenedDate);
            return ExecuteQueryForList<aers_tbl_events_yc>(stmtId, ht);
        }


    }
    
}

