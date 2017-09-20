/**
  * Name:Aersysm.SqlMapDao-aers_tbl_events_qt
  * Author: banshine
  * Description: aers_tbl_events_qt Dao层 
  * Date: 2015-6-8 11:13:02
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Domain;
using System.Collections;
using System.Data;

namespace Aersysm.Persistence
{
    public partial class aers_tbl_events_qtSqlMapDao: BaseSqlMapDao
    {
        public void Insert(aers_tbl_events_qt model)
        {
            model.EveqtId = new aers_sys_seedSqlMapDao().GetMaxID("eventsqt");
            DateTime dt = DateTime.Now;
            model.OperatorDate = dt;
            model.IsFlag = 0;
            model.AuditStatus = 0;
            model.OperatorID = model.FillStaff;
            String stmtId = "aers_tbl_events_qt_Insert";
            ExecuteInsert(stmtId, model);
        }


        public void Update(aers_tbl_events_qt model)
        {
          
            DateTime dt = DateTime.Now;
            model.OperatorDate = dt;
            model.IsFlag = 0;
            model.AuditStatus = 0;
            model.OperatorID = model.FillStaff;
            String stmtId = "aers_tbl_events_qt_Update";
            ExecuteUpdate(stmtId, model);
        }


        public IList<aers_tbl_events_qt> FindByShengHe()
        {

            
            String stmtId = "aers_tbl_events_qt_FindByShengHe";
            Hashtable ht = new Hashtable();
            IList<aers_tbl_events_qt> list= ExecuteQueryForList<aers_tbl_events_qt>(stmtId, ht);
            return list;
        }


        public IList<aers_tbl_events_qt> FindByShengHeEveresLevel(string EveresLevel)
        {


            String stmtId = "aers_tbl_events_qt_FindByShengHeEveresLevel";
            Hashtable ht = new Hashtable();
            ht.Add("EveresLevel", EveresLevel);
            IList<aers_tbl_events_qt> list = ExecuteQueryForList<aers_tbl_events_qt>(stmtId, ht);
            return list;
        }

        public DataSet FindGroupByName(DateTime time1)
        {


            String stmtId = "aers_tbl_events_qt_FindGroupByName";
            Hashtable ht = new Hashtable();
            ht.Add("HappenedDate", time1);
            DataSet ds = ExecutQueryForDataSet(stmtId, ht);
            return ds;
        }
        

        #region 根据事件汇总编码查询
        /// <summary>
        /// 根据事件汇总编码查询
        /// </summary>
        /// <param name="eid">事件汇总表编码</param>
        /// <returns></returns>
        public aers_tbl_events_qt FindByEud(string eid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("EveresId", eid);
            return ExecuteQueryForObject<aers_tbl_events_qt>("aers_tbl_events_qt_FindByEveresId", ht);
        }
        #endregion

        #region 更新状态
        public void UpdateState(string feedback, string examine, string eud)
        {
            String stmtId = "aers_tbl_events_qt_Update_State";
            Hashtable ht = new Hashtable();
            ht.Add("EveresId", eud);
            ht.Add("AuditFeedback", feedback);
            ht.Add("AuditStatus", examine);
            ExecuteUpdate(stmtId, ht);
        }
        #endregion
    }
    
}

