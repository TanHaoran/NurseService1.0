using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Domain;
using Aersysm.Persistence;

namespace Aersysm.Persistence
{
    public class CourseCatalogSqlMapDao : BaseSqlMapDao
    {

        public void CourseCatalogInsert(CourseCatalog data)
        {
            //String stmtId = "CourseCatalog_Insert";
            //ExecuteInsert(stmtId, courseCatalog);
            aers_sys_seedSqlMapDao dal = new aers_sys_seedSqlMapDao();
            data.CourseID = dal.GetMaxID("CourseCatalog");
            String stmtId = "CourseCatalog_Insert";
            Hashtable ht = new Hashtable();

            ht.Add("CatalogID", data.CatalogID);
            ht.Add("CourseID", data.CourseID);
            ht.Add("CatalogName", data.CatalogName);
            ht.Add("CatalogSort", data.CatalogSort);

            ExecuteInsert(stmtId, data);

        }

        public int Delete(string CatalogID)
        {
            try
            {
                String stmtId = "CourseCatalog_Delete";
                ExecuteDelete(stmtId, CatalogID);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public void CourseCatalogUpdate(CourseCatalog courseCatalog)
        {
            String stmtId = "CourseCatalog_Update";
            ExecuteUpdate(stmtId, courseCatalog);

        }

        public CourseCatalog CourseCatalogFind(string CatalogID)
        {
            String stmtId = "CourseCatalog_Find";

            Hashtable ht = new Hashtable();
            ht.Add("CatalogID", CatalogID);
            CourseCatalog result = ExecuteQueryForObject<CourseCatalog>(stmtId, ht);
            return result;
        }



        public IList<CourseCatalog> CourseCatalogFindAll()
        {
            String stmtId = "CourseCatalog_FindAll";
            IList<CourseCatalog> result = ExecuteQueryForList<CourseCatalog>(stmtId, null);
            return result;
        }

        public IList<CourseCatalog> CourseCatalog_FindByCourseID(string CourseID)
        {
            String stmtId = "CourseCatalog_FindByCourseID";
            Hashtable ht = new Hashtable();
            ht.Add("CourseID", CourseID);
            IList<CourseCatalog> result = ExecuteQueryForList<CourseCatalog>(stmtId, ht);
            return result;
        }
        
    }
}
