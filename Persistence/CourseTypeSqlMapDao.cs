using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Domain;
using Aersysm.Persistence;

namespace Aersysm.Persistence
{
    public class CourseTypeSqlMapDao : BaseSqlMapDao
    {


        public void CourseTypeInsert(CourseType userinfo)
        {
            String stmtId = "CourseType_Find";
            ExecuteInsert(stmtId, userinfo);

        }

        public void CourseTypeDelete(string UserID)
        {
            String stmtId = "CourseType_Delete";
            ExecuteDelete(stmtId, UserID);

        }


        public void CourseTypeUpdate(CourseType userinfo)
        {
            String stmtId = "CourseType_Update";
            ExecuteUpdate(stmtId, userinfo);

        }

        public CourseType CourseTypeFind(string UserID)
        {
            String stmtId = "Userinfo_Find";

            Hashtable ht = new Hashtable();
            ht.Add("UserID", UserID);
            CourseType result = ExecuteQueryForObject<CourseType>(stmtId, ht);
            return result;
        }



        public IList<CourseType> CourseTypeFindAll()
        {
            String stmtId = "CourseType_FindAll";
            IList<CourseType> result = ExecuteQueryForList<CourseType>(stmtId, null);
            return result;
        }


    }
}
