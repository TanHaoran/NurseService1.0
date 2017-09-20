using Aersysm.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aersysm.Persistence
{
  public partial   class aers_tbl_LoginKeySqlMapDao : BaseSqlMapDao
    {
        public void Insert(aers_tbl_LoginKey model)
        {
            model.LKId = new aers_sys_seedSqlMapDao().GetMaxID("aers_tbl_LoginKey");
            String stmtId = "aers_tbl_LoginKey_Insert"; // Message_Insert
            ExecuteInsert(stmtId, model);
        }

        public void Update(aers_tbl_LoginKey model)
        {
            String stmtId = "aers_tbl_LoginKey_Update";
            ExecuteUpdate(stmtId, model);
        }

        public void Delete(string ID)
        {
            String stmtId = "aers_tbl_LoginKey_Delete";
            ExecuteDelete(stmtId, ID);
        }

        public IList<aers_tbl_LoginKey> FindAll()
        {
            String stmtId = "aers_tbl_LoginKey_FindAll";
            IList<aers_tbl_LoginKey> result = ExecuteQueryForList<aers_tbl_LoginKey>(stmtId, null);
            return result;
        }
        public void UpdateStatusByReguserId(string ReguserId)
        {
            String stmtId = "aers_tbl_LoginKey_UpdateStatus";
            Hashtable ht = new Hashtable();
            ht.Add("ReguserId", ReguserId);
            ExecuteUpdate(stmtId, ht);

        }

        public aers_tbl_LoginKey FindLoginKeyByReuId(string ReguserId)
        {
            String stmtId = "aers_tbl_LoginKey_FindLoginKeyByReuId";
            Hashtable ht = new Hashtable();
            ht.Add("ReguserId", ReguserId);
            aers_tbl_LoginKey result = ExecuteQueryForObject<aers_tbl_LoginKey>(stmtId, ht);
            return result;
        }

    }
}
