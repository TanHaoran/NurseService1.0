using Aersysm.Domain;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;


namespace Services {
    #region json拼装
    public class RsList<T> where T : class {
        [DataMemberAttribute(Order = 0)]
        public int code { get; set; }

        [DataMemberAttribute(Order = 1)]
        public string msg { get; set; }

        [DataMemberAttribute(Order = 2)]
        public IList<T> body { get; set; }
    }

    public class Rsslist<T> where T : class {
        [DataMemberAttribute(Order = 0)]
        public string Phone { get; set; }
    }

    public class RsModel<T> where T : class {
        [DataMemberAttribute(Order = 0)]
        public int code { get; set; }

        [DataMemberAttribute(Order = 1)]
        public string msg { get; set; }

        [DataMemberAttribute(Order = 2)]
        public T body { get; set; }
    }
    #endregion

    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IUserService”。
    [ServiceContract]
    public interface IUserService {

        //获取全部国家编码
        [OperationContract]
        [WebGet(UriTemplate = "GetCountryCodeAll", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<country> GetCountryCodeAll();

        ////获取全部国家编码
        //[OperationContract]
        //[WebGet(UriTemplate = "GetHospitalAll", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        //RsList<Hospital > GetHospitalAll();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string GetUser(aers_tbl_registereduser model);


        //根据手机号获取验证码
        [OperationContract]
        [WebGet(UriTemplate = "GetSMSCodeByPhone?CountryCode={CountryCode}&Phone={Phone}&Type={Type}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<string> GetSMSCodeByPhone(string CountryCode, string Phone, int Type);

        //验证验证码
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "IsOKSMSCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<UserFirstInfo> IsOKSMSCode(ViewSMS model);

        //注册完成后修改用户注册信息
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateUserRegisterInfo", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> UpdateUserRegisterInfo(userregister model);
        //// 注册
        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "Register", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //string Register(ViewRegister model);

        // 登录
        [OperationContract]   //login/phone/1
        [WebInvoke(Method = "POST", UriTemplate = "Login", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<UserFirstInfo> Login(userregister model);

        // 根据注册Id获取用户注册信息
        [OperationContract]
        [WebGet(UriTemplate = "GetUserReginfoById?RegisterId={RegisterId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        userregister GetUserReginfoById(string RegisterId);

        // 根据Id获取用户基本信息
        [OperationContract]
        [WebGet(UriTemplate = "GetUserReginfoByPhone?Phone={Phone}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        userregister GetUserReginfoByPhone(string Phone);

        //根据手机号修改密码
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "SetPwdByPhone", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string SetPwdByPhone(ViewRegister model);



        // 根据Id获取用户基本信息
        [OperationContract]
        [WebGet(UriTemplate = "GetUserBasicinfoById?RegisterId={RegisterId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        UserBasicInfo GetUserBasicinfoById(string RegisterId);





        // 根据注册Id获取用户护士资格证信息
        [OperationContract]
        [WebGet(UriTemplate = "GetUserQuacetById?RegisterId={RegisterId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<Userquacertificate> GetUserQuacetById(string RegisterId);


        //  根据注册Id获取用户护士执业证信息
        [OperationContract]
        [WebGet(UriTemplate = "GetUserPtccetById?RegisterId={RegisterId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<Userpracticecertificate> GetUserPtccetById(string RegisterId);

        //根据注册Id获取医院科室护理单元组织关系Id信息
        [OperationContract]
        [WebGet(UriTemplate = "GetUserRelcodById?RegisterId={RegisterId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Userrelrecord GetUserRelcodById(string RegisterId);

        //根据医院ID获取医院信息
        [OperationContract]
        [WebGet(UriTemplate = "GethospitalById?HospitalId={HospitalId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Hospital GethospitalById(string HospitalId);

        //根据科室Id获取科室信息
        [OperationContract]
        [WebGet(UriTemplate = "GetdepartmentById?RegisterId={RegisterId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Department GetdepartmentById(string RegisterId);

        //设置昵称
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "SetNickNameById", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string SetNickNameById(userregister model);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Updateuserbasicinfo", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> Updateuserbasicinfo(UserBasicInfo model);




        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateuserrelrecordInfo", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> UpdateuserrelrecordInfo(Userrelrecord model);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateuserquacertificateInfo", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> UpdateuserquacertificateInfo(Userquacertificate model);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateuserpracticecertificateInfo", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> UpdateuserpracticecertificateInfo(Userpracticecertificate model);


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AdduserbasicinfoInfo", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string AdduserbasicinfoInfo(UserBasicInfo model);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AdduserpracticecertificateInfo", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string AdduserpracticecertificateInfo(Userpracticecertificate model);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AdduserquacertificateInfo", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string AdduserquacertificateInfo(Userquacertificate model);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ResetPassword", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> ResetPassword(ViewResetPassword model);

        //提交证件审核
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddcertificateverifyInfo", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string AddcertificateverifyInfo(Certificateverify model);


        //查证件审核
        [OperationContract]
        [WebGet(UriTemplate = "GetcertificateverifyInfo?VerifyId={VerifyId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Certificateverify GetcertificateverifyInfo(string VerifyId);

        //提交证件审核
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddfeedbackInfo", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> AddfeedbackInfo(Feedback model);


        [OperationContract]
        [WebGet(UriTemplate = "GetReleaseversionInfo", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<Releaseversion> GetReleaseversionInfo();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddreleaseversionInfo", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string AddreleaseversionInfo(Releaseversion model);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdatereleaseversionInfo", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string UpdatereleaseversionInfo(Releaseversion model);

        [OperationContract]
        [WebGet(UriTemplate = "GetAddressByLngLat?lng={lng}&lat={lat}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<XMLDatatable> GetAddressByLngLat(string lng, string lat);

        //根据医院ID查所有科室信息
        [OperationContract]
        [WebGet(UriTemplate = "GetDepartmentList?HospitalId={HospitalId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<Department> GetDepartmentList(string HospitalId);

        //根据医院ID查所有科室信息
        //[OperationContract]
        //[WebGet(UriTemplate = "GetContactByHopDepId?HospitalId={HospitalId}&DepartmentId={DepartmentId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        //RsList<userregister> GetContactByHopDepId(string HospitalId, string DepartmentId);

        [OperationContract]
        [WebGet(UriTemplate = "GetContactByHopDepId?HospitalId={HospitalId}&DepartmentId={DepartmentId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<ViewFriendInfo> GetContactByHopDepId(string HospitalId, string DepartmentId);

        ////第三方登陆   qq
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ThirdPartLoginQQ", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<UserFirstInfo> ThirdPartLoginQQ(Qq model);

        //生成二维码
        //[OperationContract]
        //[WebGet(UriTemplate = "GetQRCodeById?RegisterId={RegisterId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        //string GetQRCodeById(string RegisterId);

        //生成二维码New
        [OperationContract]
        [WebGet(UriTemplate = "GetQRCodeById?RegisterId={RegisterId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<ViewJsonCommon> GetQRCodeById(string RegisterId);

        //第三方登陆
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CreatHXUser", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string CreatHXUser();

        //第三方登陆
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateAuditStatus", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string UpdateAuditStatus(Certificateverify model);

        [OperationContract]
        [WebGet(UriTemplate = "GetQCInfo?operatorId={operatorId}&pageSize={pageSize}&pageNumber={pageNumber}&CertificateId={CertificateId}&Name={Name}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<Userquacertificate> GetQCInfo(string operatorId, int pageSize, int pageNumber, string CertificateId, string Name);

        [OperationContract]
        [WebGet(UriTemplate = "GetPCInfo?operatorId={operatorId}&pageSize={pageSize}&pageNumber={pageNumber}&CertificateId={CertificateId}&Name={Name}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<Userpracticecertificate> GetPCInfo(string operatorId, int pageSize, int pageNumber, string CertificateId, string Name);

        //获得qq昵称
        [OperationContract]
        [WebGet(UriTemplate = "GetQQNickNameById?RegisterId={RegisterId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Qq GetQQNickNameById(string RegisterId);

        //绑定qq
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "SetQQBing", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string SetQQBind(Qq model);



        //qq绑定手机号
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "QQBindPhone", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string QQBindPhone(Qq model);

        //获取公告信息
        [OperationContract]
        [WebGet(UriTemplate = "GetNotice?pageNumber={pageNumber}&HospitalId={HospitalId}&DepartmentId={DepartmentId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<Notice> GetNotice(int pageNumber, string HospitalId, string DepartmentId);

        //获取banner信息
        [OperationContract]
        [WebGet(UriTemplate = "GetBanner?HospitalId={HospitalId}&DepartmentId={DepartmentId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<Banner> GetBanner(string HospitalId, string DepartmentId);

        //8.6首页登陆后获取可见信息
        [OperationContract]
        [WebGet(UriTemplate = "GetUserFirstInfoById?RegisterId={RegisterId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<UserFirstInfo> GetUserFirstInfoById(string RegisterId);


        //8.6首页登陆后获取可见信息
        [OperationContract]
        [WebGet(UriTemplate = "GetUserFirstInfoByPhone?Phone={Phone}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<UserFirstInfo> GetUserFirstInfoByPhone(string Phone);

        //8.6首页登陆后获取可见信息
        [OperationContract]
        [WebGet(UriTemplate = "GetUserBindInfo?RegisterId={RegisterId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<ViewUserBindInfo> GetUserBindInfo(string RegisterId);

        //8.6首页登陆后获取可见信息
        [OperationContract]
        [WebGet(UriTemplate = "GetUserInfo?RegisterId={RegisterId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<ViewUserInfo> GetUserInfo(string RegisterId);

        //解绑
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UnBind", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> UnBind(ViewBind model);

        //绑定
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Bind", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<UserFirstInfo> Bind(ViewBind model);


        //第三方院内账号登陆
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ThirdPartLoginHospital", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<UserFirstInfo> ThirdPartLoginHospital(userregister model);

        [OperationContract]
        [WebGet(UriTemplate = "GetFriendsList?RegisterId={RegisterId}&KeyWord={KeyWord}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<userregister> GetFriendsList(string RegisterId, string KeyWord);

        [OperationContract]
        [WebGet(UriTemplate = "GetFriendInfo?MyId={MyId}&FriendId={FriendId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<ViewFriendInfo> GetFriendInfo(string MyId, string FriendId);

        [OperationContract]
        [WebGet(UriTemplate = "AddEMFriend?MyId={MyId}&FriendId={FriendId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<string> AddEMFriend(string MyId, string FriendId);

        [OperationContract]
        [WebGet(UriTemplate = "SendFriendMsg?MyId={MyId}&FriendId={FriendId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<string> SendFriendMsg(string MyId, string FriendId);

        [OperationContract]
        [WebGet(UriTemplate = "GetEMFriend?MyId={MyId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<ViewFriendInfo> GetEMFriend(string MyId);

        [OperationContract]
        [WebGet(UriTemplate = "DeleteEMFriend?MyId={MyId}&FriendId={FriendId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<string> DeleteEMFriend(string MyId, string FriendId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetContactInfo", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsList<ViewContact> GetContactInfo(List<ViewContact> model);

        //创建一个群
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "CreateGroup", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<ViewGroupList> CreateGroup(ViewGroupList model);

        //获取个人群信息
        [OperationContract]
        [WebGet(UriTemplate = "GetXHGroupInfoList?RegisterId={RegisterId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<ViewGroupList> GetXHGroupInfoList(string RegisterId);

        [OperationContract]
        [WebGet(UriTemplate = "GetXHGroupList?GroupId={GroupId}&RegisterId={RegisterId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<ViewGroupList> GetXHGroupList(string GroupId, string RegisterId);

        //修改群昵称
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateGroupNickName", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> UpdateGroupNickName(ViewGroupList model);

        //添加群成员
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddGroupMember", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> AddGroupMember(ViewGroupList model);

        //退群
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "QuitGroup", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> QuitGroup(ViewGroupList model);


        //[OperationContract]
        //[WebInvoke(Method = "POST", UriTemplate = "emojiTest", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //string emojiTest(Qq model);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ThirdPartLoginWeixin", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<UserFirstInfo> ThirdPartLoginWeixin(Weixin model);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ThirdPartLoginWeibo", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<UserFirstInfo> ThirdPartLoginWeibo(Weibo model);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "emojiTest", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> emojiTest(Qq model);

        [OperationContract]
        [WebGet(UriTemplate = "GetEmoji?RegisterId={RegisterId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<Qq> GetEmoji(string RegisterId);




        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "testEmoji", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> testEmoji(Qq model);

        [OperationContract]
        [WebGet(UriTemplate = "GetEmojiTest", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<Qq> GetEmojiTest();



        //获取所有医院信息
        [OperationContract]
        [WebGet(UriTemplate = "GetHospitalAll?operatorId={operatorId}&pageSize={pageSize}&pageNumber={pageNumber}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<Hospital> GetHospitalAll(string operatorId, int pageSize, int pageNumber);

        [OperationContract]
        [WebGet(UriTemplate = "GetHospitalNameAll", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<Hospital> GetHospitalNameAll();


        //添加医院
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddHospital", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> AddHospital(Hospital model);
        // [WebInvoke(Method = "POST", UriTemplate = "UpdateUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        //修改医院
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateHospital", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> UpdateHospital(Hospital model);

        //获取所有科室
        [OperationContract]
        [WebGet(UriTemplate = "GetDepartmentAll?operatorId={operatorId}&pageSize={pageSize}&pageNumber={pageNumber}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<Department> GetDepartmentAll(string operatorId, int pageSize, int pageNumber);

        //根据医院Id获取科室
        [OperationContract]
        [WebGet(UriTemplate = "GetDepartmentByHospitalId?HospitalId={HospitalId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<Department> GetDepartmentByHospitalId(string HospitalId);

        //添加科室
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddDepartment", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> AddDepartment(Department model);

        //修改科室
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateDepartment", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> UpdateDepartment(Department model);

        //删除医院
        [OperationContract]
        [WebGet(UriTemplate = "DeleteHospital?operatorId={operatorId}&HospitalId={HospitalId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<string> DeleteHospital(string operatorId, string HospitalId);

        //删除科室
        [OperationContract]
        [WebGet(UriTemplate = "DeleteDepartment?operatorId={operatorId}&DepartmentId={DepartmentId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<string> DeleteDepartment(string operatorId, string DepartmentId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddAdministrator", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> AddAdministrator(Administrator model);

        //修改管理员
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateAdministrator", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> UpdateAdministrator(Administrator model);

        //删除科室
        [OperationContract]
        [WebGet(UriTemplate = "DeleteAdministrator?operatorId={operatorId}&AdmId={AdmId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<string> DeleteAdministrator(string operatorId, string AdmId);

        //获取管理员
        [OperationContract]
        [WebGet(UriTemplate = "GetAdministrator?operatorId={operatorId}&pageSize={pageSize}&pageNumber={pageNumber}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<Administrator> GetAdministrator(string operatorId, int pageSize, int pageNumber);



        //banner修改
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateBanner", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> UpdateBanner(Banner model);

        //banner删除
        [OperationContract]
        [WebGet(UriTemplate = "DeleteBanner?operatorId={operatorId}&BannerId={BannerId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<string> DeleteBanner(string operatorId, string BannerId);

        //添加公告
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddNotice", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> AddNotice(Notice model);

        //修改公告
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateNotice", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> UpdateNotice(Notice model);

        //banner删除
        [OperationContract]
        [WebGet(UriTemplate = "UpdateDelNotice?NoticeId={NoticeId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsModel<string> UpdateDelNotice(string NoticeId);

        //后端获取公告
        [OperationContract]
        [WebGet(UriTemplate = "GetNoticeAll?operatorId={operatorId}&pageSize={pageSize}&pageNumber={pageNumber}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<Notice> GetNoticeAll(string operatorId, int pageSize, int pageNumber);

        // 设置好友备注
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateFriend", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> UpdateFriend(Emchat model);


        /***********************************************************************************************************/
        /***********************************************************************************************************/
        /************************************************** 交接后 **************************************************/
        /***********************************************************************************************************/
        /***********************************************************************************************************/

        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetPermissionList", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<Permission> GetPermissionList();

        /// <summary>
        /// 管理后台登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ManagementLogin", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<Administrator> ManagementLogin(Administrator model);

        /// <summary>
        /// 获取管理员权限
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetAdminPermission?AdminId={adminId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<Admpermission> GetAdminPermission(string adminId);

        /// <summary>
        /// 获取管理员管理科室
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetAdminDepartment?AdminId={adminId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<Admdepartment> GetAdminDepartment(string adminId);

        /// <summary>
        /// 管理员重置密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ResetAdminPassword", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<Administrator> ResetAdminPassword(Administrator model);

        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetAllNurseInfo?adminId={adminId}&hospitalId={hospitalId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<Nurse> GetAllNurseInfo(string adminId, string hospitalId);

        /// <summary>
        /// 添加一个护士
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddNurse", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> AddNurse(Nurse model);

        /// <summary>
        /// 修改护士信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ///   [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateNurse", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> UpdateNurse(Nurse model);

        /// <summary>
        /// 删除一个护士
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        ///   [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DeleteNurse", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        RsModel<string> DeleteNurse(Nurse model);

        /// <summary>
        /// 获取公告科室
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "GetNoticeDepartmentByNoticeId?operatorId={operatorId}&noticeId={noticeId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<NoticeDepartment> GetNoticeDepartmentByNoticeId(string operatorId, string noticeId);

        /// <summary>
        /// 管理后台获取Banner
        /// </summary>
        /// <param name="operatorId"></param>
        /// <returns></returns>
        ///  [OperationContract]
        [WebGet(UriTemplate = "GetAdminBanner?operatorId={operatorId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        RsList<Banner> GetAdminBanner(string operatorId);
    }
}
