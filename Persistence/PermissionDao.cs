using Aersysm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Persistence {
    public partial class PermissionDao : BaseSqlMapDao {
        public PermissionDao() {

        }

        /// <summary>
        /// 得到权限列表
        /// </summary>
        public IList<Permission> GetPermissionList() {
            return ExecuteQueryForList<Permission>("BaseSelectPermission", null);
        }

    }
}
