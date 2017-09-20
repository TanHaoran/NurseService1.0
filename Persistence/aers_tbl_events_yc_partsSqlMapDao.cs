/**
  * Name:Aersysm.SqlMapDao-aers_tbl_events_yc_parts
  * Author: banshine
  * Description: aers_tbl_events_yc_parts Dao层 
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
    public partial class aers_tbl_events_yc_partsSqlMapDao : BaseSqlMapDao
    {

        public void Delete(string EveycId)
        {
           
            String stmtId = "aers_tbl_events_yc_parts_Delete";
            Hashtable ht = new Hashtable();
            ht.Add("EveycId", EveycId);
            ExecuteDelete(stmtId, ht);
        }

        #region 新增操作
        public string Insert(aers_tbl_events_yc_parts model) 
        {
            model.PartsId = new aers_sys_seedSqlMapDao().GetMaxID("ycparts");
            DateTime dt = DateTime.Now;
            model.OperatorDate = dt;
            model.IsFlag = 0;
            String stmtId = "aers_tbl_events_yc_parts_Insert";
            ExecuteInsert(stmtId, model);
            return model.PartsId;
        }
        #endregion

        #region 新增操作
        public string Update(aers_tbl_events_yc_parts model)
        {

            DateTime dt = DateTime.Now;
            model.OperatorDate = dt;
            model.IsFlag = 0;
            String stmtId = "aers_tbl_events_yc_parts_Update";
            ExecuteUpdate(stmtId, model);
            return model.PartsId;
        }
        #endregion

        #region 根据压疮编号查询所有压疮部位信息
        /// <summary>
        /// 根据压疮编号查询所有压疮部位信息
        /// </summary>
        /// <param name="ycuid">压疮编号</param>
        /// <returns></returns>
        public IList<aers_tbl_events_yc_parts> FindByYcuid(string ycuid, string EveresName)
        {
            String stmtId = "aers_tbl_events_yc_parts_FindByEveycId";
            Hashtable ht = new Hashtable();
            ht.Add("EveycId", ycuid);
            ht.Add("EveresName", EveresName);
            return ExecuteQueryForList<aers_tbl_events_yc_parts>(stmtId, ht);
        }
        #endregion

    }
    
}

