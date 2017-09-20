using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Domain;
using Aersysm.Persistence;

namespace Aersysm.Persistence
{
    public class PlancontentsSqlMapDao : BaseSqlMapDao
    {
        public IList<plancontents> PlancontentsFindAll()
        {
            String stmtId = "plancontents_FindAll";
            IList<plancontents> result = ExecuteQueryForList<plancontents>(stmtId, null);
            return result;
        }

        
    }
}
