using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Domain;
using System.Collections;

namespace Aersysm.Persistence
{
    public  class MessageSqlMapDao : BaseSqlMapDao
    {
        public void  Insert(Message model)
        {
            model.MessageID = new aers_sys_seedSqlMapDao().GetMaxID("Message");
            String stmtId = "Message_Insert"; // Message_Insert
            ExecuteInsert(stmtId, model);
        }

        public void Update(Message model)
        {
            String stmtId = "Message_Update";
            ExecuteUpdate(stmtId, model);
        }

        public void Delete(string ID)
        {
            String stmtId = "Message_Delete";
            ExecuteDelete(stmtId, ID);
        }

        public IList<Message> FindAll()
        {
            String stmtId = "Message_FindAll";
            IList<Message> result = ExecuteQueryForList<Message>(stmtId, null);
            return result;
        }

        public void UpdateEndTime(string MessageID)
        {
            ExecSQL("Update Message set EndTime='"+DateTime .Now+"' where MessageID='"+MessageID+"'" );
        }
    }
}
