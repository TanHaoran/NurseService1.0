using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Aersysm.Persistence
{
    public class aers_tbl_eventsresumeDAL : BaseSqlMapDao
    {

        public DataSet GetEventsresumeByhospital()
        {

            string sql = "select a.HospId,b.HospName,COUNT(*) as Count from aers_tbl_eventsresume a LEFT JOIN aers_tbl_hospital b on a.HospId=b.HospId  GROUP BY a.HospId,b.HospName";
            DataSet ds = this.QueryDataSet(sql);
            return ds;
        }
    }
}
