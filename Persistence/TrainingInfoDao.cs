using Aersysm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Persistence {
    public partial class TrainingInfoDao : BaseSqlMapDao {

        public TrainingInfoDao() {
            //
            // TODO: 此处添加countrySqlMapDao的构造函数
            //
        }

        /// <summary>
        /// 得到列表
        /// </summary>
        public IList<TrainingInfo> GetTrainingInfoList() {
            return ExecuteQueryForList<TrainingInfo>("BaseSelectTrainingInfo", null);
        }

        public TrainingInfo GetTrainingInfoDetail(string trainingId) {
            return ExecuteQueryForObject<TrainingInfo>("SelectTrainingInfo", trainingId);
        }
    }
}
