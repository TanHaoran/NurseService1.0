/**
  * Name:Aersysm.SqlMapDao-aers_tbl_events_gy
  * Author: banshine
  * Description: aers_tbl_events_gy Dao层 
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
    public partial class aers_tbl_events_gySqlMapDao: BaseSqlMapDao
    {
        public void Insert(aers_tbl_events_gy model)
        {
            model.EvegyId = new aers_sys_seedSqlMapDao().GetMaxID("eventsgy");
            DateTime dt = DateTime.Now;
            model.OperatorDate = dt;
            model.IsFlag = 0;
            model.AuditStatus = 0;
            model.OperatorID = model.FillStaff;
            String stmtId = "aers_tbl_events_gy_Insert";
            ExecuteInsert(stmtId, model);
        }


        public void Update(aers_tbl_events_gy model)
        {
        
            DateTime dt = DateTime.Now;
            model.OperatorDate = dt;
            model.IsFlag = 0;
            model.AuditStatus = 0;
            model.OperatorID = model.FillStaff;
            String stmtId = "aers_tbl_events_gy_Update";
            ExecuteUpdate(stmtId, model);
        }

        #region 根据事件汇总编码查询
        /// <summary>
        /// 根据事件汇总编码查询
        /// </summary>
        /// <param name="eid">事件汇总表编码</param>
        /// <returns></returns>
        public aers_tbl_events_gy FindByEud(string eid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("EveresId", eid);
            return ExecuteQueryForObject<aers_tbl_events_gy>("aers_tbl_events_gy_FindByEveresId", ht);
        }
        #endregion

        #region 更新状态
        public void UpdateState(string feedback, string examine, string eud)
        {
            String stmtId = "aers_tbl_events_gy_Update_State";
            Hashtable ht = new Hashtable();
            ht.Add("EveresId", eud);
            ht.Add("AuditFeedback", feedback);
            ht.Add("AuditStatus", examine);
            ExecuteUpdate(stmtId, ht);
        }
        #endregion



        public IList<aers_tbl_events_gy> FindListByData(DateTime HappenedDate)
        {
            String stmtId = "aers_tbl_events_gy_FindByData";
            Hashtable ht = new Hashtable();
            ht.Add("HappenedDate", HappenedDate);
            return ExecuteQueryForList<aers_tbl_events_gy>(stmtId, ht);
        }
    }
    
}

