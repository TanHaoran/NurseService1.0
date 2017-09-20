using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Domain;
using System.Collections;

namespace Aersysm.Persistence
{
    public partial class SMSMessageSqlMapDao : BaseSqlMapDao
    {
        public void Insert(SMSMessage model)
        {
            model.SMSID = new aers_sys_seedSqlMapDao().GetMaxID("SMSMessage");
            String stmtId = "SMSMessage_Insert"; // Message_Insert
            ExecuteInsert(stmtId, model);
        }

        public void Update(SMSMessage model)
        {
            String stmtId = "SMSMessage_Update";
            ExecuteUpdate(stmtId, model);
        }

        public void Delete(string ID)
        {
            String stmtId = "SMSMessage_Delete";
            ExecuteDelete(stmtId, ID);
        }

        public IList<SMSMessage> FindAll()
        {
            String stmtId = "SMSMessage_FindAll";
            IList<SMSMessage> result = ExecuteQueryForList<SMSMessage>(stmtId, null);
            return result;
        }

        public IList<SMSMessage> FindByPhoneNumber(string PhoneNumber)
        {
            String stmtId = "SMSMessage_FindByPhoneNumber";
            Hashtable ht = new Hashtable();
            ht.Add("PhoneNumber", PhoneNumber);
            IList<SMSMessage> result = ExecuteQueryForList<SMSMessage>(stmtId, ht);
            return result;
        }
    }
}
