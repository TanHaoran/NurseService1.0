using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Aersysm.Persistence
{
    public class AersDAL : BaseSqlMapDao
    {


        public DataSet FindGroupByName(DateTime time1, DateTime time2)
        {
            //select EveycId from aers_tbl_events_yc_parts where EveresName='149'   GROUP BY EveycId  
            aers_sys_seedSqlMapDao dal = new aers_sys_seedSqlMapDao();
            Hashtable ht = new Hashtable();
            string sql = "   select DetailTypeQt,count(*) as ccc from aers_tbl_events_qt   where  HappenedDate>='"+ time1 + "' and  HappenedDate<'"+ time2 + "'  and   DetailType=112  and DetailTypeQt!=''   GROUP BY DetailTypeQt  order by ccc desc ";

            

            return ExecutQueryForDataSet(sql, ht);

        }
    }
}
