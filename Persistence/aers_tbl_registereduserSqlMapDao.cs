/**
  * Name:Aersysm.SqlMapDao-aers_tbl_registereduser
  * Author: banshine
  * Description: aers_tbl_registereduser Dao层 
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
    public partial class aers_tbl_registereduserSqlMapDao: BaseSqlMapDao
    {


        

        public string Insert(aers_tbl_registereduser data)
        {
            aers_sys_seedSqlMapDao dal = new aers_sys_seedSqlMapDao();
            data.ReguserId=dal.GetMaxID("registereduser");

            String stmtId = "aers_tbl_registereduser_Insert";
            Hashtable ht = new Hashtable();
            ht.Add("ReguserId", data.ReguserId);
            ht.Add("LoginName", data.LoginName);
            ht.Add("Password", data.Password);
            ht.Add("IsFlag", data.IsFlag);
            ht.Add("Remarks", data.Remarks);
            ht.Add("OperatorId", data.OperatorId);
            ht.Add("OperatorDate", data.OperatorDate);
            try
            {
                ExecuteInsert(stmtId, ht);
                return data.ReguserId;
            }
            catch (Exception ex)
            {

                return "";
            }
        }

        public int Delete(string ReguserId)
        {
         
            String stmtId = "aers_tbl_registereduser_Delete";
            Hashtable ht = new Hashtable();
            ht.Add("ReguserId", ReguserId);
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


        public int Update(aers_tbl_registereduser data)
        {
            String stmtId = "aers_tbl_registereduser_Update";
            Hashtable ht = new Hashtable();
            ht.Add("ReguserId", data.ReguserId);
            ht.Add("LoginName", data.LoginName);
            ht.Add("Password", data.Password);
            ht.Add("IsFlag", data.IsFlag);
            ht.Add("Remarks", data.Remarks);
            ht.Add("OperatorId", data.OperatorId);
            ht.Add("OperatorDate", data.OperatorDate);


            int i = ExecuteUpdate(stmtId, ht);
            return i;
        }


        public int UpdatePwd(aers_tbl_registereduser data)
        {
            String stmtId = "aers_tbl_registereduser_UpdatePwd";
            Hashtable ht = new Hashtable();
            ht.Add("ReguserId", data.ReguserId);
            ht.Add("Password", data.Password);
            ht.Add("OperatorDate", DateTime.Now);
            int i = ExecuteUpdate(stmtId, ht);
            return i;
        }

        
        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="loginName">登录检查的用户名称</param>
        /// <param name="aers_registereduser">用户基本信息</param>
        /// <returns>是/否存在</returns>
        public aers_tbl_registereduser Uniquenessverification(string loginName)
        {
       
            String stmtId = "aers_tbl_registereduser_FindByLoginName";
            Hashtable ht = new Hashtable();
            ht.Add("LoginName", loginName);
            aers_tbl_registereduser aers_registereduser = ExecuteQueryForObject<aers_tbl_registereduser>(stmtId, ht);
            return aers_registereduser;
        }

        /// <summary>
        /// 根据注册编码查询用户所属医院/个人状态信息
        /// </summary>
        /// <param name="uid">注册编码</param>
        /// <returns></returns>
        public DataSet GetHospStateById(string uid) 
        {
            String stmtId = "aers_tbl_registereduser_FindHospState";
            Hashtable ht = new Hashtable();
            ht.Add("ReguserId", uid);
            DataSet result = ExecutQueryForDataSet(stmtId, ht);

            foreach (DataRow item in result.Tables[0].Rows)
            {
                if (item["AersState"].ToString().Contains("147"))
                {
                    item["AersRemark"] = "省厅";
                }
                else if (item["AersState"].ToString().Contains("148"))
                {
                    item["AersRemark"] = "区域";
                }
                else if(item["AersState"].ToString().Contains("145"))
                {
                    item["AersRemark"] = "护理部";
                }
                else if (item["AersState"].ToString().Contains("146"))
                {
                    item["AersRemark"] = "护士长";
                }
                else 
                {
                    item["AersRemark"] = "护士";
                }
            }
            
            return result;
        }




        /// <summary>
        /// 根据注册用户编码查询注册用户信息
        /// </summary>
        /// <param name="reguserId">注册用户编码</param>
        /// <returns>注册用户信息实体</returns>
        public aers_tbl_registereduser Find(string reguserId)
        {
            String stmtId = "aers_tbl_registereduser_Find";
            Hashtable ht = new Hashtable();
            ht.Add("ReguserId", reguserId);
            aers_tbl_registereduser result = ExecuteQueryForObject<aers_tbl_registereduser>(stmtId, ht);
            return result;
        }

        //
        public aers_tbl_registereduser FindByLoginName(string LoginName)
        {
            String stmtId = "aers_tbl_registereduser_FindByLoginName";
            Hashtable ht = new Hashtable();
            ht.Add("LoginName", LoginName);
            aers_tbl_registereduser result = ExecuteQueryForObject<aers_tbl_registereduser>(stmtId, ht);
            return result;
        }
        /// <summary>
        /// 根据注册用户编码查询注册用户信息
        /// </summary>
        /// <param name="reguserId">注册用户编码</param>
        /// <returns>注册用户信息实体</returns>
        public IList<aers_tbl_registereduser> FindAll(string HospID)
        {
            String stmtId = "aers_tbl_registereduser_FindAll";
            Hashtable ht = new Hashtable();

            if (!string.IsNullOrEmpty(HospID))
            {
                stmtId = "aers_tbl_registereduser_ByHospId";
                ht.Add("HospId", HospID);
            }

            IList<aers_tbl_registereduser> list = ExecuteQueryForList<aers_tbl_registereduser>(stmtId, ht);


            aers_tbl_registereduserSqlMapDao dal = new aers_tbl_registereduserSqlMapDao();
            aers_tbl_staffSqlMapDao dalstaff = new aers_tbl_staffSqlMapDao();
            aers_tbl_events_ycSqlMapDao dalhospital = new aers_tbl_events_ycSqlMapDao();
            aers_tbl_hospdepSqlMapDao dalhospdep = new aers_tbl_hospdepSqlMapDao();

            IList<aers_tbl_hospital> listhospital = dalhospital.hospitalFindAll();
            IList<aers_tbl_hospdep> listhospdep = dalhospdep.hospdepFindAll();

            IList<aers_tbl_staff> liststaff = dalstaff.staffFindAll();


            foreach (aers_tbl_registereduser item in list)
            {

                if (item.LoginName == null)
                {
                    item.LoginName = "";
                }

                aers_tbl_staff sta = liststaff.FirstOrDefault(o => o.ReguserId == item.ReguserId);

                if (sta != null)
                {
                    item.StaffId = sta.StaffId;
                    item.DepId = sta.DepId;
                    item.Name = sta.Name;
                    item.RoleState = sta.RoleState;
                    item.Position = sta.Position;
                    item.Phone = sta.Phone;
                    item.Address = sta.Address;
                    item.IDNumber = sta.IDNumber;
                    item.StaffRemarks = sta.Remarks;

                    if (sta.RoleState != null)
                    {
                        if (sta.RoleState.Contains("147"))
                        {
                            item.RoleState = "省厅";
                        }
                        else if (sta.RoleState.Contains("148"))
                        {
                            item.RoleState = "区域";
                        }
                        else if (sta.RoleState.Contains("145"))
                        {
                            item.RoleState = "护理部";
                        }
                        else if (sta.RoleState.Contains("146"))
                        {
                            item.RoleState = "护士长";
                        }
                        else if (sta.RoleState.Contains("402"))
                        {
                            item.RoleState = "护士";
                        }
                        else
                        {
                            item.RoleState = "未知状态";
                        }
                    }



                    if (sta.Sex == "107")
                    {
                        item.Sex = "女";
                    }
                    else if (sta.Sex == "108")
                    {
                        item.Sex = "男";
                    }
                    else
                    {
                        item.Sex = "未知";
                    }
                  

                }
                else
                {
                    
                    item.StaffId = "";
                    item.DepId = "";
                    item.Name = "";
                    item.Position = "";
                    item.Phone = "";
                    item.Address = "";
                    item.IDNumber = "";
                    item.StaffRemarks = "";
                    item.RoleState = "";
                    item.Sex = "";
                    item.RoleState = "未知状态";
                }


                



              
                

               aers_tbl_hospdep of = listhospdep.FirstOrDefault(o => o.HospdepId == item.DepId);
                if (of != null)
                {
                    item.HospdepName = of.HospdepName;


                    aers_tbl_hospital hosp = listhospital.FirstOrDefault(o => o.HospId == of.HospId);
                    if (hosp != null)
                    {
                        item.HospId = hosp.HospId;
                        item.HospName = hosp.HospName;
                        item.hospitalAddress = hosp.Address;
                        item.hospitalPhone = hosp.Phone;
                        item.Contact = hosp.Contact;
                        item.Grade = hosp.Grade;
                        item.Validitytime = hosp.Validitytime;
                    }
                }
                else
                {
                    item.HospdepName = "";
                    item.HospId = "";
                    item.HospName = "";
                    item.hospitalAddress = "";
                    item.hospitalPhone = "";
                    item.Contact = "";
                    item.Grade = "";

                }
            }

            return list;
        }

        public IList<aers_tbl_hospdep> hospdepFindByHospId(string HospId)
        {
            aers_tbl_hospdepSqlMapDao dalhospdep = new aers_tbl_hospdepSqlMapDao();
            IList<aers_tbl_hospdep>  listhospdep = dalhospdep.hospdepFindByHospId(HospId);
            return listhospdep;
        }

        //2017.6.6 对表中以前的密码进行加密  临时用
        public IList<aers_tbl_registereduser> FindAllUser()
        {
            String stmtId = "aers_tbl_registereduser_FindAllUser";
            IList<aers_tbl_registereduser> result = ExecuteQueryForList<aers_tbl_registereduser>(stmtId, null);
            return result;
        }
    }
    
}

