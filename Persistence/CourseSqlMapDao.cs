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
    public class CourseSqlMapDao : BaseSqlMapDao
    {

        public void CourseInsert(Course data)
        {   
            aers_sys_seedSqlMapDao dal = new aers_sys_seedSqlMapDao();
            data.CourseID = dal.GetMaxID("Course");
            String stmtId = "Course_Insert";
            Hashtable ht = new Hashtable();

            ht.Add("CourseID", data.CourseID);
            ht.Add("CourseType", data.CourseType);
            ht.Add("CourseName", data.CourseName);
            ht.Add("CourseTeacher", data.CourseTeacher);
            ht.Add("CourseIntro", data.CourseIntro);
            ht.Add("CourseDuration", data.CourseDuration);
            ht.Add("CourseLevel", data.CourseLevel);
            ht.Add("CourseTag", data.CourseTag);
            ht.Add("CourseThumb", data.CourseThumb);
            ht.Add("SuitableDuty", data.SuitableDuty );
            ht.Add("SuitableYearUp", data.SuitableYearUp);
            ht.Add("SuitableYearDown", data.SuitableYearDown);
            ht.Add("CourseHot", data.CourseHot);
            ht.Add("CourseSort", data.CourseSort);
            ht.Add("AuthorID", data.AuthorID);
            ht.Add("OperatorID", data.OperatorID);
            ht.Add("OperateTime", data.OperateTime);
            ht.Add("Recommend", data.Recommend);
            ht.Add("TeacherIntroduction", data.TeacherIntroduction);
            ht.Add("ChapterPoints", data.ChapterPoints);
            ht.Add("HospId", data.HospId);
            ht.Add("HospdepId", data.HospdepId);
            ExecuteInsert(stmtId, ht);

        }

        public int Delete(string CourseID)
        {
            try
            {
                String stmtId = "Course_Delete";
                ExecuteDelete(stmtId, CourseID);
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }
         

        }


        public void CourseUpdate(Course data)
        {
            String stmtId = "Course_Update";
            Hashtable ht = new Hashtable();

            ht.Add("CourseID", data.CourseID);
            ht.Add("CourseType", data.CourseType);
            ht.Add("CourseName", data.CourseName);
            ht.Add("CourseTeacher", data.CourseTeacher);
            ht.Add("CourseIntro", data.CourseIntro);
            ht.Add("CourseDuration", data.CourseDuration);
            ht.Add("CourseLevel", data.CourseLevel);
            ht.Add("CourseTag", data.CourseTag);
            ht.Add("CourseThumb", data.CourseThumb);
            ht.Add("SuitableDuty", data.SuitableDuty);
            ht.Add("SuitableYearUp", data.SuitableYearUp);
            ht.Add("SuitableYearDown", data.SuitableYearDown);
            ht.Add("CourseHot", data.CourseHot);
            ht.Add("CourseSort", data.CourseSort);
            ht.Add("AuthorID", data.AuthorID);
            ht.Add("OperatorID", data.OperatorID);
            ht.Add("OperateTime", data.OperateTime);
            ht.Add("Recommend", data.Recommend);
            ht.Add("TeacherIntroduction", data.TeacherIntroduction);
            ht.Add("ChapterPoints", data.ChapterPoints);
            ht.Add("HospId", data.HospId);
            ht.Add("HospdepId", data.HospdepId);
            ExecuteUpdate(stmtId, ht);
        }

        public Course CourseFindByCourseID(string CourseID)
        {
            String stmtId = "Course_Find";

            Hashtable ht = new Hashtable();
            ht.Add("CourseID", CourseID);
            Course result = ExecuteQueryForObject<Course>(stmtId, ht);
            return result;
        }



        public IList<Course> CourseFindAll()
        {
            String stmtId = "Course_FindAll";
            IList<Course> result = ExecuteQueryForList<Course>(stmtId, null);
            return result;
        }


        public IList<Course> CourseFindByUserID(String UserID)
        {
            String stmtId = "Course_FindByUserID";
            Hashtable ht = new Hashtable();
            ht.Add("UserID", UserID);
            IList<Course> result = ExecuteQueryForList<Course>(stmtId, ht);
            return result;
        }

        

        public IList<Course> CourseFindAllByData() 
        {
            String stmtId = "Course_FindAll";
            IList<Course> result = ExecuteQueryForList<Course>(stmtId, null);
            return result;
        }

        public IList<Course> CourseFindOrderByRecommend(int Number)
        {
            String stmtId = "Course_FindOrderByRecommend";
            Hashtable ht = new Hashtable();
            ht.Add("Number", Number);
            IList<Course> result = ExecuteQueryForList<Course>(stmtId, ht);
            return result;
        }


        public IList<Course> CourseFindOrderByCourseHot(int Number)
        {
            String stmtId = "Course_FindOrderByCourseHot";
            Hashtable ht = new Hashtable();
            ht.Add("Number", Number);
            IList<Course> result = ExecuteQueryForList<Course>(stmtId, ht);
            return result;
        }

        public IList<Course> CourseFindOrderByNewTime(int Number)
        {
            String stmtId = "Course_FindOrderByNewTime";
            Hashtable ht = new Hashtable();
            ht.Add("Number", Number);
            IList<Course> result = ExecuteQueryForList<Course>(stmtId, ht);
            return result;
        }



        public IList<Course> CourseFindPaging(int pageno, int pageSize)
        {
            String stmtId = "Course_FindPaging";
            Hashtable ht = new Hashtable();
            ht.Add("pageno", (pageno - 1) * pageSize);
            ht.Add("pageSize", pageSize);

            IList<Course> result = ExecuteQueryForList<Course>(stmtId, ht);
            return result;
        }

        public IList<Course> CourseFindPagingCourseType(int pageno, int pageSize, string courseType,string Data)
        {
            String stmtId = "Course_FindPagingCourseType";
            Hashtable ht = new Hashtable();


            ht.Add("CourseType", Data);
            ht.Add("pageno", (pageno - 1) * pageSize);
            ht.Add("pageSize", pageSize);

            IList<Course> result = ExecuteQueryForList<Course>(stmtId, ht);
            return result;
        }

        public IList<Course> CourseFindPagingNewTime(int pageno, int pageSize, string NewTime)
        {
            String stmtId = "Course_FindPagingNewTime";
            Hashtable ht = new Hashtable();


            ht.Add("CourseType", NewTime);
            ht.Add("pageno", (pageno - 1) * pageSize);
            ht.Add("pageSize", pageSize);

            IList<Course> result = ExecuteQueryForList<Course>(stmtId, ht);
            return result;
        }


        public IList<Course> CourseFindPagingNewTimeDesc(int pageno, int pageSize, string NewTime)
        {
            String stmtId = "Course_FindPagingNewTimeDesc";
            Hashtable ht = new Hashtable();


            ht.Add("CourseType", NewTime);
            ht.Add("pageno", (pageno - 1) * pageSize);
            ht.Add("pageSize", pageSize);

            IList<Course> result = ExecuteQueryForList<Course>(stmtId, ht);
            return result;
        }


        public IList<Course> CourseFindPagingHISID(int pageno, int pageSize, string HISID)
        {
            String stmtId = "Course_FindPagingCourseType";
            Hashtable ht = new Hashtable();


            ht.Add("CourseType", HISID);
            ht.Add("pageno", (pageno - 1) * pageSize);
            ht.Add("pageSize", pageSize);

            IList<Course> result = ExecuteQueryForList<Course>(stmtId, ht);
            return result;
        }

        
        public Course CourseFindOrderByCourseID(string CourseID)
        {
            String stmtId = "Course_FindOrderByCourseID";
            Hashtable ht = new Hashtable();
            ht.Add("CourseID", CourseID);
            Course result = ExecuteQueryForObject<Course>(stmtId, ht);
            return result;
        }

    }
}
