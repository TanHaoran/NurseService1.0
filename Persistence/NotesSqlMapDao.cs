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
    public class NotesSqlMapDao : BaseSqlMapDao
    {
        public IList<Notes> NotesFindByCourseID(string CourseID)
        {
            String stmtId = "Notes_FindByCourseID";
            Hashtable ht = new Hashtable();
            ht.Add("CourseID", CourseID);
            IList<Notes> result = ExecuteQueryForList<Notes>(stmtId, ht);
            return result;
        }


        public string Insert(Notes data)
        {
            aers_sys_seedSqlMapDao dal = new aers_sys_seedSqlMapDao();
            data.NoteID = dal.GetMaxID("Notes");

            String stmtId = "Notes_Insert";
            Hashtable ht = new Hashtable();
            
            ht.Add("NoteID", data.NoteID);
            ht.Add("CourseID", data.CourseID);
            ht.Add("Content", data.Content);
            ht.Add("UserID", data.UserID);
            ht.Add("ModifyDate", DateTime.Now);
            ht.Add("IsShare", data.IsShare);
            ht.Add("FavorCount", data.FavorCount);
            ExecuteInsert(stmtId, ht);
            return data.NoteID;
        }
        public void Delete(string NoteID)
        {
            String stmtId = "Notes_Delete";
            ExecuteDelete(stmtId, NoteID);
        }
        public IList<Notes> FindAll()
        {
            String stmtId = "Notes_FindAll";
            IList<Notes> result = ExecuteQueryForList<Notes>(stmtId, null);
            return result;
        }
    }
}
