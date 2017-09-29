using Aersysm.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aersysm.Persistence
{
    public partial  class AdmdepartmentSqlMapDao : BaseSqlMapDao
    {
        public AdmdepartmentSqlMapDao()
        {
            //
            // TODO: 此处添加userregisterSqlMapDao的构造函数
            //
        }

        /// <summary>
        /// 得到列表
        /// </summary>
        public IList<Admdepartment> GetAdmdepartmentList()
        {            
            return ExecuteQueryForList<Admdepartment>("SelectAdmdepartment", null);
        }

        public IList<Admdepartment> GetAdmDepartmentListByAdminId(string adminId) {
            return ExecuteQueryForList<Admdepartment>("SelectAdmdepartmentByAdminId", adminId);
        }

        /// <summary>
        /// 新建
        /// </summary>
        public void AddAdmdepartment(Admdepartment admdepartment)
        {

            //  userregister.RegisterId = new aers_sys_seedSqlMapDao().GetMaxID("userregister");
            //  Insertuserregister
            ExecuteInsert("InsertAdmdepartment", admdepartment);
        }
        /// <summary>
        /// 修改
        /// </summary>
        public void UpdateAdmdepartment(Admdepartment admdepartment)
        {
            ExecuteUpdate("UpdateAdmdepartment", admdepartment);
        }

        /// <summary>
        /// 得到明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Admdepartment GetAdmdepartmentDetail(System.String Id)
        {
            return ExecuteQueryForObject<Admdepartment>("SelectAdmdepartment", Id);
        }



        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAdmdepartment(System.String Id)
        {
            ExecuteDelete("DeleteAdmdepartment", Id);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void DeleteAdmdepartmentByAdminId(System.String adminId) {
            ExecuteDelete("DeleteAdmdepartmentByAdminId", adminId);
        }
    }
}
