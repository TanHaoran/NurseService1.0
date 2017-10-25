using Aersysm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Persistence {
    public class CreditStaffDao : BaseSqlMapDao {

        public CreditStaffDao() {
            //
            // TODO: 此处添加countrySqlMapDao的构造函数
            //
        }

        public CreditStaff GetCreditStaffDetail(string hospitalId, string loginName, string password) {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("hospitalId", hospitalId);
            dic.Add("loginName", loginName);
            dic.Add("password", password);
            return ExecuteQueryForObject<CreditStaff>("SelectCreditStaff", dic);
        }
    }
}
