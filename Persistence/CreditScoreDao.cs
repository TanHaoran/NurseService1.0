using Aersysm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Persistence {
    public class CreditScoreDao : BaseSqlMapDao {
        public CreditScoreDao() {
            //
            // TODO: 此处添加countrySqlMapDao的构造函数
            //
        }

        /// <summary>
        /// 得到列表
        /// </summary>
        public IList<CreditScore> GetCreditScoreList() {
            return ExecuteQueryForList<CreditScore>("BaseSelectCreditScore", null);
        }

        public IList<CreditScore> GetCreditScoreDetail(string staffId) {
            return ExecuteQueryForList<CreditScore>("SelectCreditScore", staffId);
        }
    }
}
