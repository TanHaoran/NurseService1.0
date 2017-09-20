using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Domain;
using Aersysm.Persistence;

namespace Aersysm.Persistence
{
    public class CourseOfficeSqlMapDao : BaseSqlMapDao
    {

        public void CourseOfficeInsert(CourseOffice courseOffice)
        {
            String stmtId = "CourseOffice_Find";
            ExecuteInsert(stmtId, courseOffice);

        }

        public void CourseOfficeDelete(string Id)
        {
            String stmtId = "CourseOffice_Delete";
            ExecuteDelete(stmtId, Id);

        }


        public void CourseOfficeUpdate(CourseOffice answer)
        {
            String stmtId = "CourseOffice_Update";
            ExecuteUpdate(stmtId, answer);

        }

        public CourseOffice CourseOfficeFind(string Id)
        {
            String stmtId = "CourseOffice_Find";

            Hashtable ht = new Hashtable();
            ht.Add("Id", Id);
            CourseOffice result = ExecuteQueryForObject<CourseOffice>(stmtId, ht);
            return result;
        }



        public IList<CourseOffice> CourseOfficeFindAll()
        {
            String stmtId = "CourseOffice_FindAll";
            IList<CourseOffice> result = ExecuteQueryForList<CourseOffice>(stmtId, null);
            return result;
        }
    }
}
