using Aersysm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Persistence {
    public partial class AdministratorSqlMapDao : BaseSqlMapDao {
        public AdministratorSqlMapDao() {
            //
            // TODO: 此处添加userregisterSqlMapDao的构造函数
            //
        }

        /// <summary>
        /// 得到列表
        /// </summary>
        public IList<Administrator> GetAdministratorList() {
            return ExecuteQueryForList<Administrator>("SelectAdministrator", null);
        }

        /// <summary>
        /// 新建
        /// </summary>
        public void AddAdministrator(Administrator administrator) {

            //  userregister.RegisterId = new aers_sys_seedSqlMapDao().GetMaxID("userregister");
            //  Insertuserregister
            ExecuteInsert("InsertAdministrator", administrator);
        }
        /// <summary>
        /// 修改
        /// </summary>
        public void UpdateAdministrator(Administrator administrator) {
            ExecuteUpdate("UpdateAdministrator", administrator);
        }

        /// <summary>
        /// 得到明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Administrator GetAdministratorDetail(System.String AdmId) {
            return ExecuteQueryForObject<Administrator>("SelectAdministrator", AdmId);
        }

        /// <summary>
        /// 根据用户名获取管理员信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Administrator GetAdministratorDetailByUserId(System.String userId) {
            return ExecuteQueryForObject<Administrator>("SelectAdministratorByUserId", userId);
        }



        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAdministrator(System.String AdmId) {
            ExecuteDelete("DeleteAdministrator", AdmId);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void UpdateDelAdministrator(System.String AdmId) {
            ExecuteDelete("DeleteDelAdministrator", AdmId);
        }
    }
}
