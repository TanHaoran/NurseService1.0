using Aersysm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Persistence {
    public partial class NoticeNurseDao : BaseSqlMapDao {


        public NoticeNurseDao() {
            //
            // TODO: 此处添加bannerSqlMapDao的构造函数
            //
        }

        /// <summary>
        /// 得到列表
        /// </summary>
        public IList<NoticeNurse> GetNoticeDepartmentList() {
            return ExecuteQueryForList<NoticeNurse>("BaseSelectNoticeNurse", null);
        }

        /// <summary>
        /// 根据公告Id得到列表
        /// </summary>
        public IList<NoticeNurse> GetNoticeDepartmentByNoticeId(string noticeId) {
            return ExecuteQueryForList<NoticeNurse>("SelectNoticeNurseByNoticeId", noticeId);
        }

        /// <summary>
        /// 新建
        /// </summary>
        public void AddNoticeDepartment(NoticeNurse nd) {
            ExecuteInsert("InsertNoticeNurse", nd);
        }
    }
}
