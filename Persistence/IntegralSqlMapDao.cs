using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Persistence;

namespace Aersysm.Domain
{
    public class IntegralSqlMapDao : BaseSqlMapDao
    {
        public int IntegralInsert(Integral hospital)
        {

            try
            {
                if (IntegralFind(hospital.UserID, hospital.TrainingID) == null)
                {
                    String stmtId = "Integral_Insert";
                    hospital.RecordID = new aers_sys_seedSqlMapDao().GetMaxID("Integral");
                    ExecuteInsert(stmtId, hospital);
                }

                return 1;
            }
            catch (Exception)
            {

                return 0;
            }

        
        }

        public void IntegralDelete(string RecordID)
        {
            String stmtId = "Integral_Delete";
            ExecuteDelete(stmtId, RecordID);

        }


        public void IntegralUpdate(Integral integral)
        {
            String stmtId = "Integral_Update";
            ExecuteUpdate(stmtId, integral);

        }

        public Integral IntegralFind(string UserID,string TrainingID)
        {
            String stmtId = "Integral_Find";

            Hashtable ht = new Hashtable();
            ht.Add("UserID", UserID);
            ht.Add("TrainingID", TrainingID);
            Integral result = ExecuteQueryForObject<Integral>(stmtId, ht);
            return result;
        }


        public IList<Integral> IntegralFindByUserID(string UserID)
        {
            String stmtId = "Integral_FindByUserID";

            Hashtable ht = new Hashtable();
            ht.Add("UserID", UserID);
            IList<Integral> result = ExecuteQueryForList<Integral>(stmtId, ht);
            return result;
        }


        public IList<Integral> IntegralFindAll()
        {
            String stmtId = "Integral_FindAll";
            IList<Integral> result = ExecuteQueryForList<Integral>(stmtId, null);
            return result;
        }
    }
}
