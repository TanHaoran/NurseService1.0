using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Domain;
using System.Collections;

namespace Aersysm.Persistence
{
    public class CourseUserSqlMapDao : BaseSqlMapDao
    {
        public void Insert(CourseUser model)
        {
            model.ID = new aers_sys_seedSqlMapDao().GetMaxID("CourseUser");
            String stmtId = "CourseUser_Insert"; // Message_Insert
            ExecuteInsert(stmtId, model);
        }

        public void Update(CourseUser model)
        {
            String stmtId = "CourseUser_Update";
            ExecuteUpdate(stmtId, model);
        }

        public void Delete(string ID)
        {
            String stmtId = "CourseUser_Delete";
            ExecuteDelete(stmtId, ID);
        }

        public IList<CourseUser> FindAll()
        {
            String stmtId = "CourseUser_FindAll";
            IList<CourseUser> result = ExecuteQueryForList<CourseUser>(stmtId, null);
            return result;
        }
    }
}
