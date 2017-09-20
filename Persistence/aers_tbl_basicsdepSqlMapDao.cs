/**
  * Name:Aersysm.SqlMapDao-aers_tbl_basicsdep
  * Author: banshine
  * Description: aers_tbl_basicsdep Dao层 
  * Date: 2015-6-8 11:13:02
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Domain;

namespace Aersysm.Persistence
{
    public partial class aers_tbl_basicsdepSqlMapDao: BaseSqlMapDao
    {
    
        private string ToJson(aers_tbl_basicsdep tempModel) 
        {
            string res = string.Empty;
            try
            {
                res += "{";
                         res += "\"BasdepId\":\""+tempModel.BasdepId+"\",";
                         res += "\"BasdeptName\":\""+tempModel.BasdeptName+"\",";
                         res += "\"SpellNo\":\""+tempModel.SpellNo+"\",";
                        
                         res += "\"DisplayOrder\":"+tempModel.DisplayOrder+",";
                         res += "\"Remarks\":\""+tempModel.Remarks+"\",";
                        
                         res += "\"IsFlag\":"+tempModel.IsFlag+",";
                         res += "\"OperatorId\":\""+tempModel.OperatorId+"\",";
                         res += "\"OperatorDate\":\""+tempModel.OperatorDate+"\",";
                         res += "\"Basdeplogo\":\"" + tempModel.Basdeplogo + "\",";
                res = res.Substring(0, res.Length - 1) + "}" ;
            }
            catch (Exception e)
            {
                 res = string.Empty;
            }
            return res;
        }
    
         private string ToJson(IList<aers_tbl_basicsdep> tempList) 
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

        //2017.5.5Yanming 
        public IList<aers_tbl_basicsdep> BasicsdepFindAll()
        {
            
            String stmtId = "aers_tbl_basicsdep_FindAll";
            IList<aers_tbl_basicsdep> result = ExecuteQueryForList<aers_tbl_basicsdep>(stmtId, null);
            return result;
        }

    }
    
}

