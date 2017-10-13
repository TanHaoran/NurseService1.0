using Aersysm.Domain;
using Aersysm.Persistence;
using cn.jpush.api;
using cn.jpush.api.push.mode;
using Easemob.Restfull4Net;
using Easemob.Restfull4Net.Entity.Request;
using IBatisNet.Common.Logging;
using IBatisNet.DataMapper.Configuration.Statements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Web;
using static System.Net.WebRequestMethods;

//可以在模板实现服务
namespace Services {
    [Serializable]
    [DataContract]
    public class Register {
        [DataMember]
        public string RegisterId { get; set; }
    }



    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“UserService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 UserService.svc 或 UserService.svc.cs，然后开始调试。
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class UserService : IUserService {
        int pageSize = 10; //每页10条

        #region 清空数据库 0
        public string ClearData() {
            try {
                userregisterSqlMapDao urdao = new userregisterSqlMapDao();
                int i = urdao.DeleteAll();   //删除所有相关数据
                                             // int m = urdao.UpdateSeed();  //更新最大号表字段
                if (i > 0) {
                    return "0";
                } else {
                    return "1";
                }
            } catch (Exception e) {

                return "1";
            }
        }
        #endregion

        #region 环信测试  0
        private string _userName {

            get {
                //Thread.Sleep(100);
                //return DateTime.Now.ToString("yyyyMMddHHmmssffff");
                return "";
            }
        } //用户名

        public string CreatHXUser() {
            ////单个创建
            var syncRequest = Client.DefaultSyncRequest;

            var userr = syncRequest.UserCreate(new UserCreateReqeust() {
                nickname = string.Concat("浩然就看见d", this._userName),
                password = "123456",
                username = string.Concat("00799944", this._userName),
            });
            return "0";
        }
        #endregion

        #region 获取全部国家编码 0
        /// <summary>
        /// 获取全部国家编码
        /// </summary>
        /// <returns></returns>
        public RsList<country> GetCountryCodeAll() {
            RsList<country> r = new Services.RsList<country>();
            try {
                countrySqlMapDao cdao = new countrySqlMapDao();
                var data = cdao.GetcountryList().ToList();
                r.code = 0;
                r.body = data;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "获取国家编码失败";
                return r;
            }
        }
        #endregion

        #region 根据手机号获取验证码  0
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CountryCode"></param>
        /// <param name="Phone"></param>
        /// <param name="Type">0验证码注册，1验证码登陆，2忘记密码，3验证旧手机号，4邀请好友,5修改手机号，6邀请好友</param>
        /// <returns></returns>
        public RsModel<string> GetSMSCodeByPhone(string CountryCode, string Phone, int Type) {
            // % 2B86
            //  CountryCode = "+86";
            RsModel<string> r = new RsModel<string>();
            if (string.IsNullOrWhiteSpace(CountryCode)) {
                r.code = 1;
                r.msg = "国家编码不能为空";
                return r;
            }

            if (string.IsNullOrWhiteSpace(Phone)) {
                r.code = 1;
                r.msg = "手机号不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(Type.ToString())) {
                r.code = 1;
                r.msg = "Type不能为空";
                return r;
            }
            try {
                if (!string.IsNullOrWhiteSpace(CountryCode))   //如果是空，暂时直接忽略
                {
                    string CountryCoded = System.Web.HttpUtility.UrlDecode(CountryCode, System.Text.Encoding.UTF8);  //解码
                    if (!string.IsNullOrEmpty(CountryCoded)) {
                        if (CountryCoded != "+86") {
                            r.code = 1;
                            r.msg = "暂不支持您手机号所在地区";
                            return r;
                        }
                    }
                }


                smsSqlMapDao sdao = new smsSqlMapDao();
                var SMSCount = sdao.GetsmsList().Where(o => o.Phone == Phone && o.SendTime.Date == DateTime.Now.Date).ToList().Count; //可优化
                //if (SMSCount > 5)  //正式时放开
                //{
                //    r.code = 1;
                //    r.msg = "今日使用次数已超限";
                //    return r;
                //}
                Random ra = new Random();
                var code1 = ra.Next(0, 9);
                var code2 = ra.Next(0, 9);
                var code3 = ra.Next(0, 9);
                var code4 = ra.Next(0, 9);
                List<int> NumberList = new List<int> { code1, code2, code3, code4 };
                var code5 = ra.Next(NumberList.Min(), NumberList.Max());  //生成6位随机数，有两位相同
                var code6 = ra.Next(NumberList.Min(), NumberList.Max()) == code5 ? ra.Next(NumberList.Min(), NumberList.Max()) : code5;
                var code = code1.ToString() + code2.ToString() + code3.ToString() + code4.ToString() + code4.ToString() + code6.ToString();
                string[] SMSCode = { code, "5" };   //5分钟
                var mes = SendMSMCode(Phone, SMSCode, Type);
                if (mes.Contains("成功")) {
                    try {
                        sms sm = new sms();
                        sm.SMSId = new aers_sys_seedSqlMapDao().GetMaxID("sms");
                        sm.Phone = Phone;
                        sm.Code = code;
                        sm.SendTime = DateTime.Now;
                        sm.Status = 1;   //发送成功时1
                        sm.Type = Type;
                        sdao.Addsms(sm);
                        r.code = 0;
                        return r;
                    } catch (Exception e) {
                        r.code = 1;
                        r.msg = "数据插入失败";
                        return r;
                    }
                } else {
                    sms sm = new sms();
                    sm.Phone = Phone;
                    sm.SMSId = new aers_sys_seedSqlMapDao().GetMaxID("sms");
                    sm.Code = code;
                    sm.SendTime = DateTime.Now;
                    sm.Status = 0;   //发送失败时0
                    sm.Type = Type;
                    sdao.Addsms(sm);
                    r.code = 1;
                    r.body = mes;
                    r.msg = "短信发送失败";
                    return r;
                }
            } catch (Exception e) {
                r.code = 1;
                r.msg = "获取验证码失败";
                return r;
            }
        }
        /// <summary>
        /// 第三发送短信
        /// </summary>
        /// <param name="Phone"></param>
        /// <param name="SMSCode"></param>
        /// <returns></returns>
        public string SendMSMCode(string Phone, string[] SMSCode, int Type) {
            string ret = null;
            CCPRestSDK.CCPRestSDK api = new CCPRestSDK.CCPRestSDK();
            bool isInit = api.init("app.cloopen.com", "8883");
            api.setAccount(Common.CCPREST_ACCOUNT_SID, Common.CCPREST_ACCOUNT_TOKEN);
            api.setAppId(Common.CCPREST_APP_ID);
            try {
                if (isInit) {
                    string msg = "";
                    if (Type == 0)       //验证码注册
                    {
                        msg = "200137";
                    } else if (Type == 1)  //验证码登陆
                      {
                        msg = "200138";
                    } else if (Type == 2)  //忘记密码
                      {
                        msg = "200139";
                    } else if (Type == 3)  //验证旧手机号
                      {
                        msg = "200140";
                    } else if (Type == 4)  //修改手机号
                      {
                        msg = "200141";
                    } else if (Type == 5)  //第三方绑定手机号
                      {
                        msg = "200143";
                    } else if (Type == 6) //解绑手机号
                      {
                        msg = "200144";
                    }
                      // 邀请短信
                      else if (Type == 7) {
                        msg = "204933";
                    }
                    Dictionary<string, object> retData = api.SendTemplateSMS(Phone, msg, SMSCode);
                    ret = getDictionaryData(retData);
                } else {
                    ret = "初始化失败";
                }
            } catch (Exception exc) {
                ret = exc.Message;
            } finally {
                // Response.Write(ret);
            }
            return ret;
        }
        private string getDictionaryData(Dictionary<string, object> data) {
            string ret = null;
            foreach (KeyValuePair<string, object> item in data) {
                if (item.Value != null && item.Value.GetType() == typeof(Dictionary<string, object>)) {
                    ret += item.Key.ToString() + "={";
                    ret += getDictionaryData((Dictionary<string, object>)item.Value);
                    ret += "};";
                } else {
                    ret += item.Key.ToString() + "=" + (item.Value == null ? "null" : item.Value.ToString()) + ";";
                }
            }
            return ret;
        }
        #endregion

        #region 验证验证码  0
        /// <summary>
        /// 验证验证码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public RsModel<UserFirstInfo> IsOKSMSCode(ViewSMS model)  //只返回注册id  8.11
        {
            RsModel<UserFirstInfo> r = new Services.RsModel<UserFirstInfo>();
            if (string.IsNullOrWhiteSpace(model.Phone)) {
                r.code = 1;
                r.msg = "手机号不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(model.Code)) {
                r.code = 1;
                r.msg = "验证码不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace((model.Type).ToString())) {
                r.code = 1;
                r.msg = "Type不能为空";
                return r;
            }

            smsSqlMapDao sdao = new smsSqlMapDao();
            var data = sdao.GetsmsList();
            var phoneData = data.OrderByDescending(o => o.SendTime).FirstOrDefault(o => o.Phone == model.Phone && o.Type == model.Type && o.Status == 1);  //发送失败status0，成功1
            if (phoneData == null) {
                r.code = 1;
                r.msg = "手机号有误";
                return r;
            } else {
                if ((DateTime.Now - phoneData.SendTime).Minutes > 50) {
                    r.code = 1;
                    r.msg = "验证码已过期";
                    return r;
                } else {
                    if (phoneData.Code != model.Code)   //正式时放开  第三方没给客户发送成功情况，status=0时
                    {
                        r.code = 1;
                        r.msg = "验证码错误";
                        return r;
                    } else {
                        if (model.Type == 0) //注册 
                        {
                            // return Sign(model.Phone,model.DeviceId); //170976fa8ab5fc3cda2
                            return Sign(model.Phone, model.DeviceRegId);
                        } else if (model.Type == 1) //验证码登陆 手机号不存在时，进行注册 返回需要的信息
                          {
                            return Sign(model.Phone, model.DeviceRegId);
                            // return LoginByCode(model.Phone);
                        } else if (model.Type == 2) //忘记密码  返回0
                          {
                            return Sign(model.Phone, model.DeviceRegId);
                            // return ForgetPwd();  //和注册一样
                        } else if (model.Type == 3) //验证旧手机号 返回0
                          {
                            return SureOldPhone(model.RegisterId, model.Phone);
                        } else if (model.Type == 4) //修改手机号 返回0
                          {
                            return ResetPhone(model.RegisterId, model.Phone);
                        } else if (model.Type == 5) //绑定手机号
                          {
                            return BindPhone(model.Phone, model.RegisterId);
                        } else if (model.Type == 6) //解绑手机号
                          {
                            return unBindPhone(model.Phone, model.RegisterId);
                        } else  //邀请好友
                          {
                            return SendtoFriend();
                        }
                    }
                }
            }
        }
        #endregion

        #region 修改用户注册信息 头像姓名昵称密码手机号  0
        /// <summary>
        /// 修改用户注册信息手机号昵称姓名密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RsModel<string> UpdateUserRegisterInfo(userregister model) {
            RsModel<string> r = new Services.RsModel<string>();
            if (model != null) {
                try {
                    if (string.IsNullOrWhiteSpace(model.RegisterId)) {
                        r.code = 1;
                        r.msg = "RegisterId不能为空";
                        return r;
                    }
                    userregisterSqlMapDao udao = new userregisterSqlMapDao();

                    // 从库里面查出以前的数据
                    var data = udao.GetuserregisterDetail(model.RegisterId);
                    // 如果是换绑手机，那么检测换绑的手机号是否已存在
                    if (model.Phone != null) {
                        var change = udao.GetuserregisterDetailByPhone(model.Phone);
                        if (change != null) {
                            if (data.Phone != change.Phone) {
                                r.code = 1;
                                r.msg = "该手机号已存在";
                                return r;
                            }
                        }
                    }

                    userregister u = new userregister();
                    u.RegisterId = model.RegisterId;
                    if (!string.IsNullOrWhiteSpace(model.Avatar)) {
                        u.Avatar = model.Avatar;
                    } else {
                        u.Avatar = data.Avatar;
                    }
                    if (!string.IsNullOrWhiteSpace(model.Name)) {
                        u.Name = model.Name;
                    } else {
                        u.Name = data.Name;
                    }
                    if (null != model.NickName) {
                        // u.NickName =Common .Encode(model.NickName);
                        u.NickName = model.NickName;
                    } else {
                        u.NickName = data.NickName;
                    }
                    if (!string.IsNullOrWhiteSpace(model.Phone)) {
                        u.Phone = model.Phone;
                    } else {
                        u.Phone = data.Phone;
                    }
                    if (!string.IsNullOrWhiteSpace(model.Password)) {
                        u.Password = Common.UserMd5(model.Password);
                    } else {
                        u.Password = data.Password;
                    }
                    udao.Updateuserregister(u);
                    r.code = 0;
                    return r;
                } catch (Exception e) {
                    r.code = 1;
                    r.msg = "数据更新失败";
                    return r;
                }

            } else {
                r.code = 1;
                r.msg = "所要修改信息不能为空";
                return r;
            }

        }
        #endregion

        #region 修改用户基本信息 0
        public RsModel<string> Updateuserbasicinfo(UserBasicInfo model) {
            RsModel<string> r = new Services.RsModel<string>();
            if (model == null) {
                r.code = 1;
                r.msg = "所要修改信息不能为空";
                return r;
            }
            try {
                if (string.IsNullOrWhiteSpace(model.RegisterId)) {
                    r.code = 1;
                    r.msg = "RegisterId不能为空";
                    return r;
                }
                userbasicinfoSqlMapDao udao = new userbasicinfoSqlMapDao();
                var data = udao.GetuserbasicinfoDetail(model.RegisterId);

                if (string.IsNullOrWhiteSpace(model.Address)) {
                    model.Address = data.Address;
                }
                if (string.IsNullOrWhiteSpace(model.Age.ToString())) {
                    model.Age = data.Age;
                }
                if (model.Birthday.ToString().StartsWith("0")) {
                    model.Birthday = data.Birthday;
                }
                if (string.IsNullOrWhiteSpace(model.City)) {
                    model.City = data.City;
                }
                if (string.IsNullOrWhiteSpace(model.Education)) {
                    model.Education = data.Education;
                }

                if (string.IsNullOrWhiteSpace(model.EMail)) {
                    model.EMail = data.EMail;
                }
                if (string.IsNullOrWhiteSpace(model.IDCard)) {
                    model.IDCard = data.IDCard;
                }
                if (string.IsNullOrWhiteSpace(model.MeritalStatus)) {
                    model.MeritalStatus = data.MeritalStatus;
                }
                if (string.IsNullOrWhiteSpace(model.Nation)) {
                    model.Nation = data.Nation;
                }
                if (string.IsNullOrWhiteSpace(model.Province)) {
                    model.Province = data.Province;
                }

                if (string.IsNullOrWhiteSpace(model.QQ)) {
                    model.QQ = data.QQ;
                }
                if (string.IsNullOrWhiteSpace(model.Region)) {
                    model.Region = data.Region;
                }
                if (string.IsNullOrWhiteSpace(model.Sex)) {
                    model.Sex = data.Sex;
                }

                udao.Updateuserbasicinfo(model);
                r.code = 0;
                return r;

            } catch (Exception e) {
                r.code = 1;
                r.msg = "数据更新失败";
                return r;
            }
        }
        #endregion


        #region qq登陆时进行注册
        public RsModel<UserFirstInfo> Signqq(string DeviceRegId) {
            RsModel<UserFirstInfo> r = new Services.RsModel<UserFirstInfo>();


            try {
                userregister user = new userregister();
                userregisterSqlMapDao udao = new userregisterSqlMapDao();
                userregister userrr = new userregister();
                var userregisterId = new aers_sys_seedSqlMapDao().GetMaxID("userregister");  //注册表
                userrr.RegisterId = userregisterId;
                udao.Adduserregister(userrr);

                LoginStatus(DeviceRegId, userrr.RegisterId, 1);
                // Common.PushMsg("欢迎注册智护", DeviceRegId, userrr.RegisterId);

                UserBasicInfo ubf = new UserBasicInfo();       //基本信息表
                ubf.RegisterId = userregisterId;
                ubf.Birthday = Common.StrToDateTime();
                userbasicinfoSqlMapDao ubrdao = new userbasicinfoSqlMapDao();
                ubrdao.Adduserbasicinfo(ubf);

                //注册成功后个人信息表添加注册Id,执业证，资格证，关系组织表
                //执业证
                Userpracticecertificate uptf = new Userpracticecertificate();       //执业证
                uptf.RegisterId = userregisterId;
                uptf.BirthDate = Common.StrToDateTime();             //可优化
                uptf.CertificateDate = Common.StrToDateTime();
                uptf.FirstRegisterDate = Common.StrToDateTime();
                uptf.LastRegisterDate = Common.StrToDateTime();
                uptf.RegisterToDate = Common.StrToDateTime();
                uptf.FirstJobTime = Common.StrToDateTime();
                UserpracticecertificateSqlMapDao updao = new UserpracticecertificateSqlMapDao();
                updao.Adduserpracticecertificate(uptf);


                Userquacertificate uqtf = new Userquacertificate();      //资格证
                uqtf.RegisterId = userregisterId;
                uqtf.DateBirth = Common.StrToDateTime();            //可优化
                uqtf.IssuingDate = Common.StrToDateTime();
                uqtf.ApproveDate = Common.StrToDateTime();
                UserquacertificateSqlMapDao uqdao = new UserquacertificateSqlMapDao();
                uqdao.Adduserquacertificate(uqtf);


                Userrelrecord ured = new Userrelrecord();     //组织关系表
                ured.RegisterId = userregisterId;

                UserrelrecordSqlMapDao urdao = new UserrelrecordSqlMapDao();
                urdao.Adduserrelrecord(ured);

                EmuserSqlMapDao eudao = new EmuserSqlMapDao();
                Emuser eu = new Emuser();
                eu.EmUserId = new aers_sys_seedSqlMapDao().GetMaxID("emuser");  //环信对应用户表
                eu.RegisterId = userregisterId;
                eu.IsOnline = 1; //登陆状态   0未登陆，1登陆  注册完成后默认登陆
                eu.EmNickName = Common.EMNickName(""); //开始注册时nickname没有
                eu.EmPassword = Common.EMPassword();
                eu.EmRegisterId = Common.EMRegisterId(userregisterId);
                eu.EmNickName = userregisterId;
                eu.EmPassword = Common.EMPassword();
                eu.EmRegisterId = userregisterId;
                eudao.Addemuser(eu);
                //环信注册用户单个创建
                var syncRequest = Client.DefaultSyncRequest;

                var userr = syncRequest.UserCreate(new UserCreateReqeust() {
                    nickname = string.Concat(eu.EmNickName, this._userName),
                    password = eu.EmPassword,
                    username = string.Concat(eu.EmRegisterId, this._userName),
                });

                // jPush("恭喜您注册成功");  //极光注册成功
                //  var d=syncRequest .UserFriendAdd()
                r.code = 0;
                UserFirstInfo ur = new UserFirstInfo();
                ur.RegisterId = userregisterId;  //返回注册Id
                r.body = ur;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "数据插入失败";
                return r;
            }
        }

        #endregion

        #region 注册0
        public RsModel<UserFirstInfo> Sign(string Phone, string DeviceRegId) {
            //  string registerIdd;
            RsModel<UserFirstInfo> r = new Services.RsModel<UserFirstInfo>();
            userregister user = new userregister();
            userregisterSqlMapDao udao = new userregisterSqlMapDao();
            var ishas = udao.GetuserregisterDetailByPhone(Phone);   //已经有账号
            if (ishas != null) {
                r.code = 0;
                return GetUserFirstInfoById(ishas.RegisterId);
                //  registerIdd = ishas.RegisterId;

                //var  ur = GetUserFirstInfoByPhone(Phone);
                // string[] reid = {ur.RegisterId };
                //  string s=Common.PushMsgByAliasId("您已成功注册注册智护",reid,DeviceId);
            } else {
                try {
                    userregister userrr = new userregister();
                    var userregisterId = new aers_sys_seedSqlMapDao().GetMaxID("userregister");  //注册表
                    //registerIdd = userregisterId;
                    userrr.Phone = Phone;
                    userrr.RegisterId = userregisterId;
                    udao.Adduserregister(userrr);

                    //  Common .PushMsg("欢迎注册智护", DeviceRegId, userrr.RegisterId);
                    LoginStatus(DeviceRegId, userrr.RegisterId, 1);  //普通注册1  院内0

                    UserBasicInfo ubf = new UserBasicInfo();       //基本信息表
                    ubf.RegisterId = userregisterId;
                    ubf.Birthday = Common.StrToDateTime();
                    userbasicinfoSqlMapDao ubrdao = new userbasicinfoSqlMapDao();
                    ubrdao.Adduserbasicinfo(ubf);

                    //注册成功后个人信息表添加注册Id,执业证，资格证，关系组织表
                    //执业证
                    Userpracticecertificate uptf = new Userpracticecertificate();       //执业证
                    uptf.RegisterId = userregisterId;
                    uptf.BirthDate = Common.StrToDateTime();             //可优化
                    uptf.CertificateDate = Common.StrToDateTime();
                    uptf.FirstRegisterDate = Common.StrToDateTime();
                    uptf.LastRegisterDate = Common.StrToDateTime();
                    uptf.RegisterToDate = Common.StrToDateTime();
                    uptf.FirstJobTime = Common.StrToDateTime();
                    UserpracticecertificateSqlMapDao updao = new UserpracticecertificateSqlMapDao();
                    updao.Adduserpracticecertificate(uptf);


                    Userquacertificate uqtf = new Userquacertificate();      //资格证
                    uqtf.RegisterId = userregisterId;
                    uqtf.DateBirth = Common.StrToDateTime();            //可优化
                    uqtf.IssuingDate = Common.StrToDateTime();
                    uqtf.ApproveDate = Common.StrToDateTime();
                    UserquacertificateSqlMapDao uqdao = new UserquacertificateSqlMapDao();
                    uqdao.Adduserquacertificate(uqtf);


                    Userrelrecord ured = new Userrelrecord();     //组织关系表
                    ured.RegisterId = userregisterId;

                    UserrelrecordSqlMapDao urdao = new UserrelrecordSqlMapDao();
                    urdao.Adduserrelrecord(ured);

                    EmuserSqlMapDao eudao = new EmuserSqlMapDao();
                    Emuser eu = new Emuser();
                    eu.EmUserId = new aers_sys_seedSqlMapDao().GetMaxID("emuser");  //环信对应用户表
                    eu.RegisterId = userregisterId;
                    eu.IsOnline = 1; //登陆状态   0未登陆，1登陆  注册完成后默认登陆
                    eu.EmNickName = Common.EMNickName(""); //开始注册时nickname没有
                    eu.EmPassword = Common.EMPassword();
                    eu.EmRegisterId = Common.EMRegisterId(userregisterId);
                    eu.EmNickName = userregisterId;
                    eu.EmPassword = Common.EMPassword();
                    eu.EmRegisterId = userregisterId;
                    eudao.Addemuser(eu);
                    //环信注册用户单个创建
                    var syncRequest = Client.DefaultSyncRequest;

                    var userr = syncRequest.UserCreate(new UserCreateReqeust() {
                        nickname = string.Concat(eu.EmNickName, this._userName),
                        password = eu.EmPassword,
                        username = string.Concat(eu.EmRegisterId, this._userName),
                    });


                    //var syncRequest = Client.DefaultSyncRequest;

                    //var userr = syncRequest.UserCreate(new UserCreateReqeust()
                    //{
                    //    nickname = string.Concat("浩然", this._userName),
                    //    password = "123456",
                    //    username = string.Concat("007", this._userName),
                    //});

                    // jPush("恭喜您注册成功");  //极光注册成功
                    //  var d=syncRequest .UserFriendAdd()
                    r.code = 0;
                    UserFirstInfo ur = new UserFirstInfo();
                    ur.RegisterId = userregisterId;  //返回注册Id
                    //string[] reid = { ur.RegisterId };
                    //string s = Common.PushMsgByAliasId("您已成功注册注册智护", reid, DeviceId);
                    r.body = ur;
                    return r;
                } catch (Exception e) {
                    r.code = 1;
                    r.msg = "数据插入失败";
                    return r;
                }
            }
        }
        #endregion

        #region  环信注册测试
        public string HXregiste(Emuser eu) {
            // Emuser eu = new Emuser();
            try {
                var syncRequest = Client.DefaultSyncRequest;

                var userr = syncRequest.UserCreate(new UserCreateReqeust() {
                    nickname = string.Concat(eu.EmNickName, this._userName),
                    password = eu.EmPassword,
                    username = string.Concat(eu.EmRegisterId, this._userName),
                });
                return "0";
            } catch (Exception e) {

                return e.ToString();
            }

        }


        #endregion

        #region 不良事件登陆  0
        public string GetUser(aers_tbl_registereduser model) {
            UserauthsSqlMapDao urdao = new UserauthsSqlMapDao();
            var data = urdao.GetUserauthsList().FirstOrDefault(o => o.LoginNumber == model.LoginName && o.Password == model.Password);
            return data.ReguserId;
        }

        #endregion

        #region 导入以前不良事件用户数据用  0  没用
        public string GetUserregiserId() {
            userregister user = new userregister();
            userregisterSqlMapDao udao = new userregisterSqlMapDao();

            var userregisterId = new aers_sys_seedSqlMapDao().GetMaxID("userregister");  //注册表
            user.RegisterId = userregisterId;
            // user .Name =
            udao.Adduserregister(user);

            UserBasicInfo ubf = new UserBasicInfo();       //基本信息表
            ubf.RegisterId = userregisterId;
            ubf.Birthday = Common.StrToDateTime();
            userbasicinfoSqlMapDao ubrdao = new userbasicinfoSqlMapDao();
            ubrdao.Adduserbasicinfo(ubf);

            //注册成功后个人信息表添加注册Id,执业证，资格证，关系组织表
            //执业证
            Userpracticecertificate uptf = new Userpracticecertificate();       //执业证
            uptf.RegisterId = userregisterId;
            uptf.BirthDate = Common.StrToDateTime();             //可优化
            uptf.CertificateDate = Common.StrToDateTime();
            uptf.FirstRegisterDate = Common.StrToDateTime();
            uptf.LastRegisterDate = Common.StrToDateTime();
            uptf.RegisterToDate = Common.StrToDateTime();
            uptf.FirstJobTime = Common.StrToDateTime();
            UserpracticecertificateSqlMapDao updao = new UserpracticecertificateSqlMapDao();
            updao.Adduserpracticecertificate(uptf);


            Userquacertificate uqtf = new Userquacertificate();      //资格证
            uqtf.RegisterId = userregisterId;
            uqtf.DateBirth = Common.StrToDateTime();            //可优化
            uqtf.IssuingDate = Common.StrToDateTime();
            uqtf.ApproveDate = Common.StrToDateTime();
            UserquacertificateSqlMapDao uqdao = new UserquacertificateSqlMapDao();
            uqdao.Adduserquacertificate(uqtf);


            Userrelrecord ured = new Userrelrecord();     //组织关系表
            ured.RegisterId = userregisterId;

            UserrelrecordSqlMapDao urdao = new UserrelrecordSqlMapDao();
            urdao.Adduserrelrecord(ured);

            //单个创建
            var syncRequest = Client.DefaultSyncRequest;

            var userr = syncRequest.UserCreate(new UserCreateReqeust() {
                nickname = string.Concat(userregisterId, this._userName),
                password = "WAJB357",
                username = string.Concat(userregisterId, this._userName),
            });
            return userregisterId;
        }

        #endregion

        #region 验证码登陆1 0
        private RsModel<UserFirstInfo> LoginByCode(string Phone) {
            return GetUserFirstInfoByPhone(Phone);
        }
        #endregion

        #region 忘记密码2 0
        private RsModel<UserFirstInfo> ForgetPwd() {
            RsModel<UserFirstInfo> r = new Services.RsModel<UserFirstInfo>();
            r.code = 0;
            return r;
        }
        #endregion

        #region 验证旧手机号3  0
        private RsModel<UserFirstInfo> SureOldPhone(string RegisterId, string Phone) {
            RsModel<UserFirstInfo> r = new Services.RsModel<UserFirstInfo>();
            if (string.IsNullOrWhiteSpace(RegisterId)) {
                r.code = 1;
                r.msg = "注册Id不能为空";
                return r;
            }
            userregister user = new userregister();
            userregisterSqlMapDao udao = new userregisterSqlMapDao();
            var oldPhone = udao.GetuserregisterDetail(RegisterId).Phone;
            if (oldPhone != Phone) {
                r.code = 1;
                r.msg = "手机号有误";
                return r;
            }
            r.code = 0;
            return r;
        }
        #endregion

        #region 修改手机号4  0
        private RsModel<UserFirstInfo> ResetPhone(string RegisterId, string Phone) {
            RsModel<UserFirstInfo> r = new Services.RsModel<UserFirstInfo>();
            if (string.IsNullOrWhiteSpace(RegisterId)) {
                r.code = 1;
                r.msg = "注册Id不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(Phone)) {
                r.code = 1;
                r.msg = "手机号不能为空";
                return r;
            }
            try {
                userregisterSqlMapDao udao = new userregisterSqlMapDao();
                var data = udao.GetuserregisterDetail(RegisterId);
                if (data.Phone == Phone) {
                    r.code = 1;
                    r.msg = "该手机号已注册";
                    return r;
                }
                userregister u = new userregister();
                u.RegisterId = RegisterId;
                u.Avatar = data.Avatar;
                u.Name = data.Name;
                // u.NickName = data.NickName;
                u.NickName = data.NickName;
                u.Password = data.Password;
                u.Phone = Phone;
                udao.Updateuserregister(u);
                r.code = 0;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "修改手机号失败";
                return r;
            }
        }
        #endregion

        #region 第三方绑定手机号5  0
        private RsModel<UserFirstInfo> BindPhone(string phone, string RegisterId) {
            RsModel<UserFirstInfo> r = new Services.RsModel<UserFirstInfo>();
            if (string.IsNullOrWhiteSpace(phone)) {
                r.code = 1;
                r.msg = "手机号不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(RegisterId)) {
                r.code = 1;
                r.msg = "RegisterId不能为空";
                return r;
            } else {
                userregisterSqlMapDao urdao = new userregisterSqlMapDao();
                var urdata = urdao.GetuserregisterDetailByPhone(phone);
                if (urdata != null) {
                    r.code = 1;
                    r.msg = "该手机号已被绑定";
                    return r;
                } else {
                    r.code = 0;
                    return r;
                }
            }
        }
        #endregion

        #region 第三方解绑手机号  6
        private RsModel<UserFirstInfo> unBindPhone(string phone, string RegisterId) {
            RsModel<UserFirstInfo> r = new Services.RsModel<UserFirstInfo>();
            if (string.IsNullOrWhiteSpace(phone)) {
                r.code = 1;
                r.msg = "手机号不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(RegisterId)) {
                r.code = 1;
                r.msg = "RegisterId不能为空";
                return r;
            }

            userregisterSqlMapDao urdao = new userregisterSqlMapDao();
            var urdataPhone = urdao.GetuserregisterDetail(RegisterId).Phone;
            if (urdataPhone != phone) {
                r.code = 1;
                return r;
            } else {
                r.code = 0;
                return r;
            }
        }
        #endregion

        #region 邀请好友5  未做
        private RsModel<UserFirstInfo> SendtoFriend() {
            RsModel<UserFirstInfo> r = new Services.RsModel<UserFirstInfo>();
            r.code = 0;
            return r;
        }
        #endregion

        #region 注册  弃用
        //private string Registe(string Phone)
        //{
        //    userregister user = new userregister();
        //    userregisterSqlMapDao udao = new userregisterSqlMapDao();
        //    var ishas = udao.GetuserregisterList().FirstOrDefault(o => o.Phone == Phone);
        //    if (ishas != null)
        //    {
        //        return "Phone:" + Phone + ":" + "Id:" + ishas.RegisterId;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            var userregisterId = new aers_sys_seedSqlMapDao().GetMaxID("userregister");  //注册表
        //            user.Phone = Phone;
        //            user.RegisterId = userregisterId;
        //            udao.Adduserregister(user);

        //            UserBasicInfo ubf = new UserBasicInfo();       //基本信息表
        //            ubf.RegisterId = userregisterId;
        //            ubf.Birthday = Common.StrToDateTime();
        //            userbasicinfoSqlMapDao ubrdao = new userbasicinfoSqlMapDao();
        //            ubrdao.Adduserbasicinfo(ubf);

        //            //注册成功后个人信息表添加注册Id,执业证，资格证，关系组织表
        //            //执业证
        //            Userpracticecertificate uptf = new Userpracticecertificate();       //执业证
        //            uptf.RegisterId = userregisterId;
        //            uptf.BirthDate = Common.StrToDateTime();             //可优化
        //            uptf.CertificateDate = Common.StrToDateTime();
        //            uptf.FirstRegisterDate = Common.StrToDateTime();
        //            uptf.LastRegisterDate = Common.StrToDateTime();
        //            uptf.RegisterToDate = Common.StrToDateTime();
        //            uptf.FirstJobTime = Common.StrToDateTime();
        //            uptf.VerifyStatus = 0;
        //            UserpracticecertificateSqlMapDao updao = new UserpracticecertificateSqlMapDao();
        //            updao.Adduserpracticecertificate(uptf);

        //            //资格证
        //            Userquacertificate uqtf = new Userquacertificate();      //资格证
        //            uqtf.RegisterId = userregisterId;
        //            uqtf.DateBirth = Common.StrToDateTime();            //可优化
        //            uqtf.IssuingDate = Common.StrToDateTime();
        //            uqtf.ApproveDate = Common.StrToDateTime();
        //            uqtf.VerifyStatus = 0;
        //            UserquacertificateSqlMapDao uqdao = new UserquacertificateSqlMapDao();
        //            uqdao.Adduserquacertificate(uqtf);

        //            //组织关系表
        //            Userrelrecord ured = new Userrelrecord();     //
        //            ured.RegisterId = userregisterId;

        //            UserrelrecordSqlMapDao urdao = new UserrelrecordSqlMapDao();
        //            urdao.Adduserrelrecord(ured);

        //            //单个创建
        //            var syncRequest = Client.DefaultSyncRequest;

        //            var userr = syncRequest.UserCreate(new UserCreateReqeust()
        //            {
        //                nickname = string.Concat(userregisterId, this._userName),
        //                password = "WAJB357",
        //                username = string.Concat(userregisterId, this._userName),
        //            });

        //            return "Id:" + user.RegisterId;
        //        }
        //        catch (Exception e)
        //        {
        //            return "数据插入失败" + e;
        //        }
        //    }
        //}
        #endregion

        #region 设置密码 
        /// <summary>
        /// 设置密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public string SetPwdByPhone(ViewRegister model) {
            try {
                userregisterSqlMapDao udao = new userregisterSqlMapDao();
                userregister user = new userregister();
                var data = udao.GetuserregisterList().FirstOrDefault(o => o.Phone == model.Phone);
                user.RegisterId = data.RegisterId;
                user.Phone = model.Phone;
                user.Password = Common.UserMd5(model.Password);
                user.Avatar = data.Avatar;
                user.Name = data.Name;
                user.NickName = data.NickName;
                udao.Updateuserregister(user);
                return "0";
            } catch (Exception e) {
                return "1";
            }

        }
        #endregion

        #region 查注册信息  1
        /// <summary>
        /// 根据注册Id获取用户注册信息
        /// </summary>
        /// <param name="RegisterId"></param>
        /// <returns></returns>
        public userregister GetUserReginfoById(string RegisterId) {
            userregisterSqlMapDao udao = new userregisterSqlMapDao();
            return udao.GetuserregisterDetail(RegisterId);
        }
        #endregion

        #region 查注册信息 phone  1
        public userregister GetUserReginfoByPhone(string Phone) {
            userregisterSqlMapDao udao = new userregisterSqlMapDao();
            var data = udao.GetuserregisterList().FirstOrDefault(o => o.Phone == Phone);
            return data;

        }
        #endregion

        #region 查基本信息  1
        public UserBasicInfo GetUserBasicinfoById(string RegisterId) {
            userbasicinfoSqlMapDao udao = new userbasicinfoSqlMapDao();
            var data = udao.GetuserbasicinfoDetail(RegisterId);
            return data;
        }
        #endregion

        #region 查资格证  QC   2   0
        public RsModel<Userquacertificate> GetUserQuacetById(string RegisterId) {
            RsModel<Userquacertificate> r = new RsModel<Userquacertificate>();
            if (string.IsNullOrWhiteSpace(RegisterId)) {
                r.code = 1;
                r.msg = "RegisterId不能为空";
                return r;
            }
            try {
                Userquacertificate uq = new Userquacertificate();
                UserquacertificateSqlMapDao udao = new UserquacertificateSqlMapDao();
                var data = udao.GetuserquacertificateDetail(RegisterId);
                uq.ApproveDate = data.ApproveDate;
                uq.Category = data.Category;
                uq.CertificateId = data.CertificateId;
                uq.DateBirth = data.DateBirth;
                uq.IssuingAgency = data.IssuingAgency;
                uq.IssuingDate = data.IssuingDate;
                uq.MajorName = data.MajorName;
                uq.ManageId = data.ManageId;
                uq.Name = data.Name;
                uq.Picture1 = data.Picture1;
                uq.Picture2 = data.Picture2;
                uq.QuaLevel = data.QuaLevel;
                uq.RegisterId = data.RegisterId;
                uq.Sex = data.Sex;
                CertificateverifySqlMapDao cdao = new CertificateverifySqlMapDao();
                var cdata = cdao.GetcertificateverifyList().FirstOrDefault(o => o.RegisterId == RegisterId && o.Type == 2);//资格证类型是2  从审核表中取审核数据（状态和意见）
                if (cdata != null) {
                    uq.VerifyStatus = cdata.VerifyStatus;
                    uq.VerifyView = cdata.VerifyView;
                }
                r.code = 0;
                r.body = uq;
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "查数据失败";
                return r;
            }
        }
        #endregion

        #region 查资格证后台  2假分页 0
        public RsList<Userpracticecertificate> GetPCInfo(string operatorId, int pageSize, int pageNumber, string CertificateId, string Name) {
            RsList<Userpracticecertificate> r = new Services.RsList<Userpracticecertificate>();
            if (!CheckNursePermission(operatorId, CERTIFICATE_VERIFY_PERMISSION)) {
                r.code = 0;
                r.msg = "没有权限";
                return r;
            }
            UserpracticecertificateSqlMapDao pdao = new UserpracticecertificateSqlMapDao();
            var datalist = pdao.GetuserpracticecertificateList();
            if (!string.IsNullOrEmpty(CertificateId)) {
                datalist = datalist.Where(o => o.CertificateId == CertificateId).ToList();
            }
            if (!string.IsNullOrEmpty(Name)) {
                datalist = datalist.Where(o => o.Name == Name).ToList();
            }
            CertificateverifySqlMapDao cdao = new CertificateverifySqlMapDao();
            var cdata = cdao.GetcertificateverifyList();
            if (cdata != null) {
                foreach (var item in datalist) {
                    var c = cdata.FirstOrDefault(o => o.RegisterId == item.RegisterId && o.Type == 1);
                    if (c != null) {
                        item.VerifyStatus = c.VerifyStatus;
                        item.VerifyView = c.VerifyView;
                    }
                }
            }
            datalist = datalist.Where(o => o.VerifyStatus != 0).ToList();
            List<Userpracticecertificate> uqclist = new List<Userpracticecertificate>();
            foreach (var item in datalist) {
                uqclist.Add(item);
            }
            r.code = 0;
            r.msg = uqclist.Count.ToString();
            r.body = uqclist.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return r;
        }
        #endregion

        #region 查执业证  PC   1   0    
        public RsModel<Userpracticecertificate> GetUserPtccetById(string RegisterId) {
            RsModel<Userpracticecertificate> r = new RsModel<Userpracticecertificate>();
            if (string.IsNullOrWhiteSpace(RegisterId)) {
                r.code = 1;
                r.msg = "RegisterId不能为空";
                return r;
            }
            try {
                Userpracticecertificate up = new Userpracticecertificate();
                UserpracticecertificateSqlMapDao udao = new UserpracticecertificateSqlMapDao();
                var data = udao.GetuserpracticecertificateDetail(RegisterId);
                up.BirthDate = data.BirthDate;
                up.CertificateAuthority = data.CertificateAuthority;
                up.CertificateDate = data.CertificateDate;
                up.CertificateId = data.CertificateId;
                up.Country = data.Country;
                up.FirstJobTime = data.FirstJobTime;
                up.FirstRegisterDate = data.FirstRegisterDate;
                up.IDCard = data.IDCard;
                up.LastRegisterDate = data.LastRegisterDate;
                up.Name = data.Name;
                up.Picture1 = data.Picture1;
                up.Picture2 = data.Picture2;
                up.Picture3 = data.Picture3;
                up.Picture4 = data.Picture4;
                up.PracticeAddress = data.PracticeAddress;
                up.RegisterAuthority = data.RegisterAuthority;
                up.RegisterId = data.RegisterId;
                up.RegisterToDate = data.RegisterToDate;
                up.Sex = data.Sex;

                CertificateverifySqlMapDao cdao = new CertificateverifySqlMapDao();
                var cdata = cdao.GetcertificateverifyList().FirstOrDefault(o => o.RegisterId == RegisterId && o.Type == 1);//执业证类型是1  从审核表中取审核数据（状态和意见）
                if (cdata != null) {
                    up.VerifyStatus = cdata.VerifyStatus;
                    up.VerifyView = cdata.VerifyView;
                }
                r.code = 0;
                r.body = up;
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "查数据失败";
                return r;
            }

        }
        #endregion

        #region 查执业证后台假分页1
        //public IList<Userquacertificate> GetQCInfo(int pageSize, int pageNumber, string CertificateId, string Name)
        //{
        //    UserquacertificateSqlMapDao qdao = new UserquacertificateSqlMapDao();
        //    var datalist = qdao.GetuserquacertificateList();
        //    if (!string.IsNullOrEmpty(CertificateId))
        //    {
        //        datalist = datalist.Where(o => o.CertificateId == CertificateId).ToList();
        //    }
        //    if (!string.IsNullOrEmpty(Name))
        //    {
        //        datalist = datalist.Where(o => o.Name == Name).ToList();
        //    }
        //    CertificateverifySqlMapDao cdao = new CertificateverifySqlMapDao();
        //    var cdata = cdao.GetcertificateverifyList();
        //    if (cdata != null)
        //    {
        //        foreach (var item in datalist)
        //        {
        //            var c = cdata.FirstOrDefault(o => o.RegisterId == item.RegisterId && o.Type == 1);
        //            if (c != null)
        //            {
        //                item.VerifyStatus = c.VerifyStatus;
        //                item.VerifyView = c.VerifyView;
        //            }

        //        }
        //    }
        //    datalist = datalist.Where(o => o.VerifyStatus != 0).ToList();
        //    return datalist.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        //}
        public RsList<Userquacertificate> GetQCInfo(string operatorId, int pageSize, int pageNumber, string CertificateId, string Name) {
            RsList<Userquacertificate> r = new Services.RsList<Userquacertificate>();
            if (!CheckNursePermission(operatorId, CERTIFICATE_VERIFY_PERMISSION)) {
                r.code = 0;
                r.msg = "没有权限";
                return r;
            }
            UserquacertificateSqlMapDao qdao = new UserquacertificateSqlMapDao();
            var datalist = qdao.GetuserquacertificateList();
            if (!string.IsNullOrEmpty(CertificateId)) {
                datalist = datalist.Where(o => o.CertificateId == CertificateId).ToList();
            }
            if (!string.IsNullOrEmpty(Name)) {
                datalist = datalist.Where(o => o.Name == Name).ToList();
            }
            CertificateverifySqlMapDao cdao = new CertificateverifySqlMapDao();

            var cdata = cdao.GetcertificateverifyList();
            if (cdata != null) {
                foreach (var item in datalist) {
                    var c = cdata.FirstOrDefault(o => o.RegisterId == item.RegisterId && o.Type == 2);
                    if (c != null) {
                        item.VerifyStatus = c.VerifyStatus;
                        item.VerifyView = c.VerifyView;
                    }
                }
            }
            datalist = datalist.Where(o => o.VerifyStatus != 0).ToList();
            List<Userquacertificate> uqclist = new List<Userquacertificate>();
            foreach (var item in datalist) {
                uqclist.Add(item);
            }
            r.code = 0;
            r.msg = uqclist.Count.ToString();
            r.body = uqclist.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            return r;
        }
        #endregion

        #region 证书审核认证  0 后台审核，盛
        public string UpdateAuditStatus(Certificateverify model) {
            if (!CheckNursePermission(model.operatorId, CERTIFICATE_VERIFY_PERMISSION)) {
                return "0:" + "没有权限";
            }
            try {
                CertificateverifySqlMapDao cdao = new CertificateverifySqlMapDao();
                var data = cdao.GetcertificateverifyList().FirstOrDefault(o => o.CertificateId == model.CertificateId && o.RegisterId == model.RegisterId);
                data.VerifyStatus = model.VerifyStatus;
                data.VerifyView = model.VerifyView;
                data.DealTime = model.DealTime;
                cdao.Updatecertificateverify(data);
                return "0";
            } catch (Exception e) {
                return "1";
            }
        }
        #endregion

        #region 查医院科室护理单元关系信息
        public Userrelrecord GetUserRelcodById(string RegisterId) {
            UserrelrecordSqlMapDao udao = new UserrelrecordSqlMapDao();
            return udao.GetUserrelrecordList().FirstOrDefault(o => o.RegisterId == RegisterId);
        }
        #endregion

        #region 查医院信息
        public Hospital GethospitalById(string HospitalId) {
            HospitalSqlMapDao hdao = new HospitalSqlMapDao();
            return hdao.GethospitalDetail(HospitalId);
        }
        #endregion

        #region 查科室信息
        public Department GetdepartmentById(string DepartmentId) {
            DepartmentSqlMapDao ddao = new DepartmentSqlMapDao();
            return ddao.GetdepartmentDetail(DepartmentId);
        }
        #endregion

        #region 查护理单元信息   待
        //public nursingunitid  GetdepartmentById(string DepartmentId)
        //{
        //    nursingunitidSqlMapDao ndao = new nursingunitidSqlMapDao();

        //    return ddao.GetdepartmentDetail(DepartmentId);
        //}

        #endregion

        #region 查证件审核 post  1
        public Certificateverify GetcertificateverifyInfo(string VerifyId) {
            if (string.IsNullOrEmpty(VerifyId)) {
                return null;
            } else {
                CertificateverifySqlMapDao cdao = new CertificateverifySqlMapDao();
                var data = cdao.GetcertificateverifyDetail(VerifyId);
                return data;
            }
        }
        #endregion

        #region 设置昵称  弃用
        public string SetNickNameById(userregister model) {
            try {
                if (model != null) {

                    userregisterSqlMapDao udao = new userregisterSqlMapDao();
                    udao.Updateuserregister(model);
                    return "0";
                } else {
                    return "1";
                }
            } catch (Exception e) {
                return "1";
            }
        }
        #endregion  可以进化

        #region 修改用户执业证信息  0  执业证 type=1  p
        public RsModel<string> UpdateuserpracticecertificateInfo(Userpracticecertificate model) {
            RsModel<string> r = new Services.RsModel<string>();
            if (model == null) {
                r.code = 1;
                r.msg = "证件信息不能为空";
            }
            if (string.IsNullOrWhiteSpace(model.RegisterId)) {
                r.code = 1;
                r.msg = "RegisterId不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(model.CertificateId)) {
                r.code = 1;
                r.msg = "证件编号不能为空";
                return r;
            }
            try {
                //证书表
                UserpracticecertificateSqlMapDao udao = new UserpracticecertificateSqlMapDao();
                udao.Updateuserpracticecertificate(model);


                //审核表
                CertificateverifySqlMapDao cdao = new CertificateverifySqlMapDao();
                Certificateverify c = new Certificateverify();
                var data = cdao.GetcertificateverifyList().FirstOrDefault(o => o.RegisterId == model.RegisterId && o.Type == 1);
                if (data == null) {
                    c.VerifyId = new aers_sys_seedSqlMapDao().GetMaxID("certificateverify");
                    c.CertificateId = model.CertificateId;
                    c.RegisterId = model.RegisterId;
                    c.SubmitTime = DateTime.Now;
                    c.Type = 1;   //执业证1
                    c.VerifyStatus = 1;  //0未认证
                    c.DealTime = Common.StrToDateTime();
                    cdao.Addcertificateverify(c);
                } else {
                    c.VerifyId = data.VerifyId;
                    c.CertificateId = data.CertificateId;
                    c.RegisterId = data.RegisterId;
                    c.SubmitTime = DateTime.Now;  //按最后一次修改时间处理
                    c.Type = data.Type;
                    c.VerifyStatus = 1;//认证中
                    c.DealTime = Common.StrToDateTime();
                    cdao.Updatecertificateverify(c);
                }
                r.code = 0;
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "数据操作失败";
                return r;
            }
        }
        #endregion  

        #region 修改用户资格证信息  0  资格证type=2  q
        public RsModel<string> UpdateuserquacertificateInfo(Userquacertificate model) {

            RsModel<string> r = new Services.RsModel<string>();
            if (model == null) {
                r.code = 1;
                r.msg = "证件信息不能为空";
            }
            if (string.IsNullOrWhiteSpace(model.RegisterId)) {
                r.code = 1;
                r.msg = "RegisterId不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(model.CertificateId)) {
                r.code = 1;
                r.msg = "证件编号不能为空";
                return r;
            }
            try {

                UserquacertificateSqlMapDao udao = new UserquacertificateSqlMapDao();
                udao.Updateuserquacertificate(model);

                CertificateverifySqlMapDao cdao = new CertificateverifySqlMapDao();
                Certificateverify c = new Certificateverify();
                var data = cdao.GetcertificateverifyList().FirstOrDefault(o => o.RegisterId == model.RegisterId && o.Type == 2);
                if (data == null) {
                    c.VerifyId = new aers_sys_seedSqlMapDao().GetMaxID("certificateverify");
                    c.CertificateId = model.CertificateId;
                    c.RegisterId = model.RegisterId;
                    c.SubmitTime = DateTime.Now;
                    c.Type = 2;   //资格证2
                    c.VerifyStatus = 1;  //认证中
                    c.DealTime = Common.StrToDateTime();
                    cdao.Addcertificateverify(c);
                } else {
                    c.VerifyId = data.VerifyId;
                    c.CertificateId = data.CertificateId;
                    c.RegisterId = data.RegisterId;
                    c.SubmitTime = DateTime.Now;  //按最后一次修改时间处理
                    c.Type = 2;
                    c.VerifyStatus = 1;//认证中
                    c.DealTime = Common.StrToDateTime();
                    cdao.Updatecertificateverify(c);
                }
                r.code = 0;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "数据操作失败";
                return r;
            }
        }
        #endregion

        #region 修改医院科室护理单元关系信息  0
        public RsModel<string> UpdateuserrelrecordInfo(Userrelrecord model) {
            RsModel<string> r = new Services.RsModel<string>();
            if (model == null) {
                r.code = 1;
                r.msg = "传入信息不能为null";
                return r;
            }
            if (string.IsNullOrWhiteSpace(model.RegisterId)) {
                r.code = 1;
                r.msg = "RegisterId不能为空";
                return r;
            }
            try {
                UserrelrecordSqlMapDao udao = new UserrelrecordSqlMapDao();
                var data = udao.GetuserrelrecordDetail(model.RegisterId);

                if (string.IsNullOrWhiteSpace(model.DepartmentId)) {
                    model.DepartmentId = data.DepartmentId;
                }

                if (string.IsNullOrWhiteSpace(model.DepartmentName)) {
                    model.DepartmentName = data.DepartmentName;
                }
                if (null == model.EmployeeId) {
                    model.EmployeeId = data.EmployeeId;
                }
                if (string.IsNullOrWhiteSpace(model.HospitalId)) {
                    model.HospitalId = data.HospitalId;
                }

                if (string.IsNullOrWhiteSpace(model.HospitalName)) {
                    model.HospitalName = data.HospitalName;
                }
                if (string.IsNullOrWhiteSpace(model.NursingUnitId)) {
                    model.NursingUnitId = data.NursingUnitId;
                }
                if (string.IsNullOrWhiteSpace(model.NursingUnitName)) {
                    model.NursingUnitName = data.NursingUnitName;
                }
                if (string.IsNullOrWhiteSpace(model.Role)) {
                    model.Role = data.Role;
                }
                udao.Updateuserrelrecord(model);

                r.code = 0;
                return r;

            } catch (Exception e) {
                r.code = 1;
                r.msg = "数据操作失败";
                return r;
            }
        }
        #endregion

        #region 添加用户基本信息
        public string AdduserbasicinfoInfo(UserBasicInfo model) {
            try {
                userbasicinfoSqlMapDao udao = new userbasicinfoSqlMapDao();
                udao.Adduserbasicinfo(model);
                return "0";
            } catch (Exception e) {
                return "1";
            }
        }
        #endregion

        #region 添加用户执业证信息
        public string AdduserpracticecertificateInfo(Userpracticecertificate model) {
            try {
                UserpracticecertificateSqlMapDao udao = new UserpracticecertificateSqlMapDao();
                udao.Adduserpracticecertificate(model);
                return "0";
            } catch (Exception e) {
                return "1";
            }
        }
        #endregion

        #region 添加用户资格证信息
        public string AdduserquacertificateInfo(Userquacertificate model) {
            try {
                UserquacertificateSqlMapDao udao = new UserquacertificateSqlMapDao();
                udao.Adduserquacertificate(model);
                return "0";
            } catch (Exception e) {
                return "1";
            }
        }
        #endregion


        #region 修改密码  0

        public RsModel<string> ResetPassword(ViewResetPassword model) {
            RsModel<string> r = new Services.RsModel<string>();
            if (model == null) {
                r.code = 1;
                r.msg = "model不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(model.RegisterId)) {
                r.code = 1;
                r.msg = "RegisterId不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(model.PasswordOld)) {
                r.code = 1;
                r.msg = "原密码不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(model.PasswordNew)) {
                r.code = 1;
                r.msg = "新密码不能为空";
                return r;
            }
            try {
                userregisterSqlMapDao udao = new userregisterSqlMapDao();
                var data = udao.GetuserregisterDetail(model.RegisterId);

                var oldPwd = data.Password;
                if (oldPwd != Common.UserMd5(model.PasswordOld)) {
                    r.code = 1;
                    r.msg = "原密码错误";
                    return r;
                } else {
                    userregister u = new userregister();
                    u.RegisterId = model.RegisterId;
                    u.Password = Common.UserMd5(model.PasswordNew);
                    u.Avatar = data.Avatar;
                    u.Name = data.Name;
                    u.NickName = data.NickName;
                    u.Phone = data.Phone;
                    udao.Updateuserregister(u);
                    r.code = 0;
                    r.msg = "密码修改成功";
                    return r;
                }
            } catch (Exception e) {
                r.code = 1;
                r.msg = "密码修改失败";
                return r;
            }
        }
        #endregion

        #region 提交证件审核   
        public string AddcertificateverifyInfo(Certificateverify model) {
            try {
                CertificateverifySqlMapDao cdao = new CertificateverifySqlMapDao();
                Certificateverify c = new Certificateverify();
                c.VerifyId = new aers_sys_seedSqlMapDao().GetMaxID("userregister");
                cdao.Addcertificateverify(model);
                return "0";
            } catch (Exception e) {
                return "1";
            }
        }
        #endregion

        #region 查证件审核
        //public string GetcertificateverifyInfo(Certificateverify model)
        //{
        //    try
        //    {
        //        CertificateverifySqlMapDao cdao = new CertificateverifySqlMapDao();
        //        cdao.GetcertificateverifyDetail(model.VerifyId);
        //        return "0";
        //    }
        //    catch (Exception e)
        //    {

        //        throw;
        //    }
        //}
        #endregion

        #region 根据经纬度获取医院    需优化  0
        public RsList<XMLDatatable> GetAddressByLngLat(string lng, string lat) {
            RsList<XMLDatatable> r = new Services.RsList<XMLDatatable>();
            if (string.IsNullOrWhiteSpace(lng)) {
                r.code = 1;
                r.msg = "经度不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(lat)) {
                r.code = 1;
                r.msg = "纬度不能为空";
                return r;
            }
            try {
                DataTable dataTable = Common.GetAddress(lng, lat, "医院", "2000");
                if (dataTable == null)  //?
                {
                    r.code = 0;
                    r.msg = "该经纬度未查到医院信息";
                    r.body = null;
                    return r;
                }
                var GetHospitalList = dataTable.AsEnumerable().Select(t => t.Field<string>("Name")).ToList();  //根据地图去到的医院列表

                HospitalSqlMapDao hdao = new HospitalSqlMapDao();
                var data = hdao.GethospitalList();  //从数据库中取得医院列表
                var WeHospitalList = data.Select(o => o.Name).ToList();  //从数据库中取得医院列表

                var dataList = GetHospitalList.Intersect(WeHospitalList).ToList();  //取交集

                DataTable dt = new DataTable();
                dt.Columns.Add("Name");
                dt.Columns.Add("HospitalId");

                foreach (var info in data) {
                    DataRow dr = dt.NewRow();
                    dr["Name"] = info.Name;
                    dr["HospitalId"] = info.HospitalId;
                    dt.Rows.Add(dr);
                }
                r.code = 0;
                r.body = ModelConvertHelper<XMLDatatable>.ConvertToModel(dt);
                return r;
                //  return ModelConvertHelper<XMLDatatable>.ConvertToModel(dt);
            } catch (Exception) {
                r.code = 1;
                r.msg = "获取医院失败";
                return r;
            }
        }

        #region 添加医院
        public string AddHospital() {
            try {

                // var dd= GetHospital();
                //foreach (var item in )
                //{

                //}

                //hospitalSqlMapDao hdao = new hospitalSqlMapDao();
                //hospital h = new hospital();
                //h.HospitalId = new aers_sys_seedSqlMapDao().GetMaxID("hospital");
                //h.
                //hdao.Addhospital(h);
                return "0";
            } catch (Exception e) {
                return "1";
            }
        }
        #endregion

        #endregion

        #region 软件反馈   0
        public RsModel<string> AddfeedbackInfo(Feedback model) {
            RsModel<string> r = new Services.RsModel<string>();
            if (model == null) {
                r.code = 1;
                r.msg = "model不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(model.RegisterId)) {
                r.code = 1;
                r.msg = "RegisterId不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(model.Content)) {
                r.code = 1;
                r.msg = "反馈内容不能为空";
                return r;
            }
            try {
                FeedbackSqlMapDao fdao = new FeedbackSqlMapDao();
                model.ServiceTime = Common.StrToDateTime();
                model.FeedbackId = model.RegisterId;
                fdao.Addfeedback(model);
                r.code = 0;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "反馈失败";
                return r;
            }
        }
        #endregion

        #region 查取版本号   0
        public RsModel<Releaseversion> GetReleaseversionInfo() {
            RsModel<Releaseversion> r = new Services.RsModel<Releaseversion>();
            try {
                ReleaseversionSqlMapDao rdao = new ReleaseversionSqlMapDao();
                var data = rdao.GetReleaseversionList().OrderByDescending(o => o.ReleaseTime).ToList()[0];
                r.code = 0;
                r.body = data;
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "获取版本信息失败";
                return r;
            }

        }
        #endregion

        #region 添加版本号
        public string AddreleaseversionInfo(Releaseversion model) {
            ReleaseversionSqlMapDao rdao = new ReleaseversionSqlMapDao();
            rdao.Addreleaseversion(model);
            return "0";
        }
        #endregion

        #region 更新版本号
        public string UpdatereleaseversionInfo(Releaseversion model) {
            ReleaseversionSqlMapDao rdao = new ReleaseversionSqlMapDao();
            rdao.Updatereleaseversion(model);
            return "0";
        }
        #endregion

        #region 根据医院Id获取科室信息  0
        public RsList<Department> GetDepartmentList(string HospitalId) {
            RsList<Department> r = new RsList<Department>();
            if (string.IsNullOrWhiteSpace(HospitalId)) {
                r.code = 1;
                r.msg = "医院编号不能为空";
                return r;
            }
            try {
                DepartmentSqlMapDao ddao = new DepartmentSqlMapDao();
                var data = ddao.GetdepartmentList().Where(o => o.HospitalId == HospitalId).ToList();
                r.code = 0;
                r.body = data;
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "获取数据失败";
                return r;
            }
        }
        #endregion

        #region 根据医院Id，科室Id,获取本科室联系人信息  0
        //public RsList<userregister> GetContactByHopDepId(string HospitalId, string DepartmentId) //  ViewFriendInfo
        //{
        //    RsList<userregister> r = new RsList<userregister>();
        //    if (string .IsNullOrWhiteSpace(HospitalId))
        //    {
        //        r.code = 0;
        //        r.msg = "医院Id不能为空";
        //        return r;
        //    }
        //    if (string .IsNullOrWhiteSpace(DepartmentId))
        //    {
        //        r.code = 0;
        //        r.msg = "科室Id不能为空";
        //        return r;
        //    }
        //    UserrelrecordSqlMapDao udao = new UserrelrecordSqlMapDao();
        //    var data = udao.GetUserrelrecordList();

        //    var dataList = data.Where(o => o.HospitalId == HospitalId && o.DepartmentId == DepartmentId).ToList();  //可优化
        //    if (dataList==null)
        //    {
        //        r.code = 0;
        //        r.msg = "暂无该医院科室人员信息";
        //        return r;
        //    }
        //    try
        //    {
        //        userregisterSqlMapDao rdao = new userregisterSqlMapDao();
        //        var userDataList = rdao.GetuserregisterList();
        //        List<userregister> urlist = new List<userregister>();
        //        foreach (var item in dataList)
        //        {
        //            userregister u = new userregister();
        //            var usdata = userDataList.FirstOrDefault(o => o.RegisterId == item.RegisterId);
        //            u.RegisterId = usdata.RegisterId;
        //            u.Avatar = usdata.Avatar;
        //            u.Name = usdata.Name;
        //            u.NickName = usdata.NickName;
        //            u.Phone = usdata.Phone;
        //            urlist.Add(u);
        //        }
        //        r.code = 0;
        //        r.body = urlist;
        //        return r;
        //    }
        //    catch (Exception e)
        //    {
        //        r.code = 1;
        //        r.msg = "获取信息失败";
        //        return r;
        //    }

        //}

        public RsList<ViewFriendInfo> GetContactByHopDepId(string HospitalId, string DepartmentId) //  ViewFriendInfo
        {
            RsList<ViewFriendInfo> r = new RsList<ViewFriendInfo>();
            if (string.IsNullOrWhiteSpace(HospitalId)) {
                r.code = 0;
                r.msg = "医院Id不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(DepartmentId)) {
                r.code = 0;
                r.msg = "科室Id不能为空";
                return r;
            }

            UserrelrecordSqlMapDao udao = new UserrelrecordSqlMapDao();
            var data = udao.GetUserrelrecordList();

            var dataList = data.Where(o => o.HospitalId == HospitalId && o.DepartmentId == DepartmentId).ToList();  //可优化
            if (dataList == null) {
                r.code = 0;
                r.msg = "暂无该医院科室人员信息";
                return r;
            }
            try {
                //userregisterSqlMapDao rdao = new userregisterSqlMapDao();
                //var userDataList = rdao.GetuserregisterList();
                //List<userregister> urlist = new List<userregister>();
                //foreach (var item in dataList)
                //{
                //    userregister u = new userregister();
                //    var usdata = userDataList.FirstOrDefault(o => o.RegisterId == item.RegisterId);
                //    u.RegisterId = usdata.RegisterId;
                //    u.Avatar = usdata.Avatar;
                //    u.Name = usdata.Name;
                //    u.NickName = usdata.NickName;
                //    u.Phone = usdata.Phone;
                //    urlist.Add(u);
                //}
                //r.code = 0;
                //r.body = urlist;
                //return r;

                EmchatSqlMapDao edao = new EmchatSqlMapDao();
                // var userList = edao.GetEmchatList().Where(o => o.MyId == MyId && o.IsFlag == 1);
                userregisterSqlMapDao urdao = new userregisterSqlMapDao();

                List<ViewFriendInfo> flist = new List<ViewFriendInfo>();
                foreach (var item in dataList) {
                    ViewFriendInfo fi = new ViewFriendInfo();
                    var urdata = urdao.GetuserregisterDetail(item.RegisterId);
                    fi.FriendId = item.RegisterId;
                    fi.Name = urdata.Name;
                    fi.NickName = urdata.NickName;
                    //  fi.NickName = Common.Decode(urdata.NickName);
                    fi.Avatar = urdata.Avatar;
                    fi.Phone = urdata.Phone;
                    // fi.IsFriend = true;
                    flist.Add(fi);
                }
                r.code = 0;
                r.body = flist;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "获取信息失败";
                return r;
            }

        }
        #endregion

        #region 第三方登陆 院内账号  
        public RsModel<UserFirstInfo> ThirdPartLoginHospital(userregister model) {
            RsModel<UserFirstInfo> r = new Services.RsModel<UserFirstInfo>();
            if (string.IsNullOrWhiteSpace(model.LoginType.ToString())) //院内账号登陆时，4不良事件 5学分 6排班
            {
                r.code = 1;
                r.msg = "院内账号类型不能为空";
                return r;
            }
            if (model.LoginType != 4) {
                if (string.IsNullOrWhiteSpace(model.HospitalId))  //不良事件没有医院区分，学分、排班时必须传入医院Id
                {
                    r.code = 1;
                    r.msg = "院内账号登陆时医院不能为空";
                    return r;
                }
            }

            if (string.IsNullOrWhiteSpace(model.Phone)) {
                r.code = 1;
                r.msg = "用户名不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(model.Password)) {
                r.code = 1;
                r.msg = "密码不能为空";
                return r;
            }
            if (model.LoginType == 4) //不良事件时
            {
                return ThirdPartLoginHospitalBLSJ(model);
            } else if (model.LoginType == 5) //学分时
              {
                return ThirdPartLoginHospitalXF(model);
            } else if (model.LoginType == 6) //排班时
              {
                return ThirdPartLoginHospitalPB(model);
            } else  //后续添加其他系统
              {
                r.code = 0;
                r.msg = "其他系统暂无数据";
                return r;
            }
        }
        /// <summary>
        /// 不良事件系统第三方登陆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RsModel<UserFirstInfo> ThirdPartLoginHospitalBLSJ(userregister model) {
            RsModel<UserFirstInfo> r = new Services.RsModel<UserFirstInfo>();
            //  string pwd = Common.UserMd5(model.Password);
            var vaUser = ValidateBlsjUser(model.Phone, model.Password);  //在不良事件库进行用户名密码验证
            if (vaUser.code == 1)  //验证不通过时进行返回提醒
            {
                r.code = 1;
                r.msg = "用户名或密码错误";
                return r;
            } else   //不良事件验证通过时，第一次登陆时授权表里面插入数据
              {
                try {
                    UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();
                    var udata = uadao.GetUserauthsList().FirstOrDefault(o => o.LoginType == 4 && o.LoginNumber == model.Phone);  //不良事件4 

                    if (udata == null)  //查看授权表数据库中是否有数据，没有时进行导入
                    {
                        var importdata = ImportUserData(vaUser.msg);  //如果登陆成功，则取msg中的注册Id
                        LoginStatus(model.DeviceRegId, importdata.msg, 4);  //院内账号第一次登陆时 登陆表添加数据，并进行消息推送
                        return GetUserFirstInfoById(importdata.msg);
                    } else {
                        LoginStatus(model.DeviceRegId, udata.RegisterId, 4);  //院内账号第一次登陆时 登陆表添加数据，并进行消息推送
                        return GetUserFirstInfoById(udata.RegisterId);
                    }
                } catch (Exception e) {
                    r.code = 1;
                    r.msg = "登陆失败" + e;
                    return r;
                }
            }
        }
        /// <summary>
        /// 学分系统第三方登陆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RsModel<UserFirstInfo> ThirdPartLoginHospitalXF(userregister model) {
            RsModel<UserFirstInfo> r = new Services.RsModel<UserFirstInfo>();
            string pwd = Common.UserMd5(model.Password);
            var vaUser = ValidateXFUser(model.Phone, pwd);  //在学分库进行用户名密码验证
            if (vaUser.code == 1)  //验证不通过时进行返回提醒
            {
                r.code = 1;
                r.msg = "用户名或密码错误";
                return r;
            } else {
                try {
                    r.msg = "学分系统暂无数据";
                    r.code = 0;
                    return r;
                } catch (Exception) {
                    r.code = 1;
                    r.msg = "登陆失败";
                    return r;
                }
            }
        }

        /// <summary>
        /// 排班系统第三方登陆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RsModel<UserFirstInfo> ThirdPartLoginHospitalPB(userregister model) {
            RsModel<UserFirstInfo> r = new Services.RsModel<UserFirstInfo>();
            string pwd = Common.UserMd5(model.Password);
            var vaUser = ValidatePBUser(model.Phone, pwd);  //在排班库进行用户名密码验证
            if (vaUser.code == 1)  //验证不通过时进行返回提醒
            {
                r.code = 1;
                r.msg = "用户名或密码错误";
                return r;
            } else {
                try {
                    r.msg = "学分系统暂无数据";
                    r.code = 0;
                    return r;
                } catch (Exception) {
                    r.code = 1;
                    r.msg = "登陆失败";
                    return r;
                }
            }
        }
        #endregion

        #region   第三方登陆   qq  0
        public RsModel<UserFirstInfo> ThirdPartLoginQQ(Qq model) {
            RsModel<UserFirstInfo> r = new Services.RsModel<UserFirstInfo>();
            if (model == null) {
                r.code = 1;
                r.msg = "qq信息不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(model.OpenId)) {
                r.code = 1;
                r.msg = "QQOpenId不能为空";
                return r;
            }
            try {
                UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();  //授权表

                var data = uadao.GetUserauthsList().FirstOrDefault(o => o.LoginNumber == model.OpenId && o.LoginType == 1);
                // var data = uadao.GetUserauthsList().FirstOrDefault(o => o.RegisterId == model.RegisterId && o.LoginType == 1);   //9.7改
                if (data != null) //授权表里面有数据，以前登陆过
                {
                    return GetUserFirstInfoById(data.RegisterId);

                } else //授权表里面没数据，以前没用QQ登陆过  创建新用户账号
                  {
                    Userauths ua = new Userauths();
                    //   var registerdata = Sign("");   //其他几张表进行注册  /////////////////////////////////////////////////////这样传值有问题
                    var registerdata = Signqq(model.DeviceRegId);
                    //其他5个表注册完成后，外加授权表和qq表注册
                    ua.AuthsId = new aers_sys_seedSqlMapDao().GetMaxID("userauths");  //授权表
                    ua.LoginLastTime = DateTime.Now;
                    ua.LoginNumber = model.OpenId;
                    ua.RegisterId = registerdata.body.RegisterId;
                    ua.LoginType = 1;
                    ua.IMEI = model.DeviceId;
                    ua.Verified = 1; //可用状态为1，删除时改为0
                    uadao.Adduserauths(ua);

                    QqSqlMapDao qdao = new QqSqlMapDao();  //qq基础表进行注册
                    Qq qq = new Qq();
                    qq.Id = new aers_sys_seedSqlMapDao().GetMaxID("qq");
                    qq.AccessToken = model.AccessToken;
                    qq.City = model.City;
                    qq.Expires = model.Expires;
                    qq.FigureUrl = model.FigureUrl;
                    qq.Gender = model.Gender;
                    qq.NickName = model.NickName;
                    qq.OpenId = model.OpenId;
                    qq.Province = model.Province;
                    qq.DeviceId = model.DeviceId;
                    qdao.Addqq(qq);

                    userregisterSqlMapDao urdao = new userregisterSqlMapDao();
                    var urdata = urdao.GetuserregisterDetail(registerdata.body.RegisterId);
                    userregister ur = new userregister();
                    ur.Avatar = qq.FigureUrl;   //把头像传过去
                    ur.RegisterId = registerdata.body.RegisterId;
                    ur.Phone = urdata.Phone;
                    ur.Password = urdata.Password;
                    ur.NickName = model.NickName;  //把昵称传过去
                    ur.Name = urdata.Name;
                    ur.CountryCode = urdata.CountryCode;
                    urdao.Updateuserregister(ur);
                    return GetUserFirstInfoById(ua.RegisterId);
                }

            } catch (Exception e) {
                r.code = 1;
                r.msg = "登陆失败" + e;
                return r;
            }

        }
        #endregion

        #region 第三方登陆   微信  0
        public RsModel<UserFirstInfo> ThirdPartLoginWeixin(Weixin model) {
            RsModel<UserFirstInfo> r = new Services.RsModel<UserFirstInfo>();
            if (string.IsNullOrWhiteSpace(model.OpenId)) {
                r.code = 1;
                r.msg = "微信OpenId不能为空";
            }
            try {
                UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();  //授权表

                var data = uadao.GetUserauthsList().FirstOrDefault(o => o.LoginNumber == model.OpenId && o.LoginType == 2); //微信是2  QQ是1
                if (data != null) //授权表里面有数据，以前登陆过
                {
                    r = GetUserFirstInfoById(data.RegisterId);
                    return r;
                } else //授权表里面没数据，以前没用微信登陆过  创建新用户账号
                  {
                    Userauths ua = new Userauths();
                    //   var registerdata = Sign("");   //其他几张表进行注册  /////////////////////////////////////////////////////这样传值有问题
                    var registerdata = Signqq(model.DeviceRegId);  //微信注册时和qq一样
                    //其他5个表注册完成后，外加授权表和qq表注册
                    ua.AuthsId = new aers_sys_seedSqlMapDao().GetMaxID("userauths");  //授权表
                    ua.LoginLastTime = DateTime.Now;
                    ua.LoginNumber = model.OpenId;
                    ua.RegisterId = registerdata.body.RegisterId;
                    ua.LoginType = 2;  //微信是2
                    ua.IMEI = model.DeviceId;
                    ua.Verified = 1;    //可用时为1，删除时为0
                    uadao.Adduserauths(ua);

                    WeixinSqlMapDao wdao = new WeixinSqlMapDao();//微信基础表进行注册
                    Weixin w = new Weixin();
                    w.Id = new aers_sys_seedSqlMapDao().GetMaxID("weixin");
                    w.OpenId = model.OpenId;
                    w.NickName = model.NickName;
                    w.Sex = model.Sex;
                    w.Language = model.Language;
                    w.City = model.City;
                    w.Province = model.Province;
                    w.Country = model.Country;
                    w.HeadImgurl = model.HeadImgurl;
                    wdao.AddWeixin(w);

                    userregisterSqlMapDao urdao = new userregisterSqlMapDao();
                    var urdata = urdao.GetuserregisterDetail(registerdata.body.RegisterId);
                    userregister ur = new userregister();
                    ur.Avatar = model.HeadImgurl;
                    ur.RegisterId = registerdata.body.RegisterId;
                    ur.Phone = urdata.Phone;
                    ur.Password = urdata.Password;
                    ur.NickName = model.NickName;
                    ur.Name = urdata.Name;
                    ur.CountryCode = urdata.CountryCode;
                    urdao.Updateuserregister(ur);

                    return GetUserFirstInfoById(ua.RegisterId);
                }
            } catch (Exception e) {
                r.code = 1;
                r.msg = "登陆失败" + e;
                return r;
            }
        }
        #endregion

        #region 第三方登陆  微博  0
        public RsModel<UserFirstInfo> ThirdPartLoginWeibo(Weibo model) {
            RsModel<UserFirstInfo> r = new Services.RsModel<UserFirstInfo>();
            if (string.IsNullOrWhiteSpace(model.idstr)) {
                r.code = 1;
                r.msg = "微博OpenId不能为空";
            }
            try {
                //  string Id;
                UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();  //授权表

                var data = uadao.GetUserauthsList().FirstOrDefault(o => o.LoginNumber == model.idstr && o.LoginType == 3); //微信是2  QQ是1  微博是3
                if (data != null) //授权表里面有数据，以前登陆过
                {
                    return GetUserFirstInfoById(data.RegisterId);

                } else //授权表里面没数据，以前没用微信登陆过  创建新用户账号
                  {
                    Userauths ua = new Userauths();
                    //   var registerdata = Sign("");   //其他几张表进行注册  /////////////////////////////////////////////////////这样传值有问题
                    var registerdata = Signqq(model.DeviceRegId);  //微信注册时和qq一样
                    //其他5个表注册完成后，外加授权表和qq表注册
                    ua.AuthsId = new aers_sys_seedSqlMapDao().GetMaxID("userauths");  //授权表
                    ua.LoginLastTime = DateTime.Now;
                    ua.LoginNumber = model.idstr;
                    ua.RegisterId = registerdata.body.RegisterId;
                    ua.LoginType = 3;  //微博是3
                    ua.IMEI = model.DeviceId;
                    ua.Verified = 1;  // 可用时为1，删除时为0
                    uadao.Adduserauths(ua);

                    WeiboSqlMapDao wdao = new WeiboSqlMapDao();//微信基础表进行注册
                    Weibo w = new Weibo();
                    w.Id = new aers_sys_seedSqlMapDao().GetMaxID("weibo");
                    w.idstr = model.idstr;
                    w.description = model.description;
                    w.gender = model.gender;
                    w.location = model.location;
                    w.name = model.name;
                    w.profile_image_url = model.profile_image_url;
                    wdao.AddWeibo(w);

                    userregisterSqlMapDao urdao = new userregisterSqlMapDao();
                    var urdata = urdao.GetuserregisterDetail(registerdata.body.RegisterId);
                    userregister ur = new userregister();
                    ur.Avatar = model.profile_image_url;
                    ur.RegisterId = registerdata.body.RegisterId;
                    ur.Phone = urdata.Phone;
                    ur.Password = urdata.Password;
                    ur.NickName = model.name;
                    ur.Name = urdata.Name;
                    ur.CountryCode = urdata.CountryCode;
                    urdao.Updateuserregister(ur);

                    return GetUserFirstInfoById(ua.RegisterId);
                }
            } catch (Exception e) {
                r.code = 1;
                r.msg = "登陆失败";
                return r;
            }
        }

        #endregion

        #region 
        //public RsModel<UserFirstInfo> ThirdPartLoginQQ(Qq model)
        //{
        //    RsModel<UserFirstInfo> r = new Services.RsModel<UserFirstInfo>();
        //    var emstr = Common.Encode(model.NickName);
        //    QqSqlMapDao qqdao = new QqSqlMapDao();
        //    Qq qq = new Qq();
        //    qq.Id = "0000000119";
        //    qq.NickName = emstr;
        //    qqdao.Updateqq(qq);

        //    //if (string.IsNullOrWhiteSpace(model.OpenId))
        //    //{
        //    //    r.code = 1;
        //    //    r.msg = "QQOpenId不能为空";
        //    //}
        //    //try
        //    //{
        //    //    //  string Id;
        //    //    UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();  //授权表

        //    //    var data = uadao.GetUserauthsList().FirstOrDefault(o => o.LoginNumber == model.OpenId && o.LoginType == 1);
        //    //    if (data != null) //授权表里面有数据，以前登陆过
        //    //    {
        //    //        return GetUserFirstInfoById(data.RegisterId);

        //    //    }
        //    //    else //授权表里面没数据，以前没用QQ登陆过  创建新用户账号
        //    //    {
        //    //        Userauths ua = new Userauths();
        //    //        //   var registerdata = Sign("");   //其他几张表进行注册  /////////////////////////////////////////////////////这样传值有问题
        //    //        var registerdata = Signqq(model.DeviceRegId);
        //    //        //其他5个表注册完成后，外加授权表和qq表注册
        //    //        ua.AuthsId = new aers_sys_seedSqlMapDao().GetMaxID("userauths");  //授权表
        //    //        ua.LoginLastTime = DateTime.Now;
        //    //        ua.LoginNumber = model.OpenId;
        //    //        ua.RegisterId = registerdata.body.RegisterId;
        //    //        //  ua.RegisterId = registerdata.body .
        //    //        ua.LoginType = 1;
        //    //        ua.IMEI = model.DeviceId;
        //    //        uadao.Adduserauths(ua);

        //    //        QqSqlMapDao qdao = new QqSqlMapDao();  //qq基础表进行注册
        //    //        Qq qq = new Qq();
        //    //        qq.Id = new aers_sys_seedSqlMapDao().GetMaxID("qq");
        //    //        qq.AccessToken = model.AccessToken;
        //    //        qq.City = model.City;
        //    //        qq.Expires = model.Expires;
        //    //        qq.FigureUrl = model.FigureUrl;
        //    //        qq.Gender = model.Gender;
        //    //        qq.NickName = model.NickName;
        //    //        // qq.NickName =Common.Encode(model.NickName);
        //    //        qq.OpenId = model.OpenId;
        //    //        qq.Province = model.Province;
        //    //        qq.DeviceId = model.DeviceId;
        //    //        qdao.Addqq(qq);

        //    //        userregisterSqlMapDao urdao = new userregisterSqlMapDao();
        //    //        var urdata = urdao.GetuserregisterDetail(registerdata.body.RegisterId);
        //    //        userregister ur = new userregister();
        //    //        ur.Avatar = qq.FigureUrl;
        //    //        ur.RegisterId = registerdata.body.RegisterId;
        //    //        ur.Phone = urdata.Phone;
        //    //        ur.Password = urdata.Password;
        //    //        ur.NickName = model.NickName;
        //    //        // ur.NickName =Common.Encode(model.NickName);
        //    //        ur.Name = urdata.Name;
        //    //        ur.CountryCode = urdata.CountryCode;
        //    //        urdao.Updateuserregister(ur);

        //    //        return GetUserFirstInfoById(ua.RegisterId);
        //    //        //  return registerdata;
        //    //    }

        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    r.code = 1;
        //    //    r.msg = "登陆失败" + e;
        //    //    return r;
        //    //}

        //}
        #endregion

        #region 生成二维码  并加密
        public RsModel<ViewJsonCommon> GetQRCodeById(string RegisterId) {
            RsModel<ViewJsonCommon> r = new Services.RsModel<ViewJsonCommon>();
            if (string.IsNullOrWhiteSpace(RegisterId)) {
                r.code = 1;
                r.msg = "RegisterId不能为空";
                return r;
            }
            try {
                // var dCode = Common.SaveCodeFile(RegisterId);
                //var eid = Common.DESEncrypt("bu" + RegisterId);
                //var dCode = Common.SaveCodeFile(eid);
                //ViewJsonCommon jc = new ViewJsonCommon();
                //jc.Name = dCode;
                //r.code = 0;
                //r.body = jc;
                //return r;

                // GetEnPassword
                var eid = Common.GetEnPassword(RegisterId);
                var dCode = Common.SaveCodeFile(eid);
                ViewJsonCommon jc = new ViewJsonCommon();
                jc.Name = dCode;
                r.code = 0;
                r.body = jc;
                return r;


                //Common.GenerateRSAKey(); //生成公私密钥
                //string publickey = Common.GetPublickey(); //获取公钥
                //var strRSA = Common.EncryptRSA(publickey, "buzzly"+RegisterId);  //对注册Id进行公钥加密
                //var dCode = Common.SaveCodeFile(strRSA); //获取路径
                //ViewJsonCommon jc = new ViewJsonCommon();
                //jc.Name = dCode;
                //r.code = 0;
                //r.body = jc;
                //return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "生成二维码失败" + e.ToString();
                return r;
            }
        }
        #endregion

        #region 环信修改昵称  
        //public string ResetNickName()
        //{
        //    //var user = Client.DefaultSyncRequest.UserCreate(new UserCreateReqeust()
        //    //{
        //    //    nickname = string.Concat("Test", this._userName),
        //    //    password = "123456",
        //    //    username = string.Concat("Test", this._userName),
        //    //});
        //    //Insert.AreEqual(user.StatusCode, HttpStatusCode.OK);

        //    //if (user.StatusCode == HttpStatusCode.OK)
        //    //{
        //    //    var delete = Client.DefaultSyncRequest.UserRestNickName(new UserNickNameRestRequest() { nickname = "654321", username = user.entities[0].username });
        //    //    Assert.AreEqual(delete.StatusCode, HttpStatusCode.OK);
        //    //}
        //}
        #endregion

        #region 查询qq昵称  弃用
        public Qq GetQQNickNameById(string RegisterId) {
            QqSqlMapDao qdao = new QqSqlMapDao();
            var data = qdao.GetQqList();
            UserauthsSqlMapDao udao = new UserauthsSqlMapDao();
            var udata = udao.GetUserauthsList();
            var qid = udata.FirstOrDefault(o => o.RegisterId == RegisterId);
            if (qid == null) {
                return null;
            }
            Qq qqdata = qdao.GetQqList().FirstOrDefault(o => o.OpenId == qid.LoginNumber);
            return qqdata;
        }
        #endregion

        #region 注册Id和qq进行绑定
        public string SetQQBind(Qq model) {
            try {
                if (string.IsNullOrEmpty(model.RegisterId)) {
                    return "注册ID不能为空";
                }
                UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();  //授权表
                Userauths ua = new Userauths();
                var data = uadao.GetUserauthsList().FirstOrDefault(o => o.LoginNumber == model.OpenId);
                if (data != null) {
                    return "该qq已被绑定";
                }
                ua.AuthsId = new aers_sys_seedSqlMapDao().GetMaxID("userauths");
                ua.IMEI = model.DeviceId;
                ua.LoginLastTime = DateTime.Now;
                ua.LoginNumber = model.OpenId;
                ua.LoginType = 1;  //1qq
                ua.RegisterId = model.RegisterId;
                ua.Verified = 0;
                uadao.Adduserauths(ua);

                QqSqlMapDao qdao = new QqSqlMapDao();
                Qq qq = new Qq();
                qq.AccessToken = model.AccessToken;
                qq.City = model.City;
                qq.DeviceId = model.DeviceId;
                qq.Expires = model.Expires;
                qq.FigureUrl = model.FigureUrl;
                qq.Gender = model.Gender;
                qq.Id = new aers_sys_seedSqlMapDao().GetMaxID("qq");
                qq.NickName = model.NickName;
                // qq.NickName =Common.Encode(model.NickName);
                qq.OpenId = model.OpenId;
                qq.Province = model.Province;
                qq.RegisterId = model.RegisterId;
                qdao.Addqq(qq);
                return "0";
            } catch (Exception e) {
                return "1" + e;
            }
        }
        #endregion

        #region qq绑定手机号
        public string QQBindPhone(Qq model) {
            try {
                userregisterSqlMapDao udao = new userregisterSqlMapDao();
                userregister u = new userregister();
                var ishas = udao.GetuserregisterList().FirstOrDefault(o => o.Phone == model.Phone);
                if (ishas != null) {
                    return "该手机号已绑定";
                }
                var data = udao.GetuserregisterDetail(model.RegisterId);
                u.Avatar = data.Avatar;
                u.Name = data.Name;
                u.NickName = data.NickName;
                u.Password = data.Password;
                u.Phone = model.Phone;
                u.RegisterId = data.RegisterId;
                udao.Updateuserregister(u);
                return "0";
            } catch (Exception e) {
                return "1";
            }
        }
        #endregion

        #region 解绑   0
        //public RsModel<string> UnBind(ViewBind model)
        //{
        //    RsModel<string> r = new Services.RsModel<string>();
        //    if (string.IsNullOrWhiteSpace(model.RegisterId))
        //    {
        //        r.code = 1;
        //        r.msg = "RegisterId不能为空";
        //        return r;
        //    }
        //    try
        //    {
        //        if (!string.IsNullOrWhiteSpace(model.Phone))  //手机号不为空  ，解绑当前注册Id的手机号
        //        {
        //            userregisterSqlMapDao urdao = new userregisterSqlMapDao();
        //            var udata = urdao.GetuserregisterDetail(model.RegisterId);
        //            userregister ur = new userregister();
        //            ur.Avatar = udata.Avatar;
        //            ur.Name = udata.Name;
        //            ur.NickName = udata.NickName;
        //            ur.Password = udata.Password;
        //            ur.RegisterId = udata.RegisterId;
        //            ur.Phone = null;
        //            urdao.Updateuserregister(ur);
        //            r.code = 0;

        //            return r;
        //        }
        //        if (model.QQData != null)
        //        {
        //            if (!string.IsNullOrWhiteSpace(model.QQData.OpenId))  //QQOpenId 不是空，解绑qq   需要优化
        //            {
        //                UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();
        //                var data = uadao.GetUserauthsList().FirstOrDefault(o => o.RegisterId == model.RegisterId && o.LoginType == 1 && o.LoginNumber == model.QQData.OpenId);
        //                if (data == null)
        //                {
        //                    r.code = 1;
        //                    r.msg = "LoginType不能为空";
        //                    return r;
        //                }
        //                uadao.Deleteuserauths(data.AuthsId);  //授权表里删除记录

        //                QqSqlMapDao qdao = new QqSqlMapDao();
        //                var qdata = qdao.GetQqList().FirstOrDefault(o => o.OpenId == model.QQData.OpenId); //需要优化
        //                qdao.Deleteqq(qdata.Id);
        //                r.code = 0;
        //                return r;
        //            }
        //            else
        //            {
        //                r.code = 1;
        //                r.msg = "QQopenId不能为空";
        //                return r;
        //            }
        //        }
        //        if (model.WXData != null)
        //        {
        //            if (!string.IsNullOrWhiteSpace(model.WXData.OpenId))  //QQOpenId 不是空，解绑qq   需要优化
        //            {
        //                UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();
        //                var data = uadao.GetUserauthsList().FirstOrDefault(o => o.RegisterId == model.RegisterId && o.LoginType == 2 && o.LoginNumber == model.WXData.OpenId);
        //                if (data == null)
        //                {
        //                    r.code = 1;
        //                    r.msg = "LoginType不能为空";
        //                    return r;
        //                }
        //                uadao.Deleteuserauths(data.AuthsId);  //授权表里删除记录

        //                WeixinSqlMapDao wdao = new WeixinSqlMapDao();
        //                var wdata = wdao.GetWeixinList().FirstOrDefault(o => o.OpenId == model.WXData.OpenId);
        //                wdao.DeleteWeixin(wdata.Id);
        //                r.code = 0;
        //                return r;
        //            }
        //            else
        //            {
        //                r.code = 1;
        //                r.msg = "微信openId不能为空";
        //                return r;
        //            }
        //        }
        //        if (model.WBData != null)
        //        {
        //            if (!string.IsNullOrWhiteSpace(model.WBData.idstr))  //QQOpenId 不是空，解绑qq   需要优化
        //            {
        //                UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();
        //                var data = uadao.GetUserauthsList().FirstOrDefault(o => o.RegisterId == model.RegisterId && o.LoginType == 3 && o.LoginNumber == model.WBData.idstr);
        //                if (data == null)
        //                {
        //                    r.code = 1;
        //                    r.msg = "LoginType不能为空";
        //                    return r;
        //                }
        //                uadao.Deleteuserauths(data.AuthsId);  //授权表里删除记录

        //                WeiboSqlMapDao wdao = new WeiboSqlMapDao();
        //                var wdata = wdao.GetWeiboList().FirstOrDefault(o => o.idstr == model.WBData.idstr);
        //                wdao.DeleteWeibo(wdata.Id);
        //                r.code = 0;
        //                return r;
        //            }
        //            else
        //            {
        //                r.code = 1;
        //                r.msg = "微信openId不能为空";
        //                return r;
        //            }
        //        }
        //        //if (!string.IsNullOrWhiteSpace(model.WBOpenId))  //WBOpenId 不是空，解绑微博  需要优化
        //        //{
        //        //    r.code = 1;
        //        //    r.msg = "微博解绑测试未完";
        //        //    return r;
        //        //}
        //        //if (!string.IsNullOrWhiteSpace(model.WXOpenId))  //WXOpenId 不是空，解绑微信  需要优化
        //        //{
        //        //    r.code = 1;
        //        //    r.msg = "微信解绑测试未完";
        //        //    return r;
        //        //}
        //        else
        //        {
        //            r.msg = "微博微信未完";
        //            return r;
        //        }

        //    }
        //    catch (Exception)
        //    {
        //        r.code = 1;
        //        r.msg = "解绑失败";
        //        return r;
        //    }
        //}
        #endregion

        #region 解绑   0   9.7不能删除，只改状态
        public RsModel<string> UnBind(ViewBind model) {
            RsModel<string> r = new Services.RsModel<string>();
            if (string.IsNullOrWhiteSpace(model.RegisterId)) {
                r.code = 1;
                r.msg = "RegisterId不能为空";
                return r;
            }
            try {
                if (model.LoginType == 0) //解绑手机号
                {
                    if (string.IsNullOrWhiteSpace(model.Phone))  //手机号不为空，解绑当前注册Id的手机号
                    {
                        r.code = 1;
                        r.msg = "手机号不能为空";
                        return r;
                    }
                    userregisterSqlMapDao urdao = new userregisterSqlMapDao();
                    var udata = urdao.GetuserregisterDetail(model.RegisterId);
                    userregister ur = new userregister();
                    ur.Avatar = udata.Avatar;
                    ur.Name = udata.Name;
                    ur.NickName = udata.NickName;
                    ur.Password = udata.Password;
                    ur.RegisterId = udata.RegisterId;
                    ur.Phone = null;
                    urdao.Updateuserregister(ur);
                    r.code = 0;
                    return r;
                } else if (model.LoginType == 1) //解绑qq
                  {
                    if (model.QQData != null) {
                        if (!string.IsNullOrWhiteSpace(model.QQData.OpenId))  //QQOpenId 不是空，解绑qq   需要优化
                        {
                            UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();
                            var data = uadao.GetUserauthsList().FirstOrDefault(o => o.RegisterId == model.RegisterId && o.LoginType == 1 && o.LoginNumber == model.QQData.OpenId);
                            if (data == null) {
                                r.code = 1;
                                r.msg = "LoginType不能为空";
                                return r;
                            }
                            uadao.Deleteuserauths(data.AuthsId);  //授权表里删除记录

                            QqSqlMapDao qdao = new QqSqlMapDao();
                            var qdata = qdao.GetQqList().FirstOrDefault(o => o.OpenId == model.QQData.OpenId); //需要优化
                            qdao.Deleteqq(qdata.Id);
                            r.code = 0;
                            return r;
                        } else {
                            r.code = 1;
                            r.msg = "QQopenId不能为空";
                            return r;
                        }
                    } else {
                        r.code = 1;
                        r.msg = "QQopenId不能为空";
                        return r;
                    }
                } else if (model.LoginType == 2) //解绑微信
                  {
                    if (model.WXData != null) {
                        if (!string.IsNullOrWhiteSpace(model.WXData.OpenId))  //QQOpenId 不是空，解绑qq   需要优化
                        {
                            UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();
                            var data = uadao.GetUserauthsList().FirstOrDefault(o => o.RegisterId == model.RegisterId && o.LoginType == 2 && o.LoginNumber == model.WXData.OpenId);
                            if (data == null) {
                                r.code = 1;
                                r.msg = "LoginType不能为空";
                                return r;
                            }
                            uadao.Deleteuserauths(data.AuthsId);  //授权表里删除记录

                            WeixinSqlMapDao wdao = new WeixinSqlMapDao();
                            var wdata = wdao.GetWeixinList().FirstOrDefault(o => o.OpenId == model.WXData.OpenId);
                            wdao.DeleteWeixin(wdata.Id);
                            r.code = 0;
                            return r;
                        } else {
                            r.code = 1;
                            r.msg = "微信openId不能为空";
                            return r;
                        }
                    } else {
                        r.code = 1;
                        r.msg = "微信openId不能为空";
                        return r;
                    }
                } else if (model.LoginType == 3) //解绑微博
                  {
                    if (model.WBData != null) {
                        if (!string.IsNullOrWhiteSpace(model.WBData.idstr))  //QQOpenId 不是空，解绑qq   需要优化
                        {
                            UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();
                            var data = uadao.GetUserauthsList().FirstOrDefault(o => o.RegisterId == model.RegisterId && o.LoginType == 3 && o.LoginNumber == model.WBData.idstr);
                            if (data == null) {
                                r.code = 1;
                                r.msg = "LoginType不能为空";
                                return r;
                            }
                            uadao.Deleteuserauths(data.AuthsId);  //授权表里删除记录

                            WeiboSqlMapDao wdao = new WeiboSqlMapDao();
                            var wdata = wdao.GetWeiboList().FirstOrDefault(o => o.idstr == model.WBData.idstr);
                            wdao.DeleteWeibo(wdata.Id);
                            r.code = 0;
                            return r;
                        } else {
                            r.code = 1;
                            r.msg = "微信openId不能为空";
                            return r;
                        }
                    } else {
                        r.code = 1;
                        r.msg = "微信openId不能为空";
                        return r;
                    }
                } else if (model.LoginType == 4) //不良事件  解绑时改状态，目前直接删除
                  {

                    UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();
                    var data = uadao.GetUserauthsList().FirstOrDefault(o => o.RegisterId == model.RegisterId && o.LoginType == 4 && o.LoginNumber == model.LoginName);  //phone传的是不良事件的登陆账号
                    if (data != null) {
                        uadao.Deleteuserauths(data.AuthsId); //目前先直接删除，以后需要进行改状态处理
                        r.code = 0;
                        return r;
                    } else {
                        r.code = 1;
                        r.msg = "该不良事件账号不存在";
                        return r;
                    }

                } else if (model.LoginType == 5) //学分
                  {
                    r.code = 1;
                    r.msg = "学分系统暂不可用";
                    return r;

                } else if (model.LoginType == 6) //排班
                  {
                    r.code = 1;
                    r.msg = "排班系统暂不可用";
                    return r;
                } else   //排班
                  {
                    r.code = 1;
                    r.msg = "其他系统待开发";
                    return r;
                }

            } catch (Exception) {
                r.code = 1;
                r.msg = "解绑失败";
                return r;
            }
        }
        #endregion

        #region 绑定  0
        //public RsModel<string> Bind(ViewBind model)
        //{
        //    RsModel<string> r = new Services.RsModel<string>();
        //    if (string.IsNullOrWhiteSpace(model.RegisterId))
        //    {
        //        r.code = 1;
        //        r.msg = "RegisterId不能为空";
        //        return r;
        //    }
        //    try
        //    {
        //        if (!string.IsNullOrWhiteSpace(model.Phone))  //绑定手机号
        //        {
        //            userregisterSqlMapDao urdao = new userregisterSqlMapDao();
        //            var udata = urdao.GetuserregisterDetail(model.RegisterId);
        //            userregister ur = new userregister();
        //            ur.Avatar = udata.Avatar;
        //            ur.Name = udata.Name;
        //            ur.NickName = udata.NickName;
        //            ur.Password = udata.Password;
        //            ur.RegisterId = udata.RegisterId;
        //            ur.Phone = model.Phone;
        //            urdao.Updateuserregister(ur);
        //            r.code = 0;
        //            return r;
        //        }
        //        else if (model.QQData != null)
        //        {
        //            if (model.QQData.OpenId != null)   //绑定qq号
        //            {
        //                UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();  //授权表
        //                Userauths ua = new Userauths();
        //                var data = uadao.GetUserauthsList().FirstOrDefault(o => o.LoginNumber == model.QQData.OpenId);
        //                if (data != null)
        //                {
        //                    r.code = 1;
        //                    r.msg = "该qq已被绑定";
        //                    return r;
        //                }
        //                ua.AuthsId = new aers_sys_seedSqlMapDao().GetMaxID("userauths");
        //                ua.IMEI = model.QQData.DeviceId;
        //                ua.LoginLastTime = DateTime.Now;
        //                ua.LoginNumber = model.QQData.OpenId;
        //                ua.LoginType = 1;  //1qq
        //                ua.RegisterId = model.RegisterId;
        //                ua.Verified = 0;
        //                uadao.Adduserauths(ua);

        //                QqSqlMapDao qdao = new QqSqlMapDao();
        //                Qq qq = new Qq();
        //                qq.AccessToken = model.QQData.AccessToken;
        //                qq.City = model.QQData.City;
        //                qq.DeviceId = model.QQData.DeviceId;
        //                qq.Expires = model.QQData.Expires;
        //                qq.FigureUrl = model.QQData.FigureUrl;
        //                qq.Gender = model.QQData.Gender;
        //                qq.Id = new aers_sys_seedSqlMapDao().GetMaxID("qq");
        //                qq.NickName = model.QQData.NickName;
        //                // qq.NickName =Common .Encode( model.QQData.NickName);
        //                qq.OpenId = model.QQData.OpenId;
        //                qq.Province = model.QQData.Province;
        //                qq.RegisterId = model.RegisterId;
        //                qdao.Addqq(qq);
        //                r.code = 0;
        //                return r;
        //            }
        //            else
        //            {
        //                r.code = 1;
        //                r.msg = "OpenId不能为空";
        //                return r;
        //            }
        //        }
        //        else if (model.WXData != null)
        //        {
        //            if (!string.IsNullOrWhiteSpace(model.WXData.OpenId))
        //            {
        //                UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();  //授权表
        //                Userauths ua = new Userauths();
        //                var data = uadao.GetUserauthsList().FirstOrDefault(o => o.LoginNumber == model.WXData.OpenId);
        //                if (data != null)
        //                {
        //                    r.code = 1;
        //                    r.msg = "该微信已被绑定";
        //                    return r;
        //                }
        //                ua.AuthsId = new aers_sys_seedSqlMapDao().GetMaxID("userauths");
        //                ua.IMEI = model.WXData.DeviceRegId;
        //                ua.LoginLastTime = DateTime.Now;
        //                ua.LoginNumber = model.WXData.OpenId;
        //                ua.LoginType = 2;  //1qq
        //                ua.RegisterId = model.RegisterId;
        //                ua.Verified = 0;
        //                uadao.Adduserauths(ua);

        //                WeixinSqlMapDao wdao = new WeixinSqlMapDao();
        //                Weixin w = new Weixin();
        //                w.Id = new aers_sys_seedSqlMapDao().GetMaxID("weixin");
        //                w.City = model.WXData.City;
        //                w.Country = model.WXData.Country;
        //                // w.DeviceRegId = model.WXData.DeviceRegId;
        //                w.HeadImgurl = model.WXData.HeadImgurl;
        //                w.Language = model.WXData.Language;
        //                w.NickName = model.WXData.NickName;
        //                w.OpenId = model.WXData.OpenId;
        //                w.Province = model.WXData.Province;
        //                w.Sex = model.WXData.Sex;
        //                wdao.AddWeixin(w);
        //                r.code = 0;
        //                return r;
        //            }
        //            else
        //            {
        //                r.code = 1;
        //                r.msg = "OpenId不能为空";
        //                return r;
        //            }
        //        }
        //        else if (model.WBData != null)
        //        {
        //            if (!string.IsNullOrWhiteSpace(model.WBData.idstr))
        //            {
        //                UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();  //授权表
        //                Userauths ua = new Userauths();
        //                var data = uadao.GetUserauthsList().FirstOrDefault(o => o.LoginNumber == model.WBData.idstr);
        //                if (data != null)
        //                {
        //                    r.code = 1;
        //                    r.msg = "该微信已被绑定";
        //                    return r;
        //                }
        //                ua.AuthsId = new aers_sys_seedSqlMapDao().GetMaxID("userauths");
        //                ua.IMEI = model.WBData.DeviceRegId;
        //                ua.LoginLastTime = DateTime.Now;
        //                ua.LoginNumber = model.WBData.idstr;
        //                ua.LoginType = 3;  //1qq
        //                ua.RegisterId = model.RegisterId;
        //                ua.Verified = 0;
        //                uadao.Adduserauths(ua);

        //                WeiboSqlMapDao wdao = new WeiboSqlMapDao();
        //                Weibo w = new Weibo();
        //                w.Id = new aers_sys_seedSqlMapDao().GetMaxID("weibo");
        //                w.description = model.WBData.description;
        //                //  w.DeviceRegId = model.WXDa
        //                w.gender = model.WBData.gender;
        //                w.idstr = model.WBData.idstr;
        //                w.location = model.WBData.location;
        //                w.name = model.WBData.name;
        //                w.profile_image_url = model.WBData.profile_image_url;
        //                wdao.AddWeibo(w);
        //                r.code = 0;
        //                return r;
        //            }
        //            else
        //            {
        //                r.code = 1;
        //                r.msg = "OpenId不能为空";
        //                return r;
        //            }
        //        }
        //        else if (model.LoginName != null)  //院内账号
        //        {
        //            var vacode = ValidateBlsjUser(model.LoginName, model.Password);  //不良时间用户体系进行合法性验证
        //            if (vacode.code != 0)
        //            {
        //                r.code = 1;
        //                r.msg = vacode.msg;
        //                return r;
        //            }
        //            else
        //            {

        //                UserauthsSqlMapDao uadao = new UserauthsSqlMapDao(); //建立对应关系，授权表里面插入一条数据,先判断是否已经绑定
        //                var audata = uadao.GetUserauthsList().FirstOrDefault(o => o.LoginType == 0 && o.RegisterId == model.RegisterId && o.LoginNumber == model.LoginName);
        //                if (audata != null)
        //                {
        //                    r.code = 1;
        //                    r.msg = "该院内账号已被绑定";
        //                    return r;
        //                }
        //                else
        //                {
        //                    try
        //                    {
        //                        aers_tbl_registereduserSqlMapDao ardao = new aers_tbl_registereduserSqlMapDao(); //根据用户名密码查出用户注册id
        //                        var ardataRegusterId = ardao.FindByLoginName(model.LoginName).ReguserId;
        //                        Userauths ua = new Userauths();
        //                        ua.AuthsId = new aers_sys_seedSqlMapDao().GetMaxID("userauths");
        //                        ua.LoginLastTime = DateTime.Now;
        //                        ua.LoginNumber = model.LoginName;
        //                        ua.LoginType = 0; //院内账号登陆类型  0
        //                        ua.Password = model.Password;
        //                        ua.RegisterId = model.RegisterId;
        //                        ua.ReguserId = ardataRegusterId;
        //                        ua.Verified = 0;
        //                        uadao.Adduserauths(ua);
        //                        r.code = 0;
        //                        return r;
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        r.code = 1;
        //                        r.msg = "院内账号绑定失败";
        //                        return r;
        //                    }
        //                }

        //            }
        //        }
        //        else
        //        {
        //            r.msg = "绑定微信微博未完成";
        //            return r;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        r.code = 1;
        //        r.msg = "绑定失败" + e;
        //        return r;
        //    }
        //}
        #endregion

        #region 绑定  0
        public RsModel<UserFirstInfo> Bind(ViewBind model) {
            RsModel<UserFirstInfo> r = new Services.RsModel<UserFirstInfo>();
            UserFirstInfo UFI = new UserFirstInfo();
            if (string.IsNullOrWhiteSpace(model.RegisterId)) {
                r.code = 1;
                r.msg = "RegisterId不能为空";
                return r;
            }
            try {
                if (model.LoginType == 0) //绑定手机号
                {
                    if (string.IsNullOrWhiteSpace(model.Phone)) {
                        r.code = 1;
                        r.msg = "手机号不能为空";
                        return r;
                    }
                    userregisterSqlMapDao urdao = new userregisterSqlMapDao();
                    var udata = urdao.GetuserregisterDetail(model.RegisterId);
                    userregister ur = new userregister();
                    ur.Avatar = udata.Avatar;
                    ur.Name = udata.Name;
                    ur.NickName = udata.NickName;
                    ur.Password = udata.Password;
                    ur.RegisterId = udata.RegisterId;
                    ur.Phone = model.Phone;       //添加手机号
                    urdao.Updateuserregister(ur);
                    r.code = 0;
                    return r;

                } else if (model.LoginType == 1) //QQ
                  {
                    if (model.QQData != null) {
                        if (model.QQData.OpenId != null)   //绑定qq号
                        {
                            UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();  //授权表
                            Userauths ua = new Userauths();
                            var data = uadao.GetUserauthsList().FirstOrDefault(o => o.LoginType == 1 && o.LoginNumber == model.QQData.OpenId);
                            if (data != null) {
                                r.code = 1;
                                r.msg = "该qq号已被绑定";
                                return r;
                            }
                            ua.AuthsId = new aers_sys_seedSqlMapDao().GetMaxID("userauths");
                            ua.IMEI = model.QQData.DeviceId;
                            ua.LoginLastTime = DateTime.Now;
                            ua.LoginNumber = model.QQData.OpenId;
                            ua.LoginType = 1;  //1qq
                            ua.RegisterId = model.RegisterId;
                            ua.Verified = 0;
                            uadao.Adduserauths(ua);

                            QqSqlMapDao qdao = new QqSqlMapDao();
                            Qq qq = new Qq();
                            qq.AccessToken = model.QQData.AccessToken;
                            qq.City = model.QQData.City;
                            qq.DeviceId = model.QQData.DeviceId;
                            qq.Expires = model.QQData.Expires;
                            qq.FigureUrl = model.QQData.FigureUrl;
                            qq.Gender = model.QQData.Gender;
                            qq.Id = new aers_sys_seedSqlMapDao().GetMaxID("qq");
                            qq.NickName = model.QQData.NickName;
                            // qq.NickName =Common .Encode( model.QQData.NickName);
                            qq.OpenId = model.QQData.OpenId;
                            qq.Province = model.QQData.Province;
                            qq.RegisterId = model.RegisterId;
                            qdao.Addqq(qq);
                            r.code = 0;
                            return r;
                        } else {
                            r.code = 1;
                            r.msg = "OpenId不能为空";
                            return r;
                        }
                    } else {
                        r.code = 1;
                        r.msg = "QQ信息不能为空";
                        return r;
                    }
                } else if (model.LoginType == 2) //微信
                  {
                    if (model.WXData != null) {
                        if (!string.IsNullOrWhiteSpace(model.WXData.OpenId)) {
                            UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();  //授权表
                            Userauths ua = new Userauths();
                            var data = uadao.GetUserauthsList().FirstOrDefault(o => o.LoginType == 2 && o.LoginNumber == model.WXData.OpenId);
                            if (data != null) {
                                r.code = 1;
                                r.msg = "该微信已被绑定";
                                return r;
                            }
                            ua.AuthsId = new aers_sys_seedSqlMapDao().GetMaxID("userauths");
                            ua.IMEI = model.WXData.DeviceRegId;
                            ua.LoginLastTime = DateTime.Now;
                            ua.LoginNumber = model.WXData.OpenId;
                            ua.LoginType = 2;  //1qq
                            ua.RegisterId = model.RegisterId;
                            ua.Verified = 0;
                            uadao.Adduserauths(ua);

                            WeixinSqlMapDao wdao = new WeixinSqlMapDao();
                            Weixin w = new Weixin();
                            w.Id = new aers_sys_seedSqlMapDao().GetMaxID("weixin");
                            w.City = model.WXData.City;
                            w.Country = model.WXData.Country;
                            // w.DeviceRegId = model.WXData.DeviceRegId;
                            w.HeadImgurl = model.WXData.HeadImgurl;
                            w.Language = model.WXData.Language;
                            w.NickName = model.WXData.NickName;
                            w.OpenId = model.WXData.OpenId;
                            w.Province = model.WXData.Province;
                            w.Sex = model.WXData.Sex;
                            wdao.AddWeixin(w);
                            r.code = 0;
                            return r;
                        } else {
                            r.code = 1;
                            r.msg = "OpenId不能为空";
                            return r;
                        }
                    } else {
                        r.code = 1;
                        r.msg = "微博信息不能为空";
                        return r;
                    }
                } else if (model.LoginType == 3) //微博
                  {
                    if (model.WBData != null) {
                        if (!string.IsNullOrWhiteSpace(model.WBData.idstr)) {
                            UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();  //授权表
                            Userauths ua = new Userauths();
                            var data = uadao.GetUserauthsList().FirstOrDefault(o => o.LoginType == 3 && o.LoginNumber == model.WBData.idstr);
                            if (data != null) {
                                r.code = 1;
                                r.msg = "该微博已被绑定";
                                return r;
                            }
                            ua.AuthsId = new aers_sys_seedSqlMapDao().GetMaxID("userauths");
                            ua.IMEI = model.WBData.DeviceRegId;
                            ua.LoginLastTime = DateTime.Now;
                            ua.LoginNumber = model.WBData.idstr;
                            ua.LoginType = 3;  //1qq
                            ua.RegisterId = model.RegisterId;
                            ua.Verified = 0;
                            uadao.Adduserauths(ua);

                            WeiboSqlMapDao wdao = new WeiboSqlMapDao();
                            Weibo w = new Weibo();
                            w.Id = new aers_sys_seedSqlMapDao().GetMaxID("weibo");
                            w.description = model.WBData.description;
                            //  w.DeviceRegId = model.WXDa
                            w.gender = model.WBData.gender;
                            w.idstr = model.WBData.idstr;
                            w.location = model.WBData.location;
                            w.name = model.WBData.name;
                            w.profile_image_url = model.WBData.profile_image_url;
                            wdao.AddWeibo(w);
                            r.code = 0;
                            return r;
                        } else {
                            r.code = 1;
                            r.msg = "OpenId不能为空";
                            return r;
                        }
                    } else {
                        r.code = 1;
                        r.msg = "微博信息不能为空";
                        return r;
                    }
                } else if (model.LoginType == 4) //不良事件
                  {
                    if (string.IsNullOrWhiteSpace(model.LoginName)) {
                        r.code = 1;
                        r.msg = "不良事件登陆账号不能为空";
                        return r;
                    }
                    var vacode = ValidateBlsjUser(model.LoginName, model.Password);  //不良时间用户体系进行合法性验证
                    if (vacode.code != 0) {
                        r.code = 1;
                        r.msg = vacode.msg;
                        return r;
                    } else {
                        UserauthsSqlMapDao uadao = new UserauthsSqlMapDao(); //建立对应关系，授权表里面插入一条数据,先判断是否已经绑定
                        var audata = uadao.GetUserauthsList().FirstOrDefault(o => o.LoginType == 4 && o.LoginNumber == model.LoginName);
                        if (audata != null) {
                            r.code = 1;
                            r.msg = "该院内账号已被绑定";
                            return r;
                        } else {
                            try {
                                //根据用户名密码查出用户注册id
                                aers_tbl_registereduserSqlMapDao ardao = new aers_tbl_registereduserSqlMapDao();
                                var ardataRegusterId = ardao.FindByLoginName(model.LoginName).ReguserId;
                                Userauths ua = new Userauths();
                                ua.AuthsId = new aers_sys_seedSqlMapDao().GetMaxID("userauths");
                                ua.LoginLastTime = DateTime.Now;
                                ua.LoginNumber = model.LoginName;
                                ua.LoginType = 4; //不良事件类型4
                                ua.Password = Common.UserMd5(model.Password);
                                ua.RegisterId = model.RegisterId;
                                ua.ReguserId = ardataRegusterId;
                                ua.Verified = 1;  //绑定时，可用状态为1
                                uadao.Adduserauths(ua);
                                // 更新userrelrecord表中的数据
                                UpdateRecordInfo(model.RegisterId, ardataRegusterId);

                                aers_tbl_staffSqlMapDao sDao = new aers_tbl_staffSqlMapDao();
                                var name = sDao.FindNameByRid(ardataRegusterId);
                                UFI.Name = name;
                                r.code = 0;
                                UFI.ReguserId = ardataRegusterId;
                                r.body = UFI;
                                return r;
                            } catch (Exception e) {
                                r.code = 1;
                                r.msg = "院内账号绑定失败";
                                return r;
                            }
                        }

                    }
                } else if (model.LoginType == 5) //学分
                  {
                    r.code = 0;
                    r.msg = "学分系统暂未开通";
                    return r;
                } else if (model.LoginType == 6) //排班
                  {
                    r.code = 0;
                    r.msg = "排班系统暂未开通";
                    return r;
                } else {
                    r.msg = "其他账号绑定待开发";
                    return r;
                }
            } catch (Exception e) {
                r.code = 1;
                r.msg = "绑定失败" + e;
                return r;
            }
        }

        /// <summary>
        /// 更新userrelrecord表中的数据
        /// </summary>
        /// <param name="registerId"></param>
        /// <param name="regusterId"></param>
        private void UpdateRecordInfo(string registerId, string regusterId) {
            // 首先查询到这条数据
            UserrelrecordSqlMapDao recordDao = new UserrelrecordSqlMapDao();
            Userrelrecord record = recordDao.GetuserrelrecordDetail(registerId);

            // 然后查询在院内这个人的信息
            aers_tbl_staffSqlMapDao staffDao = new aers_tbl_staffSqlMapDao();
            aers_tbl_staff staff = staffDao.FindByRUid(regusterId);

            // 查询科室信息
            aers_tbl_hospdepSqlMapDao depDao = new aers_tbl_hospdepSqlMapDao();
            aers_tbl_hospdep dep = depDao.Find(staff.DepId);

            // 查询医院信息
            aers_tbl_events_ycSqlMapDao hdao = new aers_tbl_events_ycSqlMapDao();
            aers_tbl_hospital hos = hdao.hospitalFindByHospId(dep.HospId);

            // 获取姓名
            userregisterSqlMapDao registerDao = new userregisterSqlMapDao();
            userregister register = registerDao.GetuserregisterDetail(registerId);
            register.Name = staff.Name;
            registerDao.Updateuserregister(register);

            // 赋值
            record.DepartmentId = dep.HospdepId;
            record.DepartmentName = dep.HospdepName;
            record.HospitalId = hos.HospId;
            record.HospitalName = hos.HospName;

            recordDao.Updateuserrelrecord(record);
        }
        #endregion

        #region 获取公告信息分页可优化   0  浩然只传页数，第几页 传医院，科室
        public RsList<Notice> GetNotice(int pageNumber, string HospitalId, string DepartmentId) {
            RsList<Notice> r = new Services.RsList<Notice>();
            try {
                NoticeSqlMapDao nDao = new NoticeSqlMapDao();
                IList<Notice> noticelist = new List<Notice>();
                // 科室公告
                if (!string.IsNullOrWhiteSpace(HospitalId) && !string.IsNullOrWhiteSpace(DepartmentId)) {
                    noticelist = nDao.GetNoticeList().Where(o => o.IsDelete == 0 && ((o.HospitalId == HospitalId && o.DepartmentId == DepartmentId) || o.Type == 0)).ToList();
                }
                // 平台公告
                else {
                    noticelist = nDao.GetNoticeList().Where(o => o.IsDelete == 0 && o.Type == 0).ToList();
                }

                noticelist = noticelist.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                r.code = 0;
                // r.msg = noticelist.Count.ToString();
                r.body = noticelist;
            } catch (Exception e) {
                r.code = 1;
                r.msg = e.ToString();
                r.body = null;
            }
            return r;
        }

        #endregion

        #region 获取banner信息  0
        public RsList<Banner> GetBanner(string HospitalId, string DepartmentId) {
            RsList<Banner> r = new Services.RsList<Banner>();
            try {
                BannerSqlMapDao bdao = new BannerSqlMapDao();
                var datalist = bdao.GetBannerList().OrderByDescending(o => o.DisplayOrder).Where(o=> o.IsDelete == 0).ToList();
                r.code = 0;
                r.body = datalist; //banner取前5条数据
            } catch (Exception e) {
                r.code = 1;
                r.msg = e.ToString();
                r.body = null;
            }
            return r;
        }
        #endregion

        #region 首页信息只根据手机号,比如通过验证码登陆时只有手机号 0  可改成私有
        public RsModel<UserFirstInfo> GetUserFirstInfoByPhone(string Phone) {
            RsModel<UserFirstInfo> r = new RsModel<UserFirstInfo>();
            if (string.IsNullOrEmpty(Phone)) {
                r.code = 1;
                r.msg = "手机号不能为空";
                r.body = null;
                return r;
            }
            userregisterSqlMapDao rrdao = new userregisterSqlMapDao();
            var rrdata = rrdao.GetuserregisterDetailByPhone(Phone);
            if (rrdata == null) {
                r.code = 1;
                r.msg = "手机号不存在";
                r.body = null;
                return r;
            }
            return GetUserFirstInfoById(rrdata.RegisterId);
        }
        #endregion

        #region 登录 0
        /// <summary>
        /// 登录时，根据手机号和密码，model里面只传手机号和密码
        /// </summary>
        /// <param name="Phone"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public RsModel<UserFirstInfo> Login(userregister model) {
            RsModel<UserFirstInfo> r = new RsModel<UserFirstInfo>();

            if (string.IsNullOrWhiteSpace(model.CountryCode)) {
                r.code = 1;
                r.msg = "国家编码不能为空";
                return r;
            }

            if (!string.IsNullOrWhiteSpace(model.CountryCode))   //如果是空，暂时直接忽略
            {
                // string CountryCode = System.Web.HttpUtility.UrlDecode(model.CountryCode, System.Text.Encoding.UTF8);  //解码
                if (!string.IsNullOrEmpty(model.CountryCode)) {
                    if (model.CountryCode != "+86") {
                        r.code = 1;
                        r.msg = "暂不支持您手机号所在地区";
                        return r;
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(model.Phone)) {
                r.code = 1;
                r.msg = "手机号不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(model.Password)) {
                r.code = 1;
                r.msg = "密码不能为空";
                return r;
            }

            userregisterSqlMapDao udao = new userregisterSqlMapDao();
            var rdata = udao.GetuserregisterDetailByPhone(model.Phone);
            if (rdata == null) {
                r.code = 1;
                r.msg = "手机号或密码错误";
                return r;
            }
            if (rdata.Password != Common.UserMd5(model.Password)) {
                r.code = 1;
                r.msg = "手机号或密码错误";
                return r;
            }



            return GetUserFirstInfoById(rdata.RegisterId);
        }
        #endregion

        #region 获取首页信息根据注册ID 0    
        public RsModel<UserFirstInfo> GetUserFirstInfoById(string RegisterId) {
            RsModel<UserFirstInfo> r = new RsModel<UserFirstInfo>();
            if (string.IsNullOrWhiteSpace(RegisterId)) {
                r.code = 1;
                r.msg = "RegisterId不能为空";
                return r;
            }
            try {
                UserFirstInfo ufi = new UserFirstInfo();
                UserrelrecordSqlMapDao urdao = new UserrelrecordSqlMapDao();
                var urdata = urdao.GetuserrelrecordDetail(RegisterId);
                if (urdata != null) {
                    ufi.HospitalId = urdata.HospitalId;
                    ufi.HospitalName = urdata.HospitalName;
                    ufi.DepartmentId = urdata.DepartmentId;
                    ufi.DepartmentName = urdata.DepartmentName;
                    ufi.DepartmentUserCount = urdao.GetUserrelrecordList().Where(o => o.DepartmentId == urdata.DepartmentId && o.HospitalId == urdata.HospitalId).Count();
                }

                UserquacertificateSqlMapDao uqdao = new UserquacertificateSqlMapDao();

                var uqdata = uqdao.GetuserquacertificateDetail(RegisterId);
                if (uqdata != null) {
                    CertificateverifySqlMapDao cdao = new CertificateverifySqlMapDao();
                    var cdata = cdao.GetcertificateverifyList().FirstOrDefault(o => o.RegisterId == RegisterId && o.Type == 2);
                    if (cdata != null) {
                        ufi.QStatus = cdata.VerifyStatus;
                    }
                }
                UserpracticecertificateSqlMapDao updao = new UserpracticecertificateSqlMapDao();
                var updata = updao.GetuserpracticecertificateDetail(RegisterId);
                if (updata != null) {
                    CertificateverifySqlMapDao cdao = new CertificateverifySqlMapDao();
                    var cdata = cdao.GetcertificateverifyList().FirstOrDefault(o => o.RegisterId == RegisterId && o.Type == 1);
                    if (cdata != null) {
                        ufi.PStatus = cdata.VerifyStatus;
                    }
                }

                userregisterSqlMapDao urgdao = new userregisterSqlMapDao();
                var urgdata = urgdao.GetuserregisterDetail(RegisterId);
                if (urgdata != null)     //头像和昵称如果注册表里面没有数据，则从qq表里面取数据  //qq表绑定时
                {
                    //ufi.NickName = urgdata.NickName;
                    //ufi.Avatar = urgdata.Avatar;

                    if (ufi.PStatus == 3 && ufi.PStatus == 3) {
                        ufi.Name = updata.Name;
                    } else {
                        ufi.Name = urgdata.Name;
                    }
                    ufi.RegisterId = RegisterId;


                    if (string.IsNullOrWhiteSpace(urgdata.NickName)) {

                        UserauthsSqlMapDao uadaoo = new UserauthsSqlMapDao();
                        var uadataa = uadaoo.GetUserauthsList().FirstOrDefault(o => o.RegisterId == RegisterId);
                        if (uadataa != null) {
                            string loginnumber = uadataa.LoginNumber;

                            QqSqlMapDao qdao = new QqSqlMapDao();
                            var qqdata = qdao.GetQqList().FirstOrDefault(o => o.OpenId == loginnumber);   //查qq表
                            if (qqdata != null) {
                                ufi.NickName = qqdata.NickName;
                                // ufi.NickName =Common.Decode(qqdata.NickName);
                            }

                            WeixinSqlMapDao wdao = new WeixinSqlMapDao();
                            var wdata = wdao.GetWeixinList().FirstOrDefault(o => o.OpenId == loginnumber);   //查微信表
                            if (wdata != null) {
                                ufi.NickName = wdata.NickName;
                                // ufi.NickName =Common.Decode(qqdata.NickName);
                            }
                        }
                    } else {
                        ufi.NickName = urgdata.NickName;
                        //  ufi.NickName =Common .Decode(urgdata.NickName);
                    }

                    if (string.IsNullOrWhiteSpace(urgdata.Avatar)) {
                        UserauthsSqlMapDao uadaoo = new UserauthsSqlMapDao();
                        var uadataa = uadaoo.GetUserauthsList().FirstOrDefault(o => o.RegisterId == RegisterId);
                        if (uadataa != null) {
                            string loginnumber = uadataa.LoginNumber;

                            QqSqlMapDao qdao = new QqSqlMapDao();
                            var qqdata = qdao.GetQqList().FirstOrDefault(o => o.OpenId == loginnumber);   //查qq表
                            if (qqdata != null) {
                                ufi.Avatar = qqdata.FigureUrl;
                            }

                            WeixinSqlMapDao wdao = new WeixinSqlMapDao();
                            var wdata = wdao.GetWeixinList().FirstOrDefault(o => o.OpenId == loginnumber);   //查微信
                            if (wdata != null) {
                                ufi.Avatar = wdata.HeadImgurl;
                            }
                        }

                    } else {
                        ufi.Avatar = urgdata.Avatar;
                    }

                }

                UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();
                var uadata = uadao.GetUserauthsList().FirstOrDefault(o => o.RegisterId == RegisterId && o.LoginType == 4);   //绑定时这样写会找不到
                if (uadata != null) {
                    ufi.ReguserId = uadata.ReguserId;
                }
                r.code = 0;
                //string[] reid = { ufi.RegisterId };
                //string s = Common.PushMsgByAliasId("您已成功注册注册智护", reid, DeviceId);
                r.body = ufi;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "获取信息失败";
                return r;
            }
        }
        #endregion

        #region 获取绑定信息 0
        public RsModel<ViewUserBindInfo> GetUserBindInfo(string RegisterId) {
            RsModel<ViewUserBindInfo> r = new Services.RsModel<ViewUserBindInfo>();
            if (string.IsNullOrWhiteSpace(RegisterId)) {
                r.code = 1;
                r.msg = "RegisterId不能为空";
                return r;
            }
            try {
                ViewUserBindInfo ub = new ViewUserBindInfo();

                UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();
                var uadata = uadao.GetUserauthsList().Where(o => o.RegisterId == RegisterId);  //获取个人绑定的其他账号信息
                if (uadata == null) {
                    r.code = 1;
                    r.msg = "该账号暂无绑定信息";
                }

                int count;
                var qqdata = uadata.OrderByDescending(o => o.LoginLastTime).FirstOrDefault(o => o.LoginType == 1);

                if (qqdata != null) {
                    var qqOpenId = qqdata.LoginNumber;
                    ub.QQOpenId = qqOpenId;

                    QqSqlMapDao qdao = new QqSqlMapDao();

                    var qdata = qdao.GetQqList().FirstOrDefault(o => o.OpenId == qqOpenId);
                    if (qdata != null) {
                        ub.QQNickName = qdata.NickName;
                        // ub.QQNickName =Common .Decode(qdata.NickName);
                    }

                }

                var weixindata = uadata.OrderByDescending(o => o.LoginLastTime).FirstOrDefault(o => o.LoginType == 2);   //可优化
                if (weixindata != null) {
                    var wOpenId = weixindata.LoginNumber;
                    ub.WeixinOpenId = wOpenId;
                    WeixinSqlMapDao wdao = new WeixinSqlMapDao();
                    var wdata = wdao.GetWeixinList().FirstOrDefault(o => o.OpenId == wOpenId);
                    if (wdata != null) {
                        ub.WeixinNickName = wdata.NickName;
                    }
                }
                var weibodata = uadata.OrderByDescending(o => o.LoginLastTime).FirstOrDefault(o => o.LoginType == 3);   //可优化
                if (weibodata != null) {
                    var wOpenId = weibodata.LoginNumber;
                    ub.WeiboOpenId = wOpenId;
                    WeiboSqlMapDao wdao = new WeiboSqlMapDao();
                    var wdata = wdao.GetWeiboList().FirstOrDefault(o => o.idstr == wOpenId);
                    if (wdata != null) {
                        ub.WeiboNickName = wdata.name;
                    }
                }

                userregisterSqlMapDao urdao = new userregisterSqlMapDao();
                var urdata = urdao.GetuserregisterDetail(RegisterId);
                ub.Phone = urdata.Phone;
                ub.RegisterId = RegisterId;

                var BLSJData = uadata.FirstOrDefault(o => o.LoginType == 4 && o.RegisterId == RegisterId);  //不良事件  4
                if (BLSJData != null) {
                    ub.BLSJOpenId = BLSJData.ReguserId;
                    ub.BLSJId = BLSJData.LoginNumber;
                }

                var XFData = uadata.FirstOrDefault(o => o.LoginType == 5 && o.RegisterId == RegisterId);  //学分  5
                if (XFData != null) {
                    ub.XFOpenId = XFData.ReguserId;
                    ub.XFId = XFData.LoginNumber;
                }

                var PBData = uadata.FirstOrDefault(o => o.LoginType == 6 && o.RegisterId == RegisterId);  //排班  6
                if (PBData != null) {
                    ub.PBOpenId = PBData.ReguserId;
                    ub.PBId = PBData.LoginNumber;
                }

                ub.BindCount = uadata.Count();
                //string tsr = "";
                //int bindcount = (uadata.ToList().Where(o => o.LoginNumber != tsr)).Count(); //绑定其他账户个数   //需要修改

                //if (!string.IsNullOrWhiteSpace(urdata.Phone))
                //{
                //    ub.BindCount = bindcount + 1;
                //}
                //else
                //{
                //    ub.BindCount = bindcount;
                //}
                r.code = 0;
                r.body = ub;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "获取绑定信息失败";
                return r;
            }
        }
        #endregion

        #region 获取个人资料界面信息 0
        public RsModel<ViewUserInfo> GetUserInfo(string RegisterId) {
            RsModel<ViewUserInfo> r = new RsModel<ViewUserInfo>();
            if (string.IsNullOrWhiteSpace(RegisterId)) {
                r.code = 1;
                r.msg = "RegisterId不能为空";
                return r;
            }
            try {
                ViewUserInfo ui = new ViewUserInfo();
                ui.RegisterId = RegisterId;
                userregisterSqlMapDao urdao = new userregisterSqlMapDao();
                var urdata = urdao.GetuserregisterDetail(RegisterId);
                if (urdata != null) {
                    ui.Avatar = urdata.Avatar;
                    //  ui.NickName =Common.Decode(urdata.NickName); 
                    ui.NickName = urdata.NickName;
                    ui.Name = urdata.Name;


                    userbasicinfoSqlMapDao ubdao = new userbasicinfoSqlMapDao();
                    var ubdata = ubdao.GetuserbasicinfoDetail(RegisterId);
                    if (ubdata != null) {
                        ui.Sex = ubdata.Sex;
                        ui.Birthday = ubdata.Birthday;
                    }
                    UserpracticecertificateSqlMapDao upcdao = new UserpracticecertificateSqlMapDao();
                    var upcdata = upcdao.GetuserpracticecertificateDetail(RegisterId);
                    if (upcdata != null) {
                        ui.PCertificateId = upcdata.CertificateId;
                        ui.FirstJobTime = upcdata.FirstJobTime;
                        // ui.PVerifyStatus = upcdata.VerifyStatus;

                        CertificateverifySqlMapDao cdao = new CertificateverifySqlMapDao();
                        var cdata = cdao.GetcertificateverifyList().FirstOrDefault(o => o.RegisterId == RegisterId && o.Type == 1);
                        if (cdata != null) {
                            ui.PVerifyStatus = cdata.VerifyStatus;
                        } else {
                            ui.PVerifyStatus = 0;
                        }
                    }
                    UserquacertificateSqlMapDao uqcdao = new UserquacertificateSqlMapDao();
                    var uqcdata = uqcdao.GetuserquacertificateDetail(RegisterId);
                    if (uqcdata != null) {
                        ui.QCertificateId = uqcdata.CertificateId;
                        // ui.QVerifyStatus = uqcdata.VerifyStatus;

                        CertificateverifySqlMapDao cdao = new CertificateverifySqlMapDao();
                        var cdata = cdao.GetcertificateverifyList().FirstOrDefault(o => o.RegisterId == RegisterId && o.Type == 2);
                        if (cdata != null) {
                            ui.QVerifyStatus = cdata.VerifyStatus;
                        } else {
                            ui.QVerifyStatus = 0;
                        }

                    }
                    UserrelrecordSqlMapDao urddao = new UserrelrecordSqlMapDao();
                    var urddata = urddao.GetuserrelrecordDetail(RegisterId);
                    if (urddata != null) {
                        ui.HospitalId = urddata.HospitalId;
                        ui.HospitalName = urddata.HospitalName;
                        ui.DepartmentId = urddata.DepartmentId;
                        ui.DepartmentName = urddata.DepartmentName;
                        ui.EmployeeId = urddata.EmployeeId;

                        ui.DepartmentUserCount = urddao.GetUserrelrecordList().Where(o => o.HospitalId == urddata.HospitalId && o.DepartmentId == urddata.DepartmentId).Count();
                    }
                    r.code = 0;
                    r.body = ui;
                    return r;
                } else {
                    r.code = 0;
                    r.msg = "此账号不存在";
                    return r;
                }


            } catch (Exception) {
                r.code = 1;
                return r;
            }

        }
        #endregion

        #region  根据关键字搜索用户列表，返回给客户端数据
        public RsList<userregister> GetFriendsList(string RegisterId, string KeyWord) {
            RsList<userregister> r = new Services.RsList<userregister>();
            if (string.IsNullOrWhiteSpace(KeyWord)) {
                r.code = 1;
                r.msg = "搜索关键字不能为空";
                return r;
            }
            try {
                userregisterSqlMapDao udao = new userregisterSqlMapDao();
                var alllist = udao.GetuserregisterList().Where(o => o.RegisterId != RegisterId);
                var datalist = alllist.Where(o => (o.NickName != null && o.NickName.Contains(KeyWord)) || (o.Phone != null && o.Phone.Contains(KeyWord))).ToList();
                r.body = datalist;
                r.code = 0;
                return r;
            } catch (Exception e) {
                var ee = e;
                r.code = 1;
                r.msg = "查找好友信息失败";
                return r;
            }
        }

        public string UserLogin1() {
            return "1";
        }
        #endregion

        #region 根据本人Id,好友Id,获取好友信息，并指明是不是好友
        public RsModel<ViewFriendInfo> GetFriendInfo(string MyId, string FriendId) {
            RsModel<ViewFriendInfo> r = new Services.RsModel<ViewFriendInfo>();
            if (string.IsNullOrWhiteSpace(MyId)) {
                r.code = 1;
                r.msg = "MyId不能是空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(FriendId)) {
                r.code = 1;
                r.msg = "FriendId不能是空";
                return r;
            }
            try {
                ViewFriendInfo fi = new ViewFriendInfo();

                userregisterSqlMapDao urdao = new userregisterSqlMapDao();
                var urdata = urdao.GetuserregisterDetail(FriendId);
                if (urdata == null)   //好友没有在进行注册
                {
                    r.code = 1;
                    r.msg = "好友信息不存在";
                    return r;
                }
                fi.Avatar = urdata.Avatar;
                fi.Name = urdata.Name;
                fi.NickName = urdata.NickName;
                //  fi.NickName = Common.Decode(urdata.NickName);
                fi.Phone = urdata.Phone;

                userbasicinfoSqlMapDao ubdao = new userbasicinfoSqlMapDao();
                var ubdata = ubdao.GetuserbasicinfoDetail(FriendId);
                if (ubdata != null) {
                    fi.Sex = ubdata.Sex;
                }

                UserrelrecordSqlMapDao urldao = new UserrelrecordSqlMapDao();
                var urldata = urldao.GetuserrelrecordDetail(FriendId);
                if (urldata != null) {
                    fi.DepartmentName = urldata.DepartmentName;
                    fi.Role = urldata.Role;
                    fi.HospitalName = urldata.HospitalName;
                }

                EmchatSqlMapDao emdao = new EmchatSqlMapDao();
                var emdata = emdao.GetEmchatList().FirstOrDefault(o => o.MyId == MyId && o.FriendId == FriendId);  //
                if (emdata != null) {
                    if (emdata.IsFlag == 1) //是好友
                    {
                        fi.Remark = emdata.Remark;
                        fi.IsFriend = true;
                    } else  //不是好友 
                      {
                        fi.IsFriend = false;
                    }
                }

                UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();
                var uadata = uadao.GetUserauthsList();
                var mydata = uadata.FirstOrDefault(o => o.RegisterId == MyId && o.LoginType == 4);  //我的是院内不良事件账号
                var frienddata = uadata.FirstOrDefault(o => o.RegisterId == FriendId && o.LoginType == 4); //朋友的是院内不良事件院内账号
                if (mydata != null && frienddata != null) {
                    UserrelrecordSqlMapDao urrdao = new UserrelrecordSqlMapDao();
                    var myhdata = urrdao.GetuserrelrecordDetail(MyId);
                    var frhdata = urrdao.GetuserrelrecordDetail(FriendId);
                    if (myhdata != null && frhdata != null) {
                        if (myhdata.HospitalId == frhdata.HospitalId) {
                            fi.IsInternalHospital = true;
                        } else {
                            fi.IsInternalHospital = false;
                        }
                    } else {
                        fi.IsInternalHospital = false;
                    }

                } else {
                    fi.IsInternalHospital = false;
                }


                fi.MyId = MyId;
                fi.FriendId = FriendId;
                r.code = 0;
                r.body = fi;
                return r;

            } catch (Exception) {
                r.code = 1;
                r.msg = "获取信息失败";
                return r;
            }

        }
        #endregion

        #region 发送好友邀请
        public RsModel<string> SendFriendMsg(string MyId, string FriendId) {
            RsModel<string> r = new RsModel<string>();
            if (string.IsNullOrWhiteSpace(MyId)) {
                r.code = 1;
                r.msg = "MyId不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(FriendId)) {
                r.code = 1;
                r.msg = "FriendId";
                return r;
            }
            try {
                userregisterSqlMapDao urdao = new userregisterSqlMapDao();
                var myData = urdao.GetuserregisterDetail(MyId);
                if (myData == null) {
                    r.code = 1;
                    r.msg = "MyId账号不存在";
                    return r;
                }
                var FriendData = urdao.GetuserregisterDetail(FriendId);
                if (FriendData == null) {
                    r.code = 1;
                    r.msg = "FriendId账号不存在";
                    return r;
                }
                //var user = Client.DefaultSyncRequest.UserCreate(new UserCreateReqeust()
                //{
                //    nickname = string.Concat("Test", this._userName),
                //    password = "123456",
                //    username = string.Concat("Test", this._userName),
                //});
                //Assert.AreEqual(user.StatusCode, HttpStatusCode.OK);
                //if (user.StatusCode == HttpStatusCode.OK)
                //{
                var send = Client.DefaultSyncRequest.MsgSend<MsgText>(new MsgRequest<MsgText>() {
                    target_type = "users",
                    from = "admin",
                    //  from = 
                    msg = new MsgText() {
                        msg = myData.Name + "邀请您加为好友"
                    },
                    target = new string[] { Common.EMRegisterId(FriendId) }
                });
                Assert.AreEqual(send.StatusCode, HttpStatusCode.OK);
                // }
                r.code = 0;
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "发送失败";
                return r;
            }
        }
        #endregion

        #region  环信 加好友
        public RsModel<string> AddEMFriend(string MyId, string FriendId) {
            RsModel<string> r = new RsModel<string>();
            if (string.IsNullOrWhiteSpace(MyId)) {
                r.code = 1;
                r.msg = "MyId不能为空";

                return r;
            }
            if (string.IsNullOrWhiteSpace(FriendId)) {
                r.code = 1;
                r.msg = "FriendId不能为空";
                return r;
            }

            userregisterSqlMapDao urdao = new userregisterSqlMapDao();
            var myData = urdao.GetuserregisterDetail(MyId);
            if (myData == null) {
                r.code = 1;
                r.msg = "MyId账号不存在";
                return r;
            }
            var FriendData = urdao.GetuserregisterDetail(FriendId);
            if (FriendData == null) {
                r.code = 1;
                r.msg = "FriendId账号不存在";
                return r;
            }

            try {
                EmchatSqlMapDao edao = new EmchatSqlMapDao();

                var edata = edao.GetEmchatList();
                var eed = edata.FirstOrDefault(o => o.MyId == MyId && o.FriendId == FriendId);
                if (eed != null) //以前有记录
                {
                    Emchat ec = new Emchat();
                    ec.EMChatId = eed.EMChatId;
                    ec.MyId = eed.MyId;
                    ec.EMMyId = eed.EMMyId;
                    ec.FriendId = eed.FriendId;
                    ec.EMFriendId = eed.EMFriendId;
                    ec.IsFlag = 1;  //改为1
                    ec.Remark = eed.Remark;
                    edao.Updateemchat(ec);
                } else {
                    Emchat ec = new Emchat();
                    ec.EMChatId = new aers_sys_seedSqlMapDao().GetMaxID("Emchat");  //注册表
                    ec.MyId = MyId;
                    ec.EMMyId = MyId;
                    ec.EMFriendId = FriendId;
                    ec.FriendId = FriendId;
                    ec.IsFlag = 1;  //删除时用，1是好友，0，解除好友关系
                    //ec.Remark = MyId + "对好友" + FriendId + "的备注";
                    //ec.Remark = MyId + "对好友" + FriendId + "的备注";
                    edao.Addemchat(ec);
                }
                var efd = edata.FirstOrDefault(o => o.MyId == FriendId && o.FriendId == MyId);
                if (efd != null) {
                    Emchat ec = new Emchat();
                    ec.EMChatId = efd.EMChatId;
                    ec.MyId = efd.MyId;
                    ec.EMMyId = efd.EMMyId;
                    ec.FriendId = efd.FriendId;
                    ec.EMFriendId = efd.EMFriendId;
                    ec.IsFlag = 1;  //改为1
                    ec.Remark = efd.Remark;
                    edao.Updateemchat(ec);
                } else {
                    Emchat ec = new Emchat();
                    ec.EMChatId = new aers_sys_seedSqlMapDao().GetMaxID("Emchat");  //注册表
                    ec.MyId = FriendId;
                    ec.EMMyId = FriendId;
                    ec.EMFriendId = MyId;
                    ec.FriendId = MyId;
                    ec.IsFlag = 1;  //删除时用，1是好友，0，解除好友关系
                    //ec.Remark = FriendId + "对好友" + MyId + "的备注";
                    edao.Addemchat(ec);
                }
                r.code = 0;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "数据库插入失败";
                return r;
            }
            //try
            //{
            //    var add = Client.DefaultSyncRequest.UserFriendAdd(new UserFriendAddRequest() { friend_username = FriendId, owner_username = MyId });
            //    r.code = 0;
            //    return r;
            //}
            //catch (Exception e)
            //{
            //    r.code = 1;
            //    r.msg = e.ToString();
            //    return r;
            //}
        }
        #endregion

        #region 设置好友备注
        /// <summary>
        /// 设置好友备注
        /// </summary>
        /// <param name="emchat"></param>
        /// <returns></returns>
        public RsModel<string> UpdateFriend(Emchat emchat) {
            RsModel<string> result = new RsModel<string>();
            if (string.IsNullOrWhiteSpace(emchat.MyId)) {
                result.code = 1;
                result.msg = "MyId不能为空";
                return result;
            }
            if (string.IsNullOrWhiteSpace(emchat.FriendId)) {
                result.code = 1;
                result.msg = "FriendId不能为空";
                return result;
            }
            try {
                EmchatSqlMapDao dao = new EmchatSqlMapDao();
                Emchat first = dao.GetEmchatList().First(o => o.MyId == emchat.MyId && o.FriendId == emchat.FriendId);
                first.Remark = emchat.Remark;
                dao.Updateemchat(first);
            } catch (Exception) {
                result.code = 1;
                result.msg = "设置备注失败";
                return result;
            }
            result.code = 0;
            return result;
        }
        #endregion

        #region  读好友
        public RsList<ViewFriendInfo> GetEMFriend(string MyId) {
            RsList<ViewFriendInfo> r = new RsList<ViewFriendInfo>();
            if (string.IsNullOrWhiteSpace(MyId)) {
                r.code = 1;
                r.msg = "MyId不能为空";
                return r;
            }
            try {
                EmchatSqlMapDao edao = new EmchatSqlMapDao();
                var userList = edao.GetEmchatList().Where(o => o.MyId == MyId && o.IsFlag == 1);
                userregisterSqlMapDao urdao = new userregisterSqlMapDao();

                List<ViewFriendInfo> flist = new List<ViewFriendInfo>();
                foreach (var item in userList) {
                    ViewFriendInfo fi = new ViewFriendInfo();
                    var urdata = urdao.GetuserregisterDetail(item.FriendId);
                    fi.FriendId = item.FriendId;
                    fi.Name = urdata.Name;
                    fi.NickName = urdata.NickName;
                    //  fi.NickName = Common.Decode(urdata.NickName);
                    fi.Avatar = urdata.Avatar;
                    fi.Phone = urdata.Phone;
                    fi.IsFriend = true;
                    flist.Add(fi);
                }
                r.code = 0;
                r.body = flist;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "获取好友失败";
                // log.Error("error", new Exception(e.Message));
                return r;
            }
        }
        #endregion

        #region 环信 删好友、解除好友关系
        public RsModel<string> DeleteEMFriend(string MyId, string FriendId) {
            RsModel<string> r = new Services.RsModel<string>();
            if (string.IsNullOrWhiteSpace(MyId)) {
                r.code = 1;
                r.msg = "MyId不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(FriendId)) {
                r.code = 1;
                r.msg = "FriendId不能为空";
                return r;
            }
            try {
                EmchatSqlMapDao ecdao = new EmchatSqlMapDao();
                var data = ecdao.GetEmchatList();
                Emchat ecm = new Emchat();
                var mdata = data.FirstOrDefault(o => o.MyId == MyId && o.FriendId == FriendId); //解除关系时，只是把标识IsFlag改为0
                ecm.EMChatId = mdata.EMChatId;
                ecm.EMFriendId = mdata.EMFriendId;
                ecm.EMMyId = mdata.EMMyId;
                ecm.FriendId = mdata.FriendId;
                ecm.IsFlag = 0;
                ecm.MyId = mdata.MyId;
                ecm.Remark = mdata.Remark;
                ecdao.Updateemchat(ecm);

                Emchat ecf = new Emchat();
                var fdata = data.FirstOrDefault(o => o.MyId == FriendId && o.FriendId == MyId);
                ecf.EMChatId = fdata.EMChatId;
                ecf.EMFriendId = fdata.EMFriendId;
                ecf.EMMyId = fdata.EMMyId;
                ecf.FriendId = fdata.FriendId;
                ecf.IsFlag = 0;
                ecf.MyId = fdata.MyId;
                ecf.Remark = fdata.Remark;
                ecdao.Updateemchat(ecf);
                r.code = 0;

                var delete = Client.DefaultSyncRequest.UserFriendDelete(new UserFriendDeleteRequest() { friend_username = MyId, owner_username = FriendId });
                Assert.AreEqual(delete.StatusCode, HttpStatusCode.OK);
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "解除好友关系失败";
                return r;
            }
        }

        #endregion

        #region 创建一个群
        public RsModel<ViewGroupList> CreateGroup(ViewGroupList model) {

            RsModel<ViewGroupList> r = new Services.RsModel<ViewGroupList>();
            if (string.IsNullOrWhiteSpace(model.RegisterId))  //创建人Id
            {
                r.code = 1;
                r.msg = "创建人Id不能为空";
                return r;
            }
            if (model.groupMemberList.Count < 3) {
                r.code = 1;
                r.msg = "三个用户以上才可以创建群";
                return r;
            }
            string OwnerId = model.RegisterId; //第一项是创建人Id
            string GroupNickName = model.HXNickName;  //群昵称
            string Desc = OwnerId + "在" + DateTime.Now + "创建的群";     //群描述，可以传值过来
            int MaxCountGroup = 100; //群最大人数
            var owner = model.groupMemberList[0];
            model.groupMemberList.RemoveRange(0, 1);  //创建人第一项
            List<string> memberList = model.groupMemberList.Select(o => o.FriendId).ToList();
            // memberList.Add(model.RegisterId);  //添加第一项
            var memberarr = memberList.ToArray();
            try {
                var response = Easemob.Restfull4Net.Client.DefaultSyncRequest.ChatGroupCreate(new CreateChatGroupRequest() {
                    approval = false,
                    desc = Desc,
                    groupname = GroupNickName,
                    maxusers = MaxCountGroup,
                    members = memberarr,  //群主的不需要写在里面
                    owner = OwnerId,
                }); //环信建群

                var HXresponse = response;
                GroupinfoSqlMapDao gidao = new GroupinfoSqlMapDao();  //群信息表
                Groupinfo gi = new Groupinfo();                                          // ViewGroupList gi = new ViewGroupList();
                gi.GroupId = new aers_sys_seedSqlMapDao().GetMaxID("groupinfo");
                gi.CreaterId = OwnerId;
                gi.CreateTime = DateTime.Now;
                gi.GroupNickName = GroupNickName;
                gi.Descg = Desc;
                gi.IsFlag = 1;
                gi.MaxGroupCount = MaxCountGroup;
                gi.UserCount = model.groupMemberList.Count + 1;
                if (HXresponse.data == null) {
                    r.code = 1;
                    r.msg = "环信创建群失败" + response.ErrorMessage.error_description;
                    return r;
                }
                gi.HXGroupId = HXresponse.data.groupid;  //环信创建的群Id
                gidao.Addgroupinfo(gi);    //群信息表添加


                ViewGroupList vg = new ViewGroupList();
                vg.CreateTime = gi.CreateTime;
                vg.GroupId = gi.GroupId;
                vg.GroupUserCount = gi.UserCount;
                vg.HXGroupId = gi.HXGroupId;
                vg.HXNickName = gi.GroupNickName;

                userregisterSqlMapDao urdao = new userregisterSqlMapDao();

                GroupuserSqlMapDao gudao = new GroupuserSqlMapDao();

                // 群主添加到信息中
                Groupuser owerUser = new Groupuser();
                owerUser.GroupUserId = new aers_sys_seedSqlMapDao().GetMaxID("groupuser"); //可优化
                owerUser.IsFlag = 1;
                owerUser.IsMaster = 1;
                owerUser.NickName = urdao.GetuserregisterDetail(owner.MyId).NickName;
                owerUser.RegisterId = owner.MyId;
                owerUser.GroupId = gi.GroupId;
                owerUser.HXGroupId = HXresponse.data.groupid;
                owerUser.JoinTime = DateTime.Now;

                gudao.Addgroupuser(owerUser);

                foreach (var item in model.groupMemberList) {
                    Groupuser gup = new Groupuser();
                    gup.GroupUserId = new aers_sys_seedSqlMapDao().GetMaxID("groupuser"); //可优化
                    gup.IsFlag = 1;
                    if (gup.RegisterId == OwnerId) {
                        gup.IsMaster = 1;
                        gup.NickName = urdao.GetuserregisterDetail(item.MyId).NickName;
                        gup.RegisterId = item.MyId;
                    } else {
                        gup.IsMaster = 0;
                        gup.NickName = urdao.GetuserregisterDetail(item.FriendId).NickName;
                        gup.RegisterId = item.FriendId;
                    }
                    gup.GroupId = gi.GroupId;
                    gup.HXGroupId = HXresponse.data.groupid;
                    gup.JoinTime = DateTime.Now;

                    gudao.Addgroupuser(gup);
                }
                var response1 = Easemob.Restfull4Net.Client.DefaultSyncRequest.ChatGroupCreate(new CreateChatGroupRequest() {
                    approval = false,
                    desc = Desc,
                    groupname = GroupNickName,
                    maxusers = MaxCountGroup,
                    members = memberarr,
                    owner = OwnerId
                });
                r.code = 0;
                r.body = vg;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "创建群失败" + e;
                return r;
            }
        }

        //public RsModel<ViewGroupList> CreateGroup(List<userregister> model)
        //{

        //    RsModel<ViewGroupList> r = new Services.RsModel<ViewGroupList>();
        //    if (string.IsNullOrWhiteSpace(model[0].RegisterId))
        //    {
        //        r.code = 1;
        //        r.msg = "创建人Id不能为空";
        //        return r;
        //    }
        //    if (model.Count < 3)
        //    {
        //        r.code = 1;
        //        r.msg = "三个用户以上才可以创建群";
        //        return r;
        //    }
        //    string OwnerId = model[0].RegisterId; //第一项是创建人Id
        //    string GroupNickName = "群聊";
        //    string Desc = OwnerId + "在" + DateTime.Now + "创建的群";
        //    int MaxCountGroup = 100; //群最大人数
        //    try
        //    {

        //        var response = Easemob.Restfull4Net.Client.DefaultSyncRequest.ChatGroupCreate(new CreateChatGroupRequest()
        //        {
        //            approval = false,
        //            desc = Desc,
        //            groupname = GroupNickName,
        //            maxusers = MaxCountGroup,
        //            members = model.Select(o => o.RegisterId).ToArray(),
        //            owner = OwnerId
        //        });
        //        var HXresponse = response;
        //        GroupinfoSqlMapDao gidao = new GroupinfoSqlMapDao();  //群信息表
        //        Groupinfo gi = new Groupinfo();                                          // ViewGroupList gi = new ViewGroupList();
        //        gi.GroupId = new aers_sys_seedSqlMapDao().GetMaxID("groupinfo");
        //        gi.CreaterId = OwnerId;
        //        gi.CreateTime = DateTime.Now;
        //        gi.GroupNickName = GroupNickName;
        //        gi.Descg = Desc;
        //        gi.IsFlag = 1;
        //        gi.MaxGroupCount = MaxCountGroup;
        //        gi.UserCount = model.Count;
        //        gi.HXGroupId = HXresponse.data.groupid;  //环信创建的群Id
        //        gidao.Addgroupinfo(gi);    //群信息表添加


        //        ViewGroupList vg = new ViewGroupList();
        //        vg.CreateTime = gi.CreateTime;
        //        vg.GroupId = gi.GroupId;
        //        vg.GroupUserCount = gi.UserCount;
        //        vg.HXGroupId = gi.HXGroupId;
        //        vg.HXNickName = gi.GroupNickName;

        //        userregisterSqlMapDao urdao = new userregisterSqlMapDao();

        //        GroupuserSqlMapDao gudao = new GroupuserSqlMapDao();
        //        Groupuser gu = new Groupuser();
        //        foreach (var item in model)
        //        {
        //            Groupuser gup = new Groupuser();
        //            gup.GroupUserId = new aers_sys_seedSqlMapDao().GetMaxID("groupuser"); //可优化
        //            gup.IsFlag = 1;
        //            gup.RegisterId = item.RegisterId;
        //            if (gup.RegisterId == OwnerId)
        //            {
        //                gup.IsMaster = 1;
        //            }
        //            else
        //            {
        //                gup.IsMaster = 0;
        //            }
        //            gup.GroupId = gi.GroupId;
        //            gup.HXGroupId = HXresponse.data.groupid;
        //            gup.JoinTime = DateTime.Now;
        //            gup.NickName = urdao.GetuserregisterDetail(item.RegisterId).NickName;
        //            //  gup.NickName = Common.Decode(urdao.GetuserregisterDetail(item.RegisterId).NickName);
        //            gudao.Addgroupuser(gup);
        //        }
        //        var response1 = Easemob.Restfull4Net.Client.DefaultSyncRequest.ChatGroupCreate(new CreateChatGroupRequest()
        //        {
        //            approval = false,
        //            desc = Desc,
        //            groupname = GroupNickName,
        //            maxusers = MaxCountGroup,
        //            members = model.Select(o => o.RegisterId).ToArray(),
        //            owner = OwnerId
        //        });
        //        //string msg = "您已加入群";
        //        //int i = SendMsgToGroupmember(msg, OwnerId, HXresponse.data.groupid);  //给群发消息

        //        r.code = 0;
        //        r.body = vg;
        //        return r;
        //    }
        //    catch (Exception e)
        //    {
        //        r.code = 1;
        //        r.msg = "创建群失败" + e;
        //        return r;
        //    }
        //}
        #endregion

        #region 给群组成员发消息
        public int SendMsgToGroupmember(string msg, string OwnerId, string GroupId) {
            try {
                var send = Client.DefaultSyncRequest.MsgSend<MsgText>(new MsgRequest<MsgText>() {
                    target_type = "chatgroups",
                    from = OwnerId,
                    msg = new MsgText() {
                        msg = msg
                    },
                    target = new string[] { GroupId }
                });
                return 1;
            } catch (Exception) {
                return 0;
            }
        }
        #endregion

        #region 从客户端手机通讯录得到数据
        public RsList<ViewContact> GetContactInfo(List<ViewContact> model) {
            RsList<ViewContact> r = new Services.RsList<ViewContact>();
            if (model == null) {
                r.code = 1;
                r.msg = "model不能为空";
                return r;
            }
            try {
                var mydata = model[0];

                userregisterSqlMapDao udao = new userregisterSqlMapDao();
                var data = udao.GetuserregisterList();
                List<ViewContact> flist = new List<ViewContact>(); ;
                EmchatSqlMapDao ecdao = new EmchatSqlMapDao();
                var ecdata = ecdao.GetEmchatList();
                for (int i = 0; i < model.Count; i++) {
                    var item = model[i];
                    foreach (var d in data) {
                        if (item.Phone == null) {
                            continue;
                        }
                        if (item.Phone == d.Phone) {
                            item.Avatar = d.Avatar;
                            item.Name = d.Name;
                            item.NickName = d.NickName;
                            item.Phone = d.Phone;
                            item.RegisterId = d.RegisterId;
                            item.status = 1; //用软件

                            var isfriend = ecdata.FirstOrDefault(o => o.MyId == mydata.RegisterId && o.FriendId == item.RegisterId);
                            if (isfriend != null)  //是好友
                            {
                                item.status = 2;
                            }
                        }
                    }
                    flist.Add(item);
                }
                r.code = 0;
                flist.RemoveRange(0, 1);
                r.body = flist;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = e.ToString();
                return r;
            }
        }

        #endregion

        #region 添加一个群成员
        public RsModel<string> AddGroupOne(string RegisterId, string GroupId) {
            RsModel<string> r = new Services.RsModel<string>();
            if (string.IsNullOrWhiteSpace(RegisterId)) {
                r.code = 1;
                r.msg = "RegisterId不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(GroupId)) {
                r.code = 1;
                r.msg = "GroupId不能为空";
                return r;
            }
            try {
                GroupinfoSqlMapDao gidao = new GroupinfoSqlMapDao();
                var gidata = gidao.GetGroupinfoDetail(GroupId);
                Groupinfo gi = new Groupinfo();
                gi.GroupId = gidata.GroupId;
                gi.GroupNickName = gidata.GroupNickName;
                gi.UserCount = gidata.UserCount + 1; //群成员数+1
                gi.CreaterId = gidata.CreaterId;
                gi.CreateTime = gidata.CreateTime;
                gi.IsFlag = gidata.IsFlag;
                gi.Descg = gidata.Descg;
                gi.MaxGroupCount = gidata.MaxGroupCount;
                gidao.Updategroupinfo(gi);   //更新群信息表

                userregisterSqlMapDao ugdao = new userregisterSqlMapDao();

                GroupuserSqlMapDao gudao = new GroupuserSqlMapDao();
                Groupuser gu = new Groupuser();
                gu.GroupUserId = GroupId;
                gu.RegisterId = RegisterId;
                // gu.NickName = ""; //加群时可能无昵称
                //  gu.NickName =Common .Decode( ugdao.GetuserregisterDetail(RegisterId).NickName);
                gu.NickName = ugdao.GetuserregisterDetail(RegisterId).NickName;
                gu.JoinTime = DateTime.Now;
                gu.IsMaster = 0;
                gu.IsFlag = 1;  //加入群1，推出群0
                gudao.Addgroupuser(gu);

                var response = Easemob.Restfull4Net.Client.DefaultSyncRequest.ChatGroupMemberAdd(GroupId, RegisterId);
                // var response = Easemob.Restfull4Net.Client.DefaultSyncRequest.ChatGroupMemberAdd(create.data.groupid, user.entities[0].username);
                r.code = 0;
                return r;

            } catch (Exception) {
                r.code = 1;
                r.msg = "添加群成员失败";
                return r;
            }
        }
        #endregion

        #region 获取此人群信息  
        //public RsList<ViewGroupList> GetXHGroupInfoList(string RegisterId)
        //{
        //    RsList<ViewGroupList> r = new RsList<ViewGroupList>();
        //    if (string.IsNullOrWhiteSpace(RegisterId))
        //    {
        //        r.code = 1;
        //        r.msg = "用户Id不能为空";
        //        return r;
        //    }
        //    try
        //    {
        //         var GroupList = Easemob.Restfull4Net.Client.DefaultSyncRequest.ChatGroupUser(RegisterId);
        //        var GroupDataList = GroupList.data;
        //        List<ViewGroupList> vglist = new List<ViewGroupList>();

        //        GroupuserSqlMapDao gudao = new GroupuserSqlMapDao();
        //        var gudata = gudao.GetGroupuserList();

        //        userregisterSqlMapDao urdao = new userregisterSqlMapDao();
        //        var urdata = urdao.GetuserregisterList();

        //        EmchatSqlMapDao ecdao = new EmchatSqlMapDao();
        //        var ecdata = ecdao.GetEmchatList();

        //        UserrelrecordSqlMapDao urldao = new UserrelrecordSqlMapDao();
        //        var urldata = urldao.GetUserrelrecordList();

        //        userbasicinfoSqlMapDao ubdao = new userbasicinfoSqlMapDao();
        //        var ubdata = ubdao.GetuserbasicinfoList();
        //        foreach (var item in GroupDataList)
        //        {
        //            ViewGroupList vgl = new ViewGroupList();
        //            var userGroupIdDataList = Easemob.Restfull4Net.Client.DefaultSyncRequest.ChatGroupMemberAll(item.groupid);  //根据群Id获取群信息
        //            vgl.HXGroupId = item.groupid;
        //            vgl.HXNickName = item.groupname;
        //            //vgl .CreateTime =item .
        //            vgl.RegisterId = RegisterId;
        //            List<ViewFriendInfo> vclist = new List<ViewFriendInfo>();
        //            foreach (var u in userGroupIdDataList.data)
        //            {
        //                ViewFriendInfo vc = new ViewFriendInfo();
        //                //  vc.RegisterId = u.owner == null ? u.member : u.owner;
        //                if (!string.IsNullOrWhiteSpace(u.owner))
        //                {
        //                    var drdata = urdata.FirstOrDefault(o => o.RegisterId == u.owner);
        //                    vc.NickName = gudata.FirstOrDefault(o => o.HXGroupId == item.groupid && o.RegisterId == u.owner).NickName;
        //                    vc.Avatar = drdata.Avatar;
        //                    vc.FriendId = u.owner;
        //                    vc.Name = drdata.Name;
        //                    vc.Phone = drdata.Phone;
        //                    vc.Role = urldata.FirstOrDefault(o => o.RegisterId == u.owner).Role;
        //                    vc.Sex = ubdata.FirstOrDefault(o => o.RegisterId == u.owner).Sex;


        //                    var isfriendData = ecdata.FirstOrDefault(o => o.MyId == RegisterId && o.FriendId == u.owner);
        //                    if (isfriendData == null)
        //                    {
        //                        vc.IsFriend = false;
        //                    }
        //                    else
        //                    {
        //                        if (isfriendData.IsFlag == 0)
        //                        {
        //                            vc.IsFriend = false;
        //                        }
        //                        else
        //                        {
        //                            vc.IsFriend = true;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    //   vc.MyId = RegisterId;
        //                    var drdata = urdata.FirstOrDefault(o => o.RegisterId == u.member);
        //                    vc.NickName = gudata.FirstOrDefault(o => o.HXGroupId == item.groupid && o.RegisterId == u.member).NickName;
        //                    vc.Avatar = drdata.Avatar;
        //                    vc.FriendId = u.member;
        //                    vc.Name = drdata.Name;
        //                    vc.Phone = drdata.Phone;
        //                    vc.Role = urldata.FirstOrDefault(o => o.RegisterId == u.member).Role;
        //                    vc.Sex = ubdata.FirstOrDefault(o => o.RegisterId == u.member).Sex;
        //                    var isfriendData = ecdata.FirstOrDefault(o => o.MyId == RegisterId && o.FriendId == u.member);
        //                    if (isfriendData == null)
        //                    {
        //                        vc.IsFriend = false;
        //                    }
        //                    else
        //                    {
        //                        if (isfriendData.IsFlag == 0)
        //                        {
        //                            vc.IsFriend = false;
        //                        }
        //                        else
        //                        {
        //                            vc.IsFriend = true;
        //                        }
        //                    }
        //                }

        //                vclist.Add(vc);
        //            }
        //            vgl.groupMemberList = vclist;
        //            vglist.Add(vgl);
        //        }
        //        r.code = 0;
        //        r.body = vglist;
        //        return r;
        //    }
        //    catch (Exception e)
        //    {
        //        r.code = 1;
        //        r.msg = "获取信息失败" + e;
        //        return r;
        //    }
        //}
        #endregion

        #region 获取此人群信息  
        public RsList<ViewGroupList> GetXHGroupInfoList(string RegisterId) {
            RsList<ViewGroupList> r = new RsList<ViewGroupList>();
            if (string.IsNullOrWhiteSpace(RegisterId)) {
                r.code = 1;
                r.msg = "用户Id不能为空";
                return r;
            }
            try {
                GroupuserSqlMapDao gudao = new GroupuserSqlMapDao();
                var gudata = gudao.GetGroupuserList();
                var GroupDataList = gudata.Where(o => o.RegisterId == RegisterId);   //根据群人员表获取信息
                if (GroupDataList == null) {
                    r.code = 1;
                    r.msg = "暂无此人群信息";
                    return r;
                }
                List<string> grouplist = new List<string>();
                foreach (var item in GroupDataList) {
                    grouplist.Add(item.GroupId);   //此人所在的群list
                }
                List<ViewGroupList> vglist = new List<ViewGroupList>();

                userregisterSqlMapDao urdao = new userregisterSqlMapDao();
                var urdata = urdao.GetuserregisterList();

                EmchatSqlMapDao ecdao = new EmchatSqlMapDao();
                var ecdata = ecdao.GetEmchatList();

                UserrelrecordSqlMapDao urldao = new UserrelrecordSqlMapDao();
                var urldata = urldao.GetUserrelrecordList();

                userbasicinfoSqlMapDao ubdao = new userbasicinfoSqlMapDao();
                var ubdata = ubdao.GetuserbasicinfoList();

                GroupinfoSqlMapDao gidao = new GroupinfoSqlMapDao();
                var gidata = gidao.GetGroupinfoList();

                foreach (var item in grouplist) {
                    ViewGroupList vgl = new ViewGroupList();
                    vgl.HXGroupId = gidata.FirstOrDefault(o => o.GroupId == item).HXGroupId;
                    vgl.GroupId = item;
                    vgl.CreateTime = gidata.FirstOrDefault(o => o.GroupId == item).CreateTime;
                    vgl.HXNickName = gidata.FirstOrDefault(o => o.GroupId == item).GroupNickName;
                    vgl.RegisterId = RegisterId;
                    vgl.GroupUserCount = gidata.FirstOrDefault(o => o.GroupId == item).UserCount;
                    List<ViewFriendInfo> vclist = new List<ViewFriendInfo>();
                    var groupuserlist = gudata.Where(o => o.GroupId == item);
                    foreach (var u in groupuserlist) {
                        ViewFriendInfo vc = new ViewFriendInfo();
                        vc.NickName = u.NickName;
                        vc.Avatar = urdata.FirstOrDefault(o => o.RegisterId == u.RegisterId).Avatar;
                        vc.Name = urdata.FirstOrDefault(o => o.RegisterId == u.RegisterId).Name;
                        vc.Phone = urdata.FirstOrDefault(o => o.RegisterId == u.RegisterId).Phone;
                        vc.Role = urldata.FirstOrDefault(o => o.RegisterId == u.RegisterId).Role;
                        vc.Sex = ubdata.FirstOrDefault(o => o.RegisterId == u.RegisterId).Sex;
                        vc.FriendId = u.RegisterId;
                        vc.MyId = RegisterId;
                        var isfriend = ecdata.FirstOrDefault(o => o.MyId == RegisterId && o.FriendId == u.RegisterId);
                        if (isfriend == null) {
                            vc.IsFriend = false;
                        } else {
                            if (isfriend.IsFlag == 0) {
                                vc.IsFriend = false;
                            } else {
                                vc.IsFriend = true;
                            }
                        }
                        vclist.Add(vc);
                    }
                    vgl.groupMemberList = vclist;
                    vglist.Add(vgl);
                }
                r.code = 0;
                r.body = vglist;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "获取信息失败" + e;
                return r;
            }
        }
        #endregion

        #region 根据群Id获取群信息
        public RsModel<ViewGroupList> GetXHGroupList(string GroupId, string RegisterId) {
            RsModel<ViewGroupList> r = new RsModel<ViewGroupList>();
            if (string.IsNullOrWhiteSpace(GroupId)) {
                r.code = 1;
                r.msg = "GroupId不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(RegisterId)) {
                r.code = 1;
                r.msg = "RegisterId不能为空";
                return r;
            }
            try {
                ViewGroupList vl = new ViewGroupList();
                GroupinfoSqlMapDao gidao = new GroupinfoSqlMapDao();
                var gidata = gidao.GetGroupinfoList().FirstOrDefault(o => o.HXGroupId == GroupId);
                if (gidata == null) {
                    r.code = 1;
                    r.msg = "GroupId不存在";
                    return r;
                }
                vl.HXNickName = gidata.GroupNickName;
                vl.CreateTime = gidata.CreateTime;
                vl.GroupUserCount = gidata.UserCount;
                vl.RegisterId = RegisterId;
                vl.GroupId = gidata.GroupId;
                vl.HXGroupId = gidata.HXGroupId;
                GroupuserSqlMapDao gudao = new GroupuserSqlMapDao();
                var gudata = gudao.GetGroupuserList().Where(o => o.HXGroupId == GroupId);

                List<ViewFriendInfo> vflist = new List<ViewFriendInfo>();

                userregisterSqlMapDao urdao = new userregisterSqlMapDao();

                UserrelrecordSqlMapDao urrdao = new UserrelrecordSqlMapDao();

                userbasicinfoSqlMapDao ubdao = new userbasicinfoSqlMapDao();

                EmchatSqlMapDao ecdao = new EmchatSqlMapDao();
                var ecdata = ecdao.GetEmchatList();

                foreach (var item in gudata) {
                    ViewFriendInfo vf = new ViewFriendInfo();
                    var urdata = urdao.GetuserregisterDetail(item.RegisterId);

                    vf.Avatar = urdata.Avatar;  //注册表里面取
                    vf.NickName = gudata.FirstOrDefault(o => o.RegisterId == item.RegisterId).NickName;  //群信息表里面取
                    vf.Name = urdata.Name;
                    vf.Phone = urdata.Phone;
                    vf.Role = urrdao.GetuserrelrecordDetail(item.RegisterId).Role;
                    vf.Sex = ubdao.GetuserbasicinfoDetail(item.RegisterId).Sex;
                    vf.FriendId = item.RegisterId;
                    vf.MyId = RegisterId;
                    var isfriendData = ecdata.FirstOrDefault(o => o.MyId == RegisterId && o.FriendId == item.RegisterId);
                    if (isfriendData == null) {
                        vf.IsFriend = false;
                    } else {
                        if (isfriendData.IsFlag == 0) {
                            vf.IsFriend = false;
                        } else {
                            vf.IsFriend = true;
                        }
                    }

                    vflist.Add(vf);
                }
                vl.groupMemberList = vflist;
                r.code = 0;
                r.body = vl;
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "获取群信息失败";
                return r;

            }
        }
        #endregion

        #region 极光推送

        //public RsModel<string> Jpush()
        //{
        //    RsModel<string> r = new RsModel<string>();
        //    JPushClient client = new JPushClient(app_key, master_secret);

        //    string strPush = "极光测试";
        //    PushPayload payload = PushObject_All_All_Alert(strPush);

        //    var result = client.SendPush(payload);
        //    r.code = 0;
        //    return r;
        //}



        public string jPush(string strPush) {
            try {
                JPushClient client = new JPushClient(Common.JPUSH_APP_KEY, Common.JPUSH_MASTER_SECRET);

                //  PushPayload payload = PushObject_All_All_Alert(strPush);


                //string[] tag = { "西安" };
                //PushPayload payload = PushObject_tag_Alert("aa", tag);

                string[] registrations = { "170976fa8ab5fc3cda2" };  //根据设备标识
                PushPayload payload = PushObject_registration_Alert("aa", registrations);

                var result = client.SendPush(payload);
                return "0";
            } catch (Exception e) {

                return "1" + e;
            }
        }

        #region 推送给全部（广播）
        public static PushPayload PushObject_All_All_Alert(string strPush) {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();   //推送平台
            pushPayload.audience = Audience.all();
            pushPayload.notification = new Notification().setAlert(strPush);
            return pushPayload;
        }
        #endregion

        #region 推送给多个标签（只要在任何一个标签范围内都满足）：在深圳、广州、或者北京
        public static PushPayload PushObject_tag_Alert(string strPush, string[] tag) {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();   //推送平台
            pushPayload.audience = Audience.s_tag(tag);
            pushPayload.notification = new Notification().setAlert(strPush);
            return pushPayload;
        }
        #endregion

        #region 推送给多个别名：
        public static PushPayload PushObject_alias_Alert(string strPush, string[] alias) {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();   //推送平台
            pushPayload.audience = Audience.s_tag(alias);
            pushPayload.notification = new Notification().setAlert(strPush);
            return pushPayload;
        }
        #endregion

        #region 给指定注册Id
        public static PushPayload PushObject_registration_Alert(string strPush, string[] registrationId) {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();   //推送平台
            pushPayload.audience = Audience.s_registrationId(registrationId);
            pushPayload.notification = new Notification().setAlert(strPush);
            return pushPayload;
        }
        #endregion

        #endregion

        #region 登陆成功之后进行极光推送
        public string JpushMsg(string RegisterId, string AliasId, string PushType) {


            return "1";
        }
        #endregion

        #region 添加群成员
        public RsModel<string> AddGroupMember(ViewGroupList model) {
            RsModel<string> r = new RsModel<string>();
            if (string.IsNullOrWhiteSpace(model.HXGroupId)) {
                r.code = 1;
                r.msg = "群Id不能为空";
                return r;
            }
            GroupinfoSqlMapDao gidao = new GroupinfoSqlMapDao();
            var gidata = gidao.GetGroupinfoList().FirstOrDefault(o => o.HXGroupId == model.HXGroupId);
            if (gidata == null) {
                r.code = 1;
                r.msg = "群Id不存在";
                return r;
            }
            try {
                Groupinfo gi = new Groupinfo();
                gi.HXGroupId = gidata.HXGroupId;
                gi.CreaterId = gidata.CreaterId;
                gi.CreateTime = gidata.CreateTime;
                gi.Descg = gidata.Descg;
                gi.GroupId = gidata.GroupId;
                gi.GroupNickName = gidata.GroupNickName;
                gi.IsFlag = gidata.IsFlag;
                gi.MaxGroupCount = gidata.MaxGroupCount;
                gi.UserCount = gidata.UserCount + model.groupMemberList.Count();  //以前的加上
                gidao.Updategroupinfo(gi);   //修改群基本信息

                userregisterSqlMapDao urdao = new userregisterSqlMapDao();
                var urdata = urdao.GetuserregisterList();

                GroupuserSqlMapDao gudao = new GroupuserSqlMapDao();

                List<string> grouplist = new List<string>();
                foreach (var item in model.groupMemberList) {
                    Groupuser gu = new Groupuser();
                    gu.GroupUserId = new aers_sys_seedSqlMapDao().GetMaxID("groupuser");
                    gu.GroupId = gi.GroupId;
                    gu.HXGroupId = gi.HXGroupId;
                    gu.IsFlag = 1;
                    gu.IsMaster = 0;
                    gu.JoinTime = DateTime.Now;
                    gu.NickName = urdata.FirstOrDefault(o => o.RegisterId == item.FriendId).NickName;
                    // gu.NickName = Common.Decode(urdata.FirstOrDefault(o => o.RegisterId == item.FriendId).NickName);
                    gu.RegisterId = item.FriendId;
                    gudao.Addgroupuser(gu);     //可优化
                    grouplist.Add(gu.HXGroupId);
                }
                string[] group = grouplist.ToArray();
                var response = Easemob.Restfull4Net.Client.DefaultSyncRequest.ChatGroupMemberAddBatch(gi.HXGroupId, group);
                r.code = 0;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "添加群成员失败";
                return r;
            }
        }
        #endregion

        #region 修改群昵称
        public RsModel<string> UpdateGroupNickName(ViewGroupList model) {
            RsModel<string> r = new RsModel<string>();
            if (string.IsNullOrWhiteSpace(model.HXGroupId)) {
                r.code = 1;
                r.msg = "群Id不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(model.HXNickName)) {
                r.code = 1;
                r.msg = "群昵称不能为空";
                return r;
            }
            try {
                GroupinfoSqlMapDao gidao = new GroupinfoSqlMapDao();
                var gidata = gidao.GetGroupinfoList().FirstOrDefault(o => o.HXGroupId == model.HXGroupId);
                if (gidata == null) {
                    r.code = 1;
                    r.msg = "群信息不存在";
                    return r;
                }

                Groupinfo gi = new Groupinfo();
                gi.CreaterId = gidata.CreaterId;
                gi.CreateTime = gidata.CreateTime;
                gi.Descg = gidata.Descg;
                gi.GroupId = gidata.GroupId;
                gi.GroupNickName = model.HXNickName;  //修改昵称
                gi.HXGroupId = gidata.HXGroupId;
                gi.IsFlag = gidata.IsFlag;
                gi.MaxGroupCount = gidata.MaxGroupCount;
                gi.UserCount = gidata.UserCount;
                gidao.Updategroupinfo(gi);


                var response = Easemob.Restfull4Net.Client.DefaultSyncRequest.ChatGroupUpdate(model.HXGroupId, new UpdateChatGroupRequest() {
                    groupname = gi.HXGroupId,
                    description = gi.Descg
                });

                r.code = 0;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "修改失败";
                return r;
            }

        }
        #endregion

        #region 退群
        public RsModel<string> QuitGroup(ViewGroupList model) {
            RsModel<string> r = new Services.RsModel<string>();
            if (string.IsNullOrWhiteSpace(model.RegisterId)) {
                r.code = 1;
                r.msg = "人员ID不能为空";
                return r;
            }
            if (string.IsNullOrWhiteSpace(model.HXGroupId)) {
                r.code = 1;
                r.msg = "群Id不能为空";
                return r;
            }
            GroupuserSqlMapDao gudao = new GroupuserSqlMapDao();
            var gudata = gudao.GetGroupuserList().FirstOrDefault(o => o.HXGroupId == model.HXGroupId);
            if (gudata == null) {
                r.code = 1;
                r.msg = "群Id不存在";
                return r;
            }
            gudao.Deletegroupuser(gudata.GroupUserId); //本地数据库删除群Id
            var response = Easemob.Restfull4Net.Client.DefaultSyncRequest.ChatGroupMemberDelete(model.HXGroupId, model.RegisterId);  //环信删除
            r.code = 0;
            return r;
        }
        #endregion

        #region emoji测试
        public RsModel<string> emojiTest(Qq qq) {
            RsModel<string> r = new Services.RsModel<string>();
            if (string.IsNullOrWhiteSpace(qq.RegisterId)) {
                r.code = 1;
                r.msg = "RegisterId不能为空";
                return r;
            }
            try {
                QqSqlMapDao qqdao = new QqSqlMapDao();
                //Qq q = new Qq();
                //q.Id = qq.Id;
                //q.NickName = qq.NickName;
                //qqdao.Updateqq(q);
                //return "0";
                qqdao.Updateqq(qq);
                r.code = 0;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = e.ToString();
                return r;
            }
        }

        public RsModel<Qq> GetEmoji(string RegisterId) {
            RsModel<Qq> r = new Services.RsModel<Qq>();
            if (string.IsNullOrWhiteSpace(RegisterId)) {
                r.code = 0;
                r.msg = "RegisterId不能为空";
                return r;

            }
            try {
                QqSqlMapDao qdao = new QqSqlMapDao();
                var d = qdao.GetQqList().FirstOrDefault(o => o.Id == RegisterId);
                r.code = 0;
                r.body = d;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = e.ToString();
                return r;
            }

        }
        #endregion

        #region 登陆表 推送消息
        public RsModel<string> LoginStatus(string DeviceRegId, string RegisterId, int loginType) {
            RsModel<string> r = new RsModel<string>();
            UserLoginStatusSqlMapDao usdao = new UserLoginStatusSqlMapDao();

            if (string.IsNullOrWhiteSpace(RegisterId)) {
                r.code = 1;
                r.msg = "RegisterId不能为空";
                return r;
            }
            var usdata = usdao.GetUserLoginStatusDetail(RegisterId);
            if (usdata == null) {
                UserLoginStatus us = new UserLoginStatus();
                us.RegisterId = RegisterId;
                us.LoginType = loginType;
                us.FirstLoginTime = DateTime.Now;
                us.LastLoginTime = DateTime.Now;
                usdao.AddUserLoginStatus(us);
                Common.PushMsg("欢迎使用格格", DeviceRegId, RegisterId);
                r.code = 0;
                return r;
            } else {
                UserLoginStatus us = new UserLoginStatus();
                us.RegisterId = RegisterId;
                us.LoginType = 1;
                us.FirstLoginTime = usdata.FirstLoginTime;
                us.LastLoginTime = DateTime.Now;
                usdao.UpdateUserLoginStatus(us);
                r.code = 0;
                return r;
            }
        }
        #endregion

        #region 院内账号登陆时在不良事件用户体系中进行验证  
        public RsModel<string> ValidateBlsjUser(string LoginName, string Password) {
            RsModel<string> r = new Services.RsModel<string>();
            aers_tbl_registereduserSqlMapDao rdao = new aers_tbl_registereduserSqlMapDao();  //先在不良事件用户库里验证用户
            var rdata = rdao.FindByLoginName(LoginName);
            string pwd = Common.UserMd5(Password);
            if (rdata == null) {
                r.code = 1;
                r.msg = "用户不存在";
                return r;
            } else {
                if (rdata.Password != pwd) {
                    r.code = 1;
                    r.msg = "密码错误";
                    return r;
                } else {
                    r.code = 0;
                    r.msg = rdata.ReguserId;
                    return r;
                }
            }
            //var rdata = rdao.FindByLoginName(RegusterId);
            //if (rdata == null)
            //{
            //    r.code = 1;
            //    r.msg = "该用户不存在";
            //    return r;
            //}
            //else
            //{
            //    if (rdata.Password != Password) //和不良事件库进行对比
            //    {
            //        r.code = 1;
            //        r.msg = "密码错误";
            //        return r;
            //    }
            //    else
            //    {
            //        r.code = 0;
            //        r.msg = rdata.ReguserId;   //把注册Id带回去
            //        return r;
            //    }
            //}
        }
        #endregion

        #region 学分系统在缓存库进行验证  
        public RsModel<string> ValidateXFUser(string RegusterId, string Password) {
            RsModel<string> r = new Services.RsModel<string>();
            r.code = 0;
            r.msg = "学分系统暂无数据";
            return r;
            //aers_tbl_registereduserSqlMapDao rdao = new aers_tbl_registereduserSqlMapDao();  //先在学分缓存库中进行验证
            //var rdata = rdao.FindByLoginName(RegusterId);
            //if (rdata == null)
            //{
            //    r.code = 1;
            //    r.msg = "该用户不存在";
            //    return r;
            //}
            //else
            //{
            //    if (rdata.Password != Password) //和不良事件库进行对比
            //    {
            //        r.code = 1;
            //        r.msg = "密码错误";
            //        return r;
            //    }
            //    else
            //    {
            //        r.code = 0;
            //        r.msg = rdata.ReguserId;   //把注册Id带回去
            //        return r;
            //    }
            //}
        }
        #endregion

        #region 考试系统在缓存库进行验证  
        public RsModel<string> ValidateKSUser(string RegusterId, string Password) {
            RsModel<string> r = new Services.RsModel<string>();
            r.code = 0;
            r.msg = "考试系统暂无数据";
            return r;

            //aers_tbl_registereduserSqlMapDao rdao = new aers_tbl_registereduserSqlMapDao();  //先在学分缓存库中进行验证
            //var rdata = rdao.FindByLoginName(RegusterId);
            //if (rdata == null)
            //{
            //    r.code = 1;
            //    r.msg = "该用户不存在";
            //    return r;
            //}
            //else
            //{
            //    if (rdata.Password != Password) //和不良事件库进行对比
            //    {
            //        r.code = 1;
            //        r.msg = "密码错误";
            //        return r;
            //    }
            //    else
            //    {
            //        r.code = 0;
            //        r.msg = rdata.ReguserId;   //把注册Id带回去
            //        return r;
            //    }
            //}
        }
        #endregion

        #region 考试系统在缓存库进行验证  
        public RsModel<string> ValidatePBUser(string RegusterId, string Password) {
            RsModel<string> r = new Services.RsModel<string>();
            r.code = 0;
            r.msg = "排班系统暂无数据";
            return r;

            //aers_tbl_registereduserSqlMapDao rdao = new aers_tbl_registereduserSqlMapDao();  //先在学分缓存库中进行验证
            //var rdata = rdao.FindByLoginName(RegusterId);
            //if (rdata == null)
            //{
            //    r.code = 1;
            //    r.msg = "该用户不存在";
            //    return r;
            //}
            //else
            //{
            //    if (rdata.Password != Password) //和不良事件库进行对比
            //    {
            //        r.code = 1;
            //        r.msg = "密码错误";
            //        return r;
            //    }
            //    else
            //    {
            //        r.code = 0;
            //        r.msg = rdata.ReguserId;   //把注册Id带回去
            //        return r;
            //    }
            //}
        }
        #endregion

        #region 内部调用，导入不良事件的用户资料
        private RsModel<string> ImportUserData(string RegusterId)  //不良事件后台加一个用户，则需要导一条数据
        {
            RsModel<string> r = new RsModel<string>();
            if (string.IsNullOrWhiteSpace(RegusterId)) {
                r.code = 1;
                r.msg = "RegusterId不能为空";
                return r;
            }

            try {
                UserService userservice = new UserService();

                aers_tbl_registereduserSqlMapDao aurdao = new aers_tbl_registereduserSqlMapDao();  //注册表里面取数据 登录名，密码，regusterId
                var urdata = aurdao.Find(RegusterId);// 名字起的很shit
                if (urdata != null) {
                    var registerdata = userservice.GetUserregiserId();
                    UserauthsSqlMapDao urd = new UserauthsSqlMapDao();    //授权表导入数据   登陆名，密码，以前的注册Id
                    Userauths ur = new Userauths();
                    ur.AuthsId = new aers_sys_seedSqlMapDao().GetMaxID("Userauths");
                    ur.LoginLastTime = Common.StrToDateTime();
                    ur.LoginNumber = urdata.LoginName;
                    ur.LoginType = 4; //不良事件4
                    ur.Password = urdata.Password;
                    ur.RegisterId = registerdata;
                    ur.Verified = 1;
                    ur.ReguserId = urdata.ReguserId;
                    urd.Adduserauths(ur);

                    aers_tbl_staffSqlMapDao sdao = new aers_tbl_staffSqlMapDao();
                    aers_tbl_staff s = new aers_tbl_staff();
                    var sdata = sdao.FindByRUid(urdata.ReguserId);
                    //根据注册ID取出以前用户表里面的数据  科室id,姓名，性别，角色


                    userregisterSqlMapDao urdao = new userregisterSqlMapDao();  //注册表导入姓名
                    userregister urr = new userregister();
                    urr.RegisterId = registerdata;  //注册Id
                    urr.Name = sdata.Name;  //添加姓名
                    urdao.Updateuserregister(urr);

                    userbasicinfoSqlMapDao ubdao = new userbasicinfoSqlMapDao();  //基本信息表导入性别
                    UserBasicInfo ub = new UserBasicInfo();
                    ub.RegisterId = registerdata;
                    if (sdata.Sex == "107") {
                        ub.Sex = "女";
                    } else if (sdata.Sex == "108") {
                        ub.Sex = "男";
                    } else {
                        ub.Sex = "";
                    }
                    ub.Birthday = Common.StrToDateTime();
                    ubdao.Updateuserbasicinfo(ub);

                    UserrelrecordSqlMapDao urrrdao = new UserrelrecordSqlMapDao();   //组织关系表导入科室Id
                    Userrelrecord urrr = new Userrelrecord();
                    urrr.RegisterId = registerdata;
                    urrr.DepartmentId = sdata.DepId;  //科室Id
                    urrr.Role = sdata.RoleState;   //还用以前的标记  角色

                    aers_tbl_hospdepSqlMapDao ad = new aers_tbl_hospdepSqlMapDao();
                    urrr.DepartmentName = ad.FindhospdepByDepId(sdata.DepId).HospdepName;  //科室姓名
                    var hospId = ad.FindhospdepByDepId(sdata.DepId).HospId;  //医院ID

                    //  aers_tbl_hospitalSqlMapDao DDD = new aers_tbl_hospitalSqlMapDao(); //shit
                    aers_tbl_events_ycSqlMapDao hdao = new aers_tbl_events_ycSqlMapDao();
                    var hdat = hdao.hospitalFindByHospId(hospId);
                    urrr.HospitalName = hdat.HospName;   //医院姓名
                    urrr.HospitalId = hospId; //医院Id

                    urrrdao.Updateuserrelrecord(urrr);

                    r.msg = registerdata;
                    r.code = 0;
                    return r;
                } else {
                    r.msg = "数据错误";
                    r.code = 1;
                    return r;
                }

            } catch (Exception e) {
                r.code = 1;
                r.msg = e.ToString();
                return r;
            }
        }
        #endregion

        #region 测试表情
        public RsModel<string> testEmoji(Qq model) {
            RsModel<string> r = new RsModel<string>();
            try {
                // var emstr = Common.Encode(model .NickName);
                var emstr = model.NickName;
                QqSqlMapDao qqdao = new QqSqlMapDao();
                Qq qq = new Qq();
                qq.Id = "0000000119";
                qq.NickName = emstr;
                qqdao.Updateqq(qq);
                r.code = 0;
                return r;

            } catch (Exception e) {
                r.code = 1;
                r.msg = e.ToString();
                return r;
            }
        }

        public RsModel<Qq> GetEmojiTest() {
            RsModel<Qq> r = new Services.RsModel<Qq>();
            try {
                Qq q = new Qq();
                QqSqlMapDao qqdao = new QqSqlMapDao();
                var d = qqdao.GetQqList().FirstOrDefault(o => o.Id == "0000000119");
                q.Id = d.Id;
                q.NickName = d.NickName;
                // q.NickName = Common.Decode(d.NickName);
                r.code = 0;
                r.body = q;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = e.ToString();
                return r;
            }
        }
        #endregion

        #region 后台服务

        /// <summary>
        /// 管理员权限
        /// </summary>
        private static string ADMIN_PERMISSION = "0000000001";

        /// <summary>
        /// 护士信息维护权限
        /// </summary>
        private static string NURSE_PERMISSION = "0000000002";

        /// <summary>
        /// 医院信息维护权限
        /// </summary>
        private static string HOSPITAL_PERMISSION = "0000000003";

        /// <summary>
        /// 科室信息维护权限
        /// </summary>
        private static string DEPARTMENT_PERMISSION = "0000000004";

        /// <summary>
        /// Banner管理权限
        /// </summary>
        private static string BANNER_PERMISSION = "0000000005";

        /// <summary>
        /// 公告管理权限
        /// </summary>
        private static string NOTICE_PERMISSION = "0000000006";

        /// <summary>
        /// 两证审核权限
        /// </summary>
        private static string CERTIFICATE_VERIFY_PERMISSION = "0000000009";

        #region 医院管理
        #region 获取所有医院
        public RsList<Hospital> GetHospitalAll(string operatorId, int pageSize, int pageNumber) {
            RsList<Hospital> r = new RsList<Hospital>();
            if (!CheckNursePermission(operatorId, HOSPITAL_PERMISSION)) {
                r.code = 1;
                r.msg = "没有权限";
                return r;
            }
            try {
                HospitalSqlMapDao hdao = new HospitalSqlMapDao();
                var data = hdao.GethospitalList().Where(o => o.IsDelete == 0).ToList();  //isdelete为1时是逻辑删除
                r.code = 0;
                r.msg = data.Count.ToString();
                r.body = data.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "获取医院失败";
                return r;
            }
        }
        #endregion

        #region 获取所有医院名字和Id
        public RsList<Hospital> GetHospitalNameAll() {
            RsList<Hospital> r = new Services.RsList<Hospital>();
            try {
                HospitalSqlMapDao hdao = new HospitalSqlMapDao();
                var hdata = hdao.GethospitalList().Where(o => o.IsDelete == 0).ToList();  //新建类返回空？？？
                r.code = 0;
                r.msg = hdata.Count().ToString();
                r.body = hdata;
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "获取医院失败";
                return r;

            }
        }
        #endregion

        #region 添加医院
        public RsModel<string> AddHospital(Hospital model) {
            RsModel<string> r = new RsModel<string>();
            if (!CheckNursePermission(model.OperatorId, HOSPITAL_PERMISSION)) {
                r.code = 1;
                r.msg = "没有权限";
                return r;
            }
            try {
                HospitalSqlMapDao hdao = new HospitalSqlMapDao();
                Hospital h = new Hospital();
                model.HospitalId = new aers_sys_seedSqlMapDao().GetMaxID("hospital");  //注册表
                h.OperatorTime = DateTime.Now;
                hdao.Addhospital(h);
                r.code = 0;
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "添加医院失败";
                return r;
            }
        }
        #endregion

        #region 医院修改
        public RsModel<string> UpdateHospital(Hospital model) {
            RsModel<string> r = new RsModel<string>();
            if (!CheckNursePermission(model.OperatorId, HOSPITAL_PERMISSION)) {
                r.code = 1;
                r.msg = "没有权限";
                return r;
            }
            try {
                HospitalSqlMapDao hdao = new HospitalSqlMapDao();
                model.OperatorTime = DateTime.Now;
                hdao.Updatehospital(model);
                r.code = 0;
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "医院修改失败";
                return r;
            }
        }
        #endregion

        #region 医院逻辑删除
        public RsModel<string> DeleteHospital(string operatorId, string HospitalId) {
            RsModel<string> r = new RsModel<string>();
            if (!CheckNursePermission(operatorId, HOSPITAL_PERMISSION)) {
                r.code = 1;
                r.msg = "没有权限";
                return r;
            }
            if (string.IsNullOrWhiteSpace(HospitalId)) {
                r.code = 1;
                r.msg = "医院ID不能为空";
                return r;
            }
            HospitalSqlMapDao hdao = new HospitalSqlMapDao();
            var hdata = hdao.GethospitalDetail(HospitalId);
            if (hdata == null) {
                r.code = 1;
                r.msg = "医院ID不存在";
                return r;
            } else {
                try {
                    hdao.UpdateDelhospital(HospitalId);  //逻辑删除
                    r.code = 0;
                    return r;
                } catch (Exception) {
                    r.code = 1;
                    r.msg = "删除失败";
                    return r;
                }
            }
        }
        #endregion

        #endregion

        #region 科室管理
        #region 获取所有科室
        public RsList<Department> GetDepartmentAll(string operatorId, int pageSize, int pageNumber) {
            RsList<Department> r = new RsList<Department>();
            if (!CheckNursePermission(operatorId, DEPARTMENT_PERMISSION)) {
                r.code = 1;
                r.msg = "没有权限";
                return r;
            }

            try {
                DepartmentSqlMapDao dDao = new DepartmentSqlMapDao();
                List<Department> ds = new List<Department>();
                if (operatorId == "0000000001") {
                    ds = dDao.GetdepartmentList().ToList();
                } else {
                    AdmdepartmentSqlMapDao adDao = new AdmdepartmentSqlMapDao();
                    List<Admdepartment> ads = adDao.GetAdmDepartmentListByAdminId(operatorId).ToList();

                    foreach (var ad in ads) {
                        Department d = dDao.GetdepartmentDetail(ad.DepartmentId);
                        if (d != null) {
                            HospitalSqlMapDao hDao = new HospitalSqlMapDao();
                            Hospital h = hDao.GethospitalDetail(d.HospitalId);
                            if (h != null) {
                                d.HospitalName = h.Name;
                            }
                            ds.Add(d);
                        }
                    }
                }
                r.body = ds;
                r.code = 0;
                r.msg = ds.Count.ToString();
                r.body = ds.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "获取科室失败";
                return r;
            }
        }
        #endregion

        #region 根据医院Id查科室
        public RsList<Department> GetDepartmentByHospitalId(string HospitalId) {

            RsList<Department> r = new RsList<Department>();
            if (string.IsNullOrWhiteSpace(HospitalId)) {
                r.code = 1;
                r.msg = "医院Id为空";
                r.body = null;
                return r;
            }
            try {
                DepartmentSqlMapDao ddao = new DepartmentSqlMapDao();
                var ddatalist = ddao.GetdepartmentList().ToList();
                if (!string.IsNullOrWhiteSpace(HospitalId)) {
                    ddatalist = ddatalist.Where(o => o.HospitalId == HospitalId).ToList();
                }


                HospitalSqlMapDao hdao = new HospitalSqlMapDao();
                var hdata = hdao.GethospitalList();
                foreach (var item in ddatalist) {
                    item.HospitalName = hdata.FirstOrDefault(o => o.HospitalId == item.HospitalId).Name;
                }
                r.code = 0;
                r.msg = ddatalist.Count.ToString();
                r.body = ddatalist;
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "获取科室失败";
                return r;
            }
        }
        #endregion

        #region 添加科室
        public RsModel<string> AddDepartment(Department model) {
            RsModel<string> r = new RsModel<string>();
            if (!CheckNursePermission(model.OperatorId, DEPARTMENT_PERMISSION)) {
                r.code = 1;
                r.msg = "没有权限";
                return r;
            }
            try {
                DepartmentSqlMapDao ddao = new DepartmentSqlMapDao();
                model.DepartmentId = new aers_sys_seedSqlMapDao().GetMaxID("department");
                ddao.Updatedepartment(model);
                r.code = 0;
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "添加科室失败";
                return r;

            }
        }
        #endregion

        #region 科室修改
        public RsModel<string> UpdateDepartment(Department model) {
            RsModel<string> r = new RsModel<string>();
            if (!CheckNursePermission(model.OperatorId, DEPARTMENT_PERMISSION)) {
                r.code = 1;
                r.msg = "没有权限";
                return r;
            }
            if (string.IsNullOrWhiteSpace(model.DepartmentId)) {
                r.code = 1;
                r.msg = "科室Id不能为空";
                return r;
            }
            try {
                DepartmentSqlMapDao ddao = new DepartmentSqlMapDao();
                ddao.Updatedepartment(model);
                r.code = 0;
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "科室修改失败";
                return r;

            }
        }

        #endregion

        #region 删除科室
        public RsModel<string> DeleteDepartment(string operatorId, string DepartmentId) {
            RsModel<string> r = new RsModel<string>();
            if (!CheckNursePermission(operatorId, DEPARTMENT_PERMISSION)) {
                r.code = 1;
                r.msg = "没有权限";
                return r;
            }
            if (string.IsNullOrWhiteSpace(DepartmentId)) {
                r.code = 1;
                r.msg = "科室Id不能为空";
                return r;
            }
            try {
                DepartmentSqlMapDao ddao = new DepartmentSqlMapDao();
                ddao.Deletedepartment(DepartmentId);
                r.code = 0;
                return r;

            } catch (Exception) {
                r.code = 1;
                r.msg = "删除失败";
                return r;
            }
        }
        #endregion

        #endregion

        #region 护士管理

        #region 添加护士
        public RsModel<string> AddNurse(NurseBaseInfo model) {
            RsModel<string> r = new RsModel<string>();
            try {
                //userregisterSqlMapDao urdao = new userregisterSqlMapDao();  //注册表
                //userregister ur = new userregister();
                //ur.RegisterId = new aers_sys_seedSqlMapDao().GetMaxID("userregister");
                //ur.Phone = model.Phone;
                //ur.Password = Common.UserMd5("123456"); //先给默认密码，后续从配置文件里取
                //ur.Name = model.Name;
                //urdao.Adduserregister(ur);

                //userbasicinfoSqlMapDao ubdao = new userbasicinfoSqlMapDao(); //基本信息表
                //UserBasicInfo ub = new UserBasicInfo();
                //ub.Sex = model.Sex;
                //ub.RegisterId = ur.RegisterId;
                //ubdao.Adduserbasicinfo(ub);

                //UserrelrecordSqlMapDao urddao = new UserrelrecordSqlMapDao();  //组织关系表
                //Userrelrecord urd = new Userrelrecord();
                //urd.RegisterId = ur.RegisterId;
                //urd.DepartmentId = model.DepartmentId;
                //urd.Role = model.Role;
                //urddao.Adduserrelrecord(urd);

                //UserauthsSqlMapDao uadao = new UserauthsSqlMapDao();  //授权表

                r.code = 0;
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "添加失败";
                return r;
            }
        }
        #endregion

        #region 护士逻辑删除

        #endregion

        #endregion

        #region 管理员管理

        #region 获取管理员

        public RsList<Administrator> GetAdministrator(string operatorId, int pageSize, int pageNumber) {
            RsList<Administrator> r = new Services.RsList<Administrator>();
            if (!CheckNursePermission(operatorId, ADMIN_PERMISSION)) {
                r.code = 1;
                r.msg = "没有权限";
                return r;
            }
            try {

                AdministratorSqlMapDao adao = new AdministratorSqlMapDao();
                var adatalist = adao.GetAdministratorList().Where(o => o.OperatorId == operatorId).ToList();

                List<Administrator> adlist = new List<Administrator>();

                AdmdepartmentSqlMapDao addao = new AdmdepartmentSqlMapDao();
                var addata = addao.GetAdmdepartmentList();

                AdmpermissionSqlMapDao apdao = new AdmpermissionSqlMapDao();
                var apdata = apdao.GetAdmpermissionList();

                DepartmentSqlMapDao ddao = new DepartmentSqlMapDao();
                var ddata = ddao.GetdepartmentList();

                HospitalSqlMapDao hdao = new HospitalSqlMapDao();
                var hdata = hdao.GethospitalList();
                foreach (var item in adatalist) {
                    Hospital hospital = hdata.FirstOrDefault(o => o.HospitalId == item.HospitalId);
                    if (hospital != null) {
                        item.HospitalName = hospital.Name;
                    }
                    var ads = addata.Where(o => o.AdmId == item.AdmId);
                    if (ads != null) {
                        item.Admdepartmentlist = ads.ToList();
                    }
                    var aps = apdata.Where(o => o.AdmId == item.AdmId);
                    if (aps != null) {
                        item.Admpermissionlist = aps.ToList();
                    }
                    foreach (var d in item.Admdepartmentlist) {
                        var dpmt = ddata.FirstOrDefault(o => o.DepartmentId == d.DepartmentId);
                        if (dpmt != null) {
                            d.DepartmentName = dpmt.Name;
                        }
                    }
                    //foreach (var p in item.Admpermissionlist)
                    //{
                    //    p.PermissionName=
                    //}
                    adlist.Add(item);
                }
                r.code = 0;
                r.msg = adlist.Count.ToString();
                r.body = adlist.Where(o => o.Status != 1).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "获取管理员失败" + e.ToString();
                return r;
            }
        }
        #endregion

        #region 创建一个管理员
        /// <summary>
        /// 创建一个管理员
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RsModel<string> AddAdministrator(Administrator model) {
            RsModel<string> result = new RsModel<string>();
            if (!CheckNursePermission(model.OperatorId, ADMIN_PERMISSION)) {
                result.code = 1;
                result.msg = "没有权限";
                return result;
            }
            try {
                // 创建管理员
                AdministratorSqlMapDao adao = new AdministratorSqlMapDao();
                if (adao.GetAdministratorDetailByUserId(model.UserId) != null) {
                    result.code = 1;
                    result.msg = "用户名已存在";
                    return result;
                }
                model.AdmId = new aers_sys_seedSqlMapDao().GetMaxID("administrator");
                model.OperatorTime = DateTime.Now;
                model.Password = Common.UserMd5(model.Password);
                model.Status = 0;
                adao.AddAdministrator(model);

                // 添加管理员权限
                AdmpermissionSqlMapDao apdao = new AdmpermissionSqlMapDao();
                foreach (Admpermission ap in model.Admpermissionlist) {
                    ap.Id = new aers_sys_seedSqlMapDao().GetMaxID("admpermission");
                    ap.AdmId = model.AdmId;
                    apdao.AddAdmpermission(ap);
                }

                // 添加管理员科室
                AdmdepartmentSqlMapDao addao = new AdmdepartmentSqlMapDao();
                foreach (Admdepartment ad in model.Admdepartmentlist) {
                    ad.Id = new aers_sys_seedSqlMapDao().GetMaxID("admdepartment");
                    ad.AdmId = model.AdmId;
                    addao.AddAdmdepartment(ad);
                }
                result.code = 0;
                return result;
            } catch (Exception e) {
                result.code = 1;
                result.msg = "添加管理员失败";
                return result;
            }
        }
        #endregion

        #region 修改管理员
        public RsModel<string> UpdateAdministrator(Administrator model) {
            RsModel<string> r = new RsModel<string>();
            if (!CheckNursePermission(model.OperatorId, ADMIN_PERMISSION)) {
                r.code = 1;
                r.msg = "没有权限";
                return r;
            }
            if (model.AdmId == "0000000001") {
                r.code = 1;
                r.msg = "没有权限";
                return r;
            }
            try {
                AdministratorSqlMapDao adao = new AdministratorSqlMapDao();

                model.OperatorTime = DateTime.Now;
                model.Password = Common.UserMd5(model.Password);
                model.Status = 0;
                adao.UpdateAdministrator(model);

                AdmdepartmentSqlMapDao addao = new AdmdepartmentSqlMapDao();
                addao.DeleteAdmdepartmentByAdminId(model.AdmId);

                foreach (var item in model.Admdepartmentlist) {
                    Admdepartment ad = new Admdepartment();
                    ad.Id = new aers_sys_seedSqlMapDao().GetMaxID("admdepartment");
                    ad.AdmId = item.AdmId;
                    ad.DepartmentId = item.DepartmentId;
                    addao.AddAdmdepartment(ad);
                }

                AdmpermissionSqlMapDao apDao = new AdmpermissionSqlMapDao();
                apDao.DeleteAdmpermissionByAdminId(model.AdmId);

                foreach (var item in model.Admpermissionlist) {
                    Admpermission ap = new Admpermission();
                    ap.AdmId = item.AdmId;
                    ap.PermissionId = item.PermissionId;
                    ap.Id = item.Id;
                    apDao.AddAdmpermission(ap);
                }
                return r;

            } catch (Exception) {
                r.code = 1;
                r.msg = "修改失败";
                return r;
            }
        }

        #endregion

        #region 管理员逻辑删除
        public RsModel<string> DeleteAdministrator(string operatorId, string AdmId) {
            RsModel<string> r = new RsModel<string>();
            if (!CheckNursePermission(operatorId, ADMIN_PERMISSION)) {
                r.code = 1;
                r.msg = "没有权限";
                return r;
            }
            if (AdmId == "0000000001") {
                r.code = 1;
                r.msg = "没有权限";
                return r;
            }
            if (string.IsNullOrWhiteSpace(AdmId)) {
                r.code = 1;
                r.msg = "管理员Id不能为空";
                return r;
            }
            try {
                AdministratorSqlMapDao adao = new AdministratorSqlMapDao();
                var admin = adao.GetAdministratorDetail(AdmId);
                admin.Status = 1;
                adao.UpdateAdministrator(admin);
                r.code = 0;
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "删除失败";
                return r;
            }
        }

        #endregion

        #endregion

        #region banner管理

        #region 获取管理员Banner
        public RsList<Banner> GetAdminBanner(string operatorId) {
            RsList<Banner> result = new Services.RsList<Banner>();
            if (!CheckNursePermission(operatorId, BANNER_PERMISSION)) {
                result.code = 1;
                result.msg = "没有权限";
                return result;
            }

            BannerSqlMapDao dao = new BannerSqlMapDao();
            result.body = dao.GetBannerList();
            result.code = 0;

            return result;
        }

        #endregion

        #region 添加banner
        public RsModel<string> AddBanner(Banner model) {
            RsModel<string> r = new RsModel<string>();
            if (!CheckNursePermission(model.operatorId, BANNER_PERMISSION)) {
                r.code = 1;
                r.msg = "没有权限";
                return r;
            }
            try {
                BannerSqlMapDao bdao = new BannerSqlMapDao();
                Banner b = new Banner();
                b.BannerId = new aers_sys_seedSqlMapDao().GetMaxID("banner");
                b.Agency = model.Agency; //机构
                b.BannerTime = DateTime.Now;  //banner发布日期
                b.BannerToUrl = model.BannerToUrl; //图片链接地址
                b.BannerUrl = model.BannerUrl; //图片名字
                b.DepartmentId = model.DepartmentId;
                b.DisplayOrder = model.DisplayOrder;
                b.HospitalId = model.HospitalId;
                b.IsDelete = 0;// 是否删除，逻辑删除   1删除 0保留
                b.IsFlag = model.IsFlag; //是否启用
                b.Issuer = model.Issuer;
                b.Title = model.Title;
                b.Type = model.Type;
                bdao.Addbanner(b);
                r.code = 0;
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "添加失败";
                return r;

            }
        }
        #endregion

        #region 修改banner
        public RsModel<string> UpdateBanner(Banner model) {
            RsModel<string> r = new RsModel<string>();
            if (!CheckNursePermission(model.operatorId, BANNER_PERMISSION)) {
                r.code = 1;
                r.msg = "没有权限";
                return r;
            }
            try {
                BannerSqlMapDao bdao = new BannerSqlMapDao();
                // 原始数据
                var old = bdao.GetBannerDetail(model.BannerId);

                // 新排序数据
                var exist = bdao.GetBannerByDisplayOrder(model.DisplayOrder);

                if (old != exist) {
                    var temp = old.DisplayOrder;
                    old.DisplayOrder = exist.DisplayOrder;
                    exist.DisplayOrder = temp;
                    bdao.Updatebanner(old);
                    bdao.Updatebanner(exist);
                } else {
                    bdao.Updatebanner(old);
                }
                r.code = 0;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "修改失败" + e;
                return r;

            }
        }
        #endregion

        #region banner逻辑删除
        public RsModel<string> DeleteBanner(string operatorId, string BannerId) {
            RsModel<string> r = new RsModel<string>();
            if (!CheckNursePermission(operatorId, BANNER_PERMISSION)) {
                r.code = 1;
                r.msg = "没有权限";
                return r;
            }
            if (string.IsNullOrWhiteSpace(BannerId)) {
                r.code = 1;
                r.msg = "bannerId不能为空";
                return r;
            }
            try {
                BannerSqlMapDao bdao = new BannerSqlMapDao();
                bdao.UpdateDelbanner(BannerId);
                r.code = 0;
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "删除失败";
                return r;
            }
        }

        #endregion

        #endregion

        #region 公告管理

        #region 获取公告
        //后台传每页多少条，页数
        public RsList<Notice> GetNoticeAll(string operatorId, int pageSize, int pageNumber) {
            RsList<Notice> r = new Services.RsList<Notice>();
            RsList<NoticeDepartment> result = new RsList<NoticeDepartment>();
            if (!CheckNursePermission(operatorId, NOTICE_PERMISSION)) {
                r.code = 1;
                r.msg = "没有权限";
                return r;
            }

            try {
                // 先获取管理员
                AdministratorSqlMapDao aDao = new AdministratorSqlMapDao();
                var admin = aDao.GetAdministratorDetail(operatorId);

                // 根据管理员的类型进行 
                NoticeSqlMapDao ndao = new NoticeSqlMapDao();
                var datalist = new List<Notice>();
                if (admin.Role != 2) {
                    datalist = ndao.GetNoticeList().OrderByDescending(o => o.NoticeTime).Where(o => o.IsDelete == 0 && o.Type == 0).ToList();
                } else {
                    datalist = ndao.GetNoticeList().OrderByDescending(o => o.NoticeTime).Where(o => o.IsDelete == 0 && o.Type != 0).ToList();
                }

                r.code = 0;
                r.msg = datalist.Count.ToString();
                r.body = datalist.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = e.ToString();
                r.body = null;
            }
            return r;
        }
        #endregion

        #region 获取公告科室
        public RsList<NoticeDepartment> GetNoticeDepartmentByNoticeId(string operatorId, string noticeId) {
            RsList<NoticeDepartment> result = new RsList<NoticeDepartment>();
            if (!CheckNursePermission(operatorId, NOTICE_PERMISSION)) {
                result.code = 1;
                result.msg = "没有权限";
                return result;
            }
            try {
                NoticeDepartmentDao dao = new NoticeDepartmentDao();
                result.body = dao.GetNoticeDepartmentByNoticeId(noticeId);
                result.code = 0;
            } catch (Exception e) {
                result.code = 1;
                result.msg = "读取失败";
                throw;
            }

            return result;
        }

        #endregion

        #region 添加公告

        public RsModel<string> AddNotice(Notice model) {
            RsModel<string> r = new RsModel<string>();
            try {
                NoticeSqlMapDao ndao = new NoticeSqlMapDao();

                model.NoticeId = new aers_sys_seedSqlMapDao().GetMaxID("notice");
                model.IsFlag = 1;  //可用
                model.NoticeTime = DateTime.Now;    //发布时间/定时发布
                model.OperatorTime = DateTime.Now;


                ndao.Addnotice(model);
                //// 平台公告
                //if (model.Type == 0) {
                //    ndao.Addnotice(model);
                //}
                //// 院内公告
                //else if (model.Type == 1) {
                //    if (model.departmentlist != null) {
                //        // 将所有数据插入公告科室对应表
                //        foreach (var dep in model.departmentlist) {
                //            NoticeDepartmentDao ndDao = new NoticeDepartmentDao();
                //            NoticeDepartment nd = new NoticeDepartment();
                //            nd.id = new aers_sys_seedSqlMapDao().GetMaxID("noticedepartment");
                //            nd.noticeId = model.NoticeId;
                //            nd.departmentId = dep.DepartmentId;
                //            ndDao.AddNoticeDepartment(nd);
                //        }
                //    } else {
                //        r.code = 1;
                //        r.msg = "科室列表为空";
                //        return r;
                //    }
                //}

                r.code = 0;
                return r;
            } catch (Exception e) {
                r.code = 1;
                r.msg = "添加失败" + e;
                return r;
            }
        }
        #endregion

        #region 修改公告
        public RsModel<string> UpdateNotice(Notice model) {
            RsModel<string> r = new RsModel<string>();
            try {
                Notes n = new Notes();
                NoticeSqlMapDao ndao = new NoticeSqlMapDao();
                ndao.Updatenotice(model);
                r.code = 0;
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "更改失败";
                return r;
            }

        }
        #endregion

        #region 公告逻辑删除
        public RsModel<string> UpdateDelNotice(string NoticeId) {
            RsModel<string> r = new RsModel<string>();
            if (string.IsNullOrWhiteSpace(NoticeId)) {
                r.code = 1;
                r.msg = "NoticeId不能为空";
                return r;
            }
            try {
                NoticeSqlMapDao ndao = new NoticeSqlMapDao();
                ndao.UpdateDelNotice(NoticeId);
                r.code = 0;
                return r;
            } catch (Exception) {
                r.code = 1;
                r.msg = "删除失败";
                return r;
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// 获取所有Token
        /// </summary>
        /// <param name="RegisterId"></param>
        /// <returns></returns>
        public RsList<Token> GetAllToken(string RegisterId) {
            Console.WriteLine(RegisterId);
            RsList<Token> result = new RsList<Token>();
            if (string.IsNullOrWhiteSpace(RegisterId)) {
                result.code = 1;
                result.msg = "注册Id不能为空";
                return result;
            }

            TokenSqlMapDao dao = new TokenSqlMapDao();
            var body = dao.GetTokenList();
            result.code = 0;
            result.msg = "";
            result.body = body;

            return result;
        }

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        public RsList<Permission> GetPermissionList() {
            RsList<Permission> result = new RsList<Permission>();
            try {
                PermissionDao dao = new PermissionDao();
                result.body = dao.GetPermissionList().ToList();
                result.code = 0;
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
                result.code = 1;
                result.msg = "获取权限列表失败" + e.ToString();
                result.body = null;
            }
            return result;
        }

        /// <summary>
        /// 管理后台登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RsModel<Administrator> ManagementLogin(Administrator model) {
            RsModel<Administrator> result = new RsModel<Administrator>();
            AdministratorSqlMapDao adminDao = new AdministratorSqlMapDao();

            try {
                Administrator admin = adminDao.GetAdministratorDetailByUserId(model.UserId);
                if (admin == null) {
                    result.code = 1;
                    result.msg = "用户名或密码错误";
                }
                if (admin.Password != Common.UserMd5(model.Password) || (admin.Status == 1)) {
                    result.code = 1;
                    result.msg = "用户名或密码错误";
                } else {
                    result.code = 0;
                    result.body = admin;

                    // 获取管理员的权限
                    AdmpermissionSqlMapDao perDao = new AdmpermissionSqlMapDao();
                    admin.Admpermissionlist = perDao.GetPermissionByAdminId(admin.AdmId).ToList();
                    foreach (Admpermission a in admin.Admpermissionlist) {
                        PermissionDao pDao = new PermissionDao();
                        a.PermissionName = pDao.GetPermissionById(a.PermissionId).name;
                    }

                    // 获取管理员管理的科室
                    AdmdepartmentSqlMapDao depDao = new AdmdepartmentSqlMapDao();
                    admin.Admdepartmentlist = depDao.GetAdmDepartmentListByAdminId(admin.AdmId).ToList();
                    if (admin.Admdepartmentlist != null) {
                        DepartmentSqlMapDao dDao = new DepartmentSqlMapDao();
                        foreach (var d in admin.Admdepartmentlist) {
                            var info = dDao.GetdepartmentDetail(d.DepartmentId);
                            if (info != null) {
                                d.DepartmentName = info.Name;
                            }
                        }
                    }
                }

            } catch (Exception) {

                result.code = 1;
                result.msg = "获取管理员失败";
            }
            return result;
        }

        /// <summary>
        /// 获取管理员权限列表
        /// </summary>
        /// <returns></returns>
        public RsList<Admpermission> GetAdminPermission(string adminId) {
            RsList<Admpermission> result = new RsList<Admpermission>();

            try {
                AdmpermissionSqlMapDao dao = new AdmpermissionSqlMapDao();
                result.body = dao.GetPermissionByAdminId(adminId).ToList();
                result.code = 0;

            } catch (Exception) {
                result.code = 1;
                result.msg = "获取管理员权限失败";
            }

            return result;
        }

        /// <summary>
        /// 获取管理员管理科室
        /// </summary>
        /// <param name="adminId"></param>
        /// <returns></returns>
        public RsList<Admdepartment> GetAdminDepartment(string adminId) {
            RsList<Admdepartment> result = new RsList<Admdepartment>();

            try {
                AdmdepartmentSqlMapDao dao = new AdmdepartmentSqlMapDao();
                result.body = dao.GetAdmDepartmentListByAdminId(adminId).ToList();
                result.code = 0;

            } catch (Exception) {
                result.code = 1;
                result.msg = "获取管理员管理科室失败";
            }

            return result;
        }

        /// <summary>
        /// 重置管理员密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RsModel<Administrator> ResetAdminPassword(Administrator model) {
            RsModel<Administrator> result = new RsModel<Administrator>();
            if (string.IsNullOrWhiteSpace(model.AdmId)) {
                result.code = 1;
                result.msg = "管理员Id不能为空";
            }
            try {
                AdministratorSqlMapDao adminDao = new AdministratorSqlMapDao();
                Administrator admin = adminDao.GetAdministratorDetail(model.AdmId);

                string password = "";
                Random random = new Random();
                for (int i = 0; i < 6; i++) {
                    password += random.Next(0, 9);
                }
                admin.Password = Common.UserMd5(password);
                admin.Status = 2;
                adminDao.UpdateAdministrator(admin);
                admin.Password = password;
                result.code = 0;
                result.body = admin;
            } catch (Exception e) {
                result.code = 1;
                result.msg = "重置密码失败" + e.ToString();
            }

            return result;
        }


        /// <summary>
        /// 获取所有护士信息
        /// </summary>
        /// <returns></returns>
        public RsList<Nurse> GetAllNurseInfo(string adminId, string hospitalId) {
            RsList<Nurse> result = new RsList<Nurse>();

            if (string.IsNullOrWhiteSpace(adminId)) {
                result.code = 1;
                result.msg = "管理员Id不能为空";
                return result;
            }

            try {
                if (!CheckNursePermission(adminId, NURSE_PERMISSION)) {
                    result.code = 1;
                    result.msg = "该用户不具备护士管理权限";
                    return result;
                }

                List<Nurse> nurses = new List<Nurse>();

                UserrelrecordSqlMapDao recordDao = new UserrelrecordSqlMapDao();
                IList<Userrelrecord> userRecords = new List<Userrelrecord>();
                // 如果医院Id是空，则查询所有用户
                if (string.IsNullOrWhiteSpace(hospitalId)) {
                    userRecords = recordDao.GetUserrelrecordList();
                } else if (!string.IsNullOrWhiteSpace(hospitalId)) {
                    userRecords = recordDao.GetUserrelrecordList().Where(o => hospitalId == o.HospitalId).ToList();
                }

                userregisterSqlMapDao urDao = new userregisterSqlMapDao();
                foreach (var userRecord in userRecords) {
                    var nurse = new Nurse();
                    nurse.registerId = userRecord.RegisterId;
                    // 获取基本信息
                    var userRegister = urDao.GetuserregisterDetail(userRecord.RegisterId);
                    nurse.cellphone = userRegister.Phone;
                    nurse.name = userRegister.Name;
                    // 获取绑定信息
                    nurse.bindInfo = GetUserBindInfo(userRecord.RegisterId).body;
                    nurses.Add(nurse);
                    // 获取性别
                    var ub = new userbasicinfoSqlMapDao().GetuserbasicinfoDetail(userRecord.RegisterId);
                    nurse.sex = ub.Sex;
                    // 获取医院信息
                    UserrelrecordSqlMapDao urrDao = new UserrelrecordSqlMapDao();
                    var urr = urrDao.GetuserrelrecordDetail(userRecord.RegisterId);
                    nurse.hospitalId = urr.HospitalId;
                    nurse.hospitalName = urr.HospitalName;
                    nurse.departmentId = urr.DepartmentId;
                    nurse.departmentName = urr.DepartmentName;
                }

                result.body = nurses;
                result.code = 0;

            } catch (Exception e) {
                result.code = 1;
                result.msg = "获取用户列表失败";
            }

            return result;
        }

        /// <summary>
        /// 判断该管理员是否有某项权限
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="permission"></param>
        /// <returns></returns>
        private bool CheckNursePermission(string adminId, string permission) {
            var has = false;
            // 首先判断该用户是否有护士信息管理权限 
            AdmpermissionSqlMapDao apDao = new AdmpermissionSqlMapDao();
            var aps = apDao.GetPermissionByAdminId(adminId).ToList();
            foreach (var ap in aps) {
                if (permission == ap.PermissionId) {
                    has = true;
                    return has;
                }
            }
            return has;
        }

        /// <summary>
        /// 添加一个护士
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RsModel<string> AddNurse(Nurse model) {
            RsModel<string> result = new RsModel<string>();
            if (!CheckNursePermission(model.adminId, NURSE_PERMISSION)) {
                result.code = 1;
                result.msg = "该管理员不具备该权限";
                return result;
            }

            if (string.IsNullOrWhiteSpace(model.cellphone)) {
                result.code = 1;
                result.msg = "手机号为空";
                return result;
            }

            try {
                userregisterSqlMapDao uDao = new userregisterSqlMapDao();
                var user = uDao.GetuserregisterDetailByPhone(model.cellphone);
                if (user != null) {
                    result.code = 1;
                    result.msg = "手机号已存在";
                    return result;
                }
                string registerId = Sign(model.cellphone, "PC后台").body.RegisterId;

                // 更新护士信息
                model.registerId = registerId;
                UpdateNurseInfo(model);

                result.code = 0;
                result.msg = "添加护士成功";

            } catch (Exception e) {
                result.code = 1;
                result.msg = "添加护士失败";
                throw;
            }

            return result;
        }

        /// <summary>
        /// 更新护士信息
        /// </summary>
        /// <param name="nurse"></param>
        private void UpdateNurseInfo(Nurse model) {
            var registerId = model.registerId;
            // 设置手机号姓名
            userregisterSqlMapDao registerDao = new userregisterSqlMapDao();
            var register = registerDao.GetuserregisterDetail(registerId);
            register.Password = model.password;
            register.Name = model.name;
            registerDao.Updateuserregister(register);

            // 设置性别
            userbasicinfoSqlMapDao basicDao = new userbasicinfoSqlMapDao();
            var basic = basicDao.GetuserbasicinfoDetail(registerId);
            basic.Sex = model.sex;
            basicDao.Updateuserbasicinfo(basic);

            // 设置医院和科室
            UserrelrecordSqlMapDao recordDao = new UserrelrecordSqlMapDao();
            var record = recordDao.GetuserrelrecordDetail(registerId);
            record.HospitalId = model.hospitalId;

            HospitalSqlMapDao hDao = new HospitalSqlMapDao();
            var h = hDao.GethospitalDetail(model.hospitalId);
            if (h != null) {
                record.HospitalName = h.Name;
            }

            record.DepartmentId = model.departmentId;
            DepartmentSqlMapDao dDao = new DepartmentSqlMapDao();
            var d = dDao.GetdepartmentDetail(model.departmentId);
            if (d != null) {
                record.DepartmentName = d.Name;
            }
            recordDao.Updateuserrelrecord(record);
        }

        /// <summary>
        /// 修改一个护士
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RsModel<string> UpdateNurse(Nurse model) {
            RsModel<string> result = new RsModel<string>();
            if (!CheckNursePermission(model.adminId, NURSE_PERMISSION)) {
                result.code = 1;
                result.msg = "该管理员不具备该权限";
                return result;
            }

            try {
                UpdateNurseInfo(model);
            } catch (Exception e) {
                result.code = 1;
                result.msg = "更新护士信息失败";
                throw;
            }

            return result;
        }

        /// <summary>
        ///  删除一个护士
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RsModel<string> DeleteNurse(Nurse model) {
            RsModel<string> result = new RsModel<string>();

            try {
                userregisterSqlMapDao registerDao = new userregisterSqlMapDao();

                if (string.IsNullOrWhiteSpace(model.registerId)) {
                    result.code = 1;
                    result.msg = "用户不存在";
                } else {
                    if (registerDao.GetuserregisterDetail(model.registerId) != null) {

                        registerDao.Deleteuserregister(model.registerId);

                        UserrelrecordSqlMapDao recordDao = new UserrelrecordSqlMapDao();
                        recordDao.Deleteuserrelrecord(model.registerId);

                        userbasicinfoSqlMapDao basicDao = new userbasicinfoSqlMapDao();
                        basicDao.Deleteuserbasicinfo(model.registerId);

                        UserpracticecertificateSqlMapDao upDao = new UserpracticecertificateSqlMapDao();
                        upDao.Deleteuserpracticecertificate(model.registerId);

                        UserquacertificateSqlMapDao uqDao = new UserquacertificateSqlMapDao();
                        uqDao.Deleteuserquacertificate(model.registerId);

                        result.code = 0;
                    } else {
                        result.code = 1;
                        result.msg = "用户不存在";
                    }
                }
            } catch (Exception e) {
                result.code = 1;
                result.msg = "删除失败";
                throw;
            }


            return result;
        }

        #endregion
    }
}
