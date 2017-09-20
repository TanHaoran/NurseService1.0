using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Aersysm.Persistence;
using Aersysm.Domain;

namespace Aersysm.Persistence
{
    public class ProblemSqlMapDao : BaseSqlMapDao
    {
        public IList<Problem> ProblemFindByCourseID(string CourseID)
        {
            String stmtId = "Problem_FindByCourseID";
            Hashtable ht = new Hashtable();
            ht.Add("CourseID", CourseID);
            IList<Problem> result = ExecuteQueryForList<Problem>(stmtId, ht);
            return result;
        }


        public string Insert(Problem data)
        {
            aers_sys_seedSqlMapDao dal = new aers_sys_seedSqlMapDao();
            data.QID = dal.GetMaxID("Problem");

            String stmtId = "Problem_Insert";
            Hashtable ht = new Hashtable();
            ht.Add("QID", data.QID);
            ht.Add("CourseID", data.CourseID);
            ht.Add("Title", data.Title);
            ht.Add("UserID", data.UserID);
            ht.Add("ModifyDate", DateTime.Now);
            ht.Add("FavorCount", data.FavorCount);

            ExecuteInsert(stmtId, ht);
            return data.QID;
        }

        public void Update(Problem model)
        {
            String stmtId = "Problem_Update";
            ExecuteUpdate(stmtId, model);
        }

        public void  UpdateFavorCountByMainID(int  FavorCount,string MainID)
        {
            String stmtId = "UpdateFavorCountByMainID";
            Hashtable ht = new Hashtable();
            ht.Add("FavorCount", FavorCount);
            ht.Add("QID", MainID);
            ExecuteUpdate(stmtId, ht);
        }

        public Problem ProblemFindByQID(string QID)
        {
            String stmtId = "Problem_FindByQID";
            Hashtable ht = new Hashtable();
            ht.Add("QID", QID);
            return ExecuteQueryForObject<Problem>(stmtId, ht);
        }
    }
}
