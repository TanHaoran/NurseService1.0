using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Aersysm.Persistence
{
    public  class TrainDAL : BaseSqlMapDao
    {

        public int AddFavor(int TypeID,string UserID,string MainID)
        {

            aers_sys_seedSqlMapDao dal = new aers_sys_seedSqlMapDao();
            string sql = "insert into  Favour values (@ID,@TypeID,@UserID,@MainID,@OperatorDate)";

            MySqlParameter[] para = new MySqlParameter[]
            {
                new MySqlParameter("@ID",dal.GetMaxID("Favor")),
                new MySqlParameter("@TypeID",TypeID),
                new MySqlParameter("@UserID",UserID),
                new MySqlParameter("@MainID",MainID),
                new MySqlParameter("@OperatorDate",DateTime.Now)
            };

            return ExecSQLParam(sql, para);
 
        }


        public int AddCourseUser(string UserID, string CourseID)
        {

            aers_sys_seedSqlMapDao dal = new aers_sys_seedSqlMapDao();
            string sql = "insert into  CourseUser values (@ID,@CourseID,@UserID)";

            MySqlParameter[] para = new MySqlParameter[]
            {
                new MySqlParameter("@ID",dal.GetMaxID("CourseUser")),
                 new MySqlParameter("@CourseID",CourseID),
                new MySqlParameter("@UserID",UserID)
               
            };

            return ExecSQLParam(sql, para);

        }


        public DataSet GetFavor(int TypeID, string UserID, string MainID)
        {
            aers_sys_seedSqlMapDao dal = new aers_sys_seedSqlMapDao();
            string sql = "select * from  Favour where  TypeID="+ TypeID + " and UserID='"+ UserID + "' and MainID='"+ MainID + "'";
            return QueryDataSet(sql);

        }


        public DataSet GetCourseUser(string UserID, string CourseID)
        {
            aers_sys_seedSqlMapDao dal = new aers_sys_seedSqlMapDao();
            string sql = "select * from  CourseUser where   UserID='" + UserID + "' and CourseID='" + CourseID + "'";
            return QueryDataSet(sql);
        }


        public DataSet DeleteCourseUser(string UserID, string CourseID)
        {
            aers_sys_seedSqlMapDao dal = new aers_sys_seedSqlMapDao();
            string sql = "delete  from  CourseUser where   UserID='" + UserID + "' and CourseID='" + CourseID + "'";
            return QueryDataSet(sql);
        }

        public int UpdateProblemByQID(string QID)
        {
            string sql = "   update Problem set FavorCount = FavorCount + 1  where QID = @QID";
            MySqlParameter[] para = new MySqlParameter[]
               {

                        new MySqlParameter("@QID",QID)
               };

            return ExecSQLParam(sql, para);
        }


        public int UpdateNotesByNoteID(string NoteID)
        {
            string sql = "   update Notes set FavorCount=FavorCount+1  where NoteID=@NoteID";
            MySqlParameter[] para = new MySqlParameter[]
               {

                        new MySqlParameter("@NoteID",NoteID)
               };

            return ExecSQLParam(sql, para);
        }


        public int ExecSQL(string sql)
        {
            return base.ExecSQL(sql);
        }


    }
}
