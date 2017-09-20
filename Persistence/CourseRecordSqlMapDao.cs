using Aersysm.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Aersysm.Persistence
{
  public  class CourseRecordSqlMapDao : BaseSqlMapDao
    {
        public void CourseRecordInsert(CourseRecord courseRecord)
        {
            courseRecord.CouRecID = new aers_sys_seedSqlMapDao().GetMaxID("CourseRecord");
            String stmtId = "CourseRecord_Insert";
            ExecuteInsert(stmtId, courseRecord);

        }

        public void CourseRecordDelete(string Id)
        {
            String stmtId = "CourseRecord_Delete";
            ExecuteDelete(stmtId, Id);

        }


        public void CourseRecordUpdate(CourseRecord answer)
        {
            String stmtId = "CourseRecord_Update";

            //2017-6-2 DH 

            Hashtable ht = new Hashtable();
            ht.Add("CourseID", answer.CourseID);
            ht.Add("StaffId", answer.StaffId);

            ht.Add("StudyDateCount", answer.StudyDateCount);
            ht.Add("LastPlayChapterID", answer.LastPlayChapterID);
            ht.Add("LastPlayDate", answer.LastPlayDate);
            ht.Add("ModifyDate", answer.ModifyDate);
            ht.Add("PlayTime", answer.PlayTime);

            //2017-6-2 DH 

            ExecuteUpdate(stmtId, answer);

        }

        public CourseRecord CourseRecordFindById(string Id)
        {
            String stmtId = "CourseRecord_Find";

            Hashtable ht = new Hashtable();
            ht.Add("Id", Id);
            CourseRecord result = ExecuteQueryForObject<CourseRecord>(stmtId, ht);
            return result;
        }



        public IList<CourseRecord> CourseRecordFindAll()
        {
            String stmtId = "CourseRecord_FindAll";
            IList<CourseRecord> result = ExecuteQueryForList<CourseRecord>(stmtId, null);
            return result;
        }


    }
}
