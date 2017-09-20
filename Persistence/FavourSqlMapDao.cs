using Aersysm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aersysm.Persistence
{
   public  class FavourSqlMapDao : BaseSqlMapDao
    {
        public void Insert(Favour model)
        {
            model.ID  = new aers_sys_seedSqlMapDao().GetMaxID("Favor");   //2017.6.23 YM单词能不能统一起来  shi
            String stmtId = "Favour_Insert"; // Message_Insert
            ExecuteInsert(stmtId, model);
        }

        public void Update(Favour model)
        {
            String stmtId = "Favour_Update";
            ExecuteUpdate(stmtId, model);
        }

        public void Delete(string ID)
        {
            String stmtId = "Favour_Delete";
            ExecuteDelete(stmtId, ID);
        }

        public IList<Favour> FindAll()
        {
            String stmtId = "Favour_FindAll";
            IList<Favour> result = ExecuteQueryForList<Favour>(stmtId, null);
            return result;
        }
    }
}
