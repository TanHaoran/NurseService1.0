using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Domain;
using Aersysm.Persistence;

namespace Aersysm.Persistence
{
    public class StudyScheduleSqlMapDao : BaseSqlMapDao
    {

        public IList<StudySchedule> StudyScheduleFindAll()
        {
            String stmtId = "StudySchedule_FindAll";
            IList<StudySchedule> result = ExecuteQueryForList<StudySchedule>(stmtId, null);
            return result;
        }
    }
}
