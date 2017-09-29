using Aersysm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Persistence
{
  public partial   class AdmpermissionSqlMapDao : BaseSqlMapDao
    {
        public AdmpermissionSqlMapDao()
        {
            //
            // TODO: 此处添加userregisterSqlMapDao的构造函数
            //
        }

        /// <summary>
        /// 得到列表
        /// </summary>
        public IList<Admpermission> GetAdmpermissionList()
        {            
            return ExecuteQueryForList<Admpermission>("SelectAdmpermission", null);
        }

        public IList<Admpermission> GetPermissionByAdminId(string adminId) {
            return ExecuteQueryForList<Admpermission>("SelectPermissionByAdmId", adminId);
        }

        /// <summary>
        /// 新建
        /// </summary>
        public void AddAdmpermission(Admpermission admpermission)
        {

            //  userregister.RegisterId = new aers_sys_seedSqlMapDao().GetMaxID("userregister");
            //  Insertuserregister
            ExecuteInsert("InsertAdmpermission", admpermission);
        }
        /// <summary>
        /// 修改
        /// </summary>
        public void UpdateAdmpermission(Admpermission admpermission)
        {
            ExecuteUpdate("UpdateAdmpermission", admpermission);
        }

        /// <summary>
        /// 得到明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Admpermission GetAdmpermissionDetail(System.String Id)
        {
            return ExecuteQueryForObject<Admpermission>("SelectAdmpermission", Id);
        }



        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAdmpermission(System.String Id)
        {
            ExecuteDelete("DeleteAdmpermission", Id);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAdmpermissionByAdminId(System.String adminId) {
            ExecuteDelete("DeleteAdmpermissionByAdmId", adminId);
        }
    }
}
