using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Domain;
using Aersysm.Persistence;

namespace Aersysm.Persistence
{
    public class CreditRecordSqlMapDao : BaseSqlMapDao
    {
        public int CreditRecordInsert(CreditRecord hospital)
        {

            try
            {
                if (CreditRecordFind(hospital.UserID, hospital.TrainingID) == null)
                {
                    String stmtId = "CreditRecord_Insert";
                    hospital.RecordID = new aers_sys_seedSqlMapDao().GetMaxID("CreditRecord");
                    ExecuteInsert(stmtId, hospital);
                }

                return 1;
            }
            catch (Exception ex)
            {

                return 0;
            }


        }

        public void CreditRecordDelete(string RecordID)
        {
            String stmtId = "CreditRecord_Delete";
            ExecuteDelete(stmtId, RecordID);

        }


        public void IntegralUpdate(CreditScore integral)
        {
            String stmtId = "CreditRecord_Update";
            ExecuteUpdate(stmtId, integral);

        }

        public CreditScore CreditRecordFind(string UserID, string TrainingID)
        {
            try
            {
                String stmtId = "CreditRecord_Find";

                Hashtable ht = new Hashtable();
                ht.Add("UserID", UserID);
                ht.Add("TrainingID", TrainingID);
                CreditScore result = ExecuteQueryForObject<CreditScore>(stmtId, ht);
                return result;
            }
            catch (Exception ex)
            {

                return null;
            }

          
        }


        public IList<CreditScore> CreditRecordFindByUserID(string UserID)
        {
            String stmtId = "CreditRecord_FindByUserID";

            Hashtable ht = new Hashtable();
            ht.Add("UserID", UserID);
            IList<CreditScore> result = ExecuteQueryForList<CreditScore>(stmtId, ht);
            return result;
        }


        public IList<Integral> IntegralFindAll()
        {
            String stmtId = "CreditRecord_FindAll";
            IList<Integral> result = ExecuteQueryForList<Integral>(stmtId, null);
            return result;
        }
    }
}
