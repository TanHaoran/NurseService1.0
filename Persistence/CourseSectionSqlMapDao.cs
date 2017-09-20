using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Aersysm.Domain;
using Aersysm.Persistence;

namespace Aersysm.Persistence
{
    public class CourseSectionSqlMapDao : BaseSqlMapDao
    {

        public void CourseSectionInsert(CourseSection courseSection)
        {
            String stmtId = "CourseSection_Insert";
            ExecuteInsert(stmtId, courseSection);

        }

        public void CourseSectionDelete(string ChapterID)
        {
            String stmtId = "CourseSection_Delete";
            ExecuteDelete(stmtId, ChapterID);

        }


        public void CourseSectionUpdate(CourseSection courseSection)
        {
            String stmtId = "CourseSection_Update";
            ExecuteUpdate(stmtId, courseSection);

        }

        public CourseSection CourseSectionFind(string ChapterID)
        {
            String stmtId = "CourseSection_Find";
            Hashtable ht = new Hashtable();
            ht.Add("ChapterID", ChapterID);

            CourseSection result = ExecuteQueryForObject<CourseSection>(stmtId, ht);
            return result;

        }




        public IList<CourseSection> CourseSectionFindAll()
        {
            String stmtId = "CourseSection_FindAll";
            IList<CourseSection> result = ExecuteQueryForList<CourseSection>(stmtId, null);
            return result;
   
        }

        public IList<CourseSection> CourseSectionFindByCourseID(string CourseID)
        {
            String stmtId = "CourseSection_FindByCourseID";
            Hashtable ht = new Hashtable();
            ht.Add("CourseID", CourseID);
            IList<CourseSection> result = ExecuteQueryForList<CourseSection>(stmtId, ht);
            return result;
   
        }
        public IList<CourseSection> CourseSectionFindByCatalogID(string CatalogID)
        {
            String stmtId = "CourseSection_FindByCatalogID";
            Hashtable ht = new Hashtable();
            ht.Add("CatalogID", CatalogID);
            IList<CourseSection> result = ExecuteQueryForList<CourseSection>(stmtId, ht);
            return result;

        }
        public IList<CourseSection> CourseSectionFindByChapterID(string ChapterID)
        {
            String stmtId = "CourseSection_FindByChapterID";
            Hashtable ht = new Hashtable();
            ht.Add("ChapterID", ChapterID);
            IList<CourseSection> result = ExecuteQueryForList<CourseSection>(stmtId, ht);
            return result;

        }




    }
}
