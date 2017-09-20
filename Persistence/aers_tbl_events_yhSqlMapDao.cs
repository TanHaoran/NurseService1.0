/**
  * Name:Aersysm.SqlMapDao-aers_tbl_events_yh
  * Author: banshine
  * Description: aers_tbl_events_yh Dao层 
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
    public partial class aers_tbl_events_yhSqlMapDao: BaseSqlMapDao
    {
        public void Insert(aers_tbl_events_yh model)
        {
            model.EveYhId = new aers_sys_seedSqlMapDao().GetMaxID("eventsyh");
            DateTime dt = DateTime.Now;
            model.OperatorDate = dt;
            model.IsFlag = 0;
            model.AuditStatus = 0;
            model.OperatorID = model.FillStaff;
            String stmtId = "aers_tbl_events_yh_Insert";
            ExecuteInsert(stmtId, model);
        }

        public void Update(aers_tbl_events_yh model)
        {
   
            DateTime dt = DateTime.Now;
            model.OperatorDate = dt;
            model.IsFlag = 0;
            model.AuditStatus = 0;
            model.OperatorID = model.FillStaff;
            String stmtId = "aers_tbl_events_yh_Update";
            ExecuteUpdate(stmtId, model);
        }

        #region 根据事件汇总编码查询
        /// <summary>
        /// 根据事件汇总编码查询
        /// </summary>
        /// <param name="eid">事件汇总表编码</param>
        /// <returns></returns>
        public aers_tbl_events_yh FindByEud(string eid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("EveresId", eid);
            return ExecuteQueryForObject<aers_tbl_events_yh>("aers_tbl_events_yh_FindByEveresId", ht);
        }
        #endregion

        #region 更新状态
        public void UpdateState(string feedback, string examine, string eud)
        {
            String stmtId = "aers_tbl_events_yh_Update_State";
            Hashtable ht = new Hashtable();
            ht.Add("EveresId", eud);
            ht.Add("AuditFeedback", feedback);
            ht.Add("AuditStatus", examine);
            ExecuteUpdate(stmtId, ht);
        }
        #endregion
       
    }
    
}

