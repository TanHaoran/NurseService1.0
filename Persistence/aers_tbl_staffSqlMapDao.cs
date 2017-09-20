/**
  * Name:Aersysm.SqlMapDao-aers_tbl_staff
  * Author: banshine
  * Description: aers_tbl_staff Dao层 
  * Date: 2015-6-8 11:13:02
  * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aersysm.Domain;
using System.Collections;
using System.Data;

namespace Aersysm.Persistence
{
    public partial class aers_tbl_staffSqlMapDao: BaseSqlMapDao
    {

        public DataSet StaffFindAllByGrade(int Number)
        {
            String stmtId = "Staff_FindAllByGrade";
            Hashtable ht = new Hashtable();
            ht.Add("Number", Number);
            DataSet result = ExecutQueryForDataSet(stmtId, ht);
            return result;
        }


        public int Insert(aers_tbl_staff data)
        {
            aers_sys_seedSqlMapDao dal = new aers_sys_seedSqlMapDao();
            data.StaffId = dal.GetMaxID("staff");


            String stmtId = "aers_tbl_staff_Insert";
            Hashtable ht = new Hashtable();

            ht.Add("StaffId", data.StaffId);
            ht.Add("ReguserId", data.ReguserId);
            ht.Add("DepId", data.DepId);
            ht.Add("Name", data.Name);
            ht.Add("RoleState", data.RoleState);
            ht.Add("Sex", data.Sex);
            ht.Add("Position", data.Position);
            ht.Add("Phone", data.Phone);
            ht.Add("Address", data.Address);
            ht.Add("IDNumber", data.IDNumber);
            ht.Add("IsFlag", data.IsFlag);
            ht.Add("Remarks", data.Remarks);
            ht.Add("OperatorId", data.OperatorId);
            ht.Add("OperatorDate", data.OperatorDate);
            ht.Add("HeadImg", data.HeadImg);
            try
            {
                ExecuteInsert(stmtId, ht);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public int Delete(string ReguserId)
        {

            String stmtId = "aers_tbl_staff_Delete";
            Hashtable ht = new Hashtable();
            ht.Add("StaffId", ReguserId);
            try
            {
                ExecuteDelete(stmtId, ht);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public int Update(aers_tbl_staff data)
        {
            String stmtId = "aers_tbl_staff_Update";
            Hashtable ht = new Hashtable();

      
            ht.Add("DepId", data.DepId);
            ht.Add("Name", data.Name);
            ht.Add("RoleState", data.RoleState);
            ht.Add("Sex", data.Sex);
            ht.Add("Position", data.Position);
            ht.Add("Phone", data.Phone);
            ht.Add("Address", data.Address);
            ht.Add("IDNumber", data.IDNumber);
            ht.Add("IsFlag", data.IsFlag);
            ht.Add("Remarks", data.Remarks);
            ht.Add("OperatorId", data.OperatorId);
            ht.Add("OperatorDate", data.OperatorDate);
            ht.Add("ReguserId", data.ReguserId);
            ht.Add("HeadImg", data.HeadImg);
            int i = ExecuteUpdate(stmtId, ht);
            return i;
        }

        public IList<aers_tbl_staff> staffFindAll()
        {
            String stmtId = "aers_tbl_staff_FindAll";
            IList<aers_tbl_staff> result = ExecuteQueryForList<aers_tbl_staff>(stmtId, null);
            return result;

        }

        public aers_tbl_staff staffFindByStaffId(string StaffId)
        {
            String stmtId = "aers_tbl_staff_Find";
            Hashtable ht = new Hashtable();
            ht.Add("StaffId", StaffId);
            aers_tbl_staff result = ExecuteQueryForObject<aers_tbl_staff>(stmtId, ht);
            return result;

        }
        

        #region 根据注册用户编码查询注册用户信息
        /// <summary>
        /// 根据注册用户编码查询注册用户信息
        /// </summary>
        /// <param name="reguserId">注册用户编码</param>
        /// <returns>注册用户信息实体</returns>
        public aers_tbl_staff FindByRUid(string ruid)
        {
            String stmtId = "aers_tbl_staff_FindByReguserId";
            Hashtable ht = new Hashtable();
            ht.Add("ReguserId", ruid);
            aers_tbl_staff result = ExecuteQueryForObject<aers_tbl_staff>(stmtId, ht);
            return result;
        }
        #endregion

        #region 根据注册编码查询用户姓名
        /// <summary>
        /// 根据注册编码查询用户姓名
        /// </summary>
        /// <param name="ruid">注册用户编码</param>
        /// <returns></returns>
        public string FindNameByRid(string ruid) 
        {
            try
            {
                return FindByRUid(ruid).Name;
            }
            catch (Exception)
            {

                return "";
            }
           
        }
        #endregion

        #region 根据注册编码查询用户
        /// <summary> 
        /// 根据注册编码查询用户
        /// </summary>
        /// <param name="ruid">注册用户编码</param>
        /// <returns></returns>
        public aers_tbl_staff FindStaffByRid(string ruid)
        {
            return FindByRUid(ruid);
        }
        #endregion

        #region 2017.6.17 YM根据手机号查用户
        public aers_tbl_staff FindByPhoneNumber(string PhoneNumber)
        {
            String stmtId = "aers_tbl_staff_FindByPhoneNumber";
            Hashtable ht = new Hashtable();
            ht.Add("Phone", PhoneNumber);
            aers_tbl_staff result = ExecuteQueryForObject<aers_tbl_staff>(stmtId, ht);
            return result;
        }
        #endregion
    }
    
}

