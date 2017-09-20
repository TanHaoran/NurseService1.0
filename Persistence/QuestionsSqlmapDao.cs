using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Persistence;
using Aersysm.Domain;
using System.Collections;

namespace Aersysm.Persistence
{
    public class QuestionsSqlmapDao : BaseSqlMapDao
    {
        public IList<Questions> Questions_FindByChapterID(string ChapterID)
        {
            String stmtId = "Questions_FindByChapterID";
            Hashtable ht = new Hashtable();
            ht.Add("ChapterID", ChapterID);
            IList<Questions> result = ExecuteQueryForList<Questions>(stmtId, ht);
            return result;

        }

        public IList<Questions> QuestionsFindAll()
        {
            String stmtId = "Questions_FindAll";
            IList<Questions> result = ExecuteQueryForList<Questions>(stmtId, null);
            return result;
        }

        public int Delete(string Qid)
        {
            String stmtId = "Questions_Delete";
            Hashtable ht = new Hashtable();
            ht.Add("Qid", Qid);
            try
            {
                ExecuteDelete(stmtId, ht);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Insert(Questions data)
        {
            //QuestionsSqlmapDao dal = new QuestionsSqlmapDao();
            //data.Qid = dal.GetMaxID("staff");
            aers_sys_seedSqlMapDao dal = new aers_sys_seedSqlMapDao();
            data.Qid = dal.GetMaxID("questions");
            String stmtId = "Questions_Insert";
            Hashtable ht = new Hashtable();

            ht.Add("Qid", data.Qid);
            ht.Add("ChapterID", data.ChapterID);
            ht.Add("TypeName", data.TypeName);
            ht.Add("TitleName", data.TitleName);
            ht.Add("SpellNo", data.SpellNo);
            ht.Add("A", data.A );
            ht.Add("B", data.B);
            ht.Add("C", data.C);
            ht.Add("D", data.D);
            ht.Add("E", data.E);
            ht.Add("F", data.F);
            ht.Add("Score", data.Score );
            ht.Add("Result", data.Result);
            ht.Add("OperatorID", data.OperatorID );
            ht.Add("ModifyDate", data.ModifyDate);
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

      
        public int Update(Questions data)
        {                    
            String stmtId = "Questions_Update";
            Hashtable ht = new Hashtable();
            ht.Add("Qid", data.Qid);
            ht.Add("ChapterID", data.ChapterID);
            ht.Add("TypeName", data.TypeName);
            ht.Add("TitleName", data.TitleName);
            ht.Add("SpellNo", data.SpellNo);
            ht.Add("A", data.A);
            ht.Add("B", data.B);
            ht.Add("C", data.C);
            ht.Add("D", data.D);
            ht.Add("E", data.E);
            ht.Add("F", data.F);
            ht.Add("Score", data.Score);
            ht.Add("Result", data.Result);
            ht.Add("OperatorID", data.OperatorID);
            ht.Add("ModifyDate", data.ModifyDate);
            int i = ExecuteUpdate(stmtId, ht);
            return i;
        }
    }
}
