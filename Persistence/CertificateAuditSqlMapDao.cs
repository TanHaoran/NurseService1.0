using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Domain;
using System.Collections;

namespace Aersysm.Persistence
{
  public partial  class CertificateAuditSqlMapDao : BaseSqlMapDao
    {
        public void Insert(CertificateAudit model)
        {
            model.AuditID  = new aers_sys_seedSqlMapDao().GetMaxID("CertificateAudit");
            String stmtId = "CertificateAudit_Insert"; // Message_Insert
            ExecuteInsert(stmtId, model);
        }

        public void Update(CertificateAudit model)
        {
            String stmtId = "CertificateAudit_Update";
            ExecuteUpdate(stmtId, model);
        }

        public void Delete(string ID)
        {
            String stmtId = "CertificateAudit_Delete";
            ExecuteDelete(stmtId, ID);
        }

        public IList<CertificateAudit> FindAll()
        {
            String stmtId = "CertificateAudit_FindAll";
            IList<CertificateAudit> result = ExecuteQueryForList<CertificateAudit>(stmtId, null);
            return result;
        }

        public IList<CertificateAudit> CertificateAuditByReguserID(string ReguserID)
        {
            String stmtId = "CertificateAudit_ByReguserID";

            Hashtable ht = new Hashtable();
            ht.Add("ReguserID", ReguserID);
            IList<CertificateAudit> result = ExecuteQueryForList<CertificateAudit>(stmtId, ht);
            return result;
        }
        public int CertificateAuditUpdateByReguserID(string ReguserID,int CertificateType)
        {
            try
            {
                String stmtId = "CertificateAudit_UpdateByReguserID";

                Hashtable ht = new Hashtable();
                ht.Add("ReguserID", ReguserID);
                ht.Add("CertificateType", CertificateType);
                ExecuteQueryForObject<CertificateAudit>(stmtId, ht);
                return 1;
            }
            catch (Exception)
            {

                return 0;
            }
        }
        
    }
}
