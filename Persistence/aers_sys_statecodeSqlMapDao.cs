/**
  * Name:Aersysm.SqlMapDao-aers_sys_statecode
  * Author: banshine
  * Description: aers_sys_statecode Dao层 
  * Date: 2015-6-8 11:13:02
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Domain;

namespace Aersysm.Persistence
{
    public partial class aers_sys_statecodeSqlMapDao: BaseSqlMapDao
    {
        public IList<aers_sys_statecode> FindAll() 
        {
            return ExecuteQueryForList<aers_sys_statecode>("aers_sys_statecode_FindAll", null);
        }
    }
    
}

