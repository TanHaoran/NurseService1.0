using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Domain;
using Aersysm.Persistence;

namespace Aersysm.Persistence
{
    public  class AnswerSqlMapDao : BaseSqlMapDao
    {

        public Int32 InsertAnswerlist(IList<Answer> list)
        {
            int count = 0;
            foreach (Answer item in list)
            {
                if (Answer_FindQID(item.UserID, item.CourseID, item.QID) != null)
                {
                    count += AnswerUpdate(item);
                }
                else
                {
                    count += Insert(item);
                }



               
            }
            return count;

        }


        public Int32 Insert(Answer answer)
        {
            try
            {
                aers_sys_seedSqlMapDao dal = new aers_sys_seedSqlMapDao();
                answer.AID = dal.GetMaxID("Answer");
                //2017-6-2 DH
                answer.ModifyDate = DateTime.Now;
                //2017-6-2 DH
                String stmtId = "Answer_Insert";
                ExecuteInsert(stmtId, answer);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public void AnswerDelete(string AID)
        {
            String stmtId = "Answer_Delete";
            ExecuteDelete(stmtId, AID);

        }


        public int AnswerUpdate(Answer answer)
        {
            try
            {
                String stmtId = "Answer_Update";
                answer.ModifyDate = DateTime.Now;
                ExecuteUpdate(stmtId, answer);
                return 1;
            }
            catch (Exception ex)
            {

                return 0;
            }
                    
          

        }

        

        public Answer Answer_FindQID(string UserID, string CourseID,string QID)
        {
            String stmtId = "Answer_FindQID";

            Hashtable ht = new Hashtable();
            ht.Add("UserID", UserID);
            ht.Add("CourseID", CourseID);
            ht.Add("QID", QID);
            Answer result = ExecuteQueryForObject<Answer>(stmtId, ht);
            return result;
        }

        public Answer AnswerFind(string AID)
        {
            String stmtId = "Answer_Find";

            Hashtable ht = new Hashtable();
            ht.Add("AID", AID);
            Answer result = ExecuteQueryForObject<Answer>(stmtId, ht);
            return result;
        }



        public IList<Answer> AnswerFindAll()
        {
            String stmtId = "Answer_FindAll";
            IList<Answer> result = ExecuteQueryForList<Answer>(stmtId, null);
            return result;
        }


        public IList<Answer> GetAnswerByUserCourseID(string UserID,string CourseID)
        {
            String stmtId = "Answer_ByUserCourseID";
            Hashtable ht = new Hashtable();
            ht.Add("UserID", UserID);
            ht.Add("CourseID", CourseID);
            IList<Answer> result = ExecuteQueryForList<Answer>(stmtId, ht);
            return result;
        }
    }
}
