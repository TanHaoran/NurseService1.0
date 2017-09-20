using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Domain;
using System.Collections;

namespace Aersysm.Persistence
{
    public partial class CourseDistributeSqlMapDao : BaseSqlMapDao
    {
        public void Insert(CourseDistribute model)
        {
            model.DistributeID = new aers_sys_seedSqlMapDao().GetMaxID("CourseDistribute");
            String stmtId = "CourseDistribute_Insert"; // Message_Insert
            ExecuteInsert(stmtId, model);
        }

        public void Update(CourseDistribute model)
        {
            String stmtId = "CourseDistribute_Update";
            ExecuteUpdate(stmtId, model);
        }

        public void Delete(string ID)
        {
            String stmtId = "CourseDistribute_Delete";
            ExecuteDelete(stmtId, ID);
        }

        public IList<CourseDistribute> FindAll()
        {
            String stmtId = "CourseDistribute_FindAll";
            IList<CourseDistribute> result = ExecuteQueryForList<CourseDistribute>(stmtId, null);
            return result;
        }
    }
}
