/**
  * Name:Aersysm.SqlMapDao-aers_tbl_hospdep
  * Author: banshine
  * Description: aers_tbl_hospdep Dao层 
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
    public partial class aers_tbl_hospdepSqlMapDao: BaseSqlMapDao
    {

        public int Insert(aers_tbl_hospdep data)
        {
            aers_sys_seedSqlMapDao dal = new aers_sys_seedSqlMapDao();
            data.HospdepId = dal.GetMaxID("hospdep");

            String stmtId = "aers_tbl_hospdep_Insert";
            Hashtable ht = new Hashtable();
            ht.Add("HospdepId", data.HospdepId);
            ht.Add("BasedepId", data.BasedepId);
            ht.Add("HospId", data.HospId);
            ht.Add("HospdepName", data.HospdepName);
            ht.Add("SpellNo", data.SpellNo);
            ht.Add("HospdepLogo", data.HospdepLogo);
            ht.Add("DisplayOrder", data.DisplayOrder);
            ht.Add("IsFlag", data.IsFlag);
            ht.Add("Remarks", data.Remarks);
            ht.Add("OperatorId", data.OperatorId);
            ht.Add("OperatorDate", data.OperatorDate);
            try
            {
                ExecuteInsert(stmtId, ht);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(string HospdepId)
        {

            String stmtId = "aers_tbl_hospdep_Delete";
            Hashtable ht = new Hashtable();
            ht.Add("HospdepId", HospdepId);
            try
            {
                ExecuteDelete(stmtId, ht);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public int Update(aers_tbl_hospdep data)
        {
            String stmtId = "aers_tbl_hospdep_Update";
            Hashtable ht = new Hashtable();
            ht.Add("HospdepId", data.HospdepId);
            ht.Add("BasedepId", data.BasedepId);
            ht.Add("HospId", data.HospId);
            ht.Add("HospdepName", data.HospdepName);
            ht.Add("SpellNo", data.SpellNo);
            ht.Add("HospdepLogo", data.HospdepLogo);
            ht.Add("DisplayOrder", data.DisplayOrder);
            ht.Add("IsFlag", data.IsFlag);
            ht.Add("Remarks", data.Remarks);
            ht.Add("OperatorId", data.OperatorId);
            ht.Add("OperatorDate", data.OperatorDate);


            int i = ExecuteUpdate(stmtId, ht);
            return i;
        }



        public IList<aers_tbl_hospdep> hospdepFindAll()
        {
            String stmtId = "aers_tbl_hospdep_FindAll";
            IList<aers_tbl_hospdep> result = ExecuteQueryForList<aers_tbl_hospdep>(stmtId, null);
            return result;

        }
        public IList<aers_tbl_hospdep> hospdepFindByHospId(string HospId)
        {
            String stmtId = "aers_tbl_hospdep_FindByHospId";
            Hashtable ht = new Hashtable();
            ht.Add("HospId", HospId);
            IList<aers_tbl_hospdep> result = ExecuteQueryForList<aers_tbl_hospdep>(stmtId, ht);
            return result;

        }

        

        #region 根据科室编码查询科室名称
        /// <summary>
        /// 根据科室编码查询科室名称
        /// </summary>
        /// <param name="depid">科室编码</param>
        /// <returns>科室中文名称</returns>
        public string FindNameByDepId(string depid)
        {
            string DepName = string.Empty;
            Hashtable ht = new Hashtable();
            ht.Add("HospdepId", depid);
            aers_tbl_hospdep model = ExecuteQueryForObject<aers_tbl_hospdep>("aers_tbl_hospdep_Find", ht);
            if (model !=null)
                DepName = model.HospdepName;
            return DepName;
        }


        public aers_tbl_hospdep FindhospdepByDepId(string depid)
        {
            string DepName = string.Empty;
            Hashtable ht = new Hashtable();
            ht.Add("HospdepId", depid);
            aers_tbl_hospdep model = ExecuteQueryForObject<aers_tbl_hospdep>("aers_tbl_hospdep_Find", ht);
            return model;
        }



        public aers_tbl_hospdep FindhospdepByHospdepName(string HospId,string HospdepName)
        {
            string DepName = string.Empty;
            Hashtable ht = new Hashtable();
            ht.Add("HospId", HospId);
            ht.Add("HospdepName", HospdepName);
            aers_tbl_hospdep model = ExecuteQueryForObject<aers_tbl_hospdep>("aers_tbl_hospdep_FindByHospdepName", ht);
            return model;
        }


        #endregion

        #region 根据科室编码查询基本信息
        /// <summary>
        /// 根据科室编码查询基本信息
        /// </summary>
        /// <param name="depid">科室编码</param>
        /// <returns></returns>
        public aers_tbl_hospdep Find(string depid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("HospdepId", depid);
            return ExecuteQueryForObject<aers_tbl_hospdep>("aers_tbl_hospdep_Find", ht);
        }
        #endregion
    }
    
}

