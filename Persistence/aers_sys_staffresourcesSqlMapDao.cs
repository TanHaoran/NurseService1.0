/**
  * Name:Aersysm.SqlMapDao-aers_sys_staffresources
  * Author: banshine
  * Description: aers_sys_staffresources Dao层 
  * Date: 2015-6-8 11:13:02
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Domain;

namespace Aersysm.Persistence
{
    public partial class aers_sys_staffresourcesSqlMapDao: BaseSqlMapDao
    {
    
        private string ToJson(aers_sys_staffresources tempModel) 
        {
            string res = string.Empty;
            try
            {
                res += "{";
                         res += "\"ResId\":\""+tempModel.ResId+"\",";
                         res += "\"ResName\":\""+tempModel.ResName+"\",";
                         res += "\"ResUri\":\""+tempModel.ResUri+"\",";
                         res += "\"ParentId\":\""+tempModel.ParentId+"\",";
                         res += "\"StaffId\":\""+tempModel.StaffId+"\",";
                         res += "\"Remark\":\""+tempModel.Remark+"\",";
                        
                         res += "\"IsFlag\":"+tempModel.IsFlag+",";
                         res += "\"OperatorDate\":\""+tempModel.OperatorDate+"\",";
                         res += "\"OperatorID\":\""+tempModel.OperatorID+"\",";
                res = res.Substring(0, res.Length - 1) + "}" ;
            }
            catch (Exception e)
            {
                 res = string.Empty;
            }
            return res;
        }
    
         private string ToJson(IList<aers_sys_staffresources> tempList) 
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

