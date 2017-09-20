using Aersysm.Domain;
using Aersysm.Persistence;
using Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Web;



namespace Aersysm
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class Aers : IAers
    {


        #region 以前

        #region 可能大概的公用部分

        #region 添加修改科室信息
        public string Addhospdep(aers_tbl_hospdep model)
        {
            string res = "104";
            try
            {
                aers_tbl_hospdepSqlMapDao dalhospdep = new aers_tbl_hospdepSqlMapDao();

                aers_tbl_hospdep check = dalhospdep.FindhospdepByHospdepName(model.HospId, model.HospdepName);
                if (check == null)
                {
                    model.OperatorDate = DateTime.Now;
                    if (string.IsNullOrEmpty(model.HospdepId))
                    {
                        dalhospdep.Insert(model);
                    }
                    else
                    {
                        dalhospdep.Update(model);
                    }
                    res = "103";
                }
                else
                {
                    res = "100";
                }
            }
            catch (Exception ex)
            {
                res = "104" + ex.Message;
            }
            return res;
        }
        #endregion

        #region 删除科室
        public string DeleteHospdep(string HospdepId)
        {
            string res = "104";
            try
            {
                aers_tbl_hospdepSqlMapDao dalhospdep = new aers_tbl_hospdepSqlMapDao();

                int i = dalhospdep.Delete(HospdepId);

                if (i > 0)
                {
                    res = "103";
                }
                else
                {
                    res = "100";
                }
            }
            catch (Exception ex)
            {
                res = "104" + ex.Message;
            }
            return res;
        }
        #endregion

        #region 查询某区域Region的所有医院
        public IList<aers_tbl_hospital> Gethospital(string Region)
        {
            aers_tbl_events_ycSqlMapDao dao = new aers_tbl_events_ycSqlMapDao();
            IList<aers_tbl_hospital> listdep = dao.hospitalFindAll();





            if (!string.IsNullOrEmpty(Region))
            {
                Region rr = GetRegion().FirstOrDefault(o => o.RegionID == Region);
                if (rr != null)
                {
                    listdep = listdep.Where(o => o.Address == rr.RegionName).ToList();
                }
            }

            //敏感字段数据
            foreach (aers_tbl_hospital item in listdep)
            {
                item.Contact = "敏感字段数据";
                item.Phone = "隐藏字段数据";
            }

            return listdep;
        }
        #endregion

        #region 获取地区 GetRegion()
        public IList<Region> GetRegion()
        {
            IList<Region> list = new List<Region>();

            Region rr = new Region();
            rr.RegionID = "0";
            rr.RegionName = "省直省管单位";
            list.Add(rr);

            rr = new Region();
            rr.RegionID = "1";
            rr.RegionName = "西安市";
            list.Add(rr);

            rr = new Region();
            rr.RegionID = "2";
            rr.RegionName = "宝鸡市";
            list.Add(rr);


            rr = new Region();
            rr.RegionID = "3";
            rr.RegionName = "咸阳市";
            list.Add(rr);

            rr = new Region();
            rr.RegionID = "4";
            rr.RegionName = "渭南市";
            list.Add(rr);

            rr = new Region();
            rr.RegionID = "5";
            rr.RegionName = "铜川市";
            list.Add(rr);

            rr = new Region();
            rr.RegionID = "6";
            rr.RegionName = "延安市";
            list.Add(rr);

            rr = new Region();
            rr.RegionID = "7";
            rr.RegionName = "榆林市";
            list.Add(rr);

            rr = new Region();
            rr.RegionID = "8";
            rr.RegionName = "安康市";
            list.Add(rr);

            rr = new Region();
            rr.RegionID = "9";
            rr.RegionName = "汉中市";
            list.Add(rr);

            rr = new Region();
            rr.RegionID = "10";
            rr.RegionName = "商洛市";
            list.Add(rr);
            return list;
        }
        #endregion

        #region 用户注册 
        /// <summary>
        /// U YM 2017.3.22护士角色402
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Registeruser(aers_tbl_staff model)
        {
            string res;
            aers_tbl_staff staff = new aers_tbl_staff();
            aers_tbl_registereduser registereduser = new aers_tbl_registereduser();
            aers_tbl_registereduserSqlMapDao dal = new aers_tbl_registereduserSqlMapDao();

            aers_tbl_staffSqlMapDao dalstaff = new aers_tbl_staffSqlMapDao();
            registereduser.LoginName = model.LoginName;
            // registereduser.Password = model.pwd;
            //YM 2017.6.6密码加密
            registereduser.Password = UserMd5(model.pwd);
            aers_tbl_registereduser check = dal.Uniquenessverification(registereduser.LoginName);
            if (check != null)
            {
                res = "100";
                return res;
            }
            registereduser.OperatorId = "ru00000001";
            registereduser.OperatorDate = DateTime.Now;
            string ReguserId = dal.Insert(registereduser);
            if (!string.IsNullOrEmpty(ReguserId))
            {
                model.ReguserId = ReguserId;
                model.IsFlag = 0;
                model.DepId = "";
                model.Name = "";
                model.Sex = "";
                model.Position = "";
                model.IDNumber = "";
                model.RoleState = "402"; //注册时全部按护士角色处理  YM3.22      402是护士
                model.OperatorId = "ru00000001";
                model.OperatorDate = DateTime.Now;

                int i;
                i = dalstaff.Insert(model);

                if (i == 1)
                {
                    res = "103-" + ReguserId;
                }
                else
                {
                    res = "104-" + ReguserId;
                }
            }
            else
            {
                res = "104";
            }
            return res;
        }
        #endregion

        #region MD5 加密
        //C YM 2017.6.6加密
        //public static string MD5Encrypt64(string password)
        //{
        //    string cl = password;
        //    MD5 md5 = MD5.Create(); //实例化一个md5对像
        //    byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
        //    return Convert.ToBase64String(s);
        //}

        public static string UserMd5(string str)
        {
            string cl = str;
            string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符
                pwd = pwd + s[i].ToString("X");

            }
            return pwd;
        }

        #endregion

        #region 对数据库以前的密码进行加密处理
        public static string ChangePwd()
        {
            aers_tbl_registereduserSqlMapDao rdao = new aers_tbl_registereduserSqlMapDao();
            var dd = rdao.FindAllUser();
            foreach (var item in dd)
            {
                if (item.ReguserId == "ru00000001")
                {
                    aers_tbl_registereduser u = new aers_tbl_registereduser();
                    u.ReguserId = item.ReguserId;
                    u.Password = item.Password.ToLower();
                    u.OperatorDate = DateTime.Now;
                    rdao.UpdatePwd(u);
                }

            }
            return "1";

        }
        #endregion

        #region 用户登陆 
        /// <summary>
        /// C YM 2017.6.1用户登录时获取IP等，返回唯一识别码
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public ResModel<view_tbl_registereduser> UserLogin(view_Login model) //以前的用户名密码
        {
            string iii = IPAddress();
            string res;
            aers_tbl_registereduserSqlMapDao dao = new aers_tbl_registereduserSqlMapDao();
            aers_tbl_hospdepSqlMapDao depdao = new aers_tbl_hospdepSqlMapDao();
            aers_tbl_events_ycSqlMapDao hospdao = new aers_tbl_events_ycSqlMapDao();

            view_tbl_registereduser view = new view_tbl_registereduser();
            ResModel<view_tbl_registereduser> resmodel = new ResModel<view_tbl_registereduser>();
            aers_tbl_staffSqlMapDao aers_staffSqlMapDao = new aers_tbl_staffSqlMapDao();

            aers_tbl_registereduser check = dao.Uniquenessverification(model.Name);

            if (check != null)
            {
                model.Pwd = UserMd5(model.Pwd);                         
                if (string.Equals(model.Pwd, check.Password))
                {
                    res = "103";
                    view.ReUId = check.ReguserId;
                    view.Name = check.LoginName;
                    view.Address = check.Address;
                    aers_tbl_staff aers_staff = aers_staffSqlMapDao.FindStaffByRid(view.ReUId);
                    view.GroupRole = aers_staff.RoleState.ToString();
                    view.HospDepId = aers_staff.DepId;
                    view.HospId = string.Empty;
                    view.RoleState = aers_staff.RoleState;
                    view.SystemTime = DateTime.Now;


                    aers_tbl_hospdep aers_hospdep = depdao.Find(aers_staff.DepId);
                    if (aers_hospdep != null)
                    {
                        view.HospId = depdao.Find(aers_staff.DepId).HospId;
                    }
                    aers_tbl_hospital hp = hospdao.hospitalFindByHospId(view.HospId);
                    if (hp != null)
                    {
                        view.IsFlag = hp.IsFlag;
                    }

                    view.LoginKey = GetLoginKey(); //6.1YM返回唯一识别码

                    //状态为1的改成2
                    aers_tbl_LoginKeySqlMapDao lkDao = new aers_tbl_LoginKeySqlMapDao();
                    lkDao.UpdateStatusByReguserId(view.ReUId);
                    aers_tbl_LoginKey lk = new aers_tbl_LoginKey();

                    lk.LoginKey = view.LoginKey;
                    lk.LoginTime = DateTime.Now;
                    lk.LoginIP = "";
                    lk.ReguserId = view.ReUId;
                    lk.LoginStatus = 1;
                    lkDao.Insert(lk);

                }
                else
                {
                    res = "102";
                    view.ReUId = string.Empty;
                    view.Name = string.Empty;
                    view.Address = string.Empty;
                    view.GroupRole = string.Empty;
                    view.HospDepId = string.Empty;
                    view.HospId = string.Empty;
                    view.RoleState = string.Empty;
                    view.SystemTime = DateTime.Now;
                    view.HospId = string.Empty;
                    view.IsFlag = 0;
                    view.LoginKey = string.Empty;


                }
            }
            else
            {
                res = "101";  //用户不存在时返回101
                view.ReUId = string.Empty;
                view.Name = string.Empty;
                view.Address = string.Empty;
                view.GroupRole = string.Empty;
                view.HospDepId = string.Empty;
                view.HospId = string.Empty;
                view.RoleState = string.Empty;
                view.SystemTime = DateTime.Now;
                view.HospId = string.Empty;
                view.IsFlag = 0;
                view.LoginKey = string.Empty;
            }

            resmodel.restag = res;
            resmodel.model = view;
            return resmodel;
        }
        #endregion

        #region 现在的登陆
        /// <summary>
        ///以前是用户名密码，现在是ruid
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public ResModel<view_tbl_registereduser> UserLoginNew(view_Login model)
        {
            string iii = IPAddress();
            string res;
            aers_tbl_registereduserSqlMapDao dao = new aers_tbl_registereduserSqlMapDao();
            aers_tbl_hospdepSqlMapDao depdao = new aers_tbl_hospdepSqlMapDao();
            aers_tbl_events_ycSqlMapDao hospdao = new aers_tbl_events_ycSqlMapDao();

            view_tbl_registereduser view = new view_tbl_registereduser();
            ResModel<view_tbl_registereduser> resmodel = new ResModel<view_tbl_registereduser>();
            aers_tbl_staffSqlMapDao aers_staffSqlMapDao = new aers_tbl_staffSqlMapDao();

          //  aers_tbl_registereduser check = dao.Uniquenessverification(model.Name);

            aers_tbl_registereduser check = dao.Find(model.Ruid);
            if (check != null)
            {
        //        model.Pwd = UserMd5(model.Pwd);
        //        if (string.Equals(model.Pwd, check.Password))  //密码错误
               // {
                    res = "103";
                    view.ReUId = check.ReguserId;
                    view.Name = check.LoginName;
                    view.Address = check.Address;
                    aers_tbl_staff aers_staff = aers_staffSqlMapDao.FindStaffByRid(view.ReUId);
                    view.GroupRole = aers_staff.RoleState.ToString();
                    view.HospDepId = aers_staff.DepId;
                    view.HospId = string.Empty;
                    view.RoleState = aers_staff.RoleState;
                    view.SystemTime = DateTime.Now;


                    aers_tbl_hospdep aers_hospdep = depdao.Find(aers_staff.DepId);
                    if (aers_hospdep != null)
                    {
                        view.HospId = depdao.Find(aers_staff.DepId).HospId;
                    }
                    aers_tbl_hospital hp = hospdao.hospitalFindByHospId(view.HospId);
                    if (hp != null)
                    {
                        view.IsFlag = hp.IsFlag;
                    }

                    view.LoginKey = GetLoginKey(); //6.1YM返回唯一识别码

                    //状态为1的改成2
                    aers_tbl_LoginKeySqlMapDao lkDao = new aers_tbl_LoginKeySqlMapDao();
                    lkDao.UpdateStatusByReguserId(view.ReUId);
                    aers_tbl_LoginKey lk = new aers_tbl_LoginKey();

                    lk.LoginKey = view.LoginKey;
                    lk.LoginTime = DateTime.Now;
                    lk.LoginIP = "";
                    lk.ReguserId = view.ReUId;
                    lk.LoginStatus = 1;
                    lkDao.Insert(lk);

              //  }
               // else
                //{
                //    res = "102";
                //    view.ReUId = string.Empty;
                //    view.Name = string.Empty;
                //    view.Address = string.Empty;
                //    view.GroupRole = string.Empty;
                //    view.HospDepId = string.Empty;
                //    view.HospId = string.Empty;
                //    view.RoleState = string.Empty;
                //    view.SystemTime = DateTime.Now;
                //    view.HospId = string.Empty;
                //    view.IsFlag = 0;
                //    view.LoginKey = string.Empty;


                //}
            }
            else
            {
                res = "101";  //用户不存在时返回101
                view.ReUId = string.Empty;
                view.Name = string.Empty;
                view.Address = string.Empty;
                view.GroupRole = string.Empty;
                view.HospDepId = string.Empty;
                view.HospId = string.Empty;
                view.RoleState = string.Empty;
                view.SystemTime = DateTime.Now;
                view.HospId = string.Empty;
                view.IsFlag = 0;
                view.LoginKey = string.Empty;
            }

            resmodel.restag = res;
            resmodel.model = view;
            return resmodel;
        }
        #endregion

        #region 获取调用者IP地址 未完
        /// <summary>  
        /// C YM 2017.6.1获取IP地址  
        /// </summary>  
        public string IPAddress()
        {

            //  string userIP = "未获取用户IP";

            //  try
            //  {
            //      if (System.Web.HttpContext.Current == null
            //      || System.Web.HttpContext.Current.Request == null
            //      || System.Web.HttpContext.Current.Request.ServerVariables == null)
            //          return "";

            //      string CustomerIP = "";

            //      //CDN加速后取到的IP   
            //      CustomerIP = System.Web.HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
            //      if (!string.IsNullOrEmpty(CustomerIP))
            //      {
            //          return CustomerIP;
            //      }

            //      CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];


            //      if (!String.IsNullOrEmpty(CustomerIP))
            //          return CustomerIP;

            //      if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            //      {
            //          CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            //          if (CustomerIP == null)
            //              CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            //      }
            //      else
            //      {
            //          CustomerIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            //      }

            //      if (string.Compare(CustomerIP, "unknown", true) == 0)
            //          return System.Web.HttpContext.Current.Request.UserHostAddress;
            //      return CustomerIP;
            //  }
            //  catch { }

            ////  return userIP;

            //  return Request.ServerVariables.Get("Remote_Addr").ToString();
            return "";
        }
        #endregion

        #region 返回唯一标识
        /// <summary>
        /// C YM 2017.6.1登录成功后返回给用户一个唯一识别码，调用安全性高的服务时进行验证
        /// </summary>
        /// <returns></returns>
        public string GetLoginKey()
        {
            return Guid.NewGuid().ToString();
        }
        #endregion

        #region 用户添加
        public string RegisterAdduser(aers_tbl_staff model)
        {
            string res;
            aers_tbl_staff staff = new aers_tbl_staff();
            aers_tbl_registereduser registereduser = new aers_tbl_registereduser();
            aers_tbl_registereduserSqlMapDao dal = new aers_tbl_registereduserSqlMapDao();

            aers_tbl_staffSqlMapDao dalstaff = new aers_tbl_staffSqlMapDao();
            registereduser.LoginName = model.LoginName;
            registereduser.Password = model.pwd;
            aers_tbl_registereduser check = dal.Uniquenessverification(registereduser.LoginName);
            if (check != null)
            {
                res = "100";
                return res;
            }
            registereduser.OperatorId = "ru00000001";
            registereduser.OperatorDate = DateTime.Now;
            string ReguserId = dal.Insert(registereduser);
            if (!string.IsNullOrEmpty(ReguserId))
            {
                model.ReguserId = ReguserId;
                model.IsFlag = 0;
                model.OperatorId = "ru00000001";
                model.OperatorDate = DateTime.Now;
                int i;
                i = dalstaff.Insert(model);

                if (i == 1)
                {
                    res = "103-" + ReguserId;
                }
                else
                {
                    res = "104-" + ReguserId;
                }
            }
            else
            {
                res = "104";
            }

            return res;
        }
        #endregion

        #region 用户修改
        public string UpdateUser(aers_tbl_staff model)
        {
            string res = "104";
            try
            {
                aers_tbl_registereduserSqlMapDao dal = new aers_tbl_registereduserSqlMapDao();
                aers_tbl_registereduser registereduser = new aers_tbl_registereduser();
                aers_tbl_staffSqlMapDao dao = new aers_tbl_staffSqlMapDao();
                aers_tbl_staff check = dao.FindStaffByRid(model.ReguserId);
                int i = 0;
                if (check == null)
                {
                    i = dao.Insert(model);
                }
                else
                {
                    i = dao.Update(model);
                }
                if (i > 0)
                {
                    res = "103";
                }
                else
                {
                    res = "104-修改失败";
                }
            }
            catch (Exception ex)
            {
                res = "104" + ex.Message;
            }

            return res;
        }
        #endregion

        #region 用户删除
        public string DeleteUser(string ReguserId)
        {
            string res = "104";
            try
            {
                aers_tbl_registereduserSqlMapDao dal = new aers_tbl_registereduserSqlMapDao();
                dal.Delete(ReguserId);
                res = "103";
            }
            catch (Exception ex)
            {
                res = "104" + ex.Message;
            }
            return res;
        }
        #endregion

        #region 根据注册码ReguserId查用户
        public aers_tbl_staff FindByRUid(string uid)
        {
            aers_tbl_staffSqlMapDao dao = new aers_tbl_staffSqlMapDao();
            aers_tbl_staff staff = dao.FindStaffByRid(uid);



            //DH  2017-6-2  修改
            aers_tbl_hospdep dep = new aers_tbl_hospdepSqlMapDao().FindhospdepByDepId(staff.DepId);

            if (dep != null)
            {
                staff.HospdepName = dep.HospdepName;
                staff.HospId = dep.HospId;

                aers_tbl_hospital hosp = new aers_tbl_events_ycSqlMapDao().hospitalFindByHospId(dep.HospId);
                if (hosp != null)
                {
                    staff.HospName = hosp.HospName;
                    staff.HospAddress = hosp.Address;
                }
            }
            //DH  2017-6-2  修改

            return staff;
        }
        #endregion

        #region 修改用户名
        public string UpdateLoginName(string uid, string name)
        {
            string res;
            aers_tbl_registereduserSqlMapDao dao = new aers_tbl_registereduserSqlMapDao();
            aers_tbl_registereduser check = dao.Uniquenessverification(name);
            if (check != null)
            {
                return res = "101";
            }
            else
            {
                aers_tbl_registereduser registereduser = new aers_tbl_registereduser();
                registereduser.ReguserId = uid;
                registereduser.LoginName = name;
                int i = dao.Update(registereduser);
                if (i > 0)
                {
                    res = "103";
                }
                else
                {
                    res = "104";
                }
            }
            return res;
        }
        #endregion

        #region 修改密码
        public string UpdatePwd(string ReguserId, string Pwd, string yPwd)
        {
            string res;
            aers_tbl_registereduserSqlMapDao dao = new aers_tbl_registereduserSqlMapDao();
            aers_tbl_registereduser registereduser = new aers_tbl_registereduser();
            aers_tbl_registereduser check = dao.Find(ReguserId);
            if (check != null && !string.IsNullOrEmpty(Pwd))
            {
                if (check.Password == UserMd5(yPwd))
                {
                    registereduser.Password = UserMd5(Pwd);
                    registereduser.ReguserId = ReguserId;
                    int i = dao.UpdatePwd(registereduser);
                    if (i > 0)
                    {
                        res = "103";
                    }
                    else
                    {
                        res = "104";
                    }
                }
                else
                {
                    res = "102";
                }
            }
            else
            {
                res = "101";
            }
            return res;
        }
        #endregion

        #region 根据注册码查询用户所属医院/个人状态信息
        public string GetHospState(string uid)
        {
            aers_tbl_registereduserSqlMapDao dao = new aers_tbl_registereduserSqlMapDao();
            return ExecutDataSetToJson(dao.GetHospStateById(uid));
        }
        #endregion

        #region 根据注册用户编码查询注册用户信息??
        public IList<aers_tbl_registereduser> GetRegistereduserByHospId(string HospId)
        {
            aers_tbl_registereduserSqlMapDao dao = new aers_tbl_registereduserSqlMapDao();
            return dao.FindAll(HospId);
        }
        #endregion

        #region 根据医院编号查询科室信息
        public IList<aers_tbl_hospdep> hospdepFindByHospId(string HospId)
        {
            aers_tbl_hospdepSqlMapDao dalhospdep = new aers_tbl_hospdepSqlMapDao();
            IList<aers_tbl_hospdep> listhospdep = dalhospdep.hospdepFindByHospId(HospId);
            return listhospdep;
        }
        #endregion

        #region 根据手机号，注册ID获取短信验证码  YM 2017.5.11

        public string SMSCodeSend(string ReguserID, string PhoneNumber)
        {
            SMSMessageSqlMapDao dao = new SMSMessageSqlMapDao();
            // return dao.FindAll().FirstOrDefault(o => o.ReguserID == ReguserID && o.PhoneNumber == PhoneNumber).VerificationCode;

            //将来调用短信接口生成6位随机数
            Random rd = new Random();
            var rnum = rd.Next(100000, 999999);
            SMSMessage smodel = new SMSMessage();
            smodel.ReguserID = ReguserID;
            smodel.PhoneNumber = PhoneNumber;
            smodel.VerificationCode = rnum.ToString();
            smodel.SMSSendTime = DateTime.Now;
            smodel.SendStatus = 0;
            dao.Insert(smodel);
            return rnum.ToString();
        }
        #endregion

        #region 查询手机号是否被注册过 YM2017.6.26
        public string IsHasPhoneNumber(string LoginName)
        {
            aers_tbl_registereduserSqlMapDao dal = new aers_tbl_registereduserSqlMapDao();
            aers_tbl_registereduser check = dal.Uniquenessverification(LoginName);
            if (check != null)
            {
                return "104";
            }
            else
            {
                return "103";
            }
        }
        #endregion

        #region 验证短信  YM 2017.5.11
        public string SMSVerification(string ReguserID, string PhoneNumber, string code)
        {
            SMSMessageSqlMapDao dao = new SMSMessageSqlMapDao();
            var backModel = dao.FindAll().FirstOrDefault(o => o.ReguserID == ReguserID && o.PhoneNumber == PhoneNumber);
            if ((DateTime.Now - backModel.SMSSendTime).Minutes > 5)
            {
                return "104验证超时";
            }
            if (backModel.VerificationCode == code)
            {
                return "102验证码错误";
            }
            else
            {
                return "103";
            }
        }
        #endregion

        #region 忘记密码 YM 2017.6.7
        public string ForgetPwd(string PhoneNumber, string Code, string pwd)
        {
            aers_tbl_registereduserSqlMapDao rsdao = new aers_tbl_registereduserSqlMapDao();
            var rsdata = rsdao.FindByLoginName(PhoneNumber);

            if (rsdata != null)
            {

                aers_tbl_staffSqlMapDao sdaov = new aers_tbl_staffSqlMapDao();
                var sdata = sdaov.FindByRUid(rsdata.ReguserId);

                SMSMessageSqlMapDao smdao = new SMSMessageSqlMapDao();
                var smdataList = smdao.FindByPhoneNumber(PhoneNumber);

                var smdata = smdataList.OrderByDescending(o => o.SMSSendTime).FirstOrDefault();

                var smdataTime = smdata.SMSSendTime;

                if ((DateTime.Now - smdataTime).Minutes < 5)
                {
                    if (smdata.VerificationCode == Code)
                    {
                        rsdata.Password = UserMd5(pwd);
                        rsdata.OperatorDate = DateTime.Now;
                        int i = rsdao.UpdatePwd(rsdata);
                        if (i > 0)
                        {
                            return "103";
                        }
                        else
                        {
                            return "104-修改失败";
                        }
                    }
                    else
                    {
                        return "验证码错误";
                    }

                }
                else
                {
                    return "验证码过期";
                }

            }
            else
            {
                return "该号码暂未进行绑定账号";
            }



        }
        #endregion

        #region 错误编码
        public IList<aers_sys_statecode> FindStatecodeAll()
        {
            aers_sys_statecodeSqlMapDao dao = new aers_sys_statecodeSqlMapDao();
            return dao.FindAll();
        }

        #endregion

        #region 认证信息  YM2017.5.17实名认证职业认证放到一起
        public string AuthenticationInfo(CertificateAudit model)
        {
            try
            {
                CertificateAuditSqlMapDao cdao = new CertificateAuditSqlMapDao();

                cdao.CertificateAuditUpdateByReguserID(model.ReguserID, model.CertificateType);


                CertificateAudit ca = new CertificateAudit();

                aers_tbl_staff st = new aers_tbl_staff();
                aers_tbl_staffSqlMapDao sdao = new aers_tbl_staffSqlMapDao();
                var name = sdao.FindNameByRid(model.ReguserID);
                if (model.CertificateType == 1)  //身份证时进行验证名字一致性
                {
                    if (name != model.Name)
                    {
                        return "101";
                    }
                }
                ca.ReguserID = model.ReguserID;
                ca.CertificateID = model.CertificateID;
                ca.CertificateType = model.CertificateType;
                ca.CertificateDate = model.CertificateDate;
                ca.CertificatePhoto = model.CertificatePhoto;
                ca.CertificatePhotos = model.CertificatePhotos;
                ca.SubmitDate = DateTime.Now;
                ca.AuditorID = model.AuditID;
                ca.AuditDate = model.AuditDate;
                ca.AuditStatus = 1;  //待审核

                //2017.6.21YM新加两个字段
                ca.Name = model.Name;
                ca.CertificateName = model.CertificateName;
                cdao.Insert(ca);
                return "103";

            }
            catch (Exception ex)
            {
                return "104" + ex;
            }
        }
        #endregion

        #region 认证信息查询状态 YM 2017.5.19
        //public IList<CertificateAudit> GetAuditStatus(int CertificateType,string ReguserID)
        //{
        //    CertificateAuditSqlMapDao cdao = new CertificateAuditSqlMapDao();
        //    //DH  2016-6-7
        //    //var rdata = cdao.CertificateAuditByReguserID(ReguserID);
        //    //var data=  rdata.Where (o => o.CertificateType == CertificateType && o.AuditStatus != 4).ToList();
        //   var data= cdao.FindAll().Where(o => o.ReguserID == ReguserID && o.CertificateType == CertificateType && o.AuditStatus != 4).ToList();
        //    //    var data1 = cdao
        //    //DH  2016-6-6
        //    if (data != null)
        //    {
        //        return data;
        //    }
        //    else
        //    {
        //        return data;
        //    }
        //}

        public string GetAuditStatus(string ReguserID, int CertificateType)
        {
            DataSet ds = new DataSet();
            CertificateAuditSqlMapDao cdao = new CertificateAuditSqlMapDao();
            var rdata = cdao.CertificateAuditByReguserID(ReguserID);
            var data = rdata.Where(o => o.CertificateType == CertificateType && o.AuditStatus != 4).ToDataTable();
            if (data != null)
            {
                ds.Tables.Add(data);
                return ExecutDataSetToJson(ds);
            }
            else
            {
                return null;
            }

        }
        #endregion



        #endregion


        #region 不良事件

        #region 零事件查找有无上报过其他事件  其他事件
        public string findnurswaitData(string uid, string yue)
        {
            IList<view_tbl_eventsresume> tempview = new List<view_tbl_eventsresume>();
            aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
            ResList<view_tbl_eventsresume> reslist = new ResList<view_tbl_eventsresume>();
            string res = "104";
            try
            {
                IList<aers_tbl_eventsresume> list = dao.FindDepByData(uid);

                DateTime time = Convert.ToDateTime(yue);

                DateTime time1 = new DateTime(time.Year, time.Month, 1);
                DateTime time2 = new DateTime(time.Year, time.Month, 1).AddMonths(1);


                list = list.Where(o => o.HappenedDate >= time1 && o.HappenedDate < time2).ToList();

                if (list.Count > 0)
                {
                    res = "376";
                }
                else
                {
                    res = "377";
                }
            }
            catch (Exception ex)
            {
                res = "104";
            }
            return res;
        }
        #endregion 


        #region 压疮事件上报5.31
        public string EventYcReport(aers_tbl_events_yc model, string ReUId, string LoginKey)
        {
           // if (HeartRate(ReUId, LoginKey))
            {

                if (model.FillStaff.Length != 10)
                {
                    return "104操作员不能为空";
                }
                string res = "104";
                aers_tbl_events_ycSqlMapDao dao = new aers_tbl_events_ycSqlMapDao();

                aers_tbl_eventsresumeSqlMapDao aers_tbl_eventsresumedao = new aers_tbl_eventsresumeSqlMapDao();
                aers_tbl_events_yc_partsSqlMapDao aers_tbl_events_yc_partsdao = new aers_tbl_events_yc_partsSqlMapDao();


                //新增 不良事件汇总记录
                aers_tbl_eventsresume aers_eventsresume = new aers_tbl_eventsresume();
                aers_eventsresume.EveresName = "149";
                aers_eventsresume.HospDepId = model.HospDepId;
                aers_eventsresume.HospId = model.HospId;
                aers_eventsresume.EveresLevel = "--";
                aers_eventsresume.HappenedDate = model.HappenedDate;
                model.SendtoDate = DateTime.Now;
                aers_eventsresume.SendtoDate = model.SendtoDate;
                aers_eventsresume.SpellNo = "YC";
                aers_eventsresume.OperatorID = model.FillStaff;
                aers_eventsresume.ExamineState = model.ExamineState;
                aers_eventsresume.FileName = model.FileName;

                string Deleteid = "";
                string ycId="";
                try
                {


                    if (string.IsNullOrEmpty(model.EveresId))
                    {
                        string aers_eventsresume_id = aers_tbl_eventsresumedao.instert(aers_eventsresume);
                        Deleteid = aers_eventsresume_id;
                        //新增压疮事件记录
                        model.EveresId = aers_eventsresume_id;
                        string aers_events_yc = dao.Insert(model);
                        ycId = aers_events_yc;
                        foreach (aers_tbl_events_yc_parts item in model.Parts)
                        {
                            //新增压疮事件-压疮体位记录
                            item.EveycId = aers_events_yc;
                            item.OperatorID = model.FillStaff;
                            item.EveresName = aers_eventsresume.EveresName;
                            aers_tbl_events_yc_partsdao.Insert(item);
                        }
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }
                    else
                    {

                        aers_eventsresume.EveresId = model.EveresId;
                        aers_tbl_eventsresumedao.Update(aers_eventsresume);

                        dao.Update(model);

                        aers_tbl_events_yc_partsdao.Delete(model.EveycId);
                        ycId = model.EveycId;
                        foreach (aers_tbl_events_yc_parts item in model.Parts)
                        {
                            //修改压疮事件-压疮体位记录
                            item.EveycId = model.EveycId;
                            item.OperatorID = model.FillStaff;
                            item.EveresName = aers_eventsresume.EveresName;
                            aers_tbl_events_yc_partsdao.Insert(item);
                        }
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }


                    return "103";
                }
                catch (Exception ex)
                {
                    //if (!string.IsNullOrEmpty(Deleteid))
                    //{
                    //    aers_tbl_eventsresumedao.Delete(Deleteid);
                    //}
                    res = "104";
                }


                return res;
            }
            //else
            //{
            //    return "-8000";
            //}

        }
        #endregion

        #region 压疮事件上报8.4
        public string EventYcReportNew(aers_tbl_events_yc model)
        {
            // if (HeartRate(ReUId, LoginKey))
            {

                if (model.FillStaff.Length != 10)
                {
                    return "104操作员不能为空";
                }
                string res = "104";
                aers_tbl_events_ycSqlMapDao dao = new aers_tbl_events_ycSqlMapDao();

                aers_tbl_eventsresumeSqlMapDao aers_tbl_eventsresumedao = new aers_tbl_eventsresumeSqlMapDao();
                aers_tbl_events_yc_partsSqlMapDao aers_tbl_events_yc_partsdao = new aers_tbl_events_yc_partsSqlMapDao();


                //新增 不良事件汇总记录
                aers_tbl_eventsresume aers_eventsresume = new aers_tbl_eventsresume();
                aers_eventsresume.EveresName = "149";
                aers_eventsresume.HospDepId = model.HospDepId;
                aers_eventsresume.HospId = model.HospId;
                aers_eventsresume.EveresLevel = "--";
                aers_eventsresume.HappenedDate = model.HappenedDate;
                model.SendtoDate = DateTime.Now;
                aers_eventsresume.SendtoDate = model.SendtoDate;
                aers_eventsresume.SpellNo = "YC";
                aers_eventsresume.OperatorID = model.FillStaff;
                aers_eventsresume.ExamineState = model.ExamineState;
                aers_eventsresume.FileName = model.FileName;

                string Deleteid = "";
                string reId;
                try
                {


                    if (string.IsNullOrEmpty(model.EveresId))
                    {
                        string aers_eventsresume_id = aers_tbl_eventsresumedao.instert(aers_eventsresume);
                        Deleteid = aers_eventsresume_id;
                        //新增压疮事件记录
                        model.EveresId = aers_eventsresume_id;
                        string aers_events_yc = dao.Insert(model);
                        reId = model.EveresId;
                        foreach (aers_tbl_events_yc_parts item in model.Parts)
                        {
                            //新增压疮事件-压疮体位记录
                            item.EveycId = aers_events_yc;
                            item.OperatorID = model.FillStaff;
                            item.EveresName = aers_eventsresume.EveresName;
                            aers_tbl_events_yc_partsdao.Insert(item);
                        }
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }
                    else
                    {

                        aers_eventsresume.EveresId = model.EveresId;
                        aers_tbl_eventsresumedao.Update(aers_eventsresume);

                        dao.Update(model);

                        aers_tbl_events_yc_partsdao.Delete(model.EveycId);
                        reId = model.EveresId;
                        foreach (aers_tbl_events_yc_parts item in model.Parts)
                        {
                            //修改压疮事件-压疮体位记录
                            item.EveycId = model.EveycId;
                            item.OperatorID = model.FillStaff;
                            item.EveresName = aers_eventsresume.EveresName;
                            aers_tbl_events_yc_partsdao.Insert(item);
                        }
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }


                    res = reId;
                }
                catch (Exception ex)
                {
                    //if (!string.IsNullOrEmpty(Deleteid))
                    //{
                    //    aers_tbl_eventsresumedao.Delete(Deleteid);
                    //}
                    res = "104";
                }


                return res;
            }
            //else
            //{
            //    return "-8000";
            //}

        }
        #endregion

        #region 其他事件上报5.31
        public string EventQtReport(aers_tbl_events_qt model, string ReUId, string LoginKey)
        {
          //  if (HeartRate(ReUId, LoginKey))
            {
                if (model.FillStaff.Length != 10)
                {
                    return "104操作员不能为空";
                }
                string res = "104";
                aers_tbl_events_qtSqlMapDao aers_tbl_events_qtdao = new aers_tbl_events_qtSqlMapDao();
                aers_tbl_eventsresumeSqlMapDao aers_tbl_eventsresumedao = new aers_tbl_eventsresumeSqlMapDao();

                //新增 不良事件汇总记录
                aers_tbl_eventsresume aers_eventsresume = new aers_tbl_eventsresume();
                aers_eventsresume.EveresName = "150";
                aers_eventsresume.HospDepId = model.HospDepId;
                aers_eventsresume.HospId = model.HospId;
                aers_eventsresume.EveresLevel = model.EventLevel;

                if (string.IsNullOrEmpty(aers_eventsresume.EveresLevel))
                {
                    aers_eventsresume.EveresLevel = "--";
                }

                aers_eventsresume.HappenedDate = model.HappenedDate;
                model.SendtoDate = DateTime.Now;
                aers_eventsresume.SendtoDate = model.SendtoDate;
                aers_eventsresume.SpellNo = "QT";
                aers_eventsresume.OperatorID = model.FillStaff;
                aers_eventsresume.ExamineState = model.ExamineState;
                aers_eventsresume.FileName = model.FileName;

                string Deleteid = "";
                try
                {
                    if (string.IsNullOrEmpty(model.EveresId))
                    {
                        string aers_eventsresume_id = aers_tbl_eventsresumedao.instert(aers_eventsresume);
                        Deleteid = aers_eventsresume_id;
                        //新增其他事件记录
                        model.EveresId = aers_eventsresume_id;
                        aers_tbl_events_qtdao.Insert(model);
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }
                    else
                    {
                        aers_eventsresume.EveresId = model.EveresId;
                        aers_tbl_eventsresumedao.Update(aers_eventsresume);

                        aers_tbl_events_qtdao.Update(model);
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }


                    res = "103";
                }
                catch (Exception)
                {
                    //if (!string.IsNullOrEmpty(Deleteid))
                    //{
                    //    aers_tbl_eventsresumedao.Delete(Deleteid);
                    //}
                    res = "104";
                }
                return res;
            }
            //else
            //{
            //    return "-8000";
            //}

        }
        #endregion

        #region 其他事件上报8.4
        public string EventQtReportNew(aers_tbl_events_qt model)
        {
            //  if (HeartRate(ReUId, LoginKey))
            {
                if (model.FillStaff.Length != 10)
                {
                    return "104操作员不能为空";
                }
                string res = "104";
                aers_tbl_events_qtSqlMapDao aers_tbl_events_qtdao = new aers_tbl_events_qtSqlMapDao();
                aers_tbl_eventsresumeSqlMapDao aers_tbl_eventsresumedao = new aers_tbl_eventsresumeSqlMapDao();

                //新增 不良事件汇总记录
                aers_tbl_eventsresume aers_eventsresume = new aers_tbl_eventsresume();
                aers_eventsresume.EveresName = "150";
                aers_eventsresume.HospDepId = model.HospDepId;
                aers_eventsresume.HospId = model.HospId;
                aers_eventsresume.EveresLevel = model.EventLevel;

                if (string.IsNullOrEmpty(aers_eventsresume.EveresLevel))
                {
                    aers_eventsresume.EveresLevel = "--";
                }

                aers_eventsresume.HappenedDate = model.HappenedDate;
                model.SendtoDate = DateTime.Now;
                aers_eventsresume.SendtoDate = model.SendtoDate;
                aers_eventsresume.SpellNo = "QT";
                aers_eventsresume.OperatorID = model.FillStaff;
                aers_eventsresume.ExamineState = model.ExamineState;
                aers_eventsresume.FileName = model.FileName;

                string Deleteid = "";
                string reId;
                try
                {
                    if (string.IsNullOrEmpty(model.EveresId))
                    {
                        string aers_eventsresume_id = aers_tbl_eventsresumedao.instert(aers_eventsresume);
                        Deleteid = aers_eventsresume_id;
                        //新增其他事件记录
                        model.EveresId = aers_eventsresume_id;
                        reId = model.EveresId;
                        aers_tbl_events_qtdao.Insert(model);
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }
                    else
                    {
                       
                        aers_eventsresume.EveresId = model.EveresId;
                        aers_tbl_eventsresumedao.Update(aers_eventsresume);
                        reId = aers_eventsresume.EveresId;
                        aers_tbl_events_qtdao.Update(model);
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }


                    res = reId ;
                }
                catch (Exception)
                {
                    //if (!string.IsNullOrEmpty(Deleteid))
                    //{
                    //    aers_tbl_eventsresumedao.Delete(Deleteid);
                    //}
                    res = "104";
                }
                return res;
            }
            //else
            //{
            //    return "-8000";
            //}

        }
        #endregion

        #region 职业暴露事件上报5.31
        public string EventZyblReport(aers_tbl_events_zybl model, string ReUId, string LoginKey)
        {
           // if (HeartRate(ReUId, LoginKey))
            {

                if (model.FillStaff.Length != 10)
                {
                    return "104操作员不能为空";
                }
                string res = "104";
                aers_tbl_events_zyblSqlMapDao aers_tbl_events_zybldao = new aers_tbl_events_zyblSqlMapDao();
                aers_tbl_eventsresumeSqlMapDao aers_tbl_eventsresumedao = new aers_tbl_eventsresumeSqlMapDao();

                //新增 不良事件汇总记录
                aers_tbl_eventsresume aers_eventsresume = new aers_tbl_eventsresume();
                aers_eventsresume.EveresName = "156";
                aers_eventsresume.HospDepId = model.HospDepId;
                aers_eventsresume.HospId = model.HospId;
                /// aers_eventsresume.EveresLevel = model.EventLevel;  //2017.03.30YM    事件等级前台去掉，传过来无数据，默认--
                aers_eventsresume.EveresLevel = "--";
                aers_eventsresume.HappenedDate = model.HappenedDate;
                model.SendtoDate = DateTime.Now;
                aers_eventsresume.SendtoDate = model.SendtoDate;
                aers_eventsresume.SpellNo = "ZYBL";
                aers_eventsresume.OperatorID = model.FillStaff;
                aers_eventsresume.ExamineState = model.ExamineState;
                aers_eventsresume.FileName = model.FileName;

                string Deleteid = "";
                try
                {
                    if (string.IsNullOrEmpty(model.EveresId))
                    {
                        string aers_eventsresume_id = aers_tbl_eventsresumedao.instert(aers_eventsresume);
                        Deleteid = aers_eventsresume_id;
                        //新增职业暴露事件记录
                        model.EveresId = aers_eventsresume_id;
                        aers_tbl_events_zybldao.Insert(model);
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }


                    }
                    else
                    {
                        aers_eventsresume.EveresId = model.EveresId;
                        aers_tbl_eventsresumedao.Update(aers_eventsresume);

                        aers_tbl_events_zybldao.Update(model);
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }


                    res = "103";
                }
                catch (Exception ex)
                {
                    //if (!string.IsNullOrEmpty(Deleteid))
                    //{
                    //    aers_tbl_eventsresumedao.Delete(Deleteid);
                    //}
                    res = "104" + ex.Message;
                }
                return res;
            }
            //else
            //{
            //    return "-8000";
            //}
        }
        #endregion

        #region 职业暴露事件上报8.4
        public string EventZyblReportNew(aers_tbl_events_zybl model)
        {
            // if (HeartRate(ReUId, LoginKey))
            {

                if (model.FillStaff.Length != 10)
                {
                    return "104操作员不能为空";
                }
                string res = "104";
                aers_tbl_events_zyblSqlMapDao aers_tbl_events_zybldao = new aers_tbl_events_zyblSqlMapDao();
                aers_tbl_eventsresumeSqlMapDao aers_tbl_eventsresumedao = new aers_tbl_eventsresumeSqlMapDao();

                //新增 不良事件汇总记录
                aers_tbl_eventsresume aers_eventsresume = new aers_tbl_eventsresume();
                aers_eventsresume.EveresName = "156";
                aers_eventsresume.HospDepId = model.HospDepId;
                aers_eventsresume.HospId = model.HospId;
                /// aers_eventsresume.EveresLevel = model.EventLevel;  //2017.03.30YM    事件等级前台去掉，传过来无数据，默认--
                aers_eventsresume.EveresLevel = "--";
                aers_eventsresume.HappenedDate = model.HappenedDate;
                model.SendtoDate = DateTime.Now;
                aers_eventsresume.SendtoDate = model.SendtoDate;
                aers_eventsresume.SpellNo = "ZYBL";
                aers_eventsresume.OperatorID = model.FillStaff;
                aers_eventsresume.ExamineState = model.ExamineState;
                aers_eventsresume.FileName = model.FileName;
                string reId;
                string Deleteid = "";
                try
                {
                    if (string.IsNullOrEmpty(model.EveresId))
                    {
                        string aers_eventsresume_id = aers_tbl_eventsresumedao.instert(aers_eventsresume);
                        Deleteid = aers_eventsresume_id;
                        //新增职业暴露事件记录
                      
                        model.EveresId = aers_eventsresume_id;
                        reId = model.EveresId;
                        aers_tbl_events_zybldao.Insert(model);
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }


                    }
                    else
                    {
                        aers_eventsresume.EveresId = model.EveresId;
                        aers_tbl_eventsresumedao.Update(aers_eventsresume);
                        reId = model.EveresId;
                        aers_tbl_events_zybldao.Update(model);
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }


                    res = reId ;
                }
                catch (Exception ex)
                {
                    //if (!string.IsNullOrEmpty(Deleteid))
                    //{
                    //    aers_tbl_eventsresumedao.Delete(Deleteid);
                    //}
                    res = "104" + ex.Message;
                }
                return res;
            }
            //else
            //{
            //    return "-8000";
            //}
        }
        #endregion

        #region 隐患事件上报5.31
        public string EventYhReport(aers_tbl_events_yh model, string ReUId, string LoginKey)
        {
           // if (HeartRate(ReUId, LoginKey))
            {
                if (model.FillStaff.Length != 10)
                {
                    return "104操作员不能为空";
                }
                string res = "104";
                aers_tbl_events_yhSqlMapDao aers_tbl_events_yhdao = new aers_tbl_events_yhSqlMapDao();
                aers_tbl_eventsresumeSqlMapDao aers_tbl_eventsresumedao = new aers_tbl_eventsresumeSqlMapDao();

                //新增 不良事件汇总记录
                aers_tbl_eventsresume aers_eventsresume = new aers_tbl_eventsresume();
                aers_eventsresume.EveresName = "154";
                aers_eventsresume.HospDepId = model.HospDepId;
                aers_eventsresume.HospId = model.HospId;
                aers_eventsresume.EveresLevel = model.EventLevel;
                aers_eventsresume.HappenedDate = model.HappenedDate;
                model.SendtoDate = DateTime.Now;
                aers_eventsresume.SendtoDate = model.SendtoDate;
                aers_eventsresume.SpellNo = "YH";
                aers_eventsresume.OperatorID = model.FillStaff;
                aers_eventsresume.ExamineState = model.ExamineState;
                aers_eventsresume.FileName = model.FileName;


                string Deleteid = "";
                try
                {
                    if (string.IsNullOrEmpty(model.EveresId))
                    {
                        string aers_eventsresume_id = aers_tbl_eventsresumedao.instert(aers_eventsresume);
                        Deleteid = aers_eventsresume_id;
                        //新增职业暴露事件记录
                        model.EveresId = aers_eventsresume_id;
                        aers_tbl_events_yhdao.Insert(model);
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }
                    else
                    {
                        aers_eventsresume.EveresId = model.EveresId;
                        aers_tbl_eventsresumedao.Update(aers_eventsresume);

                        aers_tbl_events_yhdao.Update(model);
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }
                    res = "103";
                }
                catch (Exception ex)
                {
                    //if (!string.IsNullOrEmpty(Deleteid))
                    //{
                    //    aers_tbl_eventsresumedao.Delete(Deleteid);
                    //}
                    res = "104";
                }
                return res;
            }
            //else
            //{
            //    return "-8000";
            //}
        }
        #endregion

        #region 隐患事件上报8.4
        public string EventYhReportNew(aers_tbl_events_yh model)
        {
            // if (HeartRate(ReUId, LoginKey))
            {
                if (model.FillStaff.Length != 10)
                {
                    return "104操作员不能为空";
                }
                string res = "104";
                aers_tbl_events_yhSqlMapDao aers_tbl_events_yhdao = new aers_tbl_events_yhSqlMapDao();
                aers_tbl_eventsresumeSqlMapDao aers_tbl_eventsresumedao = new aers_tbl_eventsresumeSqlMapDao();

                //新增 不良事件汇总记录
                aers_tbl_eventsresume aers_eventsresume = new aers_tbl_eventsresume();
                aers_eventsresume.EveresName = "154";
                aers_eventsresume.HospDepId = model.HospDepId;
                aers_eventsresume.HospId = model.HospId;
                aers_eventsresume.EveresLevel = model.EventLevel;
                aers_eventsresume.HappenedDate = model.HappenedDate;
                model.SendtoDate = DateTime.Now;
                aers_eventsresume.SendtoDate = model.SendtoDate;
                aers_eventsresume.SpellNo = "YH";
                aers_eventsresume.OperatorID = model.FillStaff;
                aers_eventsresume.ExamineState = model.ExamineState;
                aers_eventsresume.FileName = model.FileName;

                string reId;
                string Deleteid = "";
                try
                {
                    if (string.IsNullOrEmpty(model.EveresId))
                    {
                        string aers_eventsresume_id = aers_tbl_eventsresumedao.instert(aers_eventsresume);
                        Deleteid = aers_eventsresume_id;
                        //新增职业暴露事件记录
                        reId = model.EveresId;
                        model.EveresId = aers_eventsresume_id;
                        aers_tbl_events_yhdao.Insert(model);
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }
                    else
                    {
                        aers_eventsresume.EveresId = model.EveresId;
                        aers_tbl_eventsresumedao.Update(aers_eventsresume);
                        reId = model.EveresId;
                        aers_tbl_events_yhdao.Update(model);
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }
                    res = reId ;
                }
                catch (Exception ex)
                {
                    //if (!string.IsNullOrEmpty(Deleteid))
                    //{
                    //    aers_tbl_eventsresumedao.Delete(Deleteid);
                    //}
                    res = "104";
                }
                return res;
            }
            //else
            //{
            //    return "-8000";
            //}
        }
        #endregion

        #region 管路事件上报 5.31
        public string EventGlReport(aers_tbl_events_gl model, string ReUId, string LoginKey)
        {
           // if (HeartRate(ReUId, LoginKey))
            {
                if (model.FillStaff.Length != 10)
                {
                    return "104操作员不能为空";
                }
                string res = "104";
                aers_tbl_events_glSqlMapDao aers_tbl_events_gldao = new aers_tbl_events_glSqlMapDao();
                aers_tbl_eventsresumeSqlMapDao aers_tbl_eventsresumedao = new aers_tbl_eventsresumeSqlMapDao();


                //新增 不良事件汇总记录
                aers_tbl_eventsresume aers_eventsresume = new aers_tbl_eventsresume();
                aers_eventsresume.EveresName = "153";
                aers_eventsresume.HospDepId = model.HospDepId;
                aers_eventsresume.HospId = model.HospId;
                aers_eventsresume.EveresLevel = model.EventLevel;
                aers_eventsresume.HappenedDate = model.HappenedDate;
                model.SendtoDate = DateTime.Now;
                aers_eventsresume.SendtoDate = model.SendtoDate;
                aers_eventsresume.SpellNo = "GL";
                aers_eventsresume.OperatorID = model.FillStaff;
                aers_eventsresume.ExamineState = model.ExamineState;
                aers_eventsresume.FileName = model.FileName;

                string Deleteid = "";
                try
                {
                    if (string.IsNullOrEmpty(model.EveresId))  // 传过来的model中EveresId为空，进行增加操作
                    {
                        string aers_eventsresume_id = aers_tbl_eventsresumedao.instert(aers_eventsresume);
                        Deleteid = aers_eventsresume_id;  //返回主表主键值zhu
                                                          //新增职业暴露事件记录
                        model.EveresId = aers_eventsresume_id;   //主键值存在字表的EveresId字段中
                        aers_tbl_events_gldao.Insert(model);   //字表添加数据
                        if (model.ExamineState != "3")   //ExamineState不等于3时，3是啥？
                        {
                            ExecStatus(model.EveresId, model.OperatorID);   //进行审核
                        }
                    }
                    else
                    {
                        aers_eventsresume.EveresId = model.EveresId;    //传过来的model中EveresId不为空，把此值给主表主键，进行修改操作
                        aers_tbl_eventsresumedao.Update(aers_eventsresume);  //对主表该数据进行修改

                        aers_tbl_events_gldao.Update(model);   //对字表数据进行修改
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }
                    res = "103";
                }
                catch (Exception ex)
                {
                    //if (!string.IsNullOrEmpty(Deleteid))  //主表主键值有值
                    //{
                    //    aers_tbl_eventsresumedao.Delete(Deleteid);  //删除主表该条记录
                    //}
                    res = "104" + ex.Message;
                }
                return res;
            }
            //else
            //{
            //    return "-8000";
            //}
        }
        #endregion

        #region 管路事件上报 8.4
        public string EventGlReportNew(aers_tbl_events_gl model)
        {
            // if (HeartRate(ReUId, LoginKey))
            {
                if (model.FillStaff.Length != 10)
                {
                    return "104操作员不能为空";
                }
                string res = "104";
                aers_tbl_events_glSqlMapDao aers_tbl_events_gldao = new aers_tbl_events_glSqlMapDao();
                aers_tbl_eventsresumeSqlMapDao aers_tbl_eventsresumedao = new aers_tbl_eventsresumeSqlMapDao();


                //新增 不良事件汇总记录
                aers_tbl_eventsresume aers_eventsresume = new aers_tbl_eventsresume();
                aers_eventsresume.EveresName = "153";
                aers_eventsresume.HospDepId = model.HospDepId;
                aers_eventsresume.HospId = model.HospId;
                aers_eventsresume.EveresLevel = model.EventLevel;
                aers_eventsresume.HappenedDate = model.HappenedDate;
                model.SendtoDate = DateTime.Now;
                aers_eventsresume.SendtoDate = model.SendtoDate;
                aers_eventsresume.SpellNo = "GL";
                aers_eventsresume.OperatorID = model.FillStaff;
                aers_eventsresume.ExamineState = model.ExamineState;
                aers_eventsresume.FileName = model.FileName;
                string reId;
                string Deleteid = "";
                try
                {
                    if (string.IsNullOrEmpty(model.EveresId))  // 传过来的model中EveresId为空，进行增加操作
                    {
                        string aers_eventsresume_id = aers_tbl_eventsresumedao.instert(aers_eventsresume);
                        Deleteid = aers_eventsresume_id;  //返回主表主键值zhu
                                                          //新增职业暴露事件记录
                       
                        model.EveresId = aers_eventsresume_id;   //主键值存在字表的EveresId字段中
                        reId = model.EveresId;
                        aers_tbl_events_gldao.Insert(model);   //字表添加数据
                        if (model.ExamineState != "3")   //ExamineState不等于3时，3是啥？
                        {
                            ExecStatus(model.EveresId, model.OperatorID);   //进行审核
                        }
                    }
                    else
                    {
                        aers_eventsresume.EveresId = model.EveresId;    //传过来的model中EveresId不为空，把此值给主表主键，进行修改操作
                        aers_tbl_eventsresumedao.Update(aers_eventsresume);  //对主表该数据进行修改
                        reId = model.EveresId;
                        aers_tbl_events_gldao.Update(model);   //对字表数据进行修改
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }
                    res = reId ;
                }
                catch (Exception ex)
                {
                    //if (!string.IsNullOrEmpty(Deleteid))  //主表主键值有值
                    //{
                    //    aers_tbl_eventsresumedao.Delete(Deleteid);  //删除主表该条记录
                    //}
                    res = "104";
                }
                return res;
            }
            //else
            //{
            //    return "-8000";
            //}
        }
        #endregion

        #region 跌倒/坠床事件上报5.31
        public string EventDdzcReport(aers_tbl_events_ddzc model, string ReUId, string LoginKey)
        {
           // if (HeartRate(ReUId, LoginKey))
            {

                if (model.FillStaff.Length != 10)
                {
                    return "104操作员不能为空";
                }
                string res = "104";
                aers_tbl_events_ddzcSqlMapDao aers_tbl_events_ddzcdao = new aers_tbl_events_ddzcSqlMapDao();
                aers_tbl_eventsresumeSqlMapDao aers_tbl_eventsresumedao = new aers_tbl_eventsresumeSqlMapDao();
                aers_tbl_events_yc_partsSqlMapDao aers_tbl_events_yc_partsdao = new aers_tbl_events_yc_partsSqlMapDao();
                //新增 不良事件汇总记录
                aers_tbl_eventsresume aers_eventsresume = new aers_tbl_eventsresume();
                aers_eventsresume.EveresName = "152";
                aers_eventsresume.HospDepId = model.HospDepId;
                aers_eventsresume.HospId = model.HospId;
                aers_eventsresume.EveresLevel = model.EventLevel;
                aers_eventsresume.HappenedDate = model.HappenedDate;
                model.SendtoDate = DateTime.Now;
                aers_eventsresume.SendtoDate = model.SendtoDate;
                aers_eventsresume.SpellNo = "DDZC";
                aers_eventsresume.OperatorID = model.FillStaff;
                aers_eventsresume.ExamineState = model.ExamineState;
                aers_eventsresume.FileName = model.FileName;


                string Deleteid = "";
                try
                {
                    if (string.IsNullOrEmpty(model.EveresId))
                    {
                        string aers_eventsresume_id = aers_tbl_eventsresumedao.instert(aers_eventsresume);
                        Deleteid = aers_eventsresume_id;
                        //新增职业暴露事件记录
                        model.EveresId = aers_eventsresume_id;
                        string EveddzcId = aers_tbl_events_ddzcdao.Insert(model);
                        foreach (aers_tbl_events_yc_parts item in model.Parts)
                        {
                            //新增压疮事件-压疮体位记录
                            item.EveycId = EveddzcId;
                            item.OperatorID = model.FillStaff;
                            item.EveresName = aers_eventsresume.EveresName;
                            aers_tbl_events_yc_partsdao.Insert(item);
                        }
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }
                    else
                    {
                        aers_eventsresume.EveresId = model.EveresId;
                        aers_tbl_eventsresumedao.Update(aers_eventsresume);

                        aers_tbl_events_ddzcdao.Update(model);

                        aers_tbl_events_yc_partsdao.Delete(model.EveddzcId);

                        foreach (aers_tbl_events_yc_parts item in model.Parts)
                        {
                            //修改压疮事件-压疮体位记录
                            item.EveycId = model.EveddzcId;
                            item.OperatorID = model.FillStaff;
                            item.EveresName = aers_eventsresume.EveresName;
                            aers_tbl_events_yc_partsdao.Insert(item);
                        }
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }
                    res = "103";
                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(Deleteid))
                    {
                        aers_tbl_eventsresumedao.Delete(Deleteid);
                    }
                    res = "104";
                }
                return res;
            }
            //else
            //{
            //    return "-8000";
            //}
        }
        #endregion

        #region 跌倒/坠床事件上报8.4
        public string EventDdzcReportNew(aers_tbl_events_ddzc model)
        {
            // if (HeartRate(ReUId, LoginKey))
            {

                if (model.FillStaff.Length != 10)
                {
                    return "104操作员不能为空";
                }
                string res = "104";
                aers_tbl_events_ddzcSqlMapDao aers_tbl_events_ddzcdao = new aers_tbl_events_ddzcSqlMapDao();
                aers_tbl_eventsresumeSqlMapDao aers_tbl_eventsresumedao = new aers_tbl_eventsresumeSqlMapDao();
                aers_tbl_events_yc_partsSqlMapDao aers_tbl_events_yc_partsdao = new aers_tbl_events_yc_partsSqlMapDao();
                //新增 不良事件汇总记录
                aers_tbl_eventsresume aers_eventsresume = new aers_tbl_eventsresume();
                aers_eventsresume.EveresName = "152";
                aers_eventsresume.HospDepId = model.HospDepId;
                aers_eventsresume.HospId = model.HospId;
                aers_eventsresume.EveresLevel = model.EventLevel;
                aers_eventsresume.HappenedDate = model.HappenedDate;
                model.SendtoDate = DateTime.Now;
                aers_eventsresume.SendtoDate = model.SendtoDate;
                aers_eventsresume.SpellNo = "DDZC";
                aers_eventsresume.OperatorID = model.FillStaff;
                aers_eventsresume.ExamineState = model.ExamineState;
                aers_eventsresume.FileName = model.FileName;

                string reId;
                string Deleteid = "";
                try
                {
                    if (string.IsNullOrEmpty(model.EveresId))
                    {
                        string aers_eventsresume_id = aers_tbl_eventsresumedao.instert(aers_eventsresume);
                        Deleteid = aers_eventsresume_id;
                        //新增职业暴露事件记录
                        model.EveresId = aers_eventsresume_id;
                        string EveddzcId = aers_tbl_events_ddzcdao.Insert(model);
                        reId = model.EveresId;
                        foreach (aers_tbl_events_yc_parts item in model.Parts)
                        {
                            //新增压疮事件-压疮体位记录
                            item.EveycId = EveddzcId;
                            item.OperatorID = model.FillStaff;
                            item.EveresName = aers_eventsresume.EveresName;
                            aers_tbl_events_yc_partsdao.Insert(item);
                        }
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }
                    else
                    {
                        aers_eventsresume.EveresId = model.EveresId;
                        aers_tbl_eventsresumedao.Update(aers_eventsresume);

                        aers_tbl_events_ddzcdao.Update(model);

                        aers_tbl_events_yc_partsdao.Delete(model.EveddzcId);
                        reId = model.EveresId;
                        foreach (aers_tbl_events_yc_parts item in model.Parts)
                        {
                            //修改压疮事件-压疮体位记录
                            item.EveycId = model.EveddzcId;
                            item.OperatorID = model.FillStaff;
                            item.EveresName = aers_eventsresume.EveresName;
                            aers_tbl_events_yc_partsdao.Insert(item);
                        }
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }
                    res = reId ;
                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(Deleteid))
                    {
                        aers_tbl_eventsresumedao.Delete(Deleteid);
                    }
                    res = "104";
                }
                return res;
            }
            //else
            //{
            //    return "-8000";
            //}
        }
        #endregion

        #region 给药事件上报5.31
        public string EventGyReport(aers_tbl_events_gy model, string ReUId, string LoginKey)
        {
          //  if (HeartRate(ReUId, LoginKey))
            {
                if (model.FillStaff.Length != 10)
                {
                    return "104操作员不能为空";
                }
                string res = "104";
                aers_tbl_events_gySqlMapDao aers_tbl_events_gydao = new aers_tbl_events_gySqlMapDao();
                aers_tbl_eventsresumeSqlMapDao aers_tbl_eventsresumedao = new aers_tbl_eventsresumeSqlMapDao();

                //新增 不良事件汇总记录
                aers_tbl_eventsresume aers_eventsresume = new aers_tbl_eventsresume();
                aers_eventsresume.EveresName = "155";
                aers_eventsresume.HospDepId = model.HospDepId;
                aers_eventsresume.HospId = model.HospId;
                aers_eventsresume.EveresLevel = model.EventLevel;
                aers_eventsresume.HappenedDate = model.HappenedDate;
                model.SendtoDate = DateTime.Now;
                aers_eventsresume.SendtoDate = model.SendtoDate;
                aers_eventsresume.SpellNo = "GY";
                aers_eventsresume.OperatorID = model.FillStaff;
                aers_eventsresume.ExamineState = model.ExamineState;
                aers_eventsresume.FileName = model.FileName;


                string Deleteid = "";
                try
                {
                    if (string.IsNullOrEmpty(model.EveresId))
                    {
                        string aers_eventsresume_id = aers_tbl_eventsresumedao.instert(aers_eventsresume);
                        Deleteid = aers_eventsresume_id;
                        //新增职业暴露事件记录
                        model.EveresId = aers_eventsresume_id;
                        aers_tbl_events_gydao.Insert(model);
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }
                    else
                    {
                        aers_eventsresume.EveresId = model.EveresId;
                        aers_tbl_eventsresumedao.Update(aers_eventsresume);

                        aers_tbl_events_gydao.Update(model);
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }
                    res = "103";
                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(Deleteid))
                    {
                        aers_tbl_eventsresumedao.Delete(Deleteid);
                    }
                    res = "104" + ex.Message;
                }
                return res;
            }
            //else
            //{
            //    return "-8000";
            //}
        }
        #endregion

        #region 给药事件上报8.4
        public string EventGyReportNew(aers_tbl_events_gy model)
        {
            //  if (HeartRate(ReUId, LoginKey))
            {
                if (model.FillStaff.Length != 10)
                {
                    return "104操作员不能为空";
                }
                string res = "104";
                aers_tbl_events_gySqlMapDao aers_tbl_events_gydao = new aers_tbl_events_gySqlMapDao();
                aers_tbl_eventsresumeSqlMapDao aers_tbl_eventsresumedao = new aers_tbl_eventsresumeSqlMapDao();

                //新增 不良事件汇总记录
                aers_tbl_eventsresume aers_eventsresume = new aers_tbl_eventsresume();
                aers_eventsresume.EveresName = "155";
                aers_eventsresume.HospDepId = model.HospDepId;
                aers_eventsresume.HospId = model.HospId;
                aers_eventsresume.EveresLevel = model.EventLevel;
                aers_eventsresume.HappenedDate = model.HappenedDate;
                model.SendtoDate = DateTime.Now;
                aers_eventsresume.SendtoDate = model.SendtoDate;
                aers_eventsresume.SpellNo = "GY";
                aers_eventsresume.OperatorID = model.FillStaff;
                aers_eventsresume.ExamineState = model.ExamineState;
                aers_eventsresume.FileName = model.FileName;

                string reId;
                string Deleteid = "";
                try
                {
                    if (string.IsNullOrEmpty(model.EveresId))
                    {
                        string aers_eventsresume_id = aers_tbl_eventsresumedao.instert(aers_eventsresume);
                        Deleteid = aers_eventsresume_id;
                        //新增职业暴露事件记录
                        model.EveresId = aers_eventsresume_id;
                        reId = model.EveresId;
                        aers_tbl_events_gydao.Insert(model);
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }
                    else
                    {
                        aers_eventsresume.EveresId = model.EveresId;
                        aers_tbl_eventsresumedao.Update(aers_eventsresume);
                        reId = model.EveresId;
                        aers_tbl_events_gydao.Update(model);
                        if (model.ExamineState != "3")
                        {
                            ExecStatus(model.EveresId, model.OperatorID);
                        }
                    }
                    res = reId ;
                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(Deleteid))
                    {
                        aers_tbl_eventsresumedao.Delete(Deleteid);
                    }
                    res = "104";
                }
                return res;
            }
            //else
            //{
            //    return "-8000";
            //}
        }
        #endregion

        #region 调用身份验证 YM  2017.6.1
        public bool HeartRate(string Ruid, string LoginKey)
        {
            aers_tbl_LoginKeySqlMapDao ldao = new aers_tbl_LoginKeySqlMapDao();
            aers_tbl_LoginKey lk = ldao.FindLoginKeyByReuId(Ruid);

            if (lk != null)
            {
                if (lk.LoginKey == LoginKey)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region  不良事件查询 - 根据汇总编号查询 压疮 详细信息 5.31
        public aers_tbl_events_yc findeventsycByEid(string eid, string ReUId, string LoginKey)
        {
           // if (HeartRate(ReUId, LoginKey))
            {
                aers_tbl_events_ycSqlMapDao aers_events_ycSqlMapDao = new aers_tbl_events_ycSqlMapDao();
                aers_tbl_events_yc aers_events_yc = new aers_tbl_events_yc();
                aers_events_yc = aers_events_ycSqlMapDao.FindByEud(eid);
                if (aers_events_yc == null)
                    return null;
                aers_tbl_staffSqlMapDao aers_staffSqlMapDao = new aers_tbl_staffSqlMapDao();
                aers_events_yc.FillStaff = aers_staffSqlMapDao.FindNameByRid(aers_events_yc.FillStaff);
                aers_tbl_hospdepSqlMapDao aers_hospdepSqlMapDao = new aers_tbl_hospdepSqlMapDao();
                aers_events_yc.HospDepId = aers_hospdepSqlMapDao.FindNameByDepId(aers_events_yc.HospDepId);
                aers_tbl_events_yc_partsSqlMapDao partsdao = new aers_tbl_events_yc_partsSqlMapDao();
                aers_tbl_eventsresumeSqlMapDao eventsdao = new aers_tbl_eventsresumeSqlMapDao();
                aers_tbl_eventsresume eventname = eventsdao.Find(eid);
                aers_events_yc.SendtoDate = eventname.SendtoDate;
                aers_events_yc.FileName = eventname.FileName;
                aers_events_yc.Parts = partsdao.FindByYcuid(aers_events_yc.EveycId, eventname.EveresName);



                return aers_events_yc;
            }
            //else
            //{
            //    aers_tbl_events_yc a = new aers_tbl_events_yc();
            //    a.EveresId = "-8000";
            //    return a;
            //}

        }
        #endregion

        #region 隐患 5.31
        public aers_tbl_events_yh findeventsyhByEid(string eid, string ReUId, string LoginKey)
        {
          //  if (HeartRate(ReUId, LoginKey))
            {
                aers_tbl_events_yhSqlMapDao dao = new aers_tbl_events_yhSqlMapDao();
                aers_tbl_events_yh aers_events = dao.FindByEud(eid);
                if (aers_events == null)
                    return null;
                aers_tbl_staffSqlMapDao aers_staffSqlMapDao = new aers_tbl_staffSqlMapDao();
                aers_events.FillStaff = aers_staffSqlMapDao.FindNameByRid(aers_events.FillStaff);
                aers_tbl_hospdepSqlMapDao aers_hospdepSqlMapDao = new aers_tbl_hospdepSqlMapDao();
                aers_events.HospDepId = aers_hospdepSqlMapDao.FindNameByDepId(aers_events.HospDepId);
                aers_tbl_eventsresumeSqlMapDao eventsdao = new aers_tbl_eventsresumeSqlMapDao();
                aers_tbl_eventsresume eventname = eventsdao.Find(eid);
                aers_events.SendtoDate = eventname.SendtoDate;
                aers_events.FileName = eventname.FileName;
                return aers_events;
            }
            //else
            //{
            //    aers_tbl_events_yh a = new aers_tbl_events_yh();
            //    a.EveresId = "-8000";
            //    return a;
            //}
        }
        #endregion

        #region 其他 5.31
        public aers_tbl_events_qt findeventsqtByEid(string eid, string ReUId, string LoginKey)
        {
          //  if (HeartRate(ReUId, LoginKey))
            {
                aers_tbl_events_qtSqlMapDao dao = new aers_tbl_events_qtSqlMapDao();
                aers_tbl_events_qt aers_events = dao.FindByEud(eid);
                if (aers_events == null)
                    return null;
                aers_tbl_staffSqlMapDao aers_staffSqlMapDao = new aers_tbl_staffSqlMapDao();
                aers_events.FillStaff = aers_staffSqlMapDao.FindNameByRid(aers_events.FillStaff);
                aers_tbl_hospdepSqlMapDao aers_hospdepSqlMapDao = new aers_tbl_hospdepSqlMapDao();
                aers_events.HospDepId = aers_hospdepSqlMapDao.FindNameByDepId(aers_events.HospDepId);
                aers_tbl_eventsresumeSqlMapDao eventsdao = new aers_tbl_eventsresumeSqlMapDao();
                aers_tbl_eventsresume eventname = eventsdao.Find(eid);
                aers_events.SendtoDate = eventname.SendtoDate;
                aers_events.FileName = eventname.FileName;
                return aers_events;
            }
            //else
            //{
            //    aers_tbl_events_qt a = new aers_tbl_events_qt();
            //    a.EveresId = "-8000";
            //    return a;
            //}
        }
        #endregion

        #region  给药 5.31
        public aers_tbl_events_gy findeventsgyByEid(string eid, string ReUId, string LoginKey)
        {
           // if (HeartRate(ReUId, LoginKey))
            {
                aers_tbl_events_gySqlMapDao dao = new aers_tbl_events_gySqlMapDao();
                aers_tbl_events_gy aers_events = dao.FindByEud(eid);
                if (aers_events == null)
                    return null;
                aers_tbl_staffSqlMapDao aers_staffSqlMapDao = new aers_tbl_staffSqlMapDao();
                aers_events.FillStaff = aers_staffSqlMapDao.FindNameByRid(aers_events.FillStaff);
                aers_tbl_hospdepSqlMapDao aers_hospdepSqlMapDao = new aers_tbl_hospdepSqlMapDao();
                aers_events.HospDepId = aers_hospdepSqlMapDao.FindNameByDepId(aers_events.HospDepId);
                aers_tbl_eventsresumeSqlMapDao eventsdao = new aers_tbl_eventsresumeSqlMapDao();
                aers_tbl_eventsresume eventname = eventsdao.Find(eid);
                aers_events.SendtoDate = eventname.SendtoDate;
                aers_events.FileName = eventname.FileName;
                return aers_events;
            }
            //else
            //{
            //    aers_tbl_events_gy a = new aers_tbl_events_gy();
            //    a.EveresId = "-8000";
            //    return a;
            //}
        }
        #endregion

        #region 管路 5.31
        public aers_tbl_events_gl findeventsglByEid(string eid, string ReUId, string LoginKey)
        {
           // if (HeartRate(ReUId, LoginKey))
            {
                aers_tbl_events_glSqlMapDao dao = new aers_tbl_events_glSqlMapDao();
                aers_tbl_events_gl aers_events = dao.FindByEud(eid);
                if (aers_events == null)
                    return null;
                aers_tbl_staffSqlMapDao aers_staffSqlMapDao = new aers_tbl_staffSqlMapDao();
                aers_events.FillStaff = aers_staffSqlMapDao.FindNameByRid(aers_events.FillStaff);
                aers_tbl_hospdepSqlMapDao aers_hospdepSqlMapDao = new aers_tbl_hospdepSqlMapDao();
                aers_events.HospDepId = aers_hospdepSqlMapDao.FindNameByDepId(aers_events.HospDepId);
                aers_tbl_eventsresumeSqlMapDao eventsdao = new aers_tbl_eventsresumeSqlMapDao();
                aers_tbl_eventsresume eventname = eventsdao.Find(eid);
                aers_events.SendtoDate = eventname.SendtoDate;
                aers_events.FileName = eventname.FileName;
                return aers_events;
            }
            //else
            //{
            //    aers_tbl_events_gl a = new aers_tbl_events_gl();
            //    a.EveresId = "-8000";
            //    return a;
            //}
        }
        #endregion

        #region 职业暴露 5.31
        public aers_tbl_events_zybl findeventszyblByEid(string eid, string ReUId, string LoginKey)
        {
           // if (HeartRate(ReUId, LoginKey))
            {
                aers_tbl_events_zyblSqlMapDao dao = new aers_tbl_events_zyblSqlMapDao();
                aers_tbl_events_zybl aers_events = dao.FindByEud(eid);
                if (aers_events == null)
                    return null;
                aers_tbl_staffSqlMapDao aers_staffSqlMapDao = new aers_tbl_staffSqlMapDao();
                aers_events.FillStaff = aers_staffSqlMapDao.FindNameByRid(aers_events.FillStaff);
                aers_tbl_hospdepSqlMapDao aers_hospdepSqlMapDao = new aers_tbl_hospdepSqlMapDao();
                aers_events.HospDepId = aers_hospdepSqlMapDao.FindNameByDepId(aers_events.HospDepId);
                aers_tbl_eventsresumeSqlMapDao eventsdao = new aers_tbl_eventsresumeSqlMapDao();
                aers_tbl_eventsresume eventname = eventsdao.Find(eid);
                aers_events.SendtoDate = eventname.SendtoDate;
                aers_events.FileName = eventname.FileName;
                return aers_events;
            }
            //else
            //{
            //    aers_tbl_events_zybl a = new aers_tbl_events_zybl();
            //    a.EveresId = "-8000";
            //    return a;
            //}
        }
        #endregion

        #region 跌倒坠床 5.31
        public aers_tbl_events_ddzc findeventsddzcByEid(string eid, string ReUId, string LoginKey)
        {
         //   if (HeartRate(ReUId, LoginKey))
            {
                aers_tbl_events_ddzcSqlMapDao dao = new aers_tbl_events_ddzcSqlMapDao();
                aers_tbl_events_ddzc aers_events = dao.FindByEud(eid);
                if (aers_events == null)
                    return null;
                aers_tbl_staffSqlMapDao aers_staffSqlMapDao = new aers_tbl_staffSqlMapDao();
                aers_events.FillStaff = aers_staffSqlMapDao.FindNameByRid(aers_events.FillStaff);
                aers_tbl_hospdepSqlMapDao aers_hospdepSqlMapDao = new aers_tbl_hospdepSqlMapDao();
                aers_events.HospDepId = aers_hospdepSqlMapDao.FindNameByDepId(aers_events.HospDepId);
                aers_tbl_eventsresumeSqlMapDao eventsdao = new aers_tbl_eventsresumeSqlMapDao();
                aers_tbl_events_yc_partsSqlMapDao partsdao = new aers_tbl_events_yc_partsSqlMapDao();
                aers_tbl_eventsresume eventname = eventsdao.Find(eid);
                aers_events.SendtoDate = eventname.SendtoDate;
                aers_events.FileName = eventname.FileName;
                aers_events.Parts = partsdao.FindByYcuid(aers_events.EveddzcId, eventname.EveresName);
                return aers_events;
            }
          //  else
            //{
            //    aers_tbl_events_ddzc a = new aers_tbl_events_ddzc();
            //    a.EveresId = "-8000";
            //    return a;
            //}
        }
        #endregion

        #region 查询一键上报事件的详细信息
        //查询一键上报事件的详细信息
        public view_tabl_onekey FindonkeyinfoByEid(string eid, string ReUId, string LoginKey)
        {
          //  if (HeartRate(ReUId, LoginKey))
            {
                aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
                DataSet ds = dao.Findonkeyinfos(eid);
                view_tabl_onekey model = new view_tabl_onekey();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    model.FillStaff = ds.Tables[0].Rows[0]["FillStaff"].ToString();
                    model.HappenedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["HappenedDate"]);
                    model.HospDepId = ds.Tables[0].Rows[0]["HospDepId"].ToString();
                    model.HospdepName = ds.Tables[0].Rows[0]["HospdepName"].ToString();
                    model.SendtoDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["SendtoDate"]);
                    model.EveresId = ds.Tables[0].Rows[0]["EveresId"].ToString();
                    model.EveresName = ds.Tables[0].Rows[0]["EveresName"].ToString();
                    model.ExamineState = Convert.ToInt32(ds.Tables[0].Rows[0]["ExamineState"]);
                    model.FeedbackState = Convert.ToInt32(ds.Tables[0].Rows[0]["FeedbackState"]);
                    model.IsFlag = Convert.ToInt32(ds.Tables[0].Rows[0]["IsFlag"]);
                    //model.HospId = ds.Tables[0].Rows[0]["HospId"].ToString();
                }
                return model;
            }
            //else
            //{
            //    view_tabl_onekey a = new view_tabl_onekey();
            //    a.EveresId = "-8000";
            //    return a;
            //}
        }
        #endregion

        #region 
        public aers_tbl_eventsresume findeventsByEid(string eid)
        {
            aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
            return dao.Find(eid);
        }
        #endregion

        #region 审核通过事件

        public void ExecStatus(string EveresId, string eud)
        {
            try
            {
                aers_tbl_staffSqlMapDao aers_staffSqlMapDao = new aers_tbl_staffSqlMapDao();
                aers_tbl_staff aers_staff = aers_staffSqlMapDao.FindStaffByRid(eud);
                if (aers_staff != null)
                {
                    if (aers_staff.RoleState.Contains("145"))
                    {
                        exevent(EveresId, "已通过");
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public string exevent(string eud, string fadeBack)
        {
            string res = "104";
            try
            {
                //更新事件汇总表信息
                aers_tbl_eventsresumeSqlMapDao edao = new aers_tbl_eventsresumeSqlMapDao();
                string _exstate = "0";
                if (!string.IsNullOrEmpty(fadeBack))
                    _exstate = "1";
                edao.UpdateState(_exstate, "1", eud);
                aers_tbl_eventsresume aers_eventsresume = edao.Find(eud);
                //更新子表信息
                switch (aers_eventsresume.EveresName)
                {
                    case "149":
                        aers_tbl_events_ycSqlMapDao ycdao = new aers_tbl_events_ycSqlMapDao();
                        ycdao.UpdateState(fadeBack, "1", eud);
                        break;
                    case "150":
                        aers_tbl_events_qtSqlMapDao qtdao = new aers_tbl_events_qtSqlMapDao();
                        qtdao.UpdateState(fadeBack, "1", eud);
                        break;
                    case "151":
                        // aers_tbl_events_ycSqlMapDao ycdao = new aers_tbl_events_ycSqlMapDao();
                        // ycdao.UpdateState(fadeBack, "1", eud);
                        break;
                    case "152":
                        aers_tbl_events_ddzcSqlMapDao ddzcdao = new aers_tbl_events_ddzcSqlMapDao();
                        ddzcdao.UpdateState(fadeBack, "1", eud);
                        break;
                    case "153":
                        aers_tbl_events_glSqlMapDao gldao = new aers_tbl_events_glSqlMapDao();
                        gldao.UpdateState(fadeBack, "1", eud);
                        break;
                    case "154":
                        aers_tbl_events_yhSqlMapDao yhdao = new aers_tbl_events_yhSqlMapDao();
                        yhdao.UpdateState(fadeBack, "1", eud);
                        break;
                    case "155":
                        aers_tbl_events_gySqlMapDao gydao = new aers_tbl_events_gySqlMapDao();
                        gydao.UpdateState(fadeBack, "1", eud);
                        break;
                    case "156":
                        aers_tbl_events_zyblSqlMapDao zybldao = new aers_tbl_events_zyblSqlMapDao();
                        zybldao.UpdateState(fadeBack, "1", eud);
                        break;
                    default:
                        break;
                }
                res = "103";
            }
            catch (Exception es)
            {
                res = "104";
            }
            return res;
        }
        #endregion

        #region 审核不通过事件
        public string unexevent(string eud, string fadeBack)
        {
            string res = "104";
            try
            {
                //更新事件汇总表信息
                aers_tbl_eventsresumeSqlMapDao edao = new aers_tbl_eventsresumeSqlMapDao();
                string _exstate = "0";
                if (!string.IsNullOrEmpty(fadeBack))
                    _exstate = "1";
                edao.UpdateState(_exstate, "2", eud);
                aers_tbl_eventsresume aers_eventsresume = edao.Find(eud);
                //更新子表信息
                switch (aers_eventsresume.EveresName)
                {
                    case "149":
                        aers_tbl_events_ycSqlMapDao ycdao = new aers_tbl_events_ycSqlMapDao();
                        ycdao.UpdateState(fadeBack, "2", eud);
                        break;
                    case "150":
                        aers_tbl_events_qtSqlMapDao qtdao = new aers_tbl_events_qtSqlMapDao();
                        qtdao.UpdateState(fadeBack, "2", eud);
                        break;
                    case "151":
                        // aers_tbl_events_ycSqlMapDao ycdao = new aers_tbl_events_ycSqlMapDao();
                        // ycdao.UpdateState(fadeBack, "1", eud);
                        break;
                    case "152":
                        aers_tbl_events_ddzcSqlMapDao ddzcdao = new aers_tbl_events_ddzcSqlMapDao();
                        ddzcdao.UpdateState(fadeBack, "2", eud);
                        break;
                    case "153":
                        aers_tbl_events_glSqlMapDao gldao = new aers_tbl_events_glSqlMapDao();
                        gldao.UpdateState(fadeBack, "2", eud);
                        break;
                    case "154":
                        aers_tbl_events_yhSqlMapDao yhdao = new aers_tbl_events_yhSqlMapDao();
                        yhdao.UpdateState(fadeBack, "2", eud);
                        break;
                    case "155":
                        aers_tbl_events_gySqlMapDao gydao = new aers_tbl_events_gySqlMapDao();
                        gydao.UpdateState(fadeBack, "2", eud);
                        break;
                    case "156":
                        aers_tbl_events_zyblSqlMapDao zybldao = new aers_tbl_events_zyblSqlMapDao();
                        zybldao.UpdateState(fadeBack, "1", eud);
                        break;
                    default:
                        break;
                }
                res = "103";
            }
            catch (Exception es)
            {
                res = "104";
            }
            return res;
        }
        #endregion

        #region 检查是否符合上报条件
        // checkevents checkonekey
        public bool CheckEvents()
        {
            aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
            return dao.checkevents();
        }
        public bool CheckOnekey(string uid, string yue)
        {
            aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
            return dao.checkonekey(uid, yue);
        }

        public string  CheckOnekeyNew(string uid, string yue)
        {
            aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
            return dao.checkonekeynew(uid, yue);
        }

        #endregion

        #region 返回 事件上报（不含零事件上报）、待审核、已审核、未通过 事件总数

        //返回 事件上报（不含零事件上报）、待审核、已审核、未通过 事件总数
        public string GetEventsCounts(string roles, string uid)
        {
            aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();

            aers_tbl_staffSqlMapDao aers_staffSqlMapDao = new aers_tbl_staffSqlMapDao();

            aers_tbl_hospdepSqlMapDao depdao = new aers_tbl_hospdepSqlMapDao();



            aers_tbl_staff aers_staff = aers_staffSqlMapDao.FindStaffByRid(uid);

            aers_tbl_hospdep aers_hospdep = depdao.Find(aers_staff.DepId);

            if (roles == "145")
            {
                uid = aers_hospdep.HospId;
            }
            else
            {
                uid = aers_staff.DepId;
            }

            DataSet _tmp_ds_w = dao.FindAllByRudCount(roles, "0", uid);
            DataSet _tmp_ds_p = dao.FindAllByRudCount(roles, "1", uid);
            DataSet _tmp_ds_n = dao.FindAllByRudCount(roles, "2", uid);

            return _tmp_ds_w.Tables[0].Rows.Count + "-" + _tmp_ds_p.Tables[0].Rows.Count + "-" + _tmp_ds_n.Tables[0].Rows.Count + "-" + DateTime.Now.Year + "-" + DateTime.Now.Month; ;
        }

        #endregion

        #region 
        //920
        public string GetEveCountByRegion(string nians)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("yue", typeof(System.String)));
            dt.Columns.Add(new DataColumn("qy", typeof(System.String)));
            dt.Columns.Add(new DataColumn("ysb", typeof(System.String)));
            dt.Columns.Add(new DataColumn("wsb", typeof(System.String)));
            dt.Columns.Add(new DataColumn("hj", typeof(System.String)));
            IList<Region> listRegion = GetRegion();



            DateTime time = Convert.ToDateTime(nians);



            DateTime time1 = new DateTime(time.Year, 1, 1);
            DateTime time2 = new DateTime(time.Year, 2, 1);

            aers_tbl_eventsresumeSqlMapDao daoeven = new aers_tbl_eventsresumeSqlMapDao();
            IList<aers_tbl_eventsresume> list = daoeven.GetEventsresumeList().Where(o => o.ExamineState == "1").ToList();

            aers_tbl_events_ycSqlMapDao dao = new aers_tbl_events_ycSqlMapDao();
            IList<aers_tbl_hospital> listhosp = dao.hospitalFindAll();

            foreach (aers_tbl_eventsresume item in list)
            {
                aers_tbl_hospital hosp = listhosp.FirstOrDefault(o => o.HospId == item.HospId);
                if (hosp != null)
                {
                    item.Address = hosp.Address;
                }
            }




            for (int i = 0; i < 12; i++)
            {
                DataRow now = dt.NewRow();

                foreach (Region item in listRegion)
                {
                    DataRow dr = dt.NewRow();


                    dr["yue"] = i + 1 + "月";
                    dr["qy"] = item.RegionName;
                    dr["ysb"] = GetEveCountByhospital("2", item.RegionID, time1.AddMonths(i + 1) + "").Count;
                    dr["wsb"] = GetEveCountByhospital("1", item.RegionID, time1.AddMonths(i + 1) + "").Count;
                    dr["hj"] = list.Where(o => o.Address == item.RegionName && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;

                    dt.Rows.Add(dr);
                }
            }

            ds.Tables.Add(dt);

            return ExecutDataSetToJson(ds);


        }
        #endregion

        #region 
        public DataSet GetEveCountByRegionJidu00000(int nians)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("yue", typeof(System.String)));
            dt.Columns.Add(new DataColumn("qy", typeof(System.String)));
            dt.Columns.Add(new DataColumn("ysb", typeof(System.String)));
            dt.Columns.Add(new DataColumn("wsb", typeof(System.String)));
            dt.Columns.Add(new DataColumn("hj", typeof(System.String)));
            IList<Region> listRegion = GetRegion();



            DateTime time = DateTime.Now;

            int nian = time.Year + nians;

            DateTime time1 = new DateTime(nian, 7, 1);
            DateTime time2 = new DateTime(nian, 10, 1);

            aers_tbl_eventsresumeSqlMapDao daoeven = new aers_tbl_eventsresumeSqlMapDao();
            IList<aers_tbl_eventsresume> list = daoeven.GetEventsresumeList().Where(o => o.ExamineState == "1").ToList();


            list = list.Where(o => o.EveresName == "151" && o.HappenedDate >= time1 && o.HappenedDate < time2 && o.HospId != "hp00000002").ToList();



            aers_tbl_events_ycSqlMapDao dao = new aers_tbl_events_ycSqlMapDao();
            IList<aers_tbl_hospital> listhosp = dao.hospitalFindAll();

            foreach (aers_tbl_eventsresume item in list)
            {
                aers_tbl_hospital hosp = listhosp.FirstOrDefault(o => o.HospId == item.HospId);
                if (hosp != null)
                {
                    item.Address = hosp.Address;
                }
            }





            foreach (Region item in listRegion)
            {
                DataRow dr = dt.NewRow();
                dr["yue"] = "第3季度";
                dr["qy"] = item.RegionName;
                dr["ysb"] = list.Where(o => o.Address == item.RegionName).ToList().Count;
                dr["wsb"] = "";
                dr["hj"] = list.Where(o => o.Address == item.RegionName).ToList().Count;

                dt.Rows.Add(dr);
            }

            ds.Tables.Add(dt);

            return ds;


        }
        #endregion

        #region 
        //1013
        public DataSet GetEveCountByRegionJidu(int nian)
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("yue", typeof(System.String)));
            dt.Columns.Add(new DataColumn("qy", typeof(System.String)));
            dt.Columns.Add(new DataColumn("ysb", typeof(System.String)));
            dt.Columns.Add(new DataColumn("wsb", typeof(System.String)));
            dt.Columns.Add(new DataColumn("hj", typeof(System.String)));
            IList<Region> listRegion = GetRegion();




            aers_tbl_eventsresumeSqlMapDao daoeven = new aers_tbl_eventsresumeSqlMapDao();
            IList<aers_tbl_eventsresume> list = daoeven.GetEventsresumeList().Where(o => o.ExamineState == "1" && o.EveresName != "151").ToList();

            aers_tbl_events_ycSqlMapDao dao = new aers_tbl_events_ycSqlMapDao();
            IList<aers_tbl_hospital> listhosp = dao.hospitalFindAll();

            foreach (aers_tbl_eventsresume item in list)
            {
                aers_tbl_hospital hosp = listhosp.FirstOrDefault(o => o.HospId == item.HospId);
                if (hosp != null)
                {
                    item.Address = hosp.Address;
                }
            }



            DateTime time1 = new DateTime(nian, 10, 1);
            DateTime time2 = new DateTime(2017, 1, 1);

            foreach (Region item in listRegion)
            {
                DataRow dr = dt.NewRow();
                dr["yue"] = "第4季度";
                dr["qy"] = item.RegionName;
                dr["ysb"] = GetEveCountByhospitaljidu("2", item.RegionID, "10", "1").Count;
                dr["wsb"] = GetEveCountByhospitaljidu("1", item.RegionID, "10", "1").Count;
                dr["hj"] = list.Where(o => o.Address == item.RegionName && o.HappenedDate >= time1 && o.HappenedDate < time2).ToList().Count;

                dt.Rows.Add(dr);
            }

            ds.Tables.Add(dt);

            return ds;


        }
        #endregion

        #region 
        public DataSet GetEveresLevelCountByRegionJidu()
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("yue", typeof(System.String)));
            dt.Columns.Add(new DataColumn("I级事件", typeof(System.String)));
            dt.Columns.Add(new DataColumn("II级事件", typeof(System.String)));
            dt.Columns.Add(new DataColumn("III级事件", typeof(System.String)));
            dt.Columns.Add(new DataColumn("IV级事件", typeof(System.String)));



            DateTime time = DateTime.Now;

            int nian = time.Year;



            aers_tbl_eventsresumeSqlMapDao daoeven = new aers_tbl_eventsresumeSqlMapDao();
            IList<aers_tbl_eventsresume> list = daoeven.GetEventsresumeList().Where(o => o.ExamineState == "1").ToList();


            DateTime time1 = new DateTime(nian, 1, 1);
            DateTime time2 = new DateTime(nian, 4, 1);

            DataRow dr = dt.NewRow();
            dr["yue"] = "第1季度";
            dr["I级事件"] = list.Where(o => o.HappenedDate >= time1 && o.HappenedDate < time2 && o.EveresLevel == "129").ToList().Count;
            dr["II级事件"] = list.Where(o => o.HappenedDate >= time1 && o.HappenedDate < time2 && o.EveresLevel == "130").ToList().Count;
            dr["III级事件"] = list.Where(o => o.HappenedDate >= time1 && o.HappenedDate < time2 && o.EveresLevel == "131").ToList().Count;
            dr["IV级事件"] = list.Where(o => o.HappenedDate >= time1 && o.HappenedDate < time2 && o.EveresLevel == "122").ToList().Count;

            dt.Rows.Add(dr);


            time1 = new DateTime(nian, 4, 1);
            time2 = new DateTime(nian, 7, 1);

            dr = dt.NewRow();
            dr["yue"] = "第2季度";
            dr["I级事件"] = list.Where(o => o.HappenedDate >= time1 && o.HappenedDate < time2 && o.EveresLevel == "129").ToList().Count;
            dr["II级事件"] = list.Where(o => o.HappenedDate >= time1 && o.HappenedDate < time2 && o.EveresLevel == "130").ToList().Count;
            dr["III级事件"] = list.Where(o => o.HappenedDate >= time1 && o.HappenedDate < time2 && o.EveresLevel == "131").ToList().Count;
            dr["IV级事件"] = list.Where(o => o.HappenedDate >= time1 && o.HappenedDate < time2 && o.EveresLevel == "122").ToList().Count;

            dt.Rows.Add(dr);



            time1 = new DateTime(nian, 7, 1);
            time2 = new DateTime(nian, 10, 1);

            dr = dt.NewRow();
            dr["yue"] = "第3季度";
            dr["I级事件"] = list.Where(o => o.HappenedDate >= time1 && o.HappenedDate < time2 && o.EveresLevel == "129").ToList().Count;
            dr["II级事件"] = list.Where(o => o.HappenedDate >= time1 && o.HappenedDate < time2 && o.EveresLevel == "130").ToList().Count;
            dr["III级事件"] = list.Where(o => o.HappenedDate >= time1 && o.HappenedDate < time2 && o.EveresLevel == "131").ToList().Count;
            dr["IV级事件"] = list.Where(o => o.HappenedDate >= time1 && o.HappenedDate < time2 && o.EveresLevel == "122").ToList().Count;

            dt.Rows.Add(dr);

            ds.Tables.Add(dt);

            return ds;


        }
        #endregion

        #region 
        /// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="path"></param>
        /// <returns></returns>

        public IList<aers_tbl_hospital> GetEveCountByhospital(string Status, string Region, string yue)
        {
            aers_tbl_events_ycSqlMapDao dao = new aers_tbl_events_ycSqlMapDao();
            aers_tbl_eventsresumeSqlMapDao daoeven = new aers_tbl_eventsresumeSqlMapDao();

            IList<aers_tbl_hospital> listdep = dao.hospitalFindAll();

            DateTime time1 = Convert.ToDateTime(yue);
            DateTime time2 = Convert.ToDateTime(yue).AddMonths(1);


            IList<aers_tbl_eventsresume> listeven = daoeven.GetEventsresumeList().Where(o => o.ExamineState == "1" && o.HappenedDate >= time1 && o.HappenedDate < time2).ToList();


            foreach (aers_tbl_hospital item in listdep)
            {
                item.EveCount = listeven.Where(o => o.HospId == item.HospId).ToList().Count;
                item.Phone = "";
            }

            if (Status == "1")
            {
                listdep = listdep.Where(o => o.EveCount == 0).ToList();
            }
            else if (Status == "2")
            {
                listdep = listdep.Where(o => o.EveCount > 0).ToList();
            }


            if (!string.IsNullOrEmpty(Region))
            {
                Region rr = GetRegion().FirstOrDefault(o => o.RegionID == Region);
                if (rr != null)
                {
                    listdep = listdep.Where(o => o.Address == rr.RegionName).ToList();
                }


            }

            return listdep;
        }
        #endregion

        #region 
        public IList<aers_tbl_hospital> GetEveCountByhospitaljidu(string Status, string Region, string yue1, string yue2)
        {
            aers_tbl_events_ycSqlMapDao dao = new aers_tbl_events_ycSqlMapDao();
            aers_tbl_eventsresumeSqlMapDao daoeven = new aers_tbl_eventsresumeSqlMapDao();

            IList<aers_tbl_hospital> listdep = dao.hospitalFindAll();

            DateTime time1 = new DateTime(DateTime.Now.Year, Convert.ToInt32(yue1), 1);
            DateTime time2 = new DateTime(DateTime.Now.Year, Convert.ToInt32(yue2), 1);



            IList<aers_tbl_eventsresume> listeven = daoeven.GetEventsresumeList().Where(o => o.ExamineState == "1" && o.HappenedDate >= time1 && o.HappenedDate < time2).ToList();


            foreach (aers_tbl_hospital item in listdep)
            {
                item.EveCount = listeven.Where(o => o.HospId == item.HospId).ToList().Count;
                item.Phone = "";
            }

            if (Status == "1")
            {
                listdep = listdep.Where(o => o.EveCount == 0).ToList();
            }
            else if (Status == "2")
            {
                listdep = listdep.Where(o => o.EveCount > 0).ToList();
            }


            if (!string.IsNullOrEmpty(Region))
            {
                Region rr = GetRegion().FirstOrDefault(o => o.RegionID == Region);
                if (rr != null)
                {
                    listdep = listdep.Where(o => o.Address == rr.RegionName).ToList();
                }


            }

            return listdep;
        }
        #endregion

        #region 
        public IList<aers_tbl_hospital> GethospitalUnion()
        {
            aers_tbl_events_ycSqlMapDao dao = new aers_tbl_events_ycSqlMapDao();
            IList<aers_tbl_hospital> listdep = dao.GethospitalUnion();

            //敏感字段数据
            foreach (aers_tbl_hospital item in listdep)
            {
                item.Contact = "敏感字段数据";
                item.Phone = "隐藏字段数据";
            }


            return listdep;
        }
        #endregion
        //1020
        #region  事件分别统计分析

        public string GetEventReport(string yue1, string yue2, string Region, string HospId)
        {
            aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
            aers_tbl_events_ddzcSqlMapDao ddzcdao = new aers_tbl_events_ddzcSqlMapDao();

            DateTime time1 = Convert.ToDateTime(yue1);
            DateTime time2 = Convert.ToDateTime(yue2).AddDays(1);


            IList<aers_tbl_events_ddzc> list = ddzcdao.FindListByData(time1).Where(o => o.HappenedDate < time2).ToList();

            IList<aers_tbl_eventsresume> listsj = dao.GetEventsresumeList().Where(o => o.ExamineState == "1" && o.HappenedDate >= time1 && o.HappenedDate < time2).ToList();


            aers_tbl_events_ycSqlMapDao hospdao = new aers_tbl_events_ycSqlMapDao();
            IList<aers_tbl_hospital> listhosp = hospdao.hospitalFindAll();

            foreach (aers_tbl_events_ddzc item in list)
            {
                aers_tbl_hospital hosp = listhosp.FirstOrDefault(o => o.HospId == item.HospId);
                if (hosp != null)
                {
                    item.Address = hosp.Address;
                }
            }
            foreach (aers_tbl_eventsresume item in listsj)
            {
                aers_tbl_hospital hosp = listhosp.FirstOrDefault(o => o.HospId == item.HospId);
                if (hosp != null)
                {
                    item.Address = hosp.Address;
                }
            }


            if (!string.IsNullOrEmpty(Region))
            {
                IList<Region> listRegion = GetRegion();
                Region re = listRegion.FirstOrDefault(o => o.RegionID == Region);
                list = list.Where(o => o.Address == re.RegionName).ToList();
                listsj = listsj.Where(o => o.Address == re.RegionName).ToList();
            }

            if (!string.IsNullOrEmpty(HospId))
            {
                list = list.Where(o => o.HospId == HospId).ToList();
                listsj = listsj.Where(o => o.HospId == HospId).ToList();
            }




            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Id", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Name", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Value", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Ratio", typeof(System.String)));


            decimal count = 0;
            decimal cl = 0;





            DataRow dr = dt.NewRow();
            count = list.Where(o => o.DdzcType == "242").ToList().Count;
            cl = count * 100 / listsj.Count;

            dr["Id"] = "242";
            dr["Name"] = "跌倒事件";
            dr["Value"] = count;
            dr["Ratio"] = Math.Round(cl, 0) + "%";
            dt.Rows.Add(dr);



            dr = dt.NewRow();
            count = list.Where(o => o.DdzcType == "243").ToList().Count;
            cl = count * 100 / listsj.Count;

            dr["Id"] = "243";
            dr["Name"] = "坠床事件";
            dr["Value"] = count;
            dr["Ratio"] = Math.Round(cl, 0) + "%";
            dt.Rows.Add(dr);


            dr = dt.NewRow();
            count = listsj.Where(o => o.EveresName == "153").ToList().Count;
            cl = count * 100 / listsj.Count;

            dr["Id"] = "153";
            dr["Name"] = "管路事件";
            dr["Value"] = count;
            dr["Ratio"] = Math.Round(cl, 0) + "%";
            dt.Rows.Add(dr);




            dr = dt.NewRow();
            count = listsj.Where(o => o.EveresName == "154").ToList().Count;
            cl = count * 100 / listsj.Count;

            dr["Id"] = "154";
            dr["Name"] = "隐患事件";
            dr["Value"] = count;
            dr["Ratio"] = Math.Round(cl, 0) + "%";
            dt.Rows.Add(dr);



            dr = dt.NewRow();
            count = listsj.Where(o => o.EveresName == "155").ToList().Count;
            cl = count * 100 / listsj.Count;

            dr["Id"] = "155";
            dr["Name"] = "给药事件";
            dr["Value"] = count;
            dr["Ratio"] = Math.Round(cl, 0) + "%";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            count = listsj.Where(o => o.EveresName == "156").ToList().Count;
            cl = count * 100 / listsj.Count;

            dr["Id"] = "156";
            dr["Name"] = "职业暴露";
            dr["Value"] = count;
            dr["Ratio"] = Math.Round(cl, 0) + "%";
            dt.Rows.Add(dr);



            dr = dt.NewRow();
            count = listsj.Where(o => o.EveresName == "149").ToList().Count;
            cl = count * 100 / listsj.Count;

            dr["Id"] = "149";
            dr["Name"] = "压疮评估";
            dr["Value"] = count;
            dr["Ratio"] = Math.Round(cl, 0) + "%";
            dt.Rows.Add(dr);



            dr = dt.NewRow();
            count = listsj.Where(o => o.EveresName == "150").ToList().Count;
            cl = count * 100 / listsj.Count;

            dr["Id"] = "150";
            dr["Name"] = "其他事件";
            dr["Value"] = count;
            dr["Ratio"] = Math.Round(cl, 0) + "%";
            dt.Rows.Add(dr);




            ds.Tables.Add(dt);

            return ExecutDataSetToJson(ds);
        }
        #endregion

        #region 
        public string GetEventddzcReport(string yue1, string yue2, string Region, string HospId)
        {

            aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
            aers_tbl_events_ddzcSqlMapDao ddzcdao = new aers_tbl_events_ddzcSqlMapDao();



            aers_sys_statecodeSqlMapDao codedao = new aers_sys_statecodeSqlMapDao();
            IList<aers_sys_statecode> codelist = codedao.FindAll();

            DateTime time1 = Convert.ToDateTime(yue1);
            DateTime time2 = Convert.ToDateTime(yue2).AddDays(1);


            IList<aers_tbl_events_ddzc> list = ddzcdao.FindListByData(time1).Where(o => o.HappenedDate < time2 && o.DdzcType == "242").ToList();



            aers_tbl_events_ycSqlMapDao hospdao = new aers_tbl_events_ycSqlMapDao();
            IList<aers_tbl_hospital> listhosp = hospdao.hospitalFindAll();

            foreach (aers_tbl_events_ddzc item in list)
            {
                aers_tbl_hospital hosp = listhosp.FirstOrDefault(o => o.HospId == item.HospId);
                if (hosp != null)
                {
                    item.Address = hosp.Address;
                }

                try
                {
                    item.Age = Convert.ToInt32(item.PatientAge);
                }
                catch (Exception)
                {
                    item.Age = 0;
                }

            }


            if (!string.IsNullOrEmpty(Region))
            {
                IList<Region> listRegion = GetRegion();
                Region re = listRegion.FirstOrDefault(o => o.RegionID == Region);
                list = list.Where(o => o.Address == re.RegionName).ToList();
            }

            if (!string.IsNullOrEmpty(HospId))
            {
                list = list.Where(o => o.HospId == HospId).ToList();
            }




            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Id", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Name", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Value", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Ratio", typeof(System.String)));



            decimal count = 0;
            decimal cl = 0;
            DataRow dr = dt.NewRow();


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "level").ToList())
            {
                count = list.Where(o => o.EventLevel == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "EventLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "EventLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            count = list.Where(o => o.HappenedDate.Hour >= 8 && o.HappenedDate.Hour < 16).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "8-4组";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "8-4组";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.HappenedDate.Hour >= 16 && o.HappenedDate.Hour < 24).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "4-12组";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "4-12组";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.HappenedDate.Hour >= 0 && o.HappenedDate.Hour < 8).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "12-8组";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "12-8组";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.Age >= 0 && o.Age <= 20).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "0-20岁";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "0-20岁";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.Age > 20 && o.Age <= 40).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "20-40岁";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "20-40岁";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }



            count = list.Where(o => o.Age > 40 && o.Age <= 60).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "40-60岁";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "40-60岁";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.Age > 60).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "60岁以上";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "60岁以上";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }

            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "patientSex").ToList())
            {
                count = list.Where(o => o.PatientSex == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "patientSex";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "patientSex";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }



            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "level").ToList())
            {
                count = list.Where(o => o.NursLevel == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "NursLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "NursLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "isOrNot").ToList())
            {
                count = list.Where(o => o.IsEvaluate == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "IsEvaluate";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {

                    dr = dt.NewRow();
                    dr["Id"] = "IsEvaluate";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "fallPlace").ToList())
            {
                count = list.Where(o => o.DzPlace == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "DzPlace";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "DzPlace";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "fallState").ToList())
            {
                count = list.Where(o => o.DzState == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "DzState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "DzState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }



            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "mentalState").ToList())
            {
                count = list.Where(o => o.MentalState == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "MentalState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "MentalState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }
            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "selfLifeAbility").ToList())
            {
                count = list.Where(o => o.SelfcareState == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "SelfcareState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "SelfcareState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "dangerReason").ToList())
            {
                count = list.Where(o => o.DzHazards.Contains(item.ECodeValue)).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "DzHazards";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "DzHazards";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "fallHistory").ToList())
            {
                count = list.Where(o => o.DzHistory == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "DzHistory";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "DzHistory";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "isOrNot").ToList())
            {
                count = list.Where(o => o.IsTakePrevent.Contains(item.ECodeValue)).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "IsTakePrevent";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "IsTakePrevent";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "groundState").ToList())
            {
                count = list.Where(o => o.GroundState.Contains(item.ECodeValue)).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "GroundState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "GroundState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "indoorState").ToList())
            {
                count = list.Where(o => o.IndoorState == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "IndoorState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "IndoorState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "isOrNot").ToList())
            {
                count = list.Where(o => o.EscortState == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "EscortState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "EscortState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "position").ToList())
            {
                count = list.Where(o => o.StaffPosition == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "StaffPosition";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "StaffPosition";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "education").ToList())
            {
                count = list.Where(o => o.StaffEducation == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "StaffEducation";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = list.Where(o => o.StaffEducation == item.ECodeValue).ToList().Count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "StaffEducation";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "workYear").ToList())
            {
                count = list.Where(o => o.StaffWorkyears == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "StaffWorkyears";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = list.Where(o => o.StaffWorkyears == item.ECodeValue).ToList().Count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "StaffWorkyears";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "staffType").ToList())
            {
                count = list.Where(o => o.StaffType == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "StaffType";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = list.Where(o => o.StaffType == item.ECodeValue).ToList().Count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "StaffType";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }





            ds.Tables.Add(dt);
            return ExecutDataSetToJson(ds);
        }
        #endregion

        #region 
        public string GetEventZcReport(string yue1, string yue2, string Region, string HospId)
        {

            aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
            aers_tbl_events_ddzcSqlMapDao ddzcdao = new aers_tbl_events_ddzcSqlMapDao();



            aers_sys_statecodeSqlMapDao codedao = new aers_sys_statecodeSqlMapDao();
            IList<aers_sys_statecode> codelist = codedao.FindAll();

            DateTime time1 = Convert.ToDateTime(yue1);
            DateTime time2 = Convert.ToDateTime(yue2).AddDays(1);



            IList<aers_tbl_events_ddzc> list = ddzcdao.FindListByData(time1).Where(o => o.HappenedDate < time2 && o.DdzcType == "243").ToList();



            aers_tbl_events_ycSqlMapDao hospdao = new aers_tbl_events_ycSqlMapDao();
            IList<aers_tbl_hospital> listhosp = hospdao.hospitalFindAll();

            foreach (aers_tbl_events_ddzc item in list)
            {
                aers_tbl_hospital hosp = listhosp.FirstOrDefault(o => o.HospId == item.HospId);
                if (hosp != null)
                {
                    item.Address = hosp.Address;
                }
                try
                {
                    item.Age = Convert.ToInt32(item.PatientAge);
                }
                catch (Exception)
                {
                    item.Age = 0;
                }
            }


            if (!string.IsNullOrEmpty(Region))
            {
                IList<Region> listRegion = GetRegion();
                Region re = listRegion.FirstOrDefault(o => o.RegionID == Region);
                list = list.Where(o => o.Address == re.RegionName).ToList();
            }

            if (!string.IsNullOrEmpty(HospId))
            {
                list = list.Where(o => o.HospId == HospId).ToList();
            }




            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Id", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Name", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Value", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Ratio", typeof(System.String)));



            decimal count = 0;
            decimal cl = 0;
            DataRow dr = dt.NewRow();





            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "level").ToList())
            {
                count = list.Where(o => o.EventLevel == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "EventLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "EventLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            count = list.Where(o => o.HappenedDate.Hour >= 8 && o.HappenedDate.Hour < 16).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "8-4组";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "8-4组";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.HappenedDate.Hour >= 16 && o.HappenedDate.Hour < 24).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "4-12组";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "4-12组";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.HappenedDate.Hour >= 0 && o.HappenedDate.Hour < 8).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "12-8组";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "12-8组";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.Age >= 0 && o.Age <= 20).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "0-20岁";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "0-20岁";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.Age > 20 && o.Age <= 40).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "20-40岁";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "20-40岁";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }



            count = list.Where(o => o.Age > 40 && o.Age <= 60).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "40-60岁";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "40-60岁";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.Age > 60).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "60岁以上";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "60岁以上";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "patientSex").ToList())
            {
                count = list.Where(o => o.PatientSex == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "patientSex";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "patientSex";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }



            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "level").ToList())
            {
                count = list.Where(o => o.NursLevel == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "NursLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "NursLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "isOrNot").ToList())
            {
                count = list.Where(o => o.IsEvaluate == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "IsEvaluate";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "IsEvaluate";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "fallPlace").ToList())
            {
                count = list.Where(o => o.DzPlace == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "DzPlace";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "DzPlace";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "fallState").ToList())
            {
                count = list.Where(o => o.DzState == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "DzState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "DzState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }



            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "mentalState").ToList())
            {
                count = list.Where(o => o.MentalState == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "MentalState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "MentalState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }
            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "selfLifeAbility").ToList())
            {
                count = list.Where(o => o.SelfcareState == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "SelfcareState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "SelfcareState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "dangerReason").ToList())
            {
                count = list.Where(o => o.DzHazards.Contains(item.ECodeValue)).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "DzHazards";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "DzHazards";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "fallHistory").ToList())
            {
                count = list.Where(o => o.DzHistory == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "DzHistory";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "DzHistory";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "isOrNot").ToList())
            {
                count = list.Where(o => o.IsTakePrevent.Contains(item.ECodeValue)).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "IsTakePrevent";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "IsTakePrevent";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "groundState").ToList())
            {
                count = list.Where(o => o.GroundState.Contains(item.ECodeValue)).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "GroundState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "GroundState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "indoorState").ToList())
            {
                count = list.Where(o => o.IndoorState == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "IndoorState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "IndoorState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "isOrNot").ToList())
            {
                count = list.Where(o => o.EscortState == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "EscortState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "EscortState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "position").ToList())
            {
                count = list.Where(o => o.StaffPosition == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "StaffPosition";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "StaffPosition";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "education").ToList())
            {
                count = list.Where(o => o.StaffEducation == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "StaffEducation";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = list.Where(o => o.StaffEducation == item.ECodeValue).ToList().Count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "StaffEducation";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "workYear").ToList())
            {
                count = list.Where(o => o.StaffWorkyears == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "StaffWorkyears";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "StaffWorkyears";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "staffType").ToList())
            {
                count = list.Where(o => o.StaffType == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "StaffType";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "StaffType";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }





            ds.Tables.Add(dt);
            return ExecutDataSetToJson(ds);
        }
        #endregion

        #region 
        public string GetEventGlReport(string yue1, string yue2, string Region, string HospId)
        {

            aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
            aers_tbl_events_glSqlMapDao gldao = new aers_tbl_events_glSqlMapDao();



            aers_sys_statecodeSqlMapDao codedao = new aers_sys_statecodeSqlMapDao();
            IList<aers_sys_statecode> codelist = codedao.FindAll();

            DateTime time1 = Convert.ToDateTime(yue1);
            DateTime time2 = Convert.ToDateTime(yue2).AddDays(1);



            IList<aers_tbl_events_gl> list = gldao.FindListByData(time1).Where(o => o.HappenedDate < time2).ToList();



            aers_tbl_events_ycSqlMapDao hospdao = new aers_tbl_events_ycSqlMapDao();
            IList<aers_tbl_hospital> listhosp = hospdao.hospitalFindAll();

            foreach (aers_tbl_events_gl item in list)
            {
                aers_tbl_hospital hosp = listhosp.FirstOrDefault(o => o.HospId == item.HospId);
                if (hosp != null)
                {
                    item.Address = hosp.Address;
                }
                try
                {
                    item.Age = Convert.ToInt32(item.PatientAge);
                }
                catch (Exception)
                {
                    item.Age = 0;
                }
            }


            if (!string.IsNullOrEmpty(Region))
            {
                IList<Region> listRegion = GetRegion();
                Region re = listRegion.FirstOrDefault(o => o.RegionID == Region);
                list = list.Where(o => o.Address == re.RegionName).ToList();
            }

            if (!string.IsNullOrEmpty(HospId))
            {
                list = list.Where(o => o.HospId == HospId).ToList();
            }




            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Id", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Name", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Value", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Ratio", typeof(System.String)));



            decimal count = 0;
            decimal cl = 0;
            DataRow dr = dt.NewRow();



            count = list.Where(o => o.HappenedDate.Hour >= 8 && o.HappenedDate.Hour < 16).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "8-4组";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "8-4组";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.HappenedDate.Hour >= 16 && o.HappenedDate.Hour < 24).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "4-12组";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "4-12组";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.HappenedDate.Hour >= 0 && o.HappenedDate.Hour < 8).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "12-8组";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "12-8组";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.Age >= 0 && o.Age <= 20).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "0-20岁";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "0-20岁";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.Age > 20 && o.Age <= 40).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "20-40岁";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "20-40岁";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }



            count = list.Where(o => o.Age > 40 && o.Age <= 60).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "40-60岁";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "40-60岁";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.Age > 60).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "60岁以上";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "60岁以上";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }



            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "patientSex").ToList())
            {
                count = list.Where(o => o.PatientSex == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "patientSex";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "patientSex";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "level").ToList())
            {
                count = list.Where(o => o.EventLevel == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "EventLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "EventLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "level").ToList())
            {
                count = list.Where(o => o.NursLevel == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "NursLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "NursLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "isOrNot").ToList())
            {
                count = list.Where(o => o.IsEvaluate == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "IsEvaluate";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "IsEvaluate";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "isOrNot").ToList())
            {
                count = list.Where(o => o.IsTakePrevent.Contains(item.ECodeValue)).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "IsTakePrevent";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "IsTakePrevent";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "tube").ToList())
            {
                count = list.Where(o => o.GlTypeOne == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "GlTypeOne";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "GlTypeOne";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "fixedWay").ToList())
            {
                count = list.Where(o => o.FixedWay == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "FixedWay";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "FixedWay";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "outTubeState").ToList())
            {
                count = list.Where(o => o.OutGlState == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "OutGlState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "OutGlState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            //YM2017.4.17 数据重复
            //foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "outTubeState").ToList())
            //{
            //    count = list.Where(o => o.OutGlState == item.ECodeValue).ToList().Count;
            //    if (count != 0)
            //    {
            //        cl = count * 100 / list.Count;
            //        dr = dt.NewRow();
            //        dr["Id"] = "OutGlState";
            //        dr["Name"] = item.ECodeTag;
            //        dr["Value"] = count;
            //        dr["Ratio"] = Math.Round(cl, 0) + "%";
            //        dt.Rows.Add(dr);
            //    }
            //    else
            //    {
            //        dr = dt.NewRow();
            //        dr["Id"] = "OutGlState";
            //        dr["Name"] = item.ECodeTag;
            //        dr["Value"] = count;
            //        dr["Ratio"] = count + "%";
            //        dt.Rows.Add(dr);
            //    }
            //}



            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "mentalState").ToList())
            {
                count = list.Where(o => o.MentalState == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "MentalState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "MentalState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "activeAbility").ToList())
            {
                count = list.Where(o => o.ActivityAbility == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "ActivityAbility";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "ActivityAbility";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);

                }
            }



            //foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "activeAbility").ToList())
            //{
            //    count = list.Where(o => o.ActivityAbility == item.ECodeValue).ToList().Count;
            //    if (count != 0)
            //    {
            //        cl = count * 100 / list.Count;
            //        dr = dt.NewRow();
            //        dr["Id"] = "ActivityAbility";
            //        dr["Name"] = item.ECodeTag;
            //        dr["Value"] = list.Where(o => o.ActivityAbility == item.ECodeValue).ToList().Count;
            //        dr["Ratio"] = Math.Round(cl, 0) + "%";
            //        dt.Rows.Add(dr);
            //    }
            //    else
            //    {
            //        dr = dt.NewRow();
            //        dr["Id"] = "ActivityAbility";
            //        dr["Name"] = item.ECodeTag;
            //        dr["Value"] = count;
            //        dr["Ratio"] = count + "%";
            //        dt.Rows.Add(dr);
            //    }
            //}


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "selfLifeAbility").ToList())
            {
                count = list.Where(o => o.SelfcareAbility == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "SelfcareAbility";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "SelfcareAbility";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }



            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "outTubeReason").ToList())
            {
                count = list.Where(o => o.OutGlReason == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "OutGlReason";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "OutGlReason";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }



            //foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "outTubeReason").ToList())
            //{
            //    count = list.Where(o => o.OutGlReason == item.ECodeValue).ToList().Count;
            //    if (count != 0)
            //    {
            //        cl = count * 100 / list.Count;
            //        dr = dt.NewRow();
            //        dr["Id"] = "OutGlReason";
            //        dr["Name"] = item.ECodeTag;
            //        dr["Value"] = count;
            //        dr["Ratio"] = Math.Round(cl, 0) + "%";
            //        dt.Rows.Add(dr);
            //    }
            //    else
            //    {
            //        dr = dt.NewRow();
            //        dr["Id"] = "OutGlReason";
            //        dr["Name"] = item.ECodeTag;
            //        dr["Value"] = count;
            //        dr["Ratio"] = count + "%";
            //        dt.Rows.Add(dr);
            //    }
            //}



            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "otherReason").ToList())
            {
                count = list.Where(o => o.OtherReasons == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "OtherReasons";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "OtherReasons";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "isOrNot").ToList())
            {
                count = list.Where(o => o.ResetGl == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "ResetGl";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "ResetGl";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "complication").ToList())
            {
                count = list.Where(o => o.Complication.Contains(item.ECodeValue)).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "Complication";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "Complication";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }






            ds.Tables.Add(dt);
            return ExecutDataSetToJson(ds);
        }
        #endregion

        #region 
        public string GetEventGyReport(string yue1, string yue2, string Region, string HospId)
        {

            aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
            aers_tbl_events_gySqlMapDao gydao = new aers_tbl_events_gySqlMapDao();



            aers_sys_statecodeSqlMapDao codedao = new aers_sys_statecodeSqlMapDao();
            IList<aers_sys_statecode> codelist = codedao.FindAll();

            DateTime time1 = Convert.ToDateTime(yue1);
            DateTime time2 = Convert.ToDateTime(yue2).AddDays(1);



            IList<aers_tbl_events_gy> list = gydao.FindListByData(time1).Where(o => o.HappenedDate < time2).ToList();



            aers_tbl_events_ycSqlMapDao hospdao = new aers_tbl_events_ycSqlMapDao();
            IList<aers_tbl_hospital> listhosp = hospdao.hospitalFindAll();

            foreach (aers_tbl_events_gy item in list)
            {
                aers_tbl_hospital hosp = listhosp.FirstOrDefault(o => o.HospId == item.HospId);
                if (hosp != null)
                {
                    item.Address = hosp.Address;
                }
                try
                {
                    item.Age = Convert.ToInt32(item.PatientAge);
                }
                catch (Exception)
                {
                    item.Age = 0;
                }
            }


            if (!string.IsNullOrEmpty(Region))
            {
                IList<Region> listRegion = GetRegion();
                Region re = listRegion.FirstOrDefault(o => o.RegionID == Region);
                list = list.Where(o => o.Address == re.RegionName).ToList();
            }

            if (!string.IsNullOrEmpty(HospId))
            {
                list = list.Where(o => o.HospId == HospId).ToList();
            }




            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Id", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Name", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Value", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Ratio", typeof(System.String)));



            decimal count = 0;
            decimal cl = 0;
            DataRow dr = dt.NewRow();


            count = list.Where(o => o.HappenedDate.Hour >= 8 && o.HappenedDate.Hour < 16).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "8-4组";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "8-4组";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.HappenedDate.Hour >= 16 && o.HappenedDate.Hour < 24).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "4-12组";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "4-12组";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.HappenedDate.Hour >= 0 && o.HappenedDate.Hour < 8).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "12-8组";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "12-8组";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.Age >= 0 && o.Age <= 20).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "0-20岁";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "0-20岁";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.Age > 20 && o.Age <= 40).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "20-40岁";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "20-40岁";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }



            count = list.Where(o => o.Age > 40 && o.Age <= 60).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "40-60岁";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "40-60岁";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.Age > 60).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "60岁以上";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "60岁以上";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "patientSex").ToList())
            {
                count = list.Where(o => o.PatientSex == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "patientSex";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "patientSex";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "level").ToList())
            {
                count = list.Where(o => o.EventLevel == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "EventLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "EventLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "level").ToList())
            {
                count = list.Where(o => o.NursLevel == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "NursLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "NursLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }



            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "isOrNot").ToList())
            {
                count = list.Where(o => o.PutDrugs == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "PutDrugs";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "PutDrugs";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "isOrNot").ToList())
            {
                count = list.Where(o => o.Dispensation == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "Dispensation";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "Dispensation";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }



            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "isOrNot").ToList())
            {
                count = list.Where(o => o.PDAState == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "PDAState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "PDAState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            //foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "GyLevel").ToList())
            //{
            //    count = list.Where(o => o.GyLevel == item.ECodeValue).ToList().Count;
            //    if (count != 0)
            //    {
            //        cl = count * 100 / list.Count;
            //        dr = dt.NewRow();
            //        dr["Id"] = "GyLevel";
            //        dr["Name"] = item.ECodeTag;
            //        dr["Value"] = count;
            //        dr["Ratio"] = Math.Round(cl, 0) + "%";
            //        dt.Rows.Add(dr);
            //    }
            //    else
            //    {
            //        dr = dt.NewRow();
            //        dr["Id"] = "GyLevel";
            //        dr["Name"] = item.ECodeTag;
            //        dr["Value"] = count;
            //        dr["Ratio"] = count + "%";
            //        dt.Rows.Add(dr);
            //    }
            //}

            ds.Tables.Add(dt);
            return ExecutDataSetToJson(ds);
        }
        #endregion

        #region 压疮事件上报
        public string GetEventYcReport(string yue1, string yue2, string Region, string HospId)
        {

            aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
            aers_tbl_events_ycSqlMapDao ycdao = new aers_tbl_events_ycSqlMapDao();



            aers_sys_statecodeSqlMapDao codedao = new aers_sys_statecodeSqlMapDao();
            IList<aers_sys_statecode> codelist = codedao.FindAll();

            DateTime time1 = Convert.ToDateTime(yue1);
            DateTime time2 = Convert.ToDateTime(yue2).AddDays(1);



            IList<aers_tbl_events_yc> list = ycdao.FindListByData(time1).Where(o => o.HappenedDate < time2).ToList();



            aers_tbl_events_ycSqlMapDao hospdao = new aers_tbl_events_ycSqlMapDao();
            IList<aers_tbl_hospital> listhosp = hospdao.hospitalFindAll();

            foreach (aers_tbl_events_yc item in list)
            {
                aers_tbl_hospital hosp = listhosp.FirstOrDefault(o => o.HospId == item.HospId);
                if (hosp != null)
                {
                    item.Address = hosp.Address;
                }

                try
                {
                    item.Age = Convert.ToInt32(item.PatientAge);
                }
                catch (Exception)
                {
                    item.Age = 0;
                }
            }


            if (!string.IsNullOrEmpty(Region))
            {
                IList<Region> listRegion = GetRegion();
                Region re = listRegion.FirstOrDefault(o => o.RegionID == Region);
                list = list.Where(o => o.Address == re.RegionName).ToList();
            }

            if (!string.IsNullOrEmpty(HospId))
            {
                list = list.Where(o => o.HospId == HospId).ToList();
            }




            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Id", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Name", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Value", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Ratio", typeof(System.String)));



            decimal count = 0;
            decimal cl = 0;
            DataRow dr = dt.NewRow();


            count = list.Where(o => o.HappenedDate.Hour >= 8 && o.HappenedDate.Hour < 16).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "8-4组";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "8-4组";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.HappenedDate.Hour >= 16 && o.HappenedDate.Hour < 24).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "4-12组";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "4-12组";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.HappenedDate.Hour >= 0 && o.HappenedDate.Hour < 8).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "12-8组";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "12-8组";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.Age >= 0 && o.Age <= 20).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "0-20岁";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "0-20岁";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.Age > 20 && o.Age <= 40).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "20-40岁";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "20-40岁";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }



            count = list.Where(o => o.Age > 40 && o.Age <= 60).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "40-60岁";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "40-60岁";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.Age > 60).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "60岁以上";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "60岁以上";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }



            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "patientSex").ToList())
            {
                count = list.Where(o => o.PatientSex == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "patientSex";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "patientSex";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "level").ToList())
            {
                count = list.Where(o => o.NursLevel == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "NursLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "NursLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }



            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "isOrNot").ToList())
            {
                count = list.Where(o => o.IsEdema == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "IsEdema";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "IsEdema";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "assessment").ToList())
            {
                count = list.Where(o => o.Assessment == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "Assessment";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "Assessment";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "takePrevent").ToList())
            {
                count = list.Where(o => o.IsTakePrevent.Contains(item.ECodeValue)).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "IsTakePrevent";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "IsTakePrevent";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "highRiskGrade").ToList())
            {
                count = list.Where(o => o.Highriskgrade == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "Highriskgrade";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "Highriskgrade";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "isOrNot").ToList())
            {
                count = list.Where(o => o.HighRiskLevel == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "HighRiskLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "HighRiskLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }



            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "outComeState").ToList())
            {
                count = list.Where(o => o.OutcomeState == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "OutcomeState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "OutcomeState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            ///无改成其他，属于转归里面，去掉无项

            //foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "").ToList())
            //{
            //    count = list.Where(o => o.OutcomeState == item.ECodeValue).ToList().Count;
            //    if (count != 0)
            //    {
            //        cl = count * 100 / list.Count;
            //        dr = dt.NewRow();
            //        dr["Id"] = "OutcomeState";
            //        dr["Name"] = item.ECodeTag;
            //        dr["Value"] = count;
            //        dr["Ratio"] = Math.Round(cl, 0) + "%";
            //        dt.Rows.Add(dr);
            //    }
            //    else
            //    {
            //        dr = dt.NewRow();
            //        dr["Id"] = "OutcomeState";
            //        dr["Name"] = item.ECodeTag;
            //        dr["Value"] = count;
            //        dr["Ratio"] = count + "%";
            //        dt.Rows.Add(dr);
            //    }
            //}




            ds.Tables.Add(dt);
            return ExecutDataSetToJson(ds);

        }
        #endregion

        #region 
        public string GetEventZyblReport(string yue1, string yue2, string Region, string HospId)
        {

            aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
            aers_tbl_events_zyblSqlMapDao ycdao = new aers_tbl_events_zyblSqlMapDao();



            aers_sys_statecodeSqlMapDao codedao = new aers_sys_statecodeSqlMapDao();
            IList<aers_sys_statecode> codelist = codedao.FindAll();

            DateTime time1 = Convert.ToDateTime(yue1);
            DateTime time2 = Convert.ToDateTime(yue2).AddDays(1);



            IList<aers_tbl_events_zybl> list = ycdao.FindListByData(time1).Where(o => o.HappenedDate < time2).ToList();



            aers_tbl_events_ycSqlMapDao hospdao = new aers_tbl_events_ycSqlMapDao();
            IList<aers_tbl_hospital> listhosp = hospdao.hospitalFindAll();

            foreach (aers_tbl_events_zybl item in list)
            {
                aers_tbl_hospital hosp = listhosp.FirstOrDefault(o => o.HospId == item.HospId);
                if (hosp != null)
                {
                    item.Address = hosp.Address;
                }


            }


            if (!string.IsNullOrEmpty(Region))
            {
                IList<Region> listRegion = GetRegion();
                Region re = listRegion.FirstOrDefault(o => o.RegionID == Region);
                list = list.Where(o => o.Address == re.RegionName).ToList();
            }

            if (!string.IsNullOrEmpty(HospId))
            {
                list = list.Where(o => o.HospId == HospId).ToList();
            }




            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Id", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Name", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Value", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Ratio", typeof(System.String)));



            decimal count = 0;
            decimal cl = 0;
            DataRow dr = dt.NewRow();


            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "level").ToList())
            {
                count = list.Where(o => o.EventLevel == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "EventLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "EventLevel";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }


            count = list.Where(o => o.HappenedDate.Hour >= 8 && o.HappenedDate.Hour < 16).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "8-4组";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "8-4组";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.HappenedDate.Hour >= 16 && o.HappenedDate.Hour < 24).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "4-12组";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "4-12组";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.HappenedDate.Hour >= 0 && o.HappenedDate.Hour < 8).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "12-8组";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "timeslot";
                dr["Name"] = "12-8组";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.Age >= 0 && o.Age <= 20).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "0-20岁";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "0-20岁";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.Age > 20 && o.Age <= 40).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "20-40岁";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "20-40岁";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }



            count = list.Where(o => o.Age > 40 && o.Age <= 60).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "40-60岁";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "40-60岁";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }


            count = list.Where(o => o.Age > 60).ToList().Count;
            if (count != 0)
            {
                cl = count * 100 / list.Count;
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "60岁以上";
                dr["Value"] = count;
                dr["Ratio"] = Math.Round(cl, 0) + "%";
                dt.Rows.Add(dr);
            }
            else
            {
                dr = dt.NewRow();
                dr["Id"] = "ageslot";
                dr["Name"] = "60岁以上";
                dr["Value"] = count;
                dr["Ratio"] = count + "%";
                dt.Rows.Add(dr);
            }



            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "patientExpose").ToList())
            {
                count = list.Where(o => o.PatientExpose == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "PatientExpose";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "PatientExpose";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }



            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "exposeTypeOne").ToList())
            {
                count = list.Where(o => o.ExposeTypeOne == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "ExposeTypeOne";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "ExposeTypeOne";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "harmDegree").ToList())
            {
                count = list.Where(o => o.HarmDegree == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "HarmDegree";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "HarmDegree";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            //foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "harmDegree").ToList())
            //{
            //    count = list.Where(o => o.HarmDegree == item.ECodeValue).ToList().Count;
            //    if (count != 0)
            //    {
            //        cl = count * 100 / list.Count;
            //        dr = dt.NewRow();
            //        dr["Id"] = "HarmDegree";
            //        dr["Name"] = item.ECodeTag;
            //        dr["Value"] = count;
            //        dr["Ratio"] = Math.Round(cl, 0) + "%";
            //        dt.Rows.Add(dr);
            //    }
            //    else
            //    {
            //        dr = dt.NewRow();
            //        dr["Id"] = "HarmDegree";
            //        dr["Name"] = item.ECodeTag;
            //        dr["Value"] = count;
            //        dr["Ratio"] = count + "%";
            //        dt.Rows.Add(dr);
            //    }
            //}

            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "fomesFrom").ToList())
            {
                count = list.Where(o => o.FomesFrom == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "FomesFrom";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "FomesFrom";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "isOrNot").ToList())
            {
                count = list.Where(o => o.PharmacyState == item.ECodeValue).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "PharmacyState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "PharmacyState";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "exposeUseDrug").ToList())
            {
                count = list.Where(o => o.UseDrug.Contains(item.ECodeValue)).ToList().Count;
                if (count != 0)
                {
                    cl = count * 100 / list.Count;
                    dr = dt.NewRow();
                    dr["Id"] = "UseDrug";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = Math.Round(cl, 0) + "%";
                    dt.Rows.Add(dr);
                }
                else
                {
                    dr = dt.NewRow();
                    dr["Id"] = "UseDrug";
                    dr["Name"] = item.ECodeTag;
                    dr["Value"] = count;
                    dr["Ratio"] = count + "%";
                    dt.Rows.Add(dr);
                }
            }

            ds.Tables.Add(dt);
            return ExecutDataSetToJson(ds);
        }


        #endregion

        #region 
        public string FindGroupByName(string time1)
        {
            aers_tbl_events_qtSqlMapDao dao = new aers_tbl_events_qtSqlMapDao();

            DateTime time3 = Convert.ToDateTime(time1);

            DataSet ds = dao.FindGroupByName(time3);

            return ExecutDataSetToJson(ds);
        }
        #endregion

        #region 一键上报
        public string simplyevent(aers_tbl_eventsresume model)
        {
            string res = "104";
            try
            {
                aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
                string EveresId = dao.instertbyonekey(model);
                exevent(EveresId, "已通过");
                res = "103";
            }
            catch (Exception)
            {

                res = "104";
            }

            return res;
        }
        #endregion

        #region 
        public string DeleteEventsresume(string eid, string uid)
        {
            string res = "";
            try
            {
                aers_tbl_eventsresumeSqlMapDao aers_tbl_eventsresumedao = new aers_tbl_eventsresumeSqlMapDao();

                aers_tbl_eventsresume eventname = aers_tbl_eventsresumedao.Find(eid);

                aers_tbl_staff aers_staff = new aers_tbl_staffSqlMapDao().FindStaffByRid(uid);

                aers_tbl_hospdep aers_hospdep = new aers_tbl_hospdepSqlMapDao().Find(aers_staff.DepId);
                if (aers_hospdep != null)
                {
                    if (eventname.ExamineState != "0" && eventname.ExamineState != "1" && eventname.HospId == aers_hospdep.HospId)
                    {
                        aers_tbl_eventsresumedao.Delete(eid);
                        res = "103";
                    }
                    else
                    {
                        res = "104不能删除";
                    }
                }
                else
                {
                    res = "104-不能删除";
                }
            }
            catch (Exception ex)
            {
                res = "104" + ex;
            }
            return res;

        }
        #endregion

        #region 护理部权限 - 查询 未通过审核事件   已拒绝5.31
        /// <summary>
        ///  护理部权限 - 查询 未通过审核事件
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IList<aers_tbl_eventsresume> findndepnoevent(string uid, string Time1, string Time2, string EveresLevel, string EventType, string ReUId, string LoginKey)
        {
            //if (HeartRate(ReUId, LoginKey))
            {
                IList<view_tbl_eventsresume> tempview = new List<view_tbl_eventsresume>();
                aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
                ResList<view_tbl_eventsresume> reslist = new ResList<view_tbl_eventsresume>();
                string res = "104";
                try
                {
                    IList<aers_tbl_hospdep> listdep = new aers_tbl_hospdepSqlMapDao().hospdepFindAll();

                    DateTime StaTime = Convert.ToDateTime(Time1);
                    DateTime EndTime = Convert.ToDateTime(Time2).AddDays(1);

                    IList<aers_tbl_eventsresume> list = null;

                    aers_tbl_staff staff = new aers_tbl_staffSqlMapDao().FindStaffByRid(uid);

                    if (staff.RoleState.Contains("145"))
                    {
                        list = dao.FindHospByRud(uid, "2");

                    }
                    else
                    {
                        list = dao.FindDepByRud(uid, "2");
                    }

                    list = list.Where(o => o.HappenedDate >= StaTime && o.HappenedDate < EndTime).ToList();

                    if (!string.IsNullOrEmpty(EveresLevel))
                    {
                        list = list.Where(o => o.EveresLevel == EveresLevel).ToList();
                    }
                    if (!string.IsNullOrEmpty(EventType))
                    {
                        list = list.Where(o => o.EveresName == EventType).ToList();
                    }

                    foreach (aers_tbl_eventsresume item in list)
                    {

                        aers_tbl_hospdep dep = listdep.FirstOrDefault(o => o.HospdepId == item.HospDepId);
                        if (dep != null)
                        {
                            item.HospDepId = dep.HospdepName;
                        }


                        switch (item.ExamineState)
                        {
                            case "0":
                                item.ExamineState = "审核中";
                                break;
                            case "1":
                                item.ExamineState = "已审核";
                                break;
                            case "2":
                                item.ExamineState = "未通过";
                                break;
                            case "3":
                                item.ExamineState = "未上报";
                                break;
                            default:
                                item.ExamineState = "--";
                                break;
                        }
                    }
                    return list;



                }
                catch (Exception ex)
                {
                    res = "104";
                    return null;
                }
            }
            //else
            //{
            //    IList<aers_tbl_eventsresume> list = new List<aers_tbl_eventsresume>();
            //    aers_tbl_eventsresume a = new aers_tbl_eventsresume();
            //    a.EveresId = "-8000";
            //    list.Add(a);
            //    return list;
            //}

        }
        #endregion  

        #region 护理部权限 - 查询 通过审核事件   已审核5.31
        /// <summary>
        ///  护理部权限 - 查询 通过审核事件
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IList<aers_tbl_eventsresume> findndepevent(string uid, string Time1, string Time2, string EveresLevel, string EventType, string ReUId, string LoginKey)
        {
           // if (HeartRate(ReUId, LoginKey))
            {
                IList<view_tbl_eventsresume> tempview = new List<view_tbl_eventsresume>();
                aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
                ResList<view_tbl_eventsresume> reslist = new ResList<view_tbl_eventsresume>();
                string res = "104";
                try
                {
                    IList<aers_tbl_hospdep> listdep = new aers_tbl_hospdepSqlMapDao().hospdepFindAll();



                    DateTime StaTime = Convert.ToDateTime(Time1);
                    DateTime EndTime = Convert.ToDateTime(Time2).AddDays(1);

                    IList<aers_tbl_eventsresume> list = null;


                    aers_tbl_staff staff = new aers_tbl_staffSqlMapDao().FindStaffByRid(uid);

                    if (staff.RoleState.Contains("145"))
                    {
                        list = dao.FindHospByRud(uid, "1");

                    }
                    else
                    {
                        list = dao.FindDepByRud(uid, "1");
                    }

                    list = list.Where(o => o.HappenedDate >= StaTime && o.HappenedDate < EndTime).ToList();

                    if (!string.IsNullOrEmpty(EveresLevel))
                    {
                        list = list.Where(o => o.EveresLevel == EveresLevel).ToList();
                    }



                    if (!string.IsNullOrEmpty(EventType))
                    {
                        list = list.Where(o => o.EveresName == EventType).ToList();
                    }


                    foreach (aers_tbl_eventsresume item in list)
                    {

                        aers_tbl_hospdep dep = listdep.FirstOrDefault(o => o.HospdepId == item.HospDepId);
                        if (dep != null)
                        {
                            item.HospDepId = dep.HospdepName;
                        }

                        switch (item.ExamineState)
                        {
                            case "0":
                                item.ExamineState = "审核中";
                                break;
                            case "1":
                                item.ExamineState = "已审核";
                                break;
                            case "2":
                                item.ExamineState = "未通过";
                                break;
                            case "3":
                                item.ExamineState = "未上报";
                                break;
                            default:
                                item.ExamineState = "--";
                                break;
                        }
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    res = "104";
                    return null;
                }
            }
            //else
            //{
            //    IList<aers_tbl_eventsresume> list = new List<aers_tbl_eventsresume>();
            //    aers_tbl_eventsresume a = new aers_tbl_eventsresume();
            //    a.EveresId = "-8000";
            //    list.Add(a);
            //    return list;
            //}

        }
        #endregion

        #region 护理部权限 - 查询 正在审核事件   未审核5.31
        /// <summary>
        ///  护理部权限 - 查询 正在审核事件
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>    
        public IList<aers_tbl_eventsresume> findndepwait(string uid, string Time1, string Time2, string EveresLevel, string EventType, string ReUId, string LoginKey)
        {
            // if (HeartRate(ReUId, LoginKey))
            {
                IList<view_tbl_eventsresume> tempview = new List<view_tbl_eventsresume>();
                aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
                ResList<view_tbl_eventsresume> reslist = new ResList<view_tbl_eventsresume>();
                string res = "104";
                try
                {
                    IList<aers_tbl_hospdep> listdep = new aers_tbl_hospdepSqlMapDao().hospdepFindAll();




                    DateTime StaTime = Convert.ToDateTime(Time1);
                    DateTime EndTime = Convert.ToDateTime(Time2).AddDays(1);

                    IList<aers_tbl_eventsresume> list = null;


                    aers_tbl_staff staff = new aers_tbl_staffSqlMapDao().FindStaffByRid(uid);

                    if (staff.RoleState.Contains("145"))
                    {
                        list = dao.FindHospByRud(uid, "0");   

                    }
                    else
                    {
                        list = dao.FindDepByRud(uid, "0");  //根据个人
                    }


                    list = list.Where(o => o.HappenedDate >= StaTime && o.HappenedDate < EndTime).ToList();

                    if (!string.IsNullOrEmpty(EveresLevel))
                    {
                        list = list.Where(o => o.EveresLevel == EveresLevel).ToList();
                    }


                    if (!string.IsNullOrEmpty(EventType))
                    {
                        list = list.Where(o => o.EveresName == EventType).ToList();
                    }

                  

                    foreach (aers_tbl_eventsresume item in list)
                    {
                        aers_tbl_hospdep dep = listdep.FirstOrDefault(o => o.HospdepId == item.HospDepId);
                        if (dep != null)
                        {
                            item.HospDepId = dep.HospdepName;
                        }

                        switch (item.ExamineState)
                        {
                            case "0":
                                item.ExamineState = "审核中";
                                break;
                            case "1":
                                item.ExamineState = "已审核";
                                break;
                            case "2":
                                item.ExamineState = "未通过";
                                break;
                            case "3":
                                item.ExamineState = "未上报";
                                break;
                            default:
                                item.ExamineState = "--";
                                break;
                        }
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    res = "104";
                    return null;
                }
            }
            //else
            //{
            //    IList<aers_tbl_eventsresume> list = new List<aers_tbl_eventsresume>();
            //    aers_tbl_eventsresume a = new aers_tbl_eventsresume();
            //    a.EveresId = "-8000";
            //    list.Add(a);
            //    return list;
            //}
        }
        #endregion

        #region 护理部权限 - 查询 正在审核事件   未审核5.31
        /// <summary>
        ///  护理部权限 - 查询 正在审核事件
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>    
        public IList<aers_tbl_eventsresume> findndepRecord(string uid, int pageSize, int pageNumber)
        {
            // if (HeartRate(ReUId, LoginKey))
            {
                IList<view_tbl_eventsresume> tempview = new List<view_tbl_eventsresume>();
                aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
                ResList<view_tbl_eventsresume> reslist = new ResList<view_tbl_eventsresume>();
                string res = "104";
                try
                {
                    IList<aers_tbl_hospdep> listdep = new aers_tbl_hospdepSqlMapDao().hospdepFindAll();




                    //DateTime StaTime = Convert.ToDateTime(Time1);
                    //DateTime EndTime = Convert.ToDateTime(Time2).AddDays(1);

                    IList<aers_tbl_eventsresume> list = null;


                    aers_tbl_staff staff = new aers_tbl_staffSqlMapDao().FindStaffByRid(uid);

                  // list = dao.FindHospByRud(uid, "0");   //0待审核  1已审核  2被拒绝   
                    var list1 = dao.FindHospByRud(uid, "1");   //0待审核  1已审核  2被拒绝     3草稿箱
                    var  list2 = dao.FindHospByRud(uid, "2");
                    list = list1.Union(list2).OrderByDescending(o=>o.OperatorDate).ToList();


                    //aers_tbl_registereduserSqlMapDao rudao = new aers_tbl_registereduserSqlMapDao();
                    //var rudata = rudao.FindAllUser();
                    aers_tbl_staffSqlMapDao sdao = new aers_tbl_staffSqlMapDao();
                    var sdata = sdao.staffFindAll();
                    //foreach (var item in list)
                    //{
                    //    item.OperatorName = sdata.FirstOrDefault(o=>o.ReguserId==item.OperatorID).Name;
                    //}
                    // list = list.Where(o => o.HappenedDate >= StaTime && o.HappenedDate < EndTime).ToList();

                    //if (!string.IsNullOrEmpty(EveresLevel))
                    //{
                    //    list = list.Where(o => o.EveresLevel == EveresLevel).ToList();
                    //}


                    //if (!string.IsNullOrEmpty(EventType))
                    //{
                    //    list = list.Where(o => o.EveresName == EventType).ToList();
                    //}

                    aers_tbl_staffSqlMapDao stdao = new aers_tbl_staffSqlMapDao();

                    foreach (var item in list)
                    {
                        item.OperatorName = stdao.FindNameByRid(item.OperatorID);
                    }


                    foreach (aers_tbl_eventsresume item in list)
                    {
                        aers_tbl_hospdep dep = listdep.FirstOrDefault(o => o.HospdepId == item.HospDepId);
                        if (dep != null)
                        {
                            item.HospDepId = dep.HospdepName;
                        }
    
                        switch (item.ExamineState)
                        {
                            case "0":
                                item.ExamineState = "审核中";
                                break;
                            case "1":
                                item.ExamineState = "已审核";
                                break;
                            case "2":
                                item.ExamineState = "未通过";
                                break;
                            case "3":
                                item.ExamineState = "未上报";
                                break;
                            default:
                                item.ExamineState = "--";
                                break;
                        }
                    }
                    return list.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                  //  return list;
                }
                catch (Exception ex)
                {
                    res = "104";
                    return null;
                }
            }
            //else
            //{
            //    IList<aers_tbl_eventsresume> list = new List<aers_tbl_eventsresume>();
            //    aers_tbl_eventsresume a = new aers_tbl_eventsresume();
            //    a.EveresId = "-8000";
            //    list.Add(a);
            //    return list;
            //}
        }
        #endregion

        #region 护理部权限 - 查询 正在审核事件   未审核8.8加上报人名字
        /// <summary>
        ///  护理部权限 - 查询 正在审核事件
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>      加上报人名字
        //public IList<aers_tbl_eventsresume> findndepwaitNew(string uid, string Time1, string Time2, string EveresLevel, string EventType, string ReUId, string LoginKey)
        //{
        //    // if (HeartRate(ReUId, LoginKey))
        //    {
        //        IList<view_tbl_eventsresume> tempview = new List<view_tbl_eventsresume>();
        //        aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
        //        ResList<view_tbl_eventsresume> reslist = new ResList<view_tbl_eventsresume>();
        //        string res = "104";
        //        try
        //        {
        //            IList<aers_tbl_hospdep> listdep = new aers_tbl_hospdepSqlMapDao().hospdepFindAll();




        //            DateTime StaTime = Convert.ToDateTime(Time1);
        //            DateTime EndTime = Convert.ToDateTime(Time2).AddDays(1);

        //            IList<aers_tbl_eventsresume> list = null;


        //            aers_tbl_staff staff = new aers_tbl_staffSqlMapDao().FindStaffByRid(uid);

        //            if (staff.RoleState.Contains("145"))
        //            {
        //                list = dao.FindHospByRud(uid, "0");

        //            }
        //            else
        //            {
        //                list = dao.FindDepByRud(uid, "0");
        //            }


        //            list = list.Where(o => o.HappenedDate >= StaTime && o.HappenedDate < EndTime).ToList();

        //            if (!string.IsNullOrEmpty(EveresLevel))
        //            {
        //                list = list.Where(o => o.EveresLevel == EveresLevel).ToList();
        //            }


        //            if (!string.IsNullOrEmpty(EventType))
        //            {
        //                list = list.Where(o => o.EveresName == EventType).ToList();
        //            }


        //            aers_tbl_staffSqlMapDao stdao = new aers_tbl_staffSqlMapDao();

        //            foreach (var item in list)
        //            {
        //                item.OperatorName = stdao.FindNameByRid(item.OperatorID);
        //            }



        //            foreach (aers_tbl_eventsresume item in list)
        //            {
        //                aers_tbl_hospdep dep = listdep.FirstOrDefault(o => o.HospdepId == item.HospDepId);
        //                if (dep != null)
        //                {
        //                    item.HospDepId = dep.HospdepName;
        //                }

        //                switch (item.ExamineState)
        //                {
        //                    case "0":
        //                        item.ExamineState = "审核中";
        //                        break;
        //                    case "1":
        //                        item.ExamineState = "已审核";
        //                        break;
        //                    case "2":
        //                        item.ExamineState = "未通过";
        //                        break;
        //                    case "3":
        //                        item.ExamineState = "未上报";
        //                        break;
        //                    default:
        //                        item.ExamineState = "--";
        //                        break;
        //                }
        //            }
        //            return list;
        //        }
        //        catch (Exception ex)
        //        {
        //            res = "104";
        //            return null;
        //        }
        //    }
        //    //else
        //    //{
        //    //    IList<aers_tbl_eventsresume> list = new List<aers_tbl_eventsresume>();
        //    //    aers_tbl_eventsresume a = new aers_tbl_eventsresume();
        //    //    a.EveresId = "-8000";
        //    //    list.Add(a);
        //    //    return list;
        //    //}
        //}
        #endregion

        #region 护理部权限 - 查询 正在审核事件   未审核8.8加上报人名字
        /// <summary>
        ///  护理部权限 - 查询 正在审核事件
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>      加上报人名字
        public IList<aers_tbl_eventsresume> findndepwaitNew(string uid, string ReUId, int pageSize, int pageNumber)
        {
            // if (HeartRate(ReUId, LoginKey))
            {
                IList<view_tbl_eventsresume> tempview = new List<view_tbl_eventsresume>();
                aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
                ResList<view_tbl_eventsresume> reslist = new ResList<view_tbl_eventsresume>();
                string res = "104";
                try
                {
                    IList<aers_tbl_hospdep> listdep = new aers_tbl_hospdepSqlMapDao().hospdepFindAll();

                    IList<aers_tbl_eventsresume> list = null;


                    aers_tbl_staff staff = new aers_tbl_staffSqlMapDao().FindStaffByRid(uid);

                    if (staff.RoleState.Contains("145"))
                    {
                        list = dao.FindHospByRud(uid, "0");

                    }
                    else
                    {
                        list = dao.FindDepByRud(uid, "0");
                    }

                    aers_tbl_staffSqlMapDao stdao = new aers_tbl_staffSqlMapDao();

                    foreach (var item in list)
                    {
                        item.OperatorName = stdao.FindNameByRid(item.OperatorID);
                    }



                    foreach (aers_tbl_eventsresume item in list)
                    {
                        aers_tbl_hospdep dep = listdep.FirstOrDefault(o => o.HospdepId == item.HospDepId);
                        if (dep != null)
                        {
                            item.HospDepId = dep.HospdepName;
                        }

                        switch (item.ExamineState)
                        {
                            case "0":
                                item.ExamineState = "审核中";
                                break;
                            case "1":
                                item.ExamineState = "已审核";
                                break;
                            case "2":
                                item.ExamineState = "未通过";
                                break;
                            case "3":
                                item.ExamineState = "未上报";
                                break;
                            default:
                                item.ExamineState = "--";
                                break;
                        }
                    }
                    return list.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                   
                }
                catch (Exception ex)
                {
                    res = "104";
                    return null;
                }
            }
            //else
            //{
            //    IList<aers_tbl_eventsresume> list = new List<aers_tbl_eventsresume>();
            //    aers_tbl_eventsresume a = new aers_tbl_eventsresume();
            //    a.EveresId = "-8000";
            //    list.Add(a);
            //    return list;
            //}
        }
        #endregion

        #region 护理部权限 - 查询 正在审核事件   未审核5.31
        /// <summary>
        ///  护理部权限 - 查询 正在审核事件
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        //public IList<aers_tbl_eventsresume> findndepallevent(string uid, string Time1, string Time2,int pageSize,int  pageNumber)
        //{
        //    // if (HeartRate(ReUId, LoginKey))
        //    {
        //        IList<view_tbl_eventsresume> tempview = new List<view_tbl_eventsresume>();
        //        aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
        //        ResList<view_tbl_eventsresume> reslist = new ResList<view_tbl_eventsresume>();
        //        string res = "104";
        //        try
        //        {
        //            IList<aers_tbl_hospdep> listdep = new aers_tbl_hospdepSqlMapDao().hospdepFindAll();




        //            DateTime StaTime = Convert.ToDateTime(Time1);
        //            DateTime EndTime = Convert.ToDateTime(Time2).AddDays(1);

        //            IList<aers_tbl_eventsresume> list = null;


        //            aers_tbl_staff staff = new aers_tbl_staffSqlMapDao().FindStaffByRid(uid);

        //            if (staff.RoleState.Contains("145"))
        //            {
        //               // list = dao.FindHospByRud(uid, "0");
        //                list = dao.FindHospByData(uid);
        //            }
        //            else
        //            {
        //               // list = dao.FindDepByRud(uid, "0");
        //                list = dao.FindDepByData(uid);
        //            }


        //            list = list.Where(o => o.HappenedDate >= StaTime && o.HappenedDate < EndTime).ToList();

        //            //if (!string.IsNullOrEmpty(EveresLevel))
        //            //{
        //            //    list = list.Where(o => o.EveresLevel == EveresLevel).ToList();
        //            //}


        //            //if (!string.IsNullOrEmpty(EventType))
        //            //{
        //            //    list = list.Where(o => o.EveresName == EventType).ToList();
        //            //}

        //            foreach (aers_tbl_eventsresume item in list)
        //            {
        //                aers_tbl_hospdep dep = listdep.FirstOrDefault(o => o.HospdepId == item.HospDepId);
        //                if (dep != null)
        //                {
        //                    item.HospDepId = dep.HospdepName;
        //                }

        //                switch (item.ExamineState)
        //                {
        //                    case "0":
        //                        item.ExamineState = "审核中";
        //                        break;
        //                    case "1":
        //                        item.ExamineState = "已审核";
        //                        break;
        //                    case "2":
        //                        item.ExamineState = "未通过";
        //                        break;
        //                    case "3":
        //                        item.ExamineState = "未上报";
        //                        break;
        //                    default:
        //                        item.ExamineState = "--";
        //                        break;
        //                }
        //            }
        //            return list.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        //          //  return list;
        //        }
        //        catch (Exception ex)
        //        {
        //            res = "104";
        //            return null;
        //        }
        //    }
        //    //else
        //    //{
        //    //    IList<aers_tbl_eventsresume> list = new List<aers_tbl_eventsresume>();
        //    //    aers_tbl_eventsresume a = new aers_tbl_eventsresume();
        //    //    a.EveresId = "-8000";
        //    //    list.Add(a);
        //    //    return list;
        //    //}
        //}
        #endregion


        #region 护理部权限 - 查询 正在审核事件   未审核5.31
        /// <summary>
        ///  护理部权限 - 查询 正在审核事件
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IList<aers_tbl_eventsresume> findndepallevent(string uid, int pageSize, int pageNumber)
        {
            // if (HeartRate(ReUId, LoginKey))
            {
                IList<view_tbl_eventsresume> tempview = new List<view_tbl_eventsresume>();
                aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
                ResList<view_tbl_eventsresume> reslist = new ResList<view_tbl_eventsresume>();
                string res = "104";
                try
                {
                    IList<aers_tbl_hospdep> listdep = new aers_tbl_hospdepSqlMapDao().hospdepFindAll();




                   

                    IList<aers_tbl_eventsresume> list = null;


                    aers_tbl_staff staff = new aers_tbl_staffSqlMapDao().FindStaffByRid(uid);

                    if (staff.RoleState.Contains("145"))  //护理权限
                    {
                        // list = dao.FindHospByRud(uid, "0");
                        list = dao.FindHospByData(uid);
                    }
                    else
                    {
                        // list = dao.FindDepByRud(uid, "0");
                        list = dao.FindDepByDataZ(uid);   //146护士长
                    }

                    aers_tbl_staffSqlMapDao stdao = new aers_tbl_staffSqlMapDao();
                    foreach (var item in list)
                    {
                        item.OperatorName = stdao.FindNameByRid(item.OperatorID);
                    }

                    //if (!string.IsNullOrEmpty(EveresLevel))
                    //{
                    //    list = list.Where(o => o.EveresLevel == EveresLevel).ToList();
                    //}


                    //if (!string.IsNullOrEmpty(EventType))
                    //{
                    //    list = list.Where(o => o.EveresName == EventType).ToList();
                    //}

                    foreach (aers_tbl_eventsresume item in list)
                    {
                        aers_tbl_hospdep dep = listdep.FirstOrDefault(o => o.HospdepId == item.HospDepId);
                        if (dep != null)
                        {
                            item.HospDepId = dep.HospdepName;
                        }

                        switch (item.ExamineState)
                        {
                            case "0":
                                item.ExamineState = "审核中";
                                break;
                            case "1":
                                item.ExamineState = "已审核";
                                break;
                            case "2":
                                item.ExamineState = "未通过";
                                break;
                            case "3":
                                item.ExamineState = "未上报";
                                break;
                            default:
                                item.ExamineState = "--";
                                break;
                        }
                    }
                    return list.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                    //  return list;
                }
                catch (Exception ex)
                {
                    res = "104";
                    return null;
                }
            }
            //else
            //{
            //    IList<aers_tbl_eventsresume> list = new List<aers_tbl_eventsresume>();
            //    aers_tbl_eventsresume a = new aers_tbl_eventsresume();
            //    a.EveresId = "-8000";
            //    list.Add(a);
            //    return list;
            //}
        }
        #endregion

        #region 护理部权限 - 查询 未上报   草稿箱5.31
        /// <summary>
        ///  护理部权限 - 查询 未上报
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IList<aers_tbl_eventsresume> findndepreport(string uid, string Time1, string Time2, string EveresLevel, string EventType, string ReUId, string LoginKey)
        {
           // if (HeartRate(ReUId, LoginKey))
            {
                IList<view_tbl_eventsresume> tempview = new List<view_tbl_eventsresume>();
                aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
                ResList<view_tbl_eventsresume> reslist = new ResList<view_tbl_eventsresume>();
                string res = "104";
                try
                {

                    IList<aers_tbl_hospdep> listdep = new aers_tbl_hospdepSqlMapDao().hospdepFindAll();

                    DateTime StaTime = Convert.ToDateTime(Time1);
                    DateTime EndTime = Convert.ToDateTime(Time2).AddDays(1);

                    IList<aers_tbl_eventsresume> list = null;

                    aers_tbl_staff staff = new aers_tbl_staffSqlMapDao().FindStaffByRid(uid);


                    if (staff.RoleState.Contains("145"))
                    {
                        list = dao.FindHospByRud(uid, "3");
                    }
                    else
                    {
                        list = dao.FindDepByRud(uid, "3");
                    }

                    list = list.Where(o => o.HappenedDate >= StaTime && o.HappenedDate < EndTime).ToList();

                    if (!string.IsNullOrEmpty(EveresLevel))
                    {
                        list = list.Where(o => o.EveresLevel == EveresLevel).ToList();
                    }



                    if (!string.IsNullOrEmpty(EventType))
                    {
                        list = list.Where(o => o.EveresName == EventType).ToList();
                    }

                    foreach (aers_tbl_eventsresume item in list)
                    {

                        aers_tbl_hospdep dep = listdep.FirstOrDefault(o => o.HospdepId == item.HospDepId);
                        if (dep != null)
                        {
                            item.HospDepId = dep.HospdepName;
                        }


                        switch (item.ExamineState)
                        {
                            case "0":
                                item.ExamineState = "审核中";
                                break;
                            case "1":
                                item.ExamineState = "已审核";
                                break;
                            case "2":
                                item.ExamineState = "未通过";
                                break;
                            case "3":
                                item.ExamineState = "未上报";
                                break;
                            default:
                                item.ExamineState = "--";
                                break;
                        }
                    }
                    return list;
                }
                catch (Exception ex)
                {
                    res = "104";
                    return null;
                }
            }
            //else
            //{
            //    IList<aers_tbl_eventsresume> list = new List<aers_tbl_eventsresume>();
            //    aers_tbl_eventsresume a = new aers_tbl_eventsresume();
            //    a.EveresId = "-8000";
            //    list.Add(a);
            //    return list;
            //}
        }
        #endregion

        #region 省厅权限 - 查询 审核事件
        /// <summary>
        ///  省厅权限 - 查询 审核事件
        /// </summary>
        /// <param name="state">审核状态</param>
        /// <returns></returns>
        public IList<aers_tbl_eventsresume> findproevent(string hid, string Region, string Time1, string Time2, string EventType, string EveresLevel)
        {
            IList<view_tbl_eventsresume> tempview = new List<view_tbl_eventsresume>();
            aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
            ResList<view_tbl_eventsresume> reslist = new ResList<view_tbl_eventsresume>();
            string res = "104";
            try
            {

                IList<aers_tbl_eventsresume> list = new List<aers_tbl_eventsresume>();

                DateTime StaTime = Convert.ToDateTime(Time1);
                DateTime EndTime = Convert.ToDateTime(Time2).AddDays(1);

                if (!string.IsNullOrEmpty(hid))
                {
                    list = dao.FindAllByRudHosp("1", hid, StaTime);
                }
                else
                {
                    list = dao.FindAllByRud("1", StaTime);
                }




                list = list.Where(o => o.HappenedDate < EndTime).ToList();

                if (!string.IsNullOrEmpty(EventType))
                {
                    list = list.Where(o => o.EveresName == EventType).ToList();
                }

                if (!string.IsNullOrEmpty(EveresLevel))
                {
                    list = list.Where(o => o.EveresLevel == EveresLevel).ToList();
                }

                IList<aers_tbl_hospital> listhosp = new aers_tbl_events_ycSqlMapDao().hospitalFindAll();

                IList<aers_tbl_hospdep> listdep = new aers_tbl_hospdepSqlMapDao().hospdepFindAll();

                foreach (aers_tbl_eventsresume dr in list)
                {
                    aers_tbl_hospital hosp = listhosp.FirstOrDefault(o => o.HospId == dr.HospId);
                    if (hosp != null)
                    {
                        dr.HospId = hosp.HospName;
                        dr.Address = hosp.Address;
                    }
                    aers_tbl_hospdep dep = listdep.FirstOrDefault(o => o.HospdepId == dr.HospDepId);
                    if (dep != null)
                    {
                        dr.HospDepId = dep.HospdepName;
                    }


                    switch (Convert.ToString(dr.ExamineState))
                    {
                        case "0":
                            dr.ExamineState = "审核中";
                            break;
                        case "1":
                            dr.ExamineState = "已审核";
                            break;
                        case "2":
                            dr.ExamineState = "未通过";
                            break;
                        case "3":
                            dr.ExamineState = "未上报";
                            break;
                        default:
                            dr.ExamineState = "--";
                            break;
                    }
                }
                if (!string.IsNullOrEmpty(Region))
                {
                    Region rr = GetRegion().FirstOrDefault(o => o.RegionID == Region);
                    if (rr != null)
                    {
                        list = list.Where(o => o.Address == rr.RegionName).ToList();
                    }
                }


                return list;
            }
            catch (Exception ex)
            {
                res = "104";
            }
            return null;
        }
        #endregion

        #region 护士长权限 - 查询 未通过审核事件
        /// <summary>
        ///  护士长权限 - 查询 未通过审核事件
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        //public string findnursnoevent(string uid)
        //{
        //    IList<view_tbl_eventsresume> tempview = new List<view_tbl_eventsresume>();
        //    aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
        //    ResList<view_tbl_eventsresume> reslist = new ResList<view_tbl_eventsresume>();
        //    string res = "104";
        //    try
        //    {
        //        DataSet ds = dao.FindDepByRud(uid, "2");
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            switch (Convert.ToString(dr["ExamineState"]))
        //            {
        //                case "0":
        //                    dr["ExamineState"] = "审核中";
        //                    break;
        //                case "1":
        //                    dr["ExamineState"] = "已审核";
        //                    break;
        //                case "2":
        //                    dr["ExamineState"] = "未通过";
        //                    break;
        //                case "3":
        //                    dr["ExamineState"] = "未上报";
        //                    break;
        //                default:
        //                    dr["ExamineState"] = "--";
        //                    break;
        //            }
        //        }
        //        return ExecutDataSetToJson(ds);
        //    }
        //    catch (Exception ex)
        //    {
        //        res = "104";
        //    }
        //    return res;
        //}
        #endregion

        #region 护士长权限 - 查询 通过审核事件
        /// <summary>
        ///  护士长权限 - 查询 通过审核事件
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        //public string findnursevent(string uid)
        //{
        //    IList<view_tbl_eventsresume> tempview = new List<view_tbl_eventsresume>();
        //    aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
        //    ResList<view_tbl_eventsresume> reslist = new ResList<view_tbl_eventsresume>();
        //    string res = "104";
        //    try
        //    {
        //        DataSet ds = dao.FindDepByRud(uid, "1");
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            switch (Convert.ToString(dr["ExamineState"]))
        //            {
        //                case "0":
        //                    dr["ExamineState"] = "审核中";
        //                    break;
        //                case "1":
        //                    dr["ExamineState"] = "已审核";
        //                    break;
        //                case "2":
        //                    dr["ExamineState"] = "未通过";
        //                    break;
        //                case "3":
        //                    dr["ExamineState"] = "未上报";
        //                    break;
        //                default:
        //                    dr["ExamineState"] = "--";
        //                    break;
        //            }
        //        }
        //        return ExecutDataSetToJson(ds);
        //    }
        //    catch (Exception ex)
        //    {
        //        res = "104";
        //    }
        //    return res;
        //}
        #endregion

        #region 护士长权限 - 查询 正在审核事件
        /// <summary>
        ///  护士长权限 - 查询 正在审核事件
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        //public string findnurswait(string uid)
        //{
        //    IList<view_tbl_eventsresume> tempview = new List<view_tbl_eventsresume>();
        //    aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
        //    ResList<view_tbl_eventsresume> reslist = new ResList<view_tbl_eventsresume>();
        //    string res = "104";
        //    try
        //    {
        //        DataSet ds = dao.FindDepByRud(uid, "0");
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            switch (Convert.ToString(dr["ExamineState"]))
        //            {
        //                case "0":
        //                    dr["ExamineState"] = "审核中";
        //                    break;
        //                case "1":
        //                    dr["ExamineState"] = "已审核";
        //                    break;
        //                case "2":
        //                    dr["ExamineState"] = "未通过";
        //                    break;
        //                case "3":
        //                    dr["ExamineState"] = "未上报";
        //                    break;
        //                default:
        //                    dr["ExamineState"] = "--";
        //                    break;
        //            }
        //        }
        //        return ExecutDataSetToJson(ds);

        //    }
        //    catch (Exception ex)
        //    {
        //        res = "104"+ ex.Message;
        //    }
        //    return res;
        //}
        #endregion

        #region 护士长权限 - 查询 未上报
        /// <summary>
        ///  护士长权限 - 查询 未上报
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public IList<aers_tbl_eventsresume> findnursreport(string uid)
        {
            IList<view_tbl_eventsresume> tempview = new List<view_tbl_eventsresume>();
            aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
            ResList<view_tbl_eventsresume> reslist = new ResList<view_tbl_eventsresume>();
            string res = "104";
            try
            {
                IList<aers_tbl_eventsresume> list = dao.FindDepByRud(uid, "3");
                foreach (aers_tbl_eventsresume item in list)
                {
                    switch (item.ExamineState)
                    {
                        case "0":
                            item.ExamineState = "审核中";
                            break;
                        case "1":
                            item.ExamineState = "已审核";
                            break;
                        case "2":
                            item.ExamineState = "未通过";
                            break;
                        case "3":
                            item.ExamineState = "未上报";
                            break;
                        default:
                            item.ExamineState = "--";
                            break;
                    }
                }
                return list;

            }
            catch (Exception ex)
            {
                res = "104" + ex.Message;
                return null;
            }

        }
        #endregion

        #region 
        //1103
        public string GetIsEveCountByDep(string uid)
        {

            aers_tbl_eventsresumeSqlMapDao daoeven = new aers_tbl_eventsresumeSqlMapDao();


            DateTime time = DateTime.Now;

            DateTime time1 = new DateTime(time.Year, time.Month, 1).AddMonths(-1);
            DateTime time2 = new DateTime(time.Year, time.Month, 1);


            IList<aers_tbl_eventsresume> list2 = daoeven.FindDepByRud(uid, "0").Where(o => o.HappenedDate >= time1 && o.HappenedDate < time2).ToList();

            IList<aers_tbl_eventsresume> list1 = daoeven.FindDepByRud(uid, "1").Where(o => o.HappenedDate >= time1 && o.HappenedDate < time2).ToList();

            IList<aers_tbl_eventsresume> list3 = daoeven.FindDepByRud(uid, "2").Where(o => o.HappenedDate >= time1 && o.HappenedDate < time2).ToList();

            return list1.Count + "-" + list2.Count + "-" + list3.Count;
        }
        #endregion

        #region 
        public string GetEventsresumeByCount(string nians, string Region, string HospId, string levelID, string GradeID, string EveresLevel)
        {
            aers_tbl_eventsresumeSqlMapDao daoeven = new aers_tbl_eventsresumeSqlMapDao();
            IList<aers_tbl_eventsresume> list = daoeven.GetEventsresumeList().Where(o => o.ExamineState == "1").ToList();

            aers_tbl_events_ycSqlMapDao dao = new aers_tbl_events_ycSqlMapDao();
            IList<aers_tbl_hospital> listhosp = dao.hospitalFindAll();

            foreach (aers_tbl_eventsresume item in list)
            {
                aers_tbl_hospital hosp = listhosp.FirstOrDefault(o => o.HospId == item.HospId);
                if (hosp != null)
                {
                    item.Address = hosp.Address;
                    item.Grade = hosp.Grade;
                }
            }

            IList<Region> listRegion = GetRegion();

            Region re = listRegion.FirstOrDefault(o => o.RegionID == Region);



            if (!string.IsNullOrEmpty(EveresLevel))
            {
                list = list.Where(o => o.EveresLevel == EveresLevel).ToList();
            }

            if (!string.IsNullOrEmpty(Region) && re != null)
            {
                list = list.Where(o => o.Address == re.RegionName).ToList();
            }

            if (!string.IsNullOrEmpty(HospId))
            {
                list = list.Where(o => o.HospId == HospId).ToList();
            }

            if (!string.IsNullOrEmpty(levelID))
            {
                if (levelID == "1")
                {
                    levelID = "三";
                }
                else if (levelID == "2")
                {
                    levelID = "二";
                }
                else
                {
                    levelID = "一";
                }

                list = list.Where(o => o.Grade.Contains(levelID)).ToList();
            }


            if (!string.IsNullOrEmpty(GradeID))
            {
                if (GradeID == "0")
                {
                    GradeID = "特";
                }
                else if (GradeID == "1")
                {
                    GradeID = "甲";
                }
                else if (GradeID == "2")
                {
                    GradeID = "乙";
                }
                else
                {
                    GradeID = "丙";
                }

                list = list.Where(o => o.Grade.Contains(GradeID)).ToList();
            }


            DataSet ds = new DataSet();

            DataTable dt = new DataTable();


            dt.Columns.Add(new DataColumn("yue", typeof(System.String)));
            dt.Columns.Add(new DataColumn("yc", typeof(System.String)));
            dt.Columns.Add(new DataColumn("qt", typeof(System.String)));
            dt.Columns.Add(new DataColumn("ddzc", typeof(System.String)));
            dt.Columns.Add(new DataColumn("gl", typeof(System.String)));
            dt.Columns.Add(new DataColumn("yh", typeof(System.String)));
            dt.Columns.Add(new DataColumn("gy", typeof(System.String)));
            dt.Columns.Add(new DataColumn("zybl", typeof(System.String)));
            dt.Columns.Add(new DataColumn("lsj", typeof(System.String)));
            dt.Columns.Add(new DataColumn("hj", typeof(System.String)));




            DateTime time = Convert.ToDateTime(nians);

            DateTime time1 = new DateTime(time.Year, 1, 1);
            DateTime time2 = new DateTime(time.Year, 2, 1);



            for (int i = 0; i < 12; i++)
            {
                DataRow now = dt.NewRow();



                now["yue"] = i + 1 + "月";
                now["yc"] = list.Where(o => o.EveresName == "149" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["qt"] = list.Where(o => o.EveresName == "150" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["ddzc"] = list.Where(o => o.EveresName == "152" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["gl"] = list.Where(o => o.EveresName == "153" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["yh"] = list.Where(o => o.EveresName == "154" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["gy"] = list.Where(o => o.EveresName == "155" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["zybl"] = list.Where(o => o.EveresName == "156" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["lsj"] = list.Where(o => o.EveresName == "151" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["hj"] = list.Where(o => o.EveresName != "151" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;


                dt.Rows.Add(now);
            }


            ds.Tables.Add(dt);

            return ExecutDataSetToJson(ds);
        }
        #endregion

        #region 
        public string GetEventQtByCount(string nians, string Region, string HospId, string levelID, string GradeID, string EveresLevel)
        {



            aers_tbl_events_qtSqlMapDao daoeven = new aers_tbl_events_qtSqlMapDao();


            IList<aers_tbl_events_qt> list = null;
            if (!string.IsNullOrEmpty(EveresLevel))
            {
                list = daoeven.FindByShengHeEveresLevel(EveresLevel);
            }
            else
            {
                list = daoeven.FindByShengHe();
            }




            aers_tbl_events_ycSqlMapDao dao = new aers_tbl_events_ycSqlMapDao();
            IList<aers_tbl_hospital> listhosp = dao.hospitalFindAll();

            foreach (aers_tbl_events_qt item in list)
            {
                aers_tbl_hospital hosp = listhosp.FirstOrDefault(o => o.HospId == item.HospId);
                if (hosp != null)
                {
                    item.Address = hosp.Address;
                    item.Grade = hosp.Grade;
                }
            }

            IList<Region> listRegion = GetRegion();

            Region re = listRegion.FirstOrDefault(o => o.RegionID == Region);


            if (!string.IsNullOrEmpty(Region) && re != null)
            {
                list = list.Where(o => o.Address == re.RegionName).ToList();
            }




            if (!string.IsNullOrEmpty(HospId))
            {
                list = list.Where(o => o.HospId == HospId).ToList();
            }




            if (!string.IsNullOrEmpty(levelID))
            {
                if (levelID == "1")
                {
                    levelID = "三";
                }
                else if (levelID == "2")
                {
                    levelID = "二";
                }
                else
                {
                    levelID = "一";
                }

                list = list.Where(o => o.Grade.Contains(levelID)).ToList();
            }


            if (!string.IsNullOrEmpty(GradeID))
            {
                if (GradeID == "0")
                {
                    GradeID = "特";
                }
                else if (GradeID == "1")
                {
                    GradeID = "甲";
                }
                else if (GradeID == "2")
                {
                    GradeID = "乙";
                }
                else
                {
                    GradeID = "丙";
                }

                list = list.Where(o => o.Grade.Contains(GradeID)).ToList();
            }


            DataSet ds = new DataSet();

            DataTable dt = new DataTable();


            dt.Columns.Add(new DataColumn("yue", typeof(System.String)));
            dt.Columns.Add(new DataColumn("157", typeof(System.String)));
            dt.Columns.Add(new DataColumn("158", typeof(System.String)));
            dt.Columns.Add(new DataColumn("159", typeof(System.String)));
            dt.Columns.Add(new DataColumn("160", typeof(System.String)));
            dt.Columns.Add(new DataColumn("161", typeof(System.String)));
            dt.Columns.Add(new DataColumn("162", typeof(System.String)));
            dt.Columns.Add(new DataColumn("163", typeof(System.String)));
            dt.Columns.Add(new DataColumn("164", typeof(System.String)));
            dt.Columns.Add(new DataColumn("165", typeof(System.String)));
            dt.Columns.Add(new DataColumn("166", typeof(System.String)));
            dt.Columns.Add(new DataColumn("167", typeof(System.String)));
            dt.Columns.Add(new DataColumn("168", typeof(System.String)));
            dt.Columns.Add(new DataColumn("112", typeof(System.String)));





            DateTime time = Convert.ToDateTime(nians);

            DateTime time1 = new DateTime(time.Year, 1, 1);
            DateTime time2 = new DateTime(time.Year, 2, 1);



            for (int i = 0; i < 12; i++)
            {
                DataRow now = dt.NewRow();

                now["yue"] = i + 1 + "月";
                now["157"] = list.Where(o => o.DetailType == "157" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["158"] = list.Where(o => o.DetailType == "158" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["159"] = list.Where(o => o.DetailType == "159" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["160"] = list.Where(o => o.DetailType == "160" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["161"] = list.Where(o => o.DetailType == "161" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["162"] = list.Where(o => o.DetailType == "162" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["163"] = list.Where(o => o.DetailType == "163" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["164"] = list.Where(o => o.DetailType == "164" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["165"] = list.Where(o => o.DetailType == "165" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["166"] = list.Where(o => o.DetailType == "166" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["167"] = list.Where(o => o.DetailType == "167" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["168"] = list.Where(o => o.DetailType == "168" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;
                now["112"] = list.Where(o => o.DetailType == "112" && o.HappenedDate >= time1.AddMonths(i) && o.HappenedDate < time2.AddMonths(i)).ToList().Count;

                dt.Rows.Add(now);
            }


            ds.Tables.Add(dt);

            return ExecutDataSetToJson(ds);
        }
        #endregion

        #region 按科室查询上报事件数量
        //按科室查询上报事件数量
        public string GetEventsresumeByhospdepCount(string HospId, string EveresLevel, string yue1, string yue2)
        {

            aers_tbl_eventsresumeSqlMapDao daoeven = new aers_tbl_eventsresumeSqlMapDao();

            IList<aers_tbl_eventsresume> list = daoeven.FindHospByHospId(HospId, "1");


            DateTime time1 = Convert.ToDateTime(yue1);
            DateTime time2 = Convert.ToDateTime(yue2).AddDays(1);


            list = list.Where(o => o.HappenedDate >= time1 && o.HappenedDate < time2).ToList();

            if (!string.IsNullOrEmpty(EveresLevel))
            {
                list = list.Where(o => o.EveresLevel == EveresLevel).ToList();
            }


            aers_tbl_events_ycSqlMapDao dao = new aers_tbl_events_ycSqlMapDao();
            IList<aers_tbl_hospital> listhosp = dao.hospitalFindAll();

            foreach (aers_tbl_eventsresume item in list)
            {
                aers_tbl_hospital hosp = listhosp.FirstOrDefault(o => o.HospId == item.HospId);
                if (hosp != null)
                {
                    item.Address = hosp.Address;
                }
            }

            aers_tbl_hospdepSqlMapDao dalhospdep = new aers_tbl_hospdepSqlMapDao();
            IList<aers_tbl_hospdep> listhospdep = dalhospdep.hospdepFindByHospId(HospId);

            DataSet ds = new DataSet();

            DataTable dt = new DataTable();



            dt.Columns.Add(new DataColumn("HospDepId", typeof(System.String)));
            dt.Columns.Add(new DataColumn("HospDepName", typeof(System.String)));
            dt.Columns.Add(new DataColumn("yc", typeof(System.String)));
            dt.Columns.Add(new DataColumn("qt", typeof(System.String)));
            dt.Columns.Add(new DataColumn("ddzc", typeof(System.String)));
            dt.Columns.Add(new DataColumn("gl", typeof(System.String)));
            dt.Columns.Add(new DataColumn("yh", typeof(System.String)));
            dt.Columns.Add(new DataColumn("gy", typeof(System.String)));
            dt.Columns.Add(new DataColumn("zybl", typeof(System.String)));
            dt.Columns.Add(new DataColumn("lsj", typeof(System.String)));
            dt.Columns.Add(new DataColumn("hj", typeof(System.String)));




            foreach (aers_tbl_hospdep item in listhospdep)
            {
                DataRow now = dt.NewRow();


                now["HospDepId"] = item.HospdepId;
                now["HospDepName"] = item.HospdepName;
                now["yc"] = list.Where(o => o.EveresName == "149" && o.HospDepId == item.HospdepId).ToList().Count;
                now["qt"] = list.Where(o => o.EveresName == "150" && o.HospDepId == item.HospdepId).ToList().Count;
                now["ddzc"] = list.Where(o => o.EveresName == "152" && o.HospDepId == item.HospdepId).ToList().Count;
                now["gl"] = list.Where(o => o.EveresName == "153" && o.HospDepId == item.HospdepId).ToList().Count;
                now["yh"] = list.Where(o => o.EveresName == "154" && o.HospDepId == item.HospdepId).ToList().Count;
                now["gy"] = list.Where(o => o.EveresName == "155" && o.HospDepId == item.HospdepId).ToList().Count;
                now["zybl"] = list.Where(o => o.EveresName == "156" && o.HospDepId == item.HospdepId).ToList().Count;
                now["lsj"] = list.Where(o => o.EveresName == "151" && o.HospDepId == item.HospdepId).ToList().Count;
                now["hj"] = list.Where(o => o.HospDepId == item.HospdepId).ToList().Count;


                dt.Rows.Add(now);
            }




            ds.Tables.Add(dt);

            return ExecutDataSetToJson(ds);
        }
        #endregion

        #region 
        public IList<aers_tbl_hospdep> GetEveCountByhospdep(string HospId, string Status, string yue)
        {

            aers_tbl_eventsresumeSqlMapDao daoeven = new aers_tbl_eventsresumeSqlMapDao();

            IList<aers_tbl_eventsresume> list = daoeven.FindHospByHospId(HospId, "1");


            DateTime time = DateTime.Now;

            DateTime time1 = Convert.ToDateTime(yue);
            DateTime time2 = Convert.ToDateTime(yue).AddMonths(1);


            list = list.Where(o => o.HappenedDate >= time1 && o.HappenedDate < time2).ToList();

            aers_tbl_hospdepSqlMapDao dalhospdep = new aers_tbl_hospdepSqlMapDao();
            IList<aers_tbl_hospdep> listdep = dalhospdep.hospdepFindByHospId(HospId);


            foreach (aers_tbl_hospdep item in listdep)
            {
                item.EveCount = list.Where(o => o.HospDepId == item.HospdepId).ToList().Count;

            }

            if (Status == "1")
            {
                listdep = listdep.Where(o => o.EveCount == 0).ToList();
            }
            else if (Status == "2")
            {
                listdep = listdep.Where(o => o.EveCount > 0).ToList();
            }



            return listdep;
        }
        #endregion

        #region 导出excel
        //  Dictionary<>可先将字典表取出来放到字典里，速度能快点

        #region 前台字典英汉转化
        Dictionary<string, string> dicData = new Dictionary<string, string>()
        {
            { "DdzcType","所属类别"},
            {"EventLevel","事件等级"},
            {"patientSex","患者性别"},
            {"NursLevel","护理级别"},
            {"IsEvaluate","是否高危"},
            {"DzPlace","跌坠位置"},
            {"DzState","跌坠时状态"},
            {"MentalState","精神状态"},
            {"SelfcareState","自理能力"},

            {"FomesFrom","污染物来源"},
            {"DzHazards","危险因素"},
            {"DzHistory","跌坠史"},
            {"IsTakePrevent","采取防范措施"},
            {"GroundState","地面情况"},
            {"IndoorState","室内光线"},
            {"EscortState","是否有人陪护"},
            {"StaffPosition","护士职称"},
            {"StaffWorkyears","护士工龄"},
            {"StaffType","护士类别"},
            {"StaffEducation","护士学历"},
            {"timeslot","发生时间"},
            {"ageslot","患者年龄"},


           // {"DdzcType","所属类别"},
          //  {"EventLevel","事件等级"},
            //{"patientSex","患者性别"},
           // {"NursLevel","护理级别"},
           // {"IsEvaluate","是否高危"},
            //{"DzPlace","跌坠位置"},
            //{"DzState","跌坠时状态"},
            //{"MentalState","精神状态"},
           // {"SelfcareState","自理能力"},
           // {"DzHazards","相关因素"},
           // {"DzHistory","跌坠史"},
           // {"IsTakePrevent","采取防范措施"},
           // {"GroundState","地面情况"},
           // {"IndoorState","室内光线"},
           // {"EscortState","是否有人陪护"},
           // {"StaffPosition","护士职称"},
           // {"StaffWorkyears","护士工龄"},
           // {"StaffType","护士类别"},
           // {"StaffEducation","护士学历"},
            //{"timeslot","发生时间"},
           // {"ageslot","患者年龄"},

            //{"DdzcType","所属类别"},
           // {"EventLevel","事件等级"},
           // {"patientSex","患者性别"},
           // {"NursLevel","护理级别"},
           // {"IsEvaluate","是否高危"},
           // {"DzPlace","跌坠位置"},
            //{"DzState","跌坠时状态"},
           // {"MentalState","精神状态"},
          //  {"SelfcareState","自理能力"},
          //  {"DzHistory","跌坠史"},
           // {"IsTakePrevent","采取防范措施"},
           // {"IndoorState","室内光线"},
           // {"EscortState","是否有人陪护"},
           // {"StaffPosition","护士职称"},
           // {"StaffWorkyears","护士工龄"},
           // {"StaffType","护士类别"},
          //  {"StaffEducation","护士学历"},
           // {"timeslot","发生时间"},
           // {"ageslot","患者年龄"},


           // {"EventLevel","事件等级"},
            {"PatientExpose","患者情况"},
            {"ExposeTypeOne","暴露方式"},
            {"HarmDegree","损伤程度"},
            {"FomesForm","污染物来源"},
            {"PharmacyState","预防性用药"},
            {"UseDrug","使用的药物"},
            //  {"timeslot","发生时间"},
            //  {"ageslot","患者年龄"},


            // {"DdzcType","所属类别"},
            //{"EventLevel","事件等级"},
            //{"patientSex","患者性别"},
            // {"NursLevel","护理级别"},
            // {"IsEvaluate","是否高危"},
            //{"DzPlace","跌坠位置"},
            //{"DzState","跌坠时状态"},
            // {"MentalState","精神状态"},
            // {"SelfcareState","自理能力"},
            // {"DzHistory","跌坠史"},
            // {"IsTakePrevent","采取防范措施"},
            // {"IndoorState","室内光线"},
            // {"EscortState","是否有人陪护"},
            // {"StaffPosition","护士职称"},
            // {"StaffWorkyears","护士工龄"},
            // {"StaffType","护士类别"},
            //  {"StaffEducation","护士学历"},
            // {"timeslot","发生时间"},
            //  {"ageslot","患者年龄"},

            {"Dispensation","静配中心配药"},
            { "PDAState","信息化管理（PDA）"},
            { "PutDrugs","实施单剂量摆药"},
           // {"patientSex","患者性别"},
           // {"NursLevel","护理级别"},
            {"IsEdema","是否水肿"},
            {"Assessment","评估量表"},
           // {"IsTakePrevent","采取防范措施"},
            {"Highriskgrade","高危等级"},
            {"HighRiskLevel","是否高危"},
            {"OutcomeState","转归"},
          //  {"timeslot","发生时间"},
           // {"ageslot","患者年龄"},

           // {"patientSex","患者性别"},
           // {"EventLevel","事件等级"},
           // {"patientSex","患者性别"},
            //{"NursLevel","护理级别"},
           // {"IsEvaluate","是否高危"},
           // {"IsTakePrevent","采取防范措施"},
            {"GlTypeOne","管路类型"},
            {"FixedWay","固定方法"},
            {"OutGlState","脱管时状态"},
           // {"MentalState","精神状态"},
            {"ActivityAbility","活动能力"},
            {"SelfcareAbility","生活自理能力"},
            {"OutGlReason","托管原因"},
            {"OtherReasons","其他因素"},
            {"ResetGl","重新再置管"},
            {"Complication","并发症"},
           // {"timeslot","发生时间"},
           // {"ageslot","患者年龄"},
        };
        #endregion

        #region 导出为excel到服务器
        public string ExportToExcel(DataTable dt, string fileName)
        {
            //  string path = "D:\\";
            string rootPath = HttpRuntime.BinDirectory.ToString();
            string path = rootPath.Remove(rootPath.Length - 4) + "ExcelData";  //剪掉bin\ 加上ExcelData
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string returnFileName = fileName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            string saveFileName = path + "\\" + returnFileName;

            try
            {
                Common.ExportToExcel(dt ,saveFileName ,null);
                return "103+" + returnFileName;
            }
            catch (Exception ex)
            {
                return "104" + ex;  //导出失败时返回104
            }
        }
        #endregion

        #region 数据分析-事件明细
        //事件明细
        public string findproeventexcel(string hid, string Region, string Time1, string Time2, string EventType, string EveresLevel)
        {
            aers_sys_statecodeSqlMapDao codedao = new aers_sys_statecodeSqlMapDao();
            IList<aers_sys_statecode> codelist = codedao.FindAll();  //取字典
                                                                     // foreach (aers_sys_statecode item in codelist.Where(o => o.Classify == "level").ToList())
            var list = findproevent(hid, Region, Time1, Time2, EventType, EveresLevel);

            DataTable dt = Common.ToDataTable(list);  //list先转化为datatable;
                                                       //datatable进行改造
            DataTable myDt = dt;
            //删除列  
            myDt.Columns.Remove("SpellNo");
            myDt.Columns.Remove("FeedbackState");
            myDt.Columns.Remove("ExamineState");
            // myDt.Columns.Remove("FeedbackState");

            myDt.Columns.Remove("Remark");
            myDt.Columns.Remove("IsFlag");
            myDt.Columns.Remove("OperatorDate");
            myDt.Columns.Remove("OperatorID");

            myDt.Columns.Remove("Address");
            myDt.Columns.Remove("FileName");
            myDt.Columns.Remove("Grade");

            //修改列标题名称  
            myDt.Columns["EveresId"].ColumnName = "事件编号";
            myDt.Columns["EveresName"].ColumnName = "事件类别";
            myDt.Columns["EveresLevel"].ColumnName = "事件等级";
            myDt.Columns["HappenedDate"].ColumnName = "发生日期";
            myDt.Columns["SendtoDate"].ColumnName = "上报日期";
            myDt.Columns["HospDepId"].ColumnName = "科室";
            myDt.Columns["HospId"].ColumnName = "医院";

            for (int i = 0; i < myDt.Rows.Count; i++)
            {
                myDt.Rows[i]["事件类别"] = codelist.FirstOrDefault(o => o.ECodeValue == (myDt.Rows[i]["事件类别"].ToString())).ECodeTag;
                myDt.Rows[i]["事件等级"] = codelist.FirstOrDefault(o => o.ECodeValue == (myDt.Rows[i]["事件等级"].ToString())).ECodeTag;
            }
            ////测试导出速度
            //DataTable ddt = new DataTable("测试");
            //try
            //{
            //    ddt.Columns.Add("测试1", typeof(string));
            //    ddt.Columns.Add("测试2", typeof(string));
            //    ddt.Columns.Add("测试3", typeof(string));
            //    ddt.Columns.Add("测试4", typeof(string));
            //    ddt.Columns.Add("测试5", typeof(string));
            //    for (int i = 0; i < 60000; i++)
            //    {
            //        DataRow dr = ddt.NewRow();
            //        dr["医院编号"] = i.ToString();
            //        dr["医院名称"] = "西京医院西京医院西京医院西京医院西京医院西京医院西京医院西京医院西京医院" + i.ToString();
            //        dr["医院等级"] = "一级";
            //        dr["上报事件数量"] = i.ToString();
            //        dr["医院编号1"] = i.ToString();
            //        dr["医院名称1"] = "西京医院西京医院西京医院西京医院西京医院西京医院西京医院西京医院西京医院" + i.ToString();
            //        dr["医院等级1"] = "一级";
            //        dr["上报事件数量1"] = i.ToString();
            //        dr["医院编号2"] = i.ToString();
            //        dr["医院名称2"] = "西京医院西京医院西京医院西京医院西京医院西京医院西京医院西京医院西京医院" + i.ToString();
            //        dr["医院等级2"] = "一级";
            //        dr["上报事件数量2"] = i.ToString();
            //        dr["医院编号3"] = i.ToString();
            //        dr["医院名称3"] = "西京医院西京医院西京医院西京医院西京医院西京医院西京医院西京医院西京医院" + i.ToString();
            //        dr["医院等级3"] = "一级";
            //        dr["上报事件数量3"] = i.ToString();
            //        dr["医院编号4"] = i.ToString();
            //        dr["医院名称4"] = "西京医院西京医院西京医院西京医院西京医院西京医院西京医院西京医院西京医院" + i.ToString();
            //        dr["医院等级4"] = "一级";
            //        dr["上报事件数量4"] = i.ToString();

            //        ddt.Rows.Add(dr);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    var ddd = ex;
            //}

            return ExportToExcel(myDt, " 数据分析-事件明细");
        }
        #endregion

        #region  数据分析-按医院统计
        //按医院统计
        public string GetEveCountByhospitalexcel(string Status, string Region, string yue)
        {
            var list = GetEveCountByhospital(Status, Region, yue);

            DataTable dt = Common.ToDataTable(list);  //list先转化为datatable;
                                                       //datatable进行改造
            DataTable myDt = dt;
            //删除列  
            myDt.Columns.Remove("Address");
            myDt.Columns.Remove("Phone");
            myDt.Columns.Remove("Contact");

            myDt.Columns.Remove("Validitytime");
            myDt.Columns.Remove("IsFlag");
            myDt.Columns.Remove("Remarks");
            myDt.Columns.Remove("OperatorID");

            myDt.Columns.Remove("OperatorDate");
            myDt.Columns.Remove("Hosplogo");
            myDt.Columns.Remove("HospUnion");

            //修改列标题名称  
            myDt.Columns["HospId"].ColumnName = "医院编号";
            myDt.Columns["HospName"].ColumnName = "医院名称";
            myDt.Columns["Grade"].ColumnName = "医院等级";
            myDt.Columns["EveCount"].ColumnName = "上报事件数量";

            return ExportToExcel(myDt, "数据分析-按医院统计");
        }
        #endregion

        #region  数据分析-按事件数量统计    
        //按事件数量统计
        public string GetEventsresumeByCountexcel(string nians, string Region, string HospId, string levelID, string GradeID, string EveresLevel)
        {
            var jsonData = GetEventsresumeByCount(nians, Region, HospId, levelID, GradeID, EveresLevel);

            DataTable dt = Common.JsonToDataTable(jsonData);
            //  DataTable dt = ToExcel.ToDataTable(list);  //list先转化为datatable;
            //datatable进行改造
            DataTable myDt = dt;
            //修改列标题名称  
            myDt.Columns["yue"].ColumnName = "月份";
            myDt.Columns["yc"].ColumnName = "压疮评估";
            myDt.Columns["qt"].ColumnName = "其他事件";
            myDt.Columns["ddzc"].ColumnName = "跌倒坠床";

            myDt.Columns["gl"].ColumnName = "管路事件";
            myDt.Columns["yh"].ColumnName = "隐患事件";
            myDt.Columns["gy"].ColumnName = "给药事件";
            myDt.Columns["zybl"].ColumnName = "职业暴露";

            myDt.Columns["lsj"].ColumnName = "零事件";
            myDt.Columns["hj"].ColumnName = "合计";
            string mes;
            if (HospId == "" && levelID == "" && GradeID == "" && EveresLevel == "")
            {
                mes = "首页";
            }
            else
            {
                mes = "数据分析-按事件数量统计";
            }

            return ExportToExcel(myDt, mes);
        }
        #endregion

        #region  数据分析-其他事件统计
        public string GetEventQtByCountexcel(string nians, string Region, string HospId, string levelID, string GradeID, string EveresLevel)
        {
            var jsonData = GetEventQtByCount(nians, Region, HospId, levelID, GradeID, EveresLevel);

            DataTable dt = Common.JsonToDataTable(jsonData);
            DataTable myDt = dt;
            //修改列标题名称  
            myDt.Columns["yue"].ColumnName = "月份";
            myDt.Columns["157"].ColumnName = "误吸/窒息事件";
            myDt.Columns["158"].ColumnName = "输血事件";
            myDt.Columns["159"].ColumnName = "烧烫伤事件";

            myDt.Columns["160"].ColumnName = "冻伤事件";
            myDt.Columns["161"].ColumnName = "输液外渗";
            myDt.Columns["162"].ColumnName = "走失事件";
            myDt.Columns["163"].ColumnName = "自杀事件";

            myDt.Columns["164"].ColumnName = "病理标本事件";
            myDt.Columns["165"].ColumnName = "检验标本事件";
            myDt.Columns["166"].ColumnName = "电击伤事件";
            myDt.Columns["167"].ColumnName = "毒麻管制药品事件";
            myDt.Columns["168"].ColumnName = "伤医事件";
            myDt.Columns["112"].ColumnName = "其他事件";
            return ExportToExcel(myDt, "数据分析-其他事件统计");
        }
        #endregion

        #region  数据分析-未列出的其他事件统计
        public string FindGroupByNameexcel(string time1)
        {
            var jsonData = FindGroupByName(time1);

            DataTable dt = Common.JsonToDataTable(jsonData);
            DataTable myDt = dt;
            myDt.Columns["DetailTypeQt"].ColumnName = "事件名称";
            myDt.Columns["ccc"].ColumnName = "数量";
            return ExportToExcel(myDt, "数据分析-暂未列出的其他事件统计");
        }
        #endregion

        #region   数据分析-各事件占比统计
        public string GetEventReportexcel(string yue1, string yue2, string Region, string HospId)
        {
            var jsonData = GetEventReport(yue1, yue2, Region, HospId);

            DataTable dt = Common.JsonToDataTable(jsonData);
            DataTable myDt = dt;
            myDt.Columns.Remove("Id");
            myDt.Columns["Name"].ColumnName = "事件名称";
            myDt.Columns["Value"].ColumnName = "数量";
            myDt.Columns["Ratio"].ColumnName = "百分比";
            return ExportToExcel(myDt, "数据分析-各事件占比统计");
        }
        #endregion

        #region  数据分析-科室上报明细
        public string GetEventsresumeByhospdepCountexcel(string HospId, string EveresLevel, string yue1, string yue2)
        {
            var jsonData = GetEventsresumeByhospdepCount(HospId, EveresLevel, yue1, yue2);

            DataTable dt = Common.JsonToDataTable(jsonData);
            DataTable myDt = dt;
            myDt.Columns.Remove("HospDepId");
            myDt.Columns["HospDepName"].ColumnName = "科室";
            myDt.Columns["yc"].ColumnName = "压疮评估";
            myDt.Columns["qt"].ColumnName = "其他事件";
            myDt.Columns["ddzc"].ColumnName = "跌倒坠床";
            myDt.Columns["gl"].ColumnName = "管路事件";
            myDt.Columns["yh"].ColumnName = "隐患事件";
            myDt.Columns["gy"].ColumnName = "给药事件";
            myDt.Columns["zybl"].ColumnName = "职业暴露";
            myDt.Columns["lsj"].ColumnName = "零事件";
            myDt.Columns["hj"].ColumnName = "合计";
            return ExportToExcel(myDt, "数据分析-科室上报明细");
        }
        #region  数据分析-已上报未上报科室
        public string GetEveCountByhospdepexcel(string HospId, string Status, string yue)
        {
            var list = GetEveCountByhospdep(HospId, Status, yue);

            DataTable dt = Common.ToDataTable(list);  //list先转化为datatable;
                                                       //datatable进行改造
            DataTable myDt = dt;
            //删除列  
            myDt.Columns.Remove("HospdepId");
            myDt.Columns.Remove("BasedepId");
            myDt.Columns.Remove("HospId");

            myDt.Columns.Remove("SpellNo");
            myDt.Columns.Remove("HospdepLogo");
            myDt.Columns.Remove("DisplayOrder");
            myDt.Columns.Remove("IsFlag");

            myDt.Columns.Remove("Remarks");
            myDt.Columns.Remove("OperatorId");
            myDt.Columns.Remove("OperatorDate");

            myDt.Columns.Remove("HospName");
            myDt.Columns.Remove("Grade");

            //修改列标题名称  
            myDt.Columns["HospdepName"].ColumnName = "科室名称";
            myDt.Columns["EveCount"].ColumnName = "上报事件数量";

            return ExportToExcel(myDt, "数据分析-已上报未上报科室");
        }
        #endregion
        #endregion

        #region 按事件统计-跌倒事件
        public string GetEventddzcReportexcel(string yue1, string yue2, string Region, string HospId)
        {
            var jsonData = GetEventddzcReport(yue1, yue2, Region, HospId);

            //DzHazards
            DataTable dt = Common.JsonToDataTable(jsonData);
            DataTable myDt = dt;
            myDt.Columns["Id"].ColumnName = "项目";
            myDt.Columns["Name"].ColumnName = "项目细分";
            myDt.Columns["Value"].ColumnName = "数量";
            myDt.Columns["Ratio"].ColumnName = "百分比";
            for (int i = 0; i < myDt.Rows.Count; i++)
            {
                myDt.Rows[i]["项目"] = dicData.FirstOrDefault(o => o.Key == (myDt.Rows[i]["项目"].ToString())).Value;
            }
            return ExportToExcel(myDt, "按事件统计-跌倒事件");
        }
        #endregion

        #region 按事件统计-坠床事件
        public string GetEventZcReportexcel(string yue1, string yue2, string Region, string HospId)
        {
            var jsonData = GetEventZcReport(yue1, yue2, Region, HospId);

            DataTable dt = Common.JsonToDataTable(jsonData);
            DataTable myDt = dt;
            myDt.Columns["Id"].ColumnName = "项目";
            myDt.Columns["Name"].ColumnName = "项目细分";
            myDt.Columns["Value"].ColumnName = "数量";
            myDt.Columns["Ratio"].ColumnName = "百分比";
            for (int i = 0; i < myDt.Rows.Count; i++)
            {
                myDt.Rows[i]["项目"] = dicData.FirstOrDefault(o => o.Key == (myDt.Rows[i]["项目"].ToString())).Value;
            }
            return ExportToExcel(myDt, "按事件统计-坠床事件");
        }
        #endregion

        #region 按事件统计-管路事件
        public string GetEventGlReportexcel(string yue1, string yue2, string Region, string HospId)
        {
            var jsonData = GetEventGlReport(yue1, yue2, Region, HospId);

            DataTable dt = Common.JsonToDataTable(jsonData);
            DataTable myDt = dt;
            myDt.Columns["Id"].ColumnName = "项目";
            myDt.Columns["Name"].ColumnName = "项目细分";
            myDt.Columns["Value"].ColumnName = "数量";
            myDt.Columns["Ratio"].ColumnName = "百分比";
            for (int i = 0; i < myDt.Rows.Count; i++)
            {
                myDt.Rows[i]["项目"] = dicData.FirstOrDefault(o => o.Key == (myDt.Rows[i]["项目"].ToString())).Value;
            }
            return ExportToExcel(myDt, "按事件统计-管路事件");
        }
        #endregion

        #region 按事件统计-给药事件
        public string GetEventGyReportexcel(string yue1, string yue2, string Region, string HospId)
        {
            var jsonData = GetEventGyReport(yue1, yue2, Region, HospId);

            DataTable dt = Common.JsonToDataTable(jsonData);
            DataTable myDt = dt;
            myDt.Columns["Id"].ColumnName = "项目";
            myDt.Columns["Name"].ColumnName = "项目细分";
            myDt.Columns["Value"].ColumnName = "数量";
            myDt.Columns["Ratio"].ColumnName = "百分比";
            for (int i = 0; i < myDt.Rows.Count; i++)
            {
                myDt.Rows[i]["项目"] = dicData.FirstOrDefault(o => o.Key == (myDt.Rows[i]["项目"].ToString())).Value;
            }
            return ExportToExcel(myDt, "按事件统计-给药事件");
        }
        #endregion

        #region  按事件统计-压疮事件
        public string GetEventYcReportexcel(string yue1, string yue2, string Region, string HospId)
        {
            var jsonData = GetEventYcReport(yue1, yue2, Region, HospId);

            DataTable dt = Common.JsonToDataTable(jsonData);
            DataTable myDt = dt;
            myDt.Columns["Id"].ColumnName = "项目";
            myDt.Columns["Name"].ColumnName = "项目细分";
            myDt.Columns["Value"].ColumnName = "数量";
            myDt.Columns["Ratio"].ColumnName = "百分比";
            for (int i = 0; i < myDt.Rows.Count; i++)
            {
                myDt.Rows[i]["项目"] = dicData.FirstOrDefault(o => o.Key == (myDt.Rows[i]["项目"].ToString())).Value;
            }
            return ExportToExcel(myDt, "按事件统计-压疮事件");
        }
        #endregion

        #region 按事件统计-职业暴露
        public string GetEventZyblReportexcel(string yue1, string yue2, string Region, string HospId)
        {
            var jsonData = GetEventZyblReport(yue1, yue2, Region, HospId);

            DataTable dt = Common.JsonToDataTable(jsonData);
            DataTable myDt = dt;
            myDt.Columns["Id"].ColumnName = "项目";
            myDt.Columns["Name"].ColumnName = "项目细分";
            myDt.Columns["Value"].ColumnName = "数量";
            myDt.Columns["Ratio"].ColumnName = "百分比";
            for (int i = 0; i < myDt.Rows.Count; i++)
            {
                myDt.Rows[i]["项目"] = dicData.FirstOrDefault(o => o.Key == (myDt.Rows[i]["项目"].ToString())).Value;
            }
            return ExportToExcel(myDt, "按事件统计-职业暴露");
        }
        #endregion

        #region 首页
        public string GetEventsresumeByCountSYexcel(string nians, string Region)
        {
            var jsonData = GetEventsresumeByCount(nians, Region, "", "", "", "");

            DataTable dt = Common.JsonToDataTable(jsonData);
            //  DataTable dt = ToExcel.ToDataTable(list);  //list先转化为datatable;
            //datatable进行改造
            DataTable myDt = dt;
            //修改列标题名称  
            myDt.Columns["yue"].ColumnName = "月份";
            myDt.Columns["yc"].ColumnName = "压疮评估";
            myDt.Columns["qt"].ColumnName = "其他事件";
            myDt.Columns["ddzc"].ColumnName = "跌倒坠床";

            myDt.Columns["gl"].ColumnName = "管路事件";
            myDt.Columns["yh"].ColumnName = "隐患事件";
            myDt.Columns["gy"].ColumnName = "给药事件";
            myDt.Columns["zybl"].ColumnName = "职业暴露";

            myDt.Columns["lsj"].ColumnName = "零事件";
            myDt.Columns["hj"].ColumnName = "合计";

            return ExportToExcel(myDt, "首页");
        }
        #endregion

        #region 事件审核-待审核
        public string findndepwaitexcel(string uid, string Time1, string Time2, string EveresLevel, string EventType, string ReUId, string LoginKey)
        {
            var list = findndepwait(uid, Time1, Time2, EveresLevel, EventType, ReUId, LoginKey);

            DataTable dt = Common.ToDataTable(list);  //list先转化为datatable;
                                                       //datatable进行改造
            DataTable myDt = dt;
            //删除列  
            myDt.Columns.Remove("SpellNo");
            myDt.Columns.Remove("HospId");
            myDt.Columns.Remove("Remark");

            myDt.Columns.Remove("IsFlag");
            myDt.Columns.Remove("OperatorDate");
            myDt.Columns.Remove("OperatorID");

            myDt.Columns.Remove("Address");
            myDt.Columns.Remove("FileName");
            myDt.Columns.Remove("Grade");

            //此处FeedbackState数据库字段存的是int类型，0和1，要改成 无 和 有 ，需要改变dt中的数据类型，可以克隆dt进行修改，但数据量大，此处可添加新列 反馈  ，把FeedbackState中的数据转存过来，然后删掉FeedbackState列
            myDt.Columns.Add("反馈", typeof(string));
            //修改列标题名称  
            myDt.Columns["EveresId"].ColumnName = "事件编号";
            myDt.Columns["EveresName"].ColumnName = "事件类别";
            myDt.Columns["EveresLevel"].ColumnName = "事件等级";
            myDt.Columns["HappenedDate"].ColumnName = "发生日期";
            myDt.Columns["SendtoDate"].ColumnName = "上报日期";
            myDt.Columns["HospDepId"].ColumnName = "科室";
            // myDt.Columns["FeedbackState"].ColumnName = "反馈";
            myDt.Columns["ExamineState"].ColumnName = "上报状态";

            aers_sys_statecodeSqlMapDao codedao = new aers_sys_statecodeSqlMapDao();
            IList<aers_sys_statecode> codelist = codedao.FindAll();  //取字典
            try
            {
                //foreach (DataColumn  item in myDt.Columns)
                //{
                //    if (item .ColumnName == "反馈")
                //    {
                //        item.DataType = typeof(String);
                //    }
                //}
                for (int i = 0; i < myDt.Rows.Count; i++)
                {
                    myDt.Rows[i]["事件类别"] = codelist.FirstOrDefault(o => o.ECodeValue == (myDt.Rows[i]["事件类别"].ToString())).ECodeTag;
                    myDt.Rows[i]["事件等级"] = codelist.FirstOrDefault(o => o.ECodeValue == (myDt.Rows[i]["事件等级"].ToString())).ECodeTag;
                    myDt.Rows[i]["反馈"] = (myDt.Rows[i]["FeedbackState"].ToString() == "0") ? "无" : "有";  //根据FeedbackState的值给 反馈 中填值
                }
                myDt.Columns.Remove("FeedbackState");
            }
            catch (Exception ex)
            {
                var dfs = ex;
            }
            return ExportToExcel(myDt, "事件审核-待审核");
        }
        #endregion

        #region 事件审核-已通过
        public string findndepeventexcel(string uid, string Time1, string Time2, string EveresLevel, string EventType, string ReUId, string LoginKey)
        {
            var list = findndepevent(uid, Time1, Time2, EveresLevel, EventType, ReUId, LoginKey);

            DataTable dt = Common.ToDataTable(list);  //list先转化为datatable;
                                                       //datatable进行改造
            DataTable myDt = dt;
            //删除列  
            myDt.Columns.Remove("SpellNo");
            myDt.Columns.Remove("HospId");
            myDt.Columns.Remove("Remark");

            myDt.Columns.Remove("IsFlag");
            myDt.Columns.Remove("OperatorDate");
            myDt.Columns.Remove("OperatorID");

            myDt.Columns.Remove("Address");
            myDt.Columns.Remove("FileName");
            myDt.Columns.Remove("Grade");

            //此处FeedbackState数据库字段存的是int类型，0和1，要改成 无 和 有 ，需要改变dt中的数据类型，可以克隆dt进行修改，但数据量大，此处可添加新列 反馈  ，把FeedbackState中的数据转存过来，然后删掉FeedbackState列
            myDt.Columns.Add("反馈", typeof(string));
            //修改列标题名称  
            myDt.Columns["EveresId"].ColumnName = "事件编号";
            myDt.Columns["EveresName"].ColumnName = "事件类别";
            myDt.Columns["EveresLevel"].ColumnName = "事件等级";
            myDt.Columns["HappenedDate"].ColumnName = "发生日期";
            myDt.Columns["SendtoDate"].ColumnName = "上报日期";
            myDt.Columns["HospDepId"].ColumnName = "科室";
            // myDt.Columns["FeedbackState"].ColumnName = "反馈";
            myDt.Columns["ExamineState"].ColumnName = "上报状态";

            aers_sys_statecodeSqlMapDao codedao = new aers_sys_statecodeSqlMapDao();
            IList<aers_sys_statecode> codelist = codedao.FindAll();  //取字典
            try
            {
                for (int i = 0; i < myDt.Rows.Count; i++)
                {
                    myDt.Rows[i]["事件类别"] = codelist.FirstOrDefault(o => o.ECodeValue == (myDt.Rows[i]["事件类别"].ToString())).ECodeTag;
                    myDt.Rows[i]["事件等级"] = codelist.FirstOrDefault(o => o.ECodeValue == (myDt.Rows[i]["事件等级"].ToString())).ECodeTag;
                    myDt.Rows[i]["反馈"] = (myDt.Rows[i]["FeedbackState"].ToString() == "0") ? "无" : "有";  //根据FeedbackState的值给 反馈 中填值
                }
                myDt.Columns.Remove("FeedbackState");
            }
            catch (Exception ex)
            {
                var dfs = ex;
            }
            return ExportToExcel(myDt, "事件审核-已通过");
        }
        #endregion

        #region 事件审核-未通过
        public string findndepnoeventexcel(string uid, string Time1, string Time2, string EveresLevel, string EventType, string ReUId, string LoginKey)
        {
            var list = findndepnoevent(uid, Time1, Time2, EveresLevel, EventType, ReUId, LoginKey);
            DataTable dt = Common.ToDataTable(list);  //list先转化为datatable;                                         //datatable进行改造
            DataTable myDt = dt;
            //删除列  
            myDt.Columns.Remove("SpellNo");
            myDt.Columns.Remove("HospId");
            myDt.Columns.Remove("Remark");

            myDt.Columns.Remove("IsFlag");
            myDt.Columns.Remove("OperatorDate");
            myDt.Columns.Remove("OperatorID");

            myDt.Columns.Remove("Address");
            myDt.Columns.Remove("FileName");
            myDt.Columns.Remove("Grade");

            //此处FeedbackState数据库字段存的是int类型，0和1，要改成 无 和 有 ，需要改变dt中的数据类型，可以克隆dt进行修改，但数据量大，此处可添加新列 反馈  ，把FeedbackState中的数据转存过来，然后删掉FeedbackState列
            myDt.Columns.Add("反馈", typeof(string));
            //修改列标题名称  
            myDt.Columns["EveresId"].ColumnName = "事件编号";
            myDt.Columns["EveresName"].ColumnName = "事件类别";
            myDt.Columns["EveresLevel"].ColumnName = "事件等级";
            myDt.Columns["HappenedDate"].ColumnName = "发生日期";
            myDt.Columns["SendtoDate"].ColumnName = "上报日期";
            myDt.Columns["HospDepId"].ColumnName = "科室";
            // myDt.Columns["FeedbackState"].ColumnName = "反馈";
            myDt.Columns["ExamineState"].ColumnName = "上报状态";

            aers_sys_statecodeSqlMapDao codedao = new aers_sys_statecodeSqlMapDao();
            IList<aers_sys_statecode> codelist = codedao.FindAll();  //取字典
            try
            {
                for (int i = 0; i < myDt.Rows.Count; i++)
                {
                    myDt.Rows[i]["事件类别"] = codelist.FirstOrDefault(o => o.ECodeValue == (myDt.Rows[i]["事件类别"].ToString())).ECodeTag;
                    myDt.Rows[i]["事件等级"] = codelist.FirstOrDefault(o => o.ECodeValue == (myDt.Rows[i]["事件等级"].ToString())).ECodeTag;
                    myDt.Rows[i]["反馈"] = (myDt.Rows[i]["FeedbackState"].ToString() == "0") ? "无" : "有";  //根据FeedbackState的值给 反馈 中填值
                }
                myDt.Columns.Remove("FeedbackState");
            }
            catch (Exception ex)
            {
                var dfs = ex;
            }
            return ExportToExcel(myDt, "事件审核-未通过");
        }
        #endregion

        #region 事件审核—草稿
        public string findndepreportexcel(string uid, string Time1, string Time2, string EveresLevel, string EventType, string ReUId, string LoginKey)
        {
            var list = findndepreport(uid, Time1, Time2, EveresLevel, EventType, ReUId, LoginKey);

            DataTable dt = Common.ToDataTable(list);  //list先转化为datatable;
                                                       //datatable进行改造
            DataTable myDt = dt;
            //删除列  
            myDt.Columns.Remove("SpellNo");
            myDt.Columns.Remove("HospId");
            myDt.Columns.Remove("Remark");

            myDt.Columns.Remove("IsFlag");
            myDt.Columns.Remove("OperatorDate");
            myDt.Columns.Remove("OperatorID");

            myDt.Columns.Remove("Address");
            myDt.Columns.Remove("FileName");
            myDt.Columns.Remove("Grade");

            //此处FeedbackState数据库字段存的是int类型，0和1，要改成 无 和 有 ，需要改变dt中的数据类型，可以克隆dt进行修改，但数据量大，此处可添加新列 反馈  ，把FeedbackState中的数据转存过来，然后删掉FeedbackState列
            myDt.Columns.Add("反馈", typeof(string));
            //修改列标题名称  
            myDt.Columns["EveresId"].ColumnName = "事件编号";
            myDt.Columns["EveresName"].ColumnName = "事件类别";
            myDt.Columns["EveresLevel"].ColumnName = "事件等级";
            myDt.Columns["HappenedDate"].ColumnName = "发生日期";
            myDt.Columns["SendtoDate"].ColumnName = "上报日期";
            myDt.Columns["HospDepId"].ColumnName = "科室";
            // myDt.Columns["FeedbackState"].ColumnName = "反馈";
            myDt.Columns["ExamineState"].ColumnName = "上报状态";

            aers_sys_statecodeSqlMapDao codedao = new aers_sys_statecodeSqlMapDao();
            IList<aers_sys_statecode> codelist = codedao.FindAll();  //取字典
            try
            {
                for (int i = 0; i < myDt.Rows.Count; i++)
                {
                    myDt.Rows[i]["事件类别"] = codelist.FirstOrDefault(o => o.ECodeValue == (myDt.Rows[i]["事件类别"].ToString())).ECodeTag;
                    myDt.Rows[i]["事件等级"] = codelist.FirstOrDefault(o => o.ECodeValue == (myDt.Rows[i]["事件等级"].ToString())).ECodeTag;
                    myDt.Rows[i]["反馈"] = (myDt.Rows[i]["FeedbackState"].ToString() == "0") ? "无" : "有";  //根据FeedbackState的值给 反馈 中填值
                }
                myDt.Columns.Remove("FeedbackState");
            }
            catch (Exception ex)
            {
                var dfs = ex;
            }
            return ExportToExcel(myDt, "事件审核-草稿箱");
        }
        #endregion

        #endregion

        #endregion


        #region 护士学堂

        #region 
        public IList<Answer> GetAnswerByUserCourseID(string UserID, string CourseID)
        {
            AnswerSqlMapDao dao = new AnswerSqlMapDao();
            IList<Answer> list = dao.GetAnswerByUserCourseID(UserID, CourseID);
            foreach (Answer item in list)
            {
                item.Results = "****";
            }
            return list;
        }
        #endregion 

        #region   答案回传
        public ResList<Answer> AddAnswerList(IList<Answer> model)
        {

            ResList<Answer> res = new ResList<Answer>();
            res.list = new List<Answer>();

            res.restag = "104";
            try
            {

                foreach (Answer item in model)
                {
                    if (string.IsNullOrEmpty(item.Results))
                    {
                        res.restag = "104答案不能为空";
                        return res;
                    }


                    if (item.QID.Length != 10)
                    {
                        res.restag = "104 问题编号格式不正确";
                        return res;
                    }
                }



                AnswerSqlMapDao dao = new AnswerSqlMapDao();


                int count = dao.InsertAnswerlist(model);

                //2016-6-2  DH
                IList<Answer> list = dao.GetAnswerByUserCourseID(model[0].UserID, model[0].CourseID);


                QuestionsSqlmapDao qdao = new QuestionsSqlmapDao();


                IList<Questions> Qlist = qdao.Questions_FindByChapterID(model[0].CourseID);

                bool isaddjf = true;

                if (list.Count > 0)
                {
                    //判断正确率  增加积分
                    foreach (Answer item in list)
                    {
                        Questions qd = Qlist.FirstOrDefault(o => o.Qid == item.QID);
                        if (qd != null)
                        {
                            if (item.Results != qd.Result)
                            {
                                isaddjf = false;
                                item.Rightkey = qd.Result;
                                res.list.Add(item);
                            }

                        }

                    }

                }


                if (isaddjf)
                {

                    CourseSectionSqlMapDao csdao = new CourseSectionSqlMapDao();

                    CourseSection cs = csdao.CourseSectionFind(model[0].CourseID);

                    if (cs != null)
                    {
                        Integral jf = new Integral();

                        jf.UserID = model[0].UserID;
                        jf.TrainingID = model[0].CourseID;
                        jf.Grade = cs.ChapterPoints.ToString();
                        jf.ModifyDate = DateTime.Now;
                        jf.OperatorID = model[0].UserID;
                        jf.HID = "hp00000001";

                        IntegralSqlMapDao indao = new IntegralSqlMapDao();

                        Integral checkjf = indao.IntegralFind(model[0].UserID, model[0].CourseID);

                        if (checkjf != null)
                        {
                            res.restag = "104已获得积分";
                            return res;
                        }
                        else
                        {
                            indao.IntegralInsert(jf);
                            CreditRecordCheck(model[0].UserID);
                        }

                    }
                }


                //2016-6-2  DH
                res.restag = "103";

            }
            catch (Exception ex)
            {
                res.restag = "104" + ex.Message;
            }

            return res;

        }

        //增加学分方法
        public void CreditRecordCheck(string UserID)
        {


            aers_tbl_staff sta = new aers_tbl_staffSqlMapDao().FindStaffByRid(UserID);
            string HospdepId = sta.DepId;

            CourseSectionSqlMapDao csdao = new CourseSectionSqlMapDao();


            IntegralSqlMapDao indao = new IntegralSqlMapDao();

            CreditRecordSqlMapDao crdao = new CreditRecordSqlMapDao();

            IList<plancontents> list = PlancontentsFindByHospdepId(HospdepId);

            foreach (plancontents item in list)
            {
                IList<CourseSection> listcs = csdao.CourseSectionFindByCourseID(item.CourseID);
                bool isaddxf = true;

                foreach (CourseSection csitem in listcs)
                {
                    Integral checkjf = indao.IntegralFind(UserID, csitem.CourseID);
                    if (checkjf == null)
                    {
                        isaddxf = false;
                    }
                }

                if (isaddxf)
                {
                    CreditRecord cr = new CreditRecord();

                    cr.UserID = UserID;
                    cr.TrainingID = item.CourseID;
                    cr.Grade = item.Credit.ToString();
                    cr.ModifyDate = DateTime.Now;
                    cr.OperatorID = UserID;
                    crdao.CreditRecordInsert(cr);
                }
            }
        }


        #endregion 

        #region 积分排名
        public string StaffFindAllByGrade(int Number)
        {
            aers_tbl_staffSqlMapDao dao = new aers_tbl_staffSqlMapDao();
            DataSet ds = dao.StaffFindAllByGrade(Number);
            return ExecutDataSetToJson(ds);
        }
        #endregion 

        #region 课程管理

        public int CourseInsert(Course course)
        {
            try
            {
                CourseSqlMapDao dao = new CourseSqlMapDao();
                course.CourseID = new aers_sys_seedSqlMapDao().GetMaxID("Course");
                dao.CourseInsert(course);
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int CourseDelete(string CourseID)
        {
            try
            {
                CourseSqlMapDao dao = new CourseSqlMapDao();
                dao.Delete(CourseID);
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int CourseUpdate(Course course)
        {
            try
            {
                CourseSqlMapDao dao = new CourseSqlMapDao();
                dao.CourseUpdate(course);
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public Course CourseFindByCourseID(string CourseID)
        {
            CourseSqlMapDao dao = new CourseSqlMapDao();
            return dao.CourseFindByCourseID(CourseID);
        }

        public IList<Course> CourseFindByUserID(string UserID)
        {
            CourseSqlMapDao dao = new CourseSqlMapDao();
            return dao.CourseFindByUserID(UserID);
        }



        public IList<Course> CourseFindAll()
        {
            CourseSqlMapDao dao = new CourseSqlMapDao();
            return dao.CourseFindAll();
        }

        //PageData<Docinfo> //科室 医院 时间 热门
        public IList<Course> CourseFindPaging(int pageno, int pageSize, string SortType, string Data)
        {
            CourseSqlMapDao dao = new CourseSqlMapDao();

            IList<Course> list = new List<Course>();

            if (SortType == "CourseHot")
            {
                list = dao.CourseFindPaging(pageno, pageSize);
            }
            else if (SortType == "CourseType")
            {
                list = dao.CourseFindPagingCourseType(pageno, pageSize, SortType, Data);
            }
            else if (SortType == "NewTime")
            {
                list = dao.CourseFindPagingNewTime(pageno, pageSize, SortType);
            }
            else if (SortType == "NewTimeDesc")
            {
                list = dao.CourseFindPagingNewTime(pageno, pageSize, SortType);
            }
            else if (SortType == "HISID")
            {
                list = dao.CourseFindPagingNewTime(pageno, pageSize, SortType);
            }
            else
            {
                return dao.CourseFindPaging(pageno, pageSize);
            }

            return list;
        }



        public IList<Course> CourseFindOrderBySortField(string FieldName, int Number)
        {
            CourseSqlMapDao dao = new CourseSqlMapDao();
            if (FieldName == "Recommend")
            {
                return dao.CourseFindOrderByRecommend(Number);
            }
            else if (FieldName == "CourseHot")
            {
                return dao.CourseFindOrderByCourseHot(Number);
            }
            else if (FieldName == "NewTime")
            {
                return dao.CourseFindOrderByNewTime(Number);
            }
            else
            {
                return dao.CourseFindAllByData();
            }
        }



        public Course CourseFindOrderByCourseID(string CourseID)
        {
            CourseSqlMapDao dao = new CourseSqlMapDao();
            return dao.CourseFindOrderByCourseID(CourseID);
        }








        #endregion

        #region 课程目录


        public CourseCatalog CourseCatalogFind(string CatalogID)
        {
            CourseCatalogSqlMapDao dao = new CourseCatalogSqlMapDao();
            return dao.CourseCatalogFind(CatalogID);
        }



        public IList<CourseCatalog> CourseCatalogFindAll()
        {
            CourseCatalogSqlMapDao dao = new CourseCatalogSqlMapDao();
            return dao.CourseCatalogFindAll();
        }

        #endregion 

        #region 课程章节



        public IList<CourseSection> CourseSectionFindAll()
        {
            CourseSectionSqlMapDao dao = new CourseSectionSqlMapDao();
            return dao.CourseSectionFindAll();
        }


        public IList<CourseSection> CourseSectionFindByCourseID(string CourseID)
        {
            CourseSectionSqlMapDao dao = new CourseSectionSqlMapDao();
            return dao.CourseSectionFindByCourseID(CourseID);
        }

        public IList<CourseSection> CourseSectionFindByCatalogID(string CatalogID)
        {
            CourseSectionSqlMapDao dao = new CourseSectionSqlMapDao();
            return dao.CourseSectionFindByCatalogID(CatalogID);
        }




        public CourseSection CourseSectionFind(string ChapterID)
        {
            CourseSectionSqlMapDao dao = new CourseSectionSqlMapDao();
            return dao.CourseSectionFind(ChapterID);
        }

        #endregion

        #region 积分管理


        public IList<Integral> IntegralFindAll()
        {
            IntegralSqlMapDao dao = new IntegralSqlMapDao();
            return dao.IntegralFindAll();
        }


        #endregion

        #region 根据课程ID获取笔记
        public IList<Notes> NotesFindByCourseIDUserID(string CourseID, string UserID)
        {
            NotesSqlMapDao dao = new NotesSqlMapDao();

            IList<Notes> list = dao.NotesFindByCourseID(CourseID);

            list = list.Where(o => o.UserID == UserID).ToList();

            //  return date;
            aers_tbl_staffSqlMapDao stadao = new aers_tbl_staffSqlMapDao();
            IList<aers_tbl_staff> liststa = stadao.staffFindAll();

            foreach (Notes item in list)
            {
                aers_tbl_staff sta = liststa.FirstOrDefault(o => o.ReguserId == item.UserID);
                if (sta != null)
                {
                    item.Name = sta.Name;
                }
            }
            return list;
        }
        #endregion

        #region 
        public string AddNotes(Notes model)
        {
            string res = "104";
            try
            {
                NotesSqlMapDao dao = new NotesSqlMapDao();
                dao.Insert(model);
                res = "103";
            }
            catch (Exception ex)
            {

                res = "104" + ex.Message;
            }
            return res;

        }
        #endregion

        #region 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CourseID"></param>
        /// <param name="pageSize">每页个数</param>
        /// <param name="pageNumber">当前页</param>
        /// <returns></returns>
        public IList<Problem> ProblemFindByCourseID(string CourseID, int pageSize, int pageNumber)
        {
            ProblemSqlMapDao dao = new ProblemSqlMapDao();

            IList<Problem> list = dao.ProblemFindByCourseID(CourseID);
            aers_tbl_staffSqlMapDao stadao = new aers_tbl_staffSqlMapDao();
            IList<aers_tbl_staff> liststa = stadao.staffFindAll();

            foreach (Problem item in list)
            {
                aers_tbl_staff sta = liststa.FirstOrDefault(o => o.ReguserId == item.UserID);
                if (sta != null)
                {
                    item.Name = sta.Name;
                    item.HeadImg = sta.HeadImg;
                }
            }
            return list.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        }
        #endregion

        #region 点赞  //弃用
        public string GetFavourByCourseIDUserID(string CourseID, string UserID)
        {
            //ProblemSqlMapDao dao = new ProblemSqlMapDao();
            //var p = dao.ProblemFindByCourseID(CourseID);


            //List<string> pidList = new List<string>();
            //foreach (var item in p)
            //{
            //    pidList.Add(item.QID);    //problem表主键
            //}

            //FavourSqlMapDao fdao = new FavourSqlMapDao();
            //var fList = fdao.FindAll().Where(o => o.UserID == UserID).AsParallel().ToList();  // Favour表根据用户ID取数据

            //List<string> fmList = new List<string>();
            //foreach (var item in fList)
            //{
            //    fmList.Add(item.MainID);      //favour表中的外键  problem主键
            //}
            //var findList = pidList.Union(fmList).ToList();
            //var findCount = findList.Count;

            //foreach (var item in p)
            //{

            //}

            //DataSet ds = new DataSet();
            //DataTable dt = new DataTable();

            //dt.Columns.Add(new DataColumn("QID", typeof(System.String)));
            //dt.Columns.Add(new DataColumn("CourseID", typeof(System.String)));
            //dt.Columns.Add(new DataColumn("Title", typeof(System.String)));
            //dt.Columns.Add(new DataColumn("UserID", typeof(System.String)));
            //dt.Columns.Add(new DataColumn("ModifyDate", typeof(System.DateTime)));
            //dt.Columns.Add(new DataColumn("FavorCount", typeof(System.Int32)));
            //dt.Columns.Add(new DataColumn("FavorUserOK", typeof(System.String)));

            //DataRow dr = dt.NewRow();

            //for (int i = 0; i < p.Count; i++)
            //{
            //    dr = dt.NewRow();
            //    dr["Qid"] = p[i].QID;
            //    dr["CourseID"] = p[i].CourseID;
            //    dr["Title"] = p[i].Title;
            //    dr["UserID"] = p[i].UserID;
            //    dr["ModifyDate"] = p[i].ModifyDate;
            //    dr["FavorCount"] = p[i].FavorCount;

            // //   dr["FavorUserOK"] = p[i]
            //    dt.Rows.Add(dr);
            //}
            //ds.Tables.Add(dt);
            //return ExecutDataSetToJson(ds);

            return "";

        }
        #endregion

        #region 添加问题
        public string AddProblem(Problem model)
        {
            string res = "104";
            try
            {
                ProblemSqlMapDao dao = new ProblemSqlMapDao();
                if (model.UserID != null)
                {
                    dao.Insert(model);
                    res = "103";
                }
            }
            catch (Exception ex)
            {

                res = "104" + " " + model.Title + " == " + ex.Message;
            }
            return res;

        }

        #endregion

        #region 根据章节ChapterID获取questions   2017.5.23 update答案放到list里面
        public string Questions_FindByChapterID(string ChapterID)
        {
            QuestionsSqlmapDao dao = new QuestionsSqlmapDao();
            var date = dao.Questions_FindByChapterID(ChapterID);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("Qid", typeof(System.String)));
            dt.Columns.Add(new DataColumn("ChapterID", typeof(System.String)));
            dt.Columns.Add(new DataColumn("TypeName", typeof(System.String)));
            dt.Columns.Add(new DataColumn("TitleName", typeof(System.String)));
            dt.Columns.Add(new DataColumn("SpellNo", typeof(System.String)));
            dt.Columns.Add(new DataColumn("Score", typeof(System.Int32)));
            dt.Columns.Add(new DataColumn("Result", typeof(System.String)));
            dt.Columns.Add(new DataColumn("OperatorID", typeof(System.String)));
            dt.Columns.Add(new DataColumn("ModifyDate", typeof(System.DateTime)));
            dt.Columns.Add(new DataColumn("AnswersList", typeof(System.String)));

            DataRow dr = dt.NewRow();


            for (int i = 0; i < date.Count; i++)
            {
                dr = dt.NewRow();
                dr["Qid"] = date[i].Qid;
                dr["ChapterID"] = date[i].ChapterID;
                dr["TypeName"] = date[i].TypeName;
                dr["TitleName"] = date[i].TitleName;
                dr["SpellNo"] = date[i].SpellNo;
                dr["Score"] = date[i].Score;
                dr["Result"] = date[i].Result;
                dr["OperatorID"] = date[i].OperatorID;
                dr["ModifyDate"] = date[i].ModifyDate;
                dr["AnswersList"] = "A:" + date[i].A + ",B:" + date[i].B + ",C:" + date[i].C + ",D:" + date[i].D + ",E:" + date[i].E + ",F:" + date[i].F;

                dt.Rows.Add(dr);

            }

            ds.Tables.Add(dt);
            return ExecutDataSetToJson(ds);

        }
        #endregion

        #region 根据课程ID取目录
        public IList<CourseCatalog> CourseCatalog_FindByCourseID(string CourseID)
        {
            CourseCatalogSqlMapDao dao = new CourseCatalogSqlMapDao();
            return dao.CourseCatalog_FindByCourseID(CourseID);
        }
        #endregion

        #region 点赞
        //2017.5.16   YM单词能不能写正确。。。   6.21已点赞的，取消该条数据的点赞，返回105，表示取消点赞成功。未点赞的，对该条数据进行点赞操作，返回103，表示点赞成功
        public string AddFavor(int TypeID, string UserID, string MainID)
        {
            FavourSqlMapDao dao = new FavourSqlMapDao();
            var data = dao.FindAll().FirstOrDefault(o => o.TypeID == TypeID && o.UserID == UserID && o.MainID == MainID);
            try
            {
                ProblemSqlMapDao pdao = new ProblemSqlMapDao();
                var count = pdao.ProblemFindByQID(MainID).FavorCount; //6.26更改problem点赞数量

                if (data != null)
                {
                    pdao.UpdateFavorCountByMainID(count - 1, MainID);
                    dao.Delete(data.ID);

                    return "105";
                }
                else
                {
                    pdao.UpdateFavorCountByMainID(count + 1, MainID);
                    Favour f = new Favour();
                    f.TypeID = TypeID;
                    f.UserID = UserID;
                    f.MainID = MainID;
                    f.OperatorDate = DateTime.Now;
                    try
                    {
                        dao.Insert(f);
                    }
                    catch (Exception e)
                    {
                        return e.ToString();
                    }
                    return "103";
                }
            }
            catch (Exception e)
            {
                return "108" + e.ToString();
            }
        }

        #endregion

        #region 取消点赞 YM 2017.5.11  弃用2017.6.21
        public string CancelFavor(int TypeID, string UserID, string MainID)
        {
            try
            {
                FavourSqlMapDao dao = new FavourSqlMapDao();
                var FID = dao.FindAll().FirstOrDefault(o => o.TypeID == TypeID && o.UserID == UserID && o.MainID == MainID).ID;
                dao.Delete(FID);
                return "103";
            }
            catch (Exception ex)
            {
                return "104" + ex;
            }
        }
        #endregion

        #region//2017.3.20保存课程观看历史   2017.6.1只保存最后一个记录，如果数据库有，则修改
        public string SaveHistoryCourse(string UserID, string CourseID, string ChapterID, string PlayTime)
        {
            try
            {
                CourseRecordSqlMapDao dao = new CourseRecordSqlMapDao();
                CourseRecord couRec = new CourseRecord();
                couRec.CourseID = CourseID;
                couRec.StaffId = UserID;
                couRec.LastPlayDate = DateTime.Now;
                couRec.LastPlayChapterID = ChapterID;
                couRec.ModifyDate = DateTime.Now;
                couRec.PlayTime = PlayTime;
                if (dao.CourseRecordFindAll().FirstOrDefault(o => o.StaffId == UserID && o.CourseID == CourseID) == null)
                {
                    dao.CourseRecordInsert(couRec);
                }
                else
                {
                    dao.CourseRecordUpdate(couRec);
                }


                return "103";
            }
            catch (Exception ex)
            {
                return "104" + ex;
            }
        }
        #endregion

        # region 由用户ID获取观看历史列表
        //2017.3.23由用户ID获取观看历史列表   去掉lasttime后返回course
        public IList<CourseRecord> CourseHistoryFindByUserID(string UserID)
        {
            CourseRecordSqlMapDao dao = new CourseRecordSqlMapDao();
            IList<CourseRecord> list = dao.CourseRecordFindAll().Where(o => o.StaffId == UserID).OrderByDescending(o => o.LastPlayDate).ToList();

            CourseSqlMapDao Coursedao = new CourseSqlMapDao();

            foreach (CourseRecord item in list)
            {
                item.Course = Coursedao.CourseFindByCourseID(item.CourseID);
            }

            return list;
        }
        #endregion

        #region    //2017.5.5Yanming 获取所有科室信息 aers_tbl_basicsdep
        public IList<aers_tbl_basicsdep> BasicsdepFindAll()
        {
            aers_tbl_basicsdepSqlMapDao dao = new aers_tbl_basicsdepSqlMapDao();

            return dao.BasicsdepFindAll();
        }
        #endregion

        #region 由课程类型获取课程
        // DH 2017-5-5
        public IList<Course> CourseFindOrderByCourseType(string CourseType)
        {
            CourseSqlMapDao dao = new CourseSqlMapDao();

            return dao.CourseFindAll().Where(o => o.CourseType == CourseType).ToList();
        }
        #endregion

        #region dh四个方法
        public IList<Course> CourseFindOrderByHospId(string HospId)
        {
            CourseSqlMapDao dao = new CourseSqlMapDao();

            return dao.CourseFindAll().Where(o => o.HospId == HospId).ToList();
        }



        public IList<StudySchedule> StudyScheduleFindByHospdepId(string HospdepId)
        {
            StudyScheduleSqlMapDao dao = new StudyScheduleSqlMapDao();
            IList<StudySchedule> list = dao.StudyScheduleFindAll();
            return list.Where(o => o.HID == HospdepId).ToList();
        }


        public IList<plancontents> PlancontentsFindByHospdepId(string HospdepId)
        {
            PlancontentsSqlMapDao dao = new PlancontentsSqlMapDao();
            IList<plancontents> list = dao.PlancontentsFindAll().Where(o => o.HID == HospdepId).ToList();


            CourseSqlMapDao Coursedao = new CourseSqlMapDao();
            IList<Course> listCourse = Coursedao.CourseFindAll();


            //DH  2017-6-7  判断该课程是否完成计划
            CreditRecordSqlMapDao crdao = new CreditRecordSqlMapDao();
            IList<CreditRecord> crlist = crdao.CreditRecordFindByUserID("ru00000513");

            foreach (plancontents item in list)
            {
                item.Course = listCourse.FirstOrDefault(o => o.CourseID == item.CourseID);


                CreditRecord cr = crlist.FirstOrDefault(o => o.TrainingID == item.CourseID);
                if (cr != null)
                {
                    item.Status = 1;
                }
                else
                {
                    item.Status = 0;
                }

            }
            //DH  2017-6-7  判断该课程是否完成计划

            return list;
        }


        public IList<plancontents> PlancontentsFindByPlanID(string PlanID)
        {
            PlancontentsSqlMapDao dao = new PlancontentsSqlMapDao();
            IList<plancontents> list = dao.PlancontentsFindAll().Where(o => o.PlanID == PlanID).ToList();

            CourseSqlMapDao Coursedao = new CourseSqlMapDao();
            IList<Course> listCourse = Coursedao.CourseFindAll();




            foreach (plancontents item in list)
            {
                item.Course = listCourse.FirstOrDefault(o => o.CourseID == item.CourseID);
            }
            return list;
        }
        #endregion

        #region Message  2017.5.8Yanming  message的CURD
        public int AddMessage(Message data)
        {
            try
            {
                MessageSqlMapDao dao = new MessageSqlMapDao();
                dao.Insert(data);
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int DeleteMessage(string MessageID)
        {
            try
            {
                MessageSqlMapDao dao = new MessageSqlMapDao();
                dao.Delete(MessageID);
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int UpdateMessage(Message data)
        {
            try
            {
                MessageSqlMapDao dao = new MessageSqlMapDao();
                dao.Update(data);
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public IList<Message> MessageFindAll()
        {
            MessageSqlMapDao dao = new MessageSqlMapDao();
            return dao.FindAll();
        }

        #endregion

        #region certificateaudit  2017.5.8Yanming  certificateaudit的CURD
        public int AddCertificateAudit(CertificateAudit data)
        {
            try
            {
                CertificateAuditSqlMapDao dao = new CertificateAuditSqlMapDao();
                dao.Insert(data);
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int DeleteCertificateAudit(string AuditID)
        {
            try
            {
                CertificateAuditSqlMapDao dao = new CertificateAuditSqlMapDao();
                dao.Delete(AuditID);
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int UpdateCertificateAudit(CertificateAudit data)
        {
            try
            {
                CertificateAuditSqlMapDao dao = new CertificateAuditSqlMapDao();
                dao.Update(data);
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public IList<CertificateAudit> CertificateAuditFindAll()
        {
            CertificateAuditSqlMapDao dao = new CertificateAuditSqlMapDao();
            return dao.FindAll();
        }

        #endregion

        #region SMSMessage  2017.5.8Yanming  SMSMessage的CURD
        public int AddSMSMessage(SMSMessage data)
        {
            try
            {
                SMSMessageSqlMapDao dao = new SMSMessageSqlMapDao();
                dao.Insert(data);
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int DeleteSMSMessage(string AuditID)
        {
            try
            {
                SMSMessageSqlMapDao dao = new SMSMessageSqlMapDao();
                dao.Delete(AuditID);
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int UpdateSMSMessage(SMSMessage data)
        {
            try
            {
                SMSMessageSqlMapDao dao = new SMSMessageSqlMapDao();
                dao.Update(data);
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public IList<SMSMessage> SMSMessageFindAll()
        {
            SMSMessageSqlMapDao dao = new SMSMessageSqlMapDao();
            return dao.FindAll();
        }

        #endregion

        #region CourseDistribute  2017.5.8Yanming  CourseDistribute的CURD
        public int AddCourseDistribute(CourseDistribute data)
        {
            try
            {
                CourseDistributeSqlMapDao dao = new CourseDistributeSqlMapDao();
                dao.Insert(data);
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int DeleteCourseDistribute(string DistributeID)
        {
            try
            {
                CourseDistributeSqlMapDao dao = new CourseDistributeSqlMapDao();
                dao.Delete(DistributeID);
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public int UpdateCourseDistribute(CourseDistribute data)
        {
            try
            {
                CourseDistributeSqlMapDao dao = new CourseDistributeSqlMapDao();
                dao.Update(data);
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public IList<CourseDistribute> CourseDistributeFindAll()
        {
            CourseDistributeSqlMapDao dao = new CourseDistributeSqlMapDao();
            return dao.FindAll();
        }

        #endregion

        #region  查询该课程是否被用户收藏 YM 2017.5.10
        public string CheckHasCollect(string UserID, string CourseID)
        {
            CourseUserSqlMapDao dao = new CourseUserSqlMapDao();
            var culist = dao.FindAll().Where(o => o.UserID == UserID && o.CourseID == CourseID).ToList();
            return culist.Count == 0 ? "104" : "103";
        }
        #endregion

        #region 删除笔记 YM 2017.5.10
        public string DeleteNote(string NoteID)
        {
            try
            {
                NotesSqlMapDao dao = new NotesSqlMapDao();
                dao.Delete(NoteID);
                return "103";
            }
            catch (Exception ex)
            {
                return "104" + ex;
            }
        }
        #endregion

        #region 查询点赞 YM 2017.5.19
        public string GetFavour(IList<Favour> model)  //2017.6.20  YM 改成POST，并且返回去传过来的Model   ！@#@￥#%￥……#%￥……&%&   IList<model>  返回时有问题，未查出原因，改为ds转json
        {
            FavourSqlMapDao fdao = new FavourSqlMapDao();
            IList<Favour> fList = new List<Favour>();
            foreach (Favour item in model)
            {
                try
                {
                    var data = fdao.FindAll().FirstOrDefault(o => o.UserID == item.UserID && o.MainID == item.MainID && o.TypeID == item.TypeID);
                    if (data != null)
                    {
                        item.res = "103";
                    }
                    else
                    {
                        item.res = "104";

                    }
                    fList.Add(item);
                }
                catch (Exception e)
                {
                    var rdes = e.ToString();
                }
            }
            DataSet ds = new DataSet();
            var dt = fList.ToDataTable();
            ds.Tables.Add(dt);
            return ExecutDataSetToJson(ds);
        }
        #endregion

        #region 通知公告  YM2017.5.11
        public IList<Message> MessageFind(int MessageType, string ReceiverID)
        {
            MessageSqlMapDao dao = new MessageSqlMapDao();
            return dao.FindAll().Where(o => o.MessageType == MessageType && o.ReceiverID == ReceiverID).ToList();
            //var dddd = dao.FindAll().Where(o => o.MessageType == MessageType && o.ReceiverID == ReceiverID);
            //IList<Message> mlist = new List<Message>();

            //foreach (var item in dddd)
            //{
            //    item.ddddd = "1111";
            //    mlist.Add(item);
            //}
            //return mlist;
        }
        #endregion

        #region 新课程推送 YM 2017.5.11
        public IList<CourseDistribute> NewCourseDistribute(int DistributeType, string ReceiverID)
        {
            CourseDistributeSqlMapDao dao = new CourseDistributeSqlMapDao();
            CourseSqlMapDao cdao = new CourseSqlMapDao();
            return dao.FindAll().Where(o => o.DistributeType == DistributeType && o.ReceiverID == ReceiverID).ToList();
        }
        #endregion 

        #region 个人主页（笔记） YM 2017.5.11      6.14添加返回的课程信息
        public IList<Notes> GetNotes(string ReguserId)
        {
            NotesSqlMapDao dao = new NotesSqlMapDao();

            var noteList = dao.FindAll().Where(o => o.UserID == ReguserId).OrderByDescending(o => o.ModifyDate).ToList();

            CourseSqlMapDao Coursedao = new CourseSqlMapDao();

            foreach (var item in noteList)
            {
                item.Course = Coursedao.CourseFindByCourseID(item.CourseID);
            }

            return noteList;
        }
        #endregion

        #region YM 2017.5.17根据科室ID返回医院信息
        //aers_tbl_hospdep hospdep = dalhospdep.FindhospdepByDepId(staff.DepId);
        public aers_tbl_hospital FindHospByDepId(string DepId)
        {
            aers_tbl_hospdepSqlMapDao dao = new aers_tbl_hospdepSqlMapDao();
            aers_tbl_events_ycSqlMapDao hdao = new aers_tbl_events_ycSqlMapDao();   //这命名。。。
            var hospId = dao.FindhospdepByDepId(DepId).HospId;
            return hdao.hospitalFindByHospId(hospId);
        }

        #endregion

        #region YM 2017.6.1根据课程CourseID,userid得到这个人当前课程章节的视频PlayTime和LastPlayChapterID
        public CourseRecord GetPlayTimeByCourseIdUserId(string CourseId, string UserId)
        {
            CourseRecordSqlMapDao cdao = new CourseRecordSqlMapDao();
            // var data = cdao.findByCourseIdUserId();
            return cdao.CourseRecordFindAll().FirstOrDefault(o => o.CourseID == CourseId && o.StaffId == UserId);
        }
        #endregion

        #region  用户收藏课程AddCourseUser(string UserID, string CourseID)
        public string AddCourseUser(string UserID, string CourseID)
        {
            string res = "104";

            try
            {

                TrainDAL dal = new TrainDAL();

                DataSet ds = dal.GetCourseUser(UserID, CourseID);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    res = "100";
                }
                else
                {
                    dal.AddCourseUser(UserID, CourseID);
                    res = "103";
                }
            }
            catch (Exception ex)
            {

                res = "104" + ex.Message;
            }
            return res;
        }
        #endregion

        #region 用户取消收藏课程DeleteCourseUser(string UserID, string CourseID)
        public string DeleteCourseUser(string UserID, string CourseID)
        {
            string res = "104";

            try
            {
                TrainDAL dal = new TrainDAL();
                dal.DeleteCourseUser(UserID, CourseID);
                res = "103";
            }
            catch (Exception ex)
            {

                res = "104" + ex.Message;
            }
            return res;
        }
        #endregion

        #region 2017-6-5  DH 查询积分方法


        public IList<Integral> IntegralFindByUserID(string UserID)
        {
            string res = "104";
            try
            {
                IntegralSqlMapDao dao = new IntegralSqlMapDao();
                IList<Integral> list = dao.IntegralFindByUserID(UserID);

                return list;
            }
            catch (Exception ex)
            {
                res = "104" + ex.Message;

                return null;
            }
        }


        public string IntegralTotalFindByUserID(string UserID)
        {
            int Grade = 0;
            try
            {
                IntegralSqlMapDao dao = new IntegralSqlMapDao();
                IList<Integral> list = dao.IntegralFindByUserID(UserID);

                foreach (Integral item in list)
                {
                    try
                    {
                        Grade += Convert.ToInt32(item.Grade);
                    }
                    catch (Exception)
                    {


                    }
                }
            }
            catch (Exception ex)
            {
                return "104" + ex.Message;
            }

            return Grade.ToString();
        }
        #endregion

        #region DH  查询学分明细
        public IList<CreditRecord> CreditRecordFindByUserID(string UserID)
        {
            string res = "104";
            try
            {

                CreditRecordSqlMapDao dao = new CreditRecordSqlMapDao();
                IList<CreditRecord> list = dao.CreditRecordFindByUserID(UserID);

                return list;
            }
            catch (Exception ex)
            {
                res = "104" + ex.Message;

                return null;
            }
        }
        #endregion

        #region 统计个人学分合计
        public string CreditRecordTotalByUserID(string UserID)
        {
            CreditRecordSqlMapDao crdao = new CreditRecordSqlMapDao();

            IList<CreditRecord> crlist = crdao.CreditRecordFindByUserID(UserID);


            decimal xf = 0;
            foreach (CreditRecord xfitem in crlist)
            {
                xf += Convert.ToDecimal(xfitem.Grade);
            }
            return xf.ToString();
        }
        #endregion

        #region 2017.6.14 YM  消息标注已读
        public string MsgReaded(string MessageID)
        {
            try
            {
                MessageSqlMapDao dao = new MessageSqlMapDao();
                dao.UpdateEndTime(MessageID);
                return "103";
            }
            catch (Exception ex)
            {
                return "102" + ex;
            }
        }
        #endregion

        #endregion


        #region     工具类

        #region DataSet转Json
        /// <summary>
        /// DataSet转Json
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        protected string ExecutDataSetToJson(DataSet ds)
        {

            string str = "";
            if (ds != null && ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows.Count == 1)
                {
                    str = "[{";
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (j != ds.Tables[0].Columns.Count - 1)
                        {

                            str += "\"" + ds.Tables[0].Columns[j].ColumnName + "\":\"" + ds.Tables[0].Rows[0][j].ToString() + "\",";
                        }
                        else
                        {
                            str += "\"" + ds.Tables[0].Columns[j].ColumnName + "\":\"" + ds.Tables[0].Rows[0][j].ToString() + "\"}";
                        }
                    }
                    str += "]";
                }
                else if (ds.Tables[0].Rows.Count > 1)
                {
                    str = "[";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {

                        str += "{";
                        for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {
                            if (j != ds.Tables[0].Columns.Count - 1)
                            {

                                str += "\"" + ds.Tables[0].Columns[j].ColumnName + "\":\"" + ds.Tables[0].Rows[i][j].ToString() + "\",";
                            }
                            else
                            {
                                str += "\"" + ds.Tables[0].Columns[j].ColumnName + "\":\"" + ds.Tables[0].Rows[i][j].ToString() + "\"}";
                            }
                        }
                        if (i != ds.Tables[0].Rows.Count - 1)
                        {
                            str += ",";
                        }
                    }
                    str += "]";
                }
            }
            return str;

        }
        #endregion

        #region obj转json
        public static string ObjectToJson<T>(T data) where T : new()
        {
            try
            {
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                return output;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region obj转JSONLIST
        public static string ObjectToJsonList<T>(IList<T> list) where T : new()
        {
            try
            {
                string output = Newtonsoft.Json.JsonConvert.SerializeObject(list);

                return output;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        //#region json组装
        //public static string StrJson(string )
        //{
        //    return Newtonsoft.Json.JsonConvert.
        //}

        //#endregion

        #endregion


        public string Register(RegSMS model)
        {
            if (model == null)
            {
                return "8";
            }
            if (string.IsNullOrEmpty(model.Phone))
            {
                return "0";
            }
            return "1";
            #endregion









        }

        [DataContract]
        public class ToMyJson
        {
            [DataMember]
            public int code { get; set; }

            [DataMember]
            public string msg { get; set; }

            [DataMember]
            public object body { get; set; }
        }
        //public class Body
        //{
        //    public int Age { get; set; }
        //}


    }
}
