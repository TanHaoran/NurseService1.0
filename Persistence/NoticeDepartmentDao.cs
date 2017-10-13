using Aersysm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Persistence {
    public partial class NoticeDepartmentDao : BaseSqlMapDao {


        public NoticeDepartmentDao() {
            //
            // TODO: 此处添加bannerSqlMapDao的构造函数
            //
        }

        /// <summary>
        /// 得到列表
        /// </summary>
        public IList<NoticeDepartment> GetNoticeDepartmentList() {
            return ExecuteQueryForList<NoticeDepartment>("BaseSelectNoticeDepartment", null);
        }

        /// <summary>
        /// 根据科室Id得到列表
        /// </summary>
        public IList<NoticeDepartment> GetNoticeDepartmentByNoticeId(string noticeId) {
            return ExecuteQueryForList<NoticeDepartment>("SelectNoticeDepartmentByNoticeId", noticeId);
        }       

        /// <summary>
        /// 新建
        /// </summary>
        public void AddNoticeDepartment(NoticeDepartment nd) {
            ExecuteInsert("InsertNoticeDepartment", nd);
        }
    }
}
