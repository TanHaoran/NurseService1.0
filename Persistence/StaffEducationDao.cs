using Aersysm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Persistence {
    public partial class StaffEducationDao : BaseSqlMapDao {

        public StaffEducationDao() {
            //
            // TODO: 此处添加countrySqlMapDao的构造函数
            //
        }

        /// <summary>
        /// 得到列表
        /// </summary>
        public IList<StaffEducation> GetTrainingInfoList() {
            return ExecuteQueryForList<StaffEducation>("BaseSelectStaffEducation", null);
        }

        public IList<StaffEducation> GetTrainingInfoDetail(string staffId) {
            return ExecuteQueryForList<StaffEducation>("SelectStaffEducation", staffId);
        }


    }
}
