/**
  * Name:Aersysm.SqlMapDao-aers_tbl_hospital
  * Author: banshine
  * Description: aers_tbl_hospital Dao层 
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
    public partial class aers_tbl_events_ycSqlMapDao: BaseSqlMapDao
    {

        public int Insert(aers_tbl_hospital data)
        {
            aers_sys_seedSqlMapDao dal = new aers_sys_seedSqlMapDao();
            data.HospId = dal.GetMaxID("hospital");

            String stmtId = "aers_tbl_hospital_Insert";
            Hashtable ht = new Hashtable();

            ht.Add("HospId", data.HospId);
            ht.Add("HospName", data.HospName);
            ht.Add("Address", data.Address);
            ht.Add("Phone", data.Phone);
            ht.Add("Contact", data.Contact);
            ht.Add("Grade", data.Grade);
            ht.Add("Validitytime", data.Validitytime);
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

        public int Delete(aers_tbl_hospital data)
        {

            String stmtId = "aers_tbl_hospital_Delete";
            Hashtable ht = new Hashtable();
            ht.Add("HospId", data.HospId);
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


        public int Update(aers_tbl_hospital data)
        {
            String stmtId = "aers_tbl_hospital_Update";
            Hashtable ht = new Hashtable();

            ht.Add("HospId", data.HospId);
            ht.Add("HospName", data.HospName);
            ht.Add("Address", data.Address);
            ht.Add("Phone", data.Phone);
            ht.Add("Contact", data.Contact);
            ht.Add("Grade", data.Grade);
            ht.Add("Validitytime", data.Validitytime);
            ht.Add("IsFlag", data.IsFlag);
            ht.Add("Remarks", data.Remarks);
            ht.Add("OperatorId", data.OperatorId);
            ht.Add("OperatorDate", data.OperatorDate);

            int i =ExecuteUpdate(stmtId, ht);
            return i;
        }

        public IList<aers_tbl_hospital> hospitalFindAll()
        {
            String stmtId = "aers_tbl_hospital_FindAll";
            IList<aers_tbl_hospital> result = ExecuteQueryForList<aers_tbl_hospital>(stmtId, null);
            return result;

        }


        public aers_tbl_hospital hospitalFindByHospId(string HospId)
        {
            String stmtId = "aers_tbl_hospital_Find";
            Hashtable ht = new Hashtable();

            ht.Add("HospId", HospId);

            aers_tbl_hospital result = ExecuteQueryForObject<aers_tbl_hospital>(stmtId, ht);
        
            return result;

        }


        public IList<aers_tbl_hospital> GethospitalUnion()
        {
            String stmtId = "aers_tbl_hospital_HospUnion";
            Hashtable ht = new Hashtable();

            ht.Add("HospUnion", 1);
            IList<aers_tbl_hospital> result = ExecuteQueryForList<aers_tbl_hospital>(stmtId, ht);
            return result;

        }



        private string ToJson(aers_tbl_hospital tempModel) 
        {
            string res = string.Empty;
            try
            {
                res += "{";
                         res += "\"HospId\":\""+tempModel.HospId+"\",";
                         res += "\"HospName\":\""+tempModel.HospName+"\",";
                         res += "\"Address\":\""+tempModel.Address+"\",";
                         res += "\"Phone\":\""+tempModel.Phone+"\",";
                         res += "\"Contact\":\""+tempModel.Contact+"\",";
                         res += "\"Grade\":\""+tempModel.Grade+"\",";
                         res += "\"Validitytime\":\""+tempModel.Validitytime+"\",";
                        
                         res += "\"IsFlag\":"+tempModel.IsFlag+",";
                         res += "\"Remarks\":\""+tempModel.Remarks+"\",";
                         res += "\"OperatorId\":\""+tempModel.OperatorId+"\",";
                         res += "\"OperatorDate\":\""+tempModel.OperatorDate+"\",";
                res = res.Substring(0, res.Length - 1) + "}" ;
            }
            catch (Exception e)
            {
                 res = string.Empty;
            }
            return res;
        }
    
         private string ToJson(IList<aers_tbl_hospital> tempList) 
            {
               string res = string.Empty;
                try
                {
                    res += "[";
                    foreach (var item in tempList)
                    {
                        res += ToJson(item)+",";
                    }
                    res = res.Substring(0, res.Length - 1) + "]" ;
                }
                catch (Exception e)
                {
                    res = string.Empty;
                }
                return res;
            }
        
       
    }
    
}

