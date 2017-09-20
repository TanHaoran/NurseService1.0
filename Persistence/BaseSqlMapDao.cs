using IBatisNet.Common.Pagination;
using IBatisNet.DataAccess.Exceptions;
using IBatisNet.DataAccess.Interfaces;
using IBatisNet.DataMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Untility.IBatisNet;
using System.Data;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.DataMapper.Scope;


namespace Aersysm.Persistence
{
    public class BaseSqlMapDao : IDao
    {

        private ISqlMapper sqlMap = SqlMapperHelper.Instance.SqlMapper;

        public BaseSqlMapDao()
        {

        }

        public ISqlMapper SqlMap
        {
            get
            {
                return sqlMap;
            }
        }

        protected const int PAGE_SIZE = 4;

        //protected ISqlMapper GetLocalSqlMap()
        //{
        //    IDaoManager daoManager = DaoManager.GetInstance(this);
        //    SqlMapDaoSession sqlMapDaoSession = (SqlMapDaoSession)daoManager.LocalDaoSession;

        //    return sqlMapDaoSession.SqlMap;
        //}

        protected IList ExecuteQueryForList(string statementName, object parameterObject)
        {
            //ISqlMapper sqlMap = GetLocalSqlMap();
            try
            {
                return sqlMap.QueryForList(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }

        protected IList ExecuteQueryForList(string statementName, object parameterObject, int skipResults, int maxResults)
        {
            //ISqlMapper sqlMap = GetLocalSqlMap();
            try
            {
                return sqlMap.QueryForList(statementName, parameterObject, skipResults, maxResults);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }

        protected IPaginatedList ExecuteQueryForPaginatedList(string statementName, object parameterObject, int pageSize)
        {
            //ISqlMapper sqlMap = GetLocalSqlMap();
            try
            {
                return sqlMap.QueryForPaginatedList(statementName, parameterObject, pageSize);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error executing query '" + statementName + "' for paginated list.  Cause: " + e.Message, e);
            }
        }

        protected object ExecuteQueryForObject(string statementName, object parameterObject)
        {
            //ISqlMapper sqlMap = GetLocalSqlMap();

            try
            {
                return sqlMap.QueryForObject(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error executing query '" + statementName + "' for object.  Cause: " + e.Message, e);
            }
        }
        protected object ExecuteQueryForObject(string statementName, object parameterObject, object resultObject)
        {
            //ISqlMapper sqlMap = GetLocalSqlMap();

            try
            {
                return sqlMap.QueryForObject(statementName, parameterObject, resultObject);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error executing query '" + statementName + "' for object.  Cause: " + e.Message, e);
            }
        }
        protected IList<T> ExecuteQueryForList<T>(string statementName, object parameterObject)
        {
            //ISqlMapper sqlMap = GetLocalSqlMap();
            try
            {
                return sqlMap.QueryForList<T>(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }

        protected IList<T> ExecuteQueryForList<T>(string statementName, object parameterObject, int skipResults, int maxResults)
        {
            //ISqlMapper sqlMap = GetLocalSqlMap();
            try
            {
                return sqlMap.QueryForList<T>(statementName, parameterObject, skipResults, maxResults);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error executing query '" + statementName + "' for list.  Cause: " + e.Message, e);
            }
        }

        protected T ExecuteQueryForObject<T>(string statementName, object parameterObject)
        {
            //ISqlMapper sqlMap = GetLocalSqlMap();

            try
            {
                return sqlMap.QueryForObject<T>(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error executing query '" + statementName + "' for object.  Cause: " + e.Message, e);
            }
        }
        protected T ExecuteQueryForObject<T>(string statementName, object parameterObject, T resultObject)
        {
            //ISqlMapper sqlMap = GetLocalSqlMap();

            try
            {
                return sqlMap.QueryForObject<T>(statementName, parameterObject, resultObject);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error executing query '" + statementName + "' for object.  Cause: " + e.Message, e);
            }
        }
        protected object ExecuteInsert(string statementName, object parameterObject)
        {
            //ISqlMapper sqlMap = GetLocalSqlMap();
            try
            {
                return sqlMap.Insert(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error executing query '" + statementName + "' for insert.  Cause: " + e.Message, e);
            }
        }

        protected int ExecuteUpdate(string statementName, object parameterObject)
        {
            //ISqlMapper sqlMap = GetLocalSqlMap();

            try
            {
                return sqlMap.Update(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error executing query '" + statementName + "' for update.  Cause: " + e.Message, e);
            }
        }

        protected object ExecuteDelete(string statementName, object parameterObject)
        {
            //ISqlMapper sqlMap = GetLocalSqlMap();

            try
            {
                return sqlMap.Delete(statementName, parameterObject);
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error executing query '" + statementName + "' for delete.  Cause: " + e.Message, e);
            }
        }


        protected DataSet ExecutQueryForDataSet(string statementName, object paramObject)
        {
            try
            {
                DataSet ds = new DataSet();
                //sqlMap.GetMappedStatement
                //ISqlMapper mapper = GetLocalSqlMap();
                IMappedStatement statement = sqlMap.GetMappedStatement(statementName);
                if (!sqlMap.IsSessionStarted)
                {
                    sqlMap.OpenConnection();
                }

                RequestScope scope = statement.Statement.Sql.GetRequestScope(statement, paramObject, sqlMap.LocalSession);
                statement.PreparedCommand.Create(scope, sqlMap.LocalSession, statement.Statement, paramObject);
                sqlMap.LocalSession.CreateDataAdapter(scope.IDbCommand).Fill(ds);

                return ds;

            }
            catch (Exception ex)
            {

                throw;
            }

        }



        protected DataSet QueryDataSet(string Sql)
        {
            try
            {
                
                String mysqlStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                MySql.Data.MySqlClient.MySqlConnection mysqlcon = new MySql.Data.MySqlClient.MySqlConnection(mysqlStr);
                MySql.Data.MySqlClient.MySqlDataAdapter mysqlad = new MySql.Data.MySqlClient.MySqlDataAdapter(Sql, mysqlcon);
                MySql.Data.MySqlClient.MySqlCommandBuilder sb1 = new MySql.Data.MySqlClient.MySqlCommandBuilder(mysqlad);
                DataSet ds = new DataSet();
                mysqlad.Fill(ds);
                return ds;
                

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        protected DataSet QueryDataSetParam(string Sql, MySql.Data.MySqlClient.MySqlParameter[] para)
        {
            try
            {

                String mysqlStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                MySql.Data.MySqlClient.MySqlConnection mysqlcon = new MySql.Data.MySqlClient.MySqlConnection();
                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand(Sql, mysqlcon);
                mySqlCommand.Parameters.AddRange(para);
                MySql.Data.MySqlClient.MySqlDataAdapter mysqlad = new MySql.Data.MySqlClient.MySqlDataAdapter(Sql, mysqlcon);
                MySql.Data.MySqlClient.MySqlCommandBuilder sb1 = new MySql.Data.MySqlClient.MySqlCommandBuilder(mysqlad);
                DataSet ds = new DataSet();
                mysqlad.Fill(ds);
                return ds;


            }
            catch (Exception ex)
            {

                throw;
            }

        }

        protected int ExecSQLParam(string Sql, MySql.Data.MySqlClient.MySqlParameter [] para)
        {
            String mysqlStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            MySql.Data.MySqlClient.MySqlConnection mysqlcon = new MySql.Data.MySqlClient.MySqlConnection(mysqlStr);
            mysqlcon.Open();
            try
            {


                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand(Sql, mysqlcon);
                mySqlCommand.Parameters.AddRange(para);
                return mySqlCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                mysqlcon.Clone();
            }

        }
        protected int ExecSQL(string Sql)
        {
            String mysqlStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            MySql.Data.MySqlClient.MySqlConnection mysqlcon = new MySql.Data.MySqlClient.MySqlConnection(mysqlStr);
            mysqlcon.Open();
            try
            {


                MySql.Data.MySqlClient.MySqlCommand mySqlCommand = new MySql.Data.MySqlClient.MySqlCommand(Sql, mysqlcon);
                return mySqlCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                mysqlcon.Clone();
            }

        }
        protected string ExecutDataSetToJson(DataSet ds)
        {


            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 0)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            }

            string str = "";
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    str = "[{";
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (j != ds.Tables[0].Columns.Count - 1)
                        {

                            str += "\"" + ds.Tables[0].Columns[j].ColumnName.ToUpper() + "\":\"" + ds.Tables[0].Rows[0][j].ToString() + "\",";
                        }
                        else
                        {
                            str += "\"" + ds.Tables[0].Columns[j].ColumnName.ToUpper() + "\":\"" + ds.Tables[0].Rows[0][j].ToString() + "\"}";
                        }
                    }
                    str += "]";
                }
                else if (ds.Tables[0].Rows.Count > 1)
                {
                    str = "[";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        str += "{";
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {
                            if (j != ds.Tables[0].Columns.Count - 1)
                            {

                                str += "\"" + ds.Tables[0].Columns[j].ColumnName.ToUpper() + "\":\"" + ds.Tables[0].Rows[i][j].ToString() + "\",";
                            }
                            else
                            {
                                str += "\"" + ds.Tables[0].Columns[j].ColumnName.ToUpper() + "\":\"" + ds.Tables[0].Rows[i][j].ToString() + "\"}";
                            }
                        }
                        if (i != ds.Tables[0].Rows.Count - 1)
                        {
                            str += ",";
                        }
                    }
                    str += "]";
                }
            }
            return str;

        }
    }
}
