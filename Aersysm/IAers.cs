using Aersysm.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Aersysm
{
    #region 用于处理JSON返回预处理的实体
    [DataContract]
    public class PageData<T> where T : class
    {
        [DataMemberAttribute(Order = 0)]
        public int total { get; set; }

        [DataMemberAttribute(Order = 1)]
        public IList<T> rows { get; set; }
    }
    public class ResModel<T> where T : class
    {
        [DataMemberAttribute(Order = 0)]
        public string restag { get; set; }

        [DataMemberAttribute(Order = 1)]
        public T model { get; set; }
    }
    public class ResList<T> where T : class
    {
        [DataMemberAttribute(Order = 0)]
        public string restag { get; set; }

        [DataMemberAttribute(Order = 1)]
        public IList<T> list { get; set; }
    }
    #endregion

    [ServiceContract]
    public interface IAers
    {


        #region 以前
        [OperationContract]
        [WebGet(UriTemplate = "GetEventReport?yue1={yue1}&yue2={yue2}&Region={Region}&HospId={HospId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        string GetEventReport(string yue1, string yue2, string Region, string HospId);



        [OperationContract]
        [WebGet(UriTemplate = "GetEventddzcReport?yue1={yue1}&yue2={yue2}&Region={Region}&HospId={HospId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        string GetEventddzcReport(string yue1, string yue2, string Region, string HospId);


        [OperationContract]
        [WebGet(UriTemplate = "GetEventZcReport?yue1={yue1}&yue2={yue2}&Region={Region}&HospId={HospId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        string GetEventZcReport(string yue1, string yue2, string Region, string HospId);

        [OperationContract]
        [WebGet(UriTemplate = "GetEventGlReport?yue1={yue1}&yue2={yue2}&Region={Region}&HospId={HospId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        string GetEventGlReport(string yue1, string yue2, string Region, string HospId);
         
        [OperationContract]
        [WebGet(UriTemplate = "GetEventGyReport?yue1={yue1}&yue2={yue2}&Region={Region}&HospId={HospId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        string GetEventGyReport(string yue1, string yue2, string Region, string HospId);

        [OperationContract]
        [WebGet(UriTemplate = "GetEventYcReport?yue1={yue1}&yue2={yue2}&Region={Region}&HospId={HospId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        string GetEventYcReport(string yue1, string yue2, string Region, string HospId);


        [OperationContract]
        [WebGet(UriTemplate = "GetEventZyblReport?yue1={yue1}&yue2={yue2}&Region={Region}&HospId={HospId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        string GetEventZyblReport(string yue1, string yue2, string Region, string HospId);


        //获取区域划分
        [OperationContract]
        [WebGet(UriTemplate = "GetRegion", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<Region> GetRegion();


        #region 用户管理操作接口
        ////用户登录验证
        //[OperationContract]
        //[WebGet(UriTemplate = "login?name={name}&pwd={pwd}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        //ResModel<view_tbl_registereduser> UserLogin(string name, string pwd);

        //以前的登陆
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "login", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
         ResModel<view_tbl_registereduser> UserLogin(view_Login model);

        //现在的登陆
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "loginNew", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResModel<view_tbl_registereduser> UserLoginNew(view_Login model);

        //忘记密码
        [OperationContract]
        [WebGet(UriTemplate =  "ForgetPwd?PhoneNumber={PhoneNumber}&Code={Code}&pwd={pwd}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string ForgetPwd(string PhoneNumber, string Code,string pwd);
        //用户注册 
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Registeruser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string Registeruser(aers_tbl_staff model);

        //添加用户   YM2017.4.18
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "RegisterAdduser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string RegisterAdduser(aers_tbl_staff model);

        //修改用户名
        [OperationContract]
        [WebGet(UriTemplate = "UpdateLoginName?rud={uid}&name={name}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string UpdateLoginName(string uid, string name);

        //修改密码
        [OperationContract]
        [WebGet(UriTemplate = "UpdatePwd?rud={uid}&Pwd={Pwd}&yPwd={yPwd}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string UpdatePwd(string uid, string Pwd, string yPwd);

        //获取人员信息
        [OperationContract]
        [WebGet(UriTemplate = "FindByRUid?rud={uid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        aers_tbl_staff FindByRUid(string uid);


        //修改人员信息
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string UpdateUser(aers_tbl_staff model);



        //删除用户
        [OperationContract]
        [WebGet(UriTemplate = "DeleteUser?ReguserId={ReguserId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string DeleteUser(string ReguserId);

        //用户登录验证
        [OperationContract]
        [WebGet(UriTemplate = "gethosps?rud={uid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string GetHospState(string uid);

        //所有医院信息
        [OperationContract]
        [WebGet(UriTemplate = "Gethospital?Region={Region}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<aers_tbl_hospital> Gethospital(string Region);


        //科室汇总统计分析

        [OperationContract]
        [WebGet(UriTemplate = "GetEveCountByhospdep?HospId={HospId}&Status={Status}&yue={yue}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<aers_tbl_hospdep> GetEveCountByhospdep(string HospId, string Status, string yue);

        [OperationContract]
        [WebGet(UriTemplate = "GetIsEveCountByDep?uid={uid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string GetIsEveCountByDep(string uid);

        //所有医院上报事件情况
        [OperationContract]
        [WebGet(UriTemplate = "GetEveCountByhospital?Status={Status}&Region={Region}&yue={yue}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        IList<aers_tbl_hospital> GetEveCountByhospital(string Status, string Region, string yue);


        //获取医院联盟
        [OperationContract]
        [WebGet(UriTemplate = "GethospitalUnion", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<aers_tbl_hospital> GethospitalUnion();

        //获取事件按月统计数量
        [OperationContract]
        [WebGet(UriTemplate = "GetEventsresumeByCount?nian={nian}&Region={Region}&HospId={HospId}&levelID={levelID}&GradeID={GradeID}&EveresLevel={EveresLevel}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        string GetEventsresumeByCount(string nian, string Region, string HospId, string levelID, string GradeID, string EveresLevel);

        //获取其他事件按月统计数量
        [OperationContract]
        [WebGet(UriTemplate = "GetEventQtByCount?nian={nian}&Region={Region}&HospId={HospId}&levelID={levelID}&GradeID={GradeID}&EveresLevel={EveresLevel}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        string GetEventQtByCount(string nian, string Region, string HospId, string levelID, string GradeID, string EveresLevel);

        //按科室查询上报事件数量
        [OperationContract]
        [WebGet(UriTemplate = "GetEventsresumeByhospdepCount?HospId={HospId}&EveresLevel={EveresLevel}&yue1={yue1}&yue2={yue2}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string GetEventsresumeByhospdepCount(string HospId, string EveresLevel, string yue1, string yue2);

        #endregion

        #region 不良事件上报接口

        //一键上报事件（本月无不良事件）
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "simply", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string simplyevent(aers_tbl_eventsresume model);


        [OperationContract]
        [WebGet(UriTemplate = "DeleteEventsresume?eud={eid}&uid={uid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string DeleteEventsresume(string eid, string uid);


        //审核通过事件
        [OperationContract]
        [WebGet(UriTemplate = "exevent?eud={eud}&fadeBack={fadeBack}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string exevent(string eud, string fadeBack);
        //审核不通过事件   
        [OperationContract]
        [WebGet(UriTemplate = "unexevent?eud={eud}&fadeBack={fadeBack}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string unexevent(string eud, string fadeBack);



        //护理部权限 - 查询 未通过审核事件  未审核
        [OperationContract]
        [WebGet(UriTemplate = "findndepnoevent?rud={uid}&Time1={Time1}&Time2={Time2}&EveresLevel={EveresLevel}&EventType={EventType}&ReUId={ReUId}&LoginKey={LoginKey}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<aers_tbl_eventsresume> findndepnoevent(string uid, string Time1, string Time2, string EveresLevel, string EventType,string ReUId,string LoginKey);
       
        //护理部权限 - 查询 通过审核事件   已审核
        [OperationContract]
        [WebGet(UriTemplate = "findndepevent?rud={uid}&Time1={Time1}&Time2={Time2}&EveresLevel={EveresLevel}&EventType={EventType}&ReUId={ReUId}&LoginKey={LoginKey}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<aers_tbl_eventsresume> findndepevent(string uid, string Time1, string Time2, string EveresLevel, string EventType, string ReUId, string LoginKey);
        //护理部权限 - 查询 正在审核事件
        [OperationContract]
        [WebGet(UriTemplate = "findndepwait?rud={uid}&Time1={Time1}&Time2={Time2}&EveresLevel={EveresLevel}&EventType={EventType}&ReUId={ReUId}&LoginKey={LoginKey}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<aers_tbl_eventsresume> findndepwait(string uid, string Time1, string Time2, string EveresLevel, string EventType, string ReUId, string LoginKey);

        [OperationContract]
        [WebGet(UriTemplate = "findndepRecord?rud={uid}&pageSize={pageSize}&pageNumber={pageNumber}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<aers_tbl_eventsresume> findndepRecord(string uid, int pageSize, int pageNumber);

        //护理部权限 - 查询 正在审核事件
        [OperationContract]
        [WebGet(UriTemplate = "findndepwaitNew?rud={uid}&ReUId={ReUId}&pageSize={pageSize}&pageNumber={pageNumber}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<aers_tbl_eventsresume> findndepwaitNew(string uid,string ReUId,int  pageSize, int pageNumber);

        [OperationContract]
        [WebGet(UriTemplate = "findndepallevent?rud={uid}&pageSize={pageSize}&pageNumber={pageNumber}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<aers_tbl_eventsresume> findndepallevent(string uid,int  pageSize,int pageNumber);
        

        //护理部权限 - 查询 未提交
        [OperationContract]
        [WebGet(UriTemplate = "findndepreport?rud={uid}&Time1={Time1}&Time2={Time2}&EveresLevel={EveresLevel}&EventType={EventType}&ReUId={ReUId}&LoginKey={LoginKey}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<aers_tbl_eventsresume> findndepreport(string uid, string Time1, string Time2, string EveresLevel, string EventType, string ReUId, string LoginKey);

        //省厅权限 - 查询 审核事件
        [OperationContract]
        [WebGet(UriTemplate = "findproevent?hid={hid}&Region={Region}&Time1={Time1}&Time2={Time2}&EventType={EventType}&EveresLevel={EveresLevel}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<aers_tbl_eventsresume> findproevent(string hid, string Region, string Time1, string Time2, string EventType, string EveresLevel);

        ////护士长权限 - 查询 未通过审核事件
        //[OperationContract]
        //[WebGet(UriTemplate = "findnursnoevent?rud={uid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        //string findnursnoevent(string uid);
        ////护士长权限 - 查询 通过审核事件
        //[OperationContract]
        //[WebGet(UriTemplate = "findnursevent?rud={uid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        //string findnursevent(string uid);
        ////护士长权限 - 查询 正在审核事件
        //[OperationContract]
        //[WebGet(UriTemplate = "findnurswait?rud={uid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        //string findnurswait(string uid);


        ////护士长权限 - 查询 正在审核事件
        //[OperationContract]
        //[WebGet(UriTemplate = "findnursreport?rud={uid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        //string findnursreport(string uid);


        //查询是否上报过事件
        [OperationContract]
        [WebGet(UriTemplate = "findnurswaitData?rud={uid}&yue={yue}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string findnurswaitData(string uid, string yue);



        //根据汇总ID查询不良事件详细信息(压疮)
        [OperationContract]
        [WebGet(UriTemplate = "findycinfo?eud={eid}&ReUId={ReUId}&LoginKey={LoginKey}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        aers_tbl_events_yc findeventsycByEid(string eid, string ReUId, string LoginKey);

        //根据汇总ID查询不良事件详细信息(隐患)
        [OperationContract]
        [WebGet(UriTemplate = "findyhinfo?eud={eid}&ReUId={ReUId}&LoginKey={LoginKey}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        aers_tbl_events_yh findeventsyhByEid(string eid, string ReUId, string LoginKey);

        //根据汇总ID查询不良事件详细信息(其他)
        [OperationContract]
        [WebGet(UriTemplate = "findqtinfo?eud={eid}&ReUId={ReUId}&LoginKey={LoginKey}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        aers_tbl_events_qt findeventsqtByEid(string eid, string ReUId, string LoginKey);

        //根据汇总ID查询不良事件详细信息(给药)
        [OperationContract]
        [WebGet(UriTemplate = "findgyinfo?eud={eid}&ReUId={ReUId}&LoginKey={LoginKey}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        aers_tbl_events_gy findeventsgyByEid(string eid, string ReUId, string LoginKey);

        //根据汇总ID查询不良事件详细信息(管路)
        [OperationContract]
        [WebGet(UriTemplate = "findglinfo?eud={eid}&ReUId={ReUId}&LoginKey={LoginKey}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        aers_tbl_events_gl findeventsglByEid(string eid, string ReUId, string LoginKey);

        //根据汇总ID查询不良事件详细信息(职业暴露)
        [OperationContract]
        [WebGet(UriTemplate = "findzyblinfo?eud={eid}&ReUId={ReUId}&LoginKey={LoginKey}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        aers_tbl_events_zybl findeventszyblByEid(string eid, string ReUId, string LoginKey);

        //根据汇总ID查询不良事件详细信息(跌倒坠床)
        [OperationContract]
        [WebGet(UriTemplate = "findddzcinfo?eud={eid}&ReUId={ReUId}&LoginKey={LoginKey}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        aers_tbl_events_ddzc findeventsddzcByEid(string eid, string ReUId, string LoginKey);

        //根据汇总ID查询不良事件详细信息(无不良事件)
        [OperationContract]
        [WebGet(UriTemplate = "findonekeyinfo?eud={eid}&ReUId={ReUId}&LoginKey={LoginKey}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        view_tabl_onekey FindonkeyinfoByEid(string eid, string ReUId, string LoginKey);


        //压疮事件上报
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "pushyc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string EventYcReport(aers_tbl_events_yc model,string ReUId, string LoginKey);

        //压疮事件上报new
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "pushycNew", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string EventYcReportNew(aers_tbl_events_yc model);

        // 其他事件上报
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "pushqt", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string EventQtReport(aers_tbl_events_qt model, string ReUId, string LoginKey);

        // 其他事件上报new
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "pushqtNew", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string EventQtReportNew(aers_tbl_events_qt model);

        // 职业暴露事件上报
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "pushzybl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string EventZyblReport(aers_tbl_events_zybl model, string ReUId, string LoginKey);

        // 职业暴露事件上报new
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "pushzyblNew", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string EventZyblReportNew(aers_tbl_events_zybl model);

        // 给药事件上报
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "pushgy", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string EventGyReport(aers_tbl_events_gy model, string ReUId, string LoginKey);

        // 给药事件上报new
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "pushgyNew", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string EventGyReportNew(aers_tbl_events_gy model);

        // 管路事件上报
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "pushgl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string EventGlReport(aers_tbl_events_gl model, string ReUId, string LoginKey);

        // 管路事件上报new
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "pushglNew", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string EventGlReportNew(aers_tbl_events_gl model);

        // 隐患事件上报
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "pushyh", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string EventYhReport(aers_tbl_events_yh model, string ReUId, string LoginKey);

        // 隐患事件上报new
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "pushyhNew", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string EventYhReportNew(aers_tbl_events_yh model);

        // 跌倒/坠床事件上报
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "pushddzc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string EventDdzcReport(aers_tbl_events_ddzc model, string ReUId, string LoginKey);

        // 跌倒/坠床事件上报new
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "pushddzcNew", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string EventDdzcReportNew(aers_tbl_events_ddzc model);
        #endregion

        #region 错误编码接口

        //错误编码接口
        [OperationContract]
        [WebGet(UriTemplate = "statecode", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<aers_sys_statecode> FindStatecodeAll();

        #endregion

        #region 检查是否符合上报条件
        [OperationContract]
        [WebGet(UriTemplate = "checkevents", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool CheckEvents();

        [OperationContract]
        [WebGet(UriTemplate = "checkonekey?eud={eid}&yue={yue}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        bool CheckOnekey(string eid, string yue);

        [OperationContract]
        [WebGet(UriTemplate = "checkonekeynew?eud={eid}&yue={yue}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string  CheckOnekeyNew(string eid, string yue);


        #endregion

        #region  返回事件总数

        //返回 事件上报（不含零事件上报）、待审核、已审核、未通过 事件总数
        [OperationContract]
        [WebGet(UriTemplate = "getevecounts?rol={roles}&eud={eid}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string GetEventsCounts(string roles, string eid);

        #endregion

        #region 获取用户积分排名

        [OperationContract]
        [WebGet(UriTemplate = "StaffFindAllByGrade?Number={Number}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string StaffFindAllByGrade(int Number);

        #endregion

        #region 课程管理



        //查询单个课程
        [OperationContract]
        [WebGet(UriTemplate = "CourseFindByCourseID?CourseID={CourseID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Course CourseFindByCourseID(String CourseID);


        //查询单个课程
        [OperationContract]
        [WebGet(UriTemplate = "CourseFindOrderByCourseID?CourseID={CourseID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        Course CourseFindOrderByCourseID(string CourseID);


        [WebGet(UriTemplate = "CourseFindAll", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<Course> CourseFindAll();

        [OperationContract]
        [WebGet(UriTemplate = "CourseFindByUserID?UserID={UserID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<Course> CourseFindByUserID(string UserID);



        [OperationContract]
        [WebGet(UriTemplate = "AddCourseUser?UserID={UserID}&CourseID={CourseID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string AddCourseUser(string UserID, string CourseID);


        [OperationContract]
        [WebGet(UriTemplate = "DeleteCourseUser?UserID={UserID}&CourseID={CourseID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string DeleteCourseUser(string UserID, string CourseID);

        //查询推荐课程  //推荐Recommend     人数最多CourseHot  最新课程 NewTime   FieldName==""  查询全部
        [OperationContract]
        [WebGet(UriTemplate = "CourseFindOrderBySortField?FieldName={FieldName}&Number={Number}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<Course> CourseFindOrderBySortField(string FieldName, int Number);


        //查询推荐课程  //推荐Recommend     人数最多CourseHot  最新课程 NewTime   FieldName==""  查询全部
        [OperationContract]
        [WebGet(UriTemplate = "CourseFindPaging?pageno={pageno}&pageSize={pageSize}&SortType={SortType}&Data={Data}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<Course> CourseFindPaging(int pageno, int pageSize, string SortType, string Data);


        #endregion

        #region 课程章节

        //目录
        [OperationContract]
        [WebGet(UriTemplate = "CourseCatalog_FindByCourseID?CourseID={CourseID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<CourseCatalog> CourseCatalog_FindByCourseID(string CourseID);

        //章节
        [OperationContract]
        [WebGet(UriTemplate = "CourseSectionFindByCourseID?CourseID={CourseID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<CourseSection> CourseSectionFindByCourseID(string CourseID);


        [OperationContract]
        [WebGet(UriTemplate = "CourseSectionFindByCatalogID?CatalogID={CatalogID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<CourseSection> CourseSectionFindByCatalogID(string CatalogID);

        //问题
        [OperationContract]
        [WebGet(UriTemplate = "Questions_FindByChapterID?ChapterID={ChapterID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string  Questions_FindByChapterID(string ChapterID);

        [OperationContract]
        [WebGet(UriTemplate = "CourseSectionFind?ChapterID={ChapterID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        CourseSection CourseSectionFind(String ChapterID);


        //课程章节表
        [OperationContract]
        [WebGet(UriTemplate = "CourseSectionFindAll", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<CourseSection> CourseSectionFindAll();

        #endregion


        //评论
        [OperationContract]
        [WebGet(UriTemplate = "ProblemFindByCourseID?CourseID={CourseID}&pageSize={pageSize}&pageNumber={pageNumber}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<Problem> ProblemFindByCourseID(string CourseID,int pageSize,int pageNumber);

        //增加评论
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddProblem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string AddProblem(Problem model);


        //笔记
        [OperationContract]
        [WebGet(UriTemplate = "NotesFindByCourseIDUserID?CourseID={CourseID}&UserID={UserID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<Notes> NotesFindByCourseIDUserID(string CourseID,string UserID);

        //增加笔记
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddNotes", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string AddNotes(Notes model);


        //点赞
        [OperationContract]
        [WebGet(UriTemplate = "AddFavor?TypeID={TypeID}&UserID={UserID}&MainID={MainID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string AddFavor(int TypeID, string UserID, string MainID);


        //获取回答结果
        [OperationContract]
        [WebGet(UriTemplate = "GetAnswerByUserCourseID?UserID={UserID}&CourseID={CourseID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<Answer> GetAnswerByUserCourseID(string UserID, string CourseID);


        //答题回传
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddAnswerList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResList<Answer> AddAnswerList(IList<Answer> model);

        //按医院查询操作员
        [OperationContract]
        [WebGet(UriTemplate = "GetRegistereduserByHospId?HospId={HospId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<aers_tbl_registereduser> GetRegistereduserByHospId(string HospId);

        //按医院查询科室
        [OperationContract]
        [WebGet(UriTemplate = "hospdepFindByHospId?HospId={HospId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<aers_tbl_hospdep> hospdepFindByHospId(string HospId);



        //增加科室
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Addhospdep", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string Addhospdep(aers_tbl_hospdep model);


        //删除科室
        [OperationContract]
        [WebGet(UriTemplate = "DeleteHospdep?HospdepId={HospdepId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string DeleteHospdep(string HospdepId);


        //
        [OperationContract]
        [WebGet(UriTemplate = "FindGroupByName?time1={time1}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string FindGroupByName(string time1);


        //2017.5.5Yanming获取所有科室信息  aers_tbl_basicsdep
        [WebGet(UriTemplate = "BasicsdepFindAll", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<aers_tbl_basicsdep> BasicsdepFindAll();



        [OperationContract]
        [WebGet(UriTemplate = "CourseFindOrderByCourseType?CourseType={CourseType}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<Course> CourseFindOrderByCourseType(string CourseType);


        [OperationContract]
        [WebGet(UriTemplate = "CourseFindOrderByHospId?HospId={HospId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<Course> CourseFindOrderByHospId(string HospId);


        [OperationContract]
        [WebGet(UriTemplate = "StudyScheduleFindByHospdepId?HospdepId={HospdepId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        IList<StudySchedule> StudyScheduleFindByHospdepId(string HospdepId);

        [OperationContract]
        [WebGet(UriTemplate = "PlancontentsFindByHospdepId?HospdepId={HospdepId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        IList<plancontents> PlancontentsFindByHospdepId(string HospdepId);



        [OperationContract]
        [WebGet(UriTemplate = "PlancontentsFindByPlanID?PlanID={PlanID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<plancontents> PlancontentsFindByPlanID(string PlanID);




        #region 2017.3.20保存课程观看历史
        [OperationContract]
        [WebGet(UriTemplate = "SaveHistoryCourse?UserID={UserID}&CourseID={CourseID}&ChapterID={ChapterID}&PlayTime={PlayTime}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string SaveHistoryCourse(string UserID, string CourseID,string ChapterID, string PlayTime);
        #endregion

        #region 2017.3.23获取课程观看历史列表
        //2017.3.23 获取课程观看历史列表
        [OperationContract]
        [WebGet(UriTemplate = "CourseHistoryFindByUserID?UserID={UserID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<CourseRecord> CourseHistoryFindByUserID(string UserID);
        #endregion

        #region  数据分析2017.03.28 导出excel
        //按事件统计-跌倒事件
        [OperationContract]
        [WebGet(UriTemplate = "GetEventddzcReportexcel?yue1={yue1}&yue2={yue2}&Region={Region}&HospId={HospId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        string GetEventddzcReportexcel(string yue1, string yue2, string Region, string HospId);

        //按事件统计-坠床事件
        [OperationContract]
        [WebGet(UriTemplate = "GetEventZcReportexcel?yue1={yue1}&yue2={yue2}&Region={Region}&HospId={HospId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        string GetEventZcReportexcel(string yue1, string yue2, string Region, string HospId);

        //按事件统计-管路事件
        [OperationContract]
        [WebGet(UriTemplate = "GetEventGlReportexcel?yue1={yue1}&yue2={yue2}&Region={Region}&HospId={HospId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        string GetEventGlReportexcel(string yue1, string yue2, string Region, string HospId);

        //按事件统计-给药事件
        [OperationContract]
        [WebGet(UriTemplate = "GetEventGyReportexcel?yue1={yue1}&yue2={yue2}&Region={Region}&HospId={HospId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        string GetEventGyReportexcel(string yue1, string yue2, string Region, string HospId);

        //按事件统计-压疮事件
        [OperationContract]
        [WebGet(UriTemplate = "GetEventYcReportexcel?yue1={yue1}&yue2={yue2}&Region={Region}&HospId={HospId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        string GetEventYcReportexcel(string yue1, string yue2, string Region, string HospId);

        //按事件统计-职业暴露
        [OperationContract]
        [WebGet(UriTemplate = "GetEventZyblReportexcel?yue1={yue1}&yue2={yue2}&Region={Region}&HospId={HospId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        string GetEventZyblReportexcel(string yue1, string yue2, string Region, string HospId);


        //首页???
        [OperationContract]
        [WebGet(UriTemplate = "GetEventsresumeByCountSYexcel?nian={nian}&Region={Region}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        string GetEventsresumeByCountSYexcel(string nian, string Region);








        // 事件明细 Aers.svc/findproevent?hid=&Region=&Time1=2017-03-01&Time2=2017-03-27&EventType=&EveresLevel=
        [OperationContract]
        [WebGet(UriTemplate = "findproeventexcel?hid={hid}&Region={Region}&Time1={Time1}&Time2={Time2}&EventType={EventType}&EveresLevel={EveresLevel}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string findproeventexcel(string hid, string Region, string Time1, string Time2, string EventType, string EveresLevel);


        //按医院统计  Aers.svc/GetEveCountByhospital?Status=2&Region=&yue=2017-03-01
        [OperationContract]
        [WebGet(UriTemplate = "GetEveCountByhospitalexcel?Status={Status}&Region={Region}&yue={yue}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        string GetEveCountByhospitalexcel(string Status, string Region, string yue);

        //按事件数量统计
        [OperationContract]
        [WebGet(UriTemplate = "GetEventsresumeByCountexcel?nian={nian}&Region={Region}&HospId={HospId}&levelID={levelID}&GradeID={GradeID}&EveresLevel={EveresLevel}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        string GetEventsresumeByCountexcel(string nian, string Region, string HospId, string levelID, string GradeID, string EveresLevel);

        //其他事件统计
        [OperationContract]
        [WebGet(UriTemplate = "GetEventQtByCountexcel?nian={nian}&Region={Region}&HospId={HospId}&levelID={levelID}&GradeID={GradeID}&EveresLevel={EveresLevel}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]

        string GetEventQtByCountexcel(string nian, string Region, string HospId, string levelID, string GradeID, string EveresLevel);

        //暂未列出的其他事件统计
        [OperationContract]
        [WebGet(UriTemplate = "FindGroupByNameexcel?time1={time1}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string FindGroupByNameexcel(string time1);

        //各事件占比统计
        [OperationContract]
        [WebGet(UriTemplate = "GetEventReportexcel?yue1={yue1}&yue2={yue2}&Region={Region}&HospId={HospId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string GetEventReportexcel(string yue1, string yue2, string Region, string HospId);

        //数据分析-科室上报明细
        [OperationContract]
        [WebGet(UriTemplate = "GetEventsresumeByhospdepCountexcel?HospId={HospId}&EveresLevel={EveresLevel}&yue1={yue1}&yue2={yue2}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string GetEventsresumeByhospdepCountexcel(string HospId, string EveresLevel, string yue1, string yue2);

        //数据分析-已上报未上报科室
        [OperationContract]
        [WebGet(UriTemplate = "GetEveCountByhospdepexcel?HospId={HospId}&Status={Status}&yue={yue}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string GetEveCountByhospdepexcel(string HospId, string Status, string yue);


        //事件审核-待审核
        [OperationContract]
        [WebGet(UriTemplate = "findndepwaitexcel?rud={uid}&Time1={Time1}&Time2={Time2}&EveresLevel={EveresLevel}&EventType={EventType}&ReUId={ReUId}&LoginKey={LoginKey}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string findndepwaitexcel(string uid, string Time1, string Time2, string EveresLevel, string EventType,string ReUId, string LoginKey);

        //事件审核-已通过
        [OperationContract]
        [WebGet(UriTemplate = "findndepeventexcel?rud={uid}&Time1={Time1}&Time2={Time2}&EveresLevel={EveresLevel}&EventType={EventType}&ReUId={ReUId}&LoginKey={LoginKey}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string findndepeventexcel(string uid, string Time1, string Time2, string EveresLevel, string EventType, string ReUId, string LoginKey);

        //事件审核-未通过
        [OperationContract]
        [WebGet(UriTemplate = "findndepnoeventexcel?rud={uid}&Time1={Time1}&Time2={Time2}&EveresLevel={EveresLevel}&EventType={EventType}&ReUId={ReUId}&LoginKey={LoginKey}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string findndepnoeventexcel(string uid, string Time1, string Time2, string EveresLevel, string EventType, string ReUId, string LoginKey);

        //草稿箱
        [OperationContract]
        [WebGet(UriTemplate = "findndepreportexcel?rud={uid}&Time1={Time1}&Time2={Time2}&EveresLevel={EveresLevel}&EventType={EventType}&ReUId={ReUId}&LoginKey={LoginKey}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string findndepreportexcel(string uid, string Time1, string Time2, string EveresLevel, string EventType, string ReUId, string LoginKey);
        #endregion

        #region Message的CURD   Yanming2017.5.8
        //增
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddMessage", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int AddMessage(Message model);
        //删
        [OperationContract]
        [WebGet(UriTemplate = "DeleteMessage?MessageId={MessageId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        int DeleteMessage(string MessageId);

        //改
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateMessage", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int UpdateMessage(Message model);

        //查u
        [WebGet(UriTemplate = "MessageFindAll", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<Message> MessageFindAll();
        #endregion

        #region CertificateAudit的CURD   Yanming2017.5.9
        //增
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddCertificateAudit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int AddCertificateAudit(CertificateAudit model);
        //删
        [OperationContract]
        [WebGet(UriTemplate = "DeleteCertificateAudit?AuditID={AuditID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        int DeleteCertificateAudit(string AuditID);

        //改
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateCertificateAudit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int UpdateCertificateAudit(CertificateAudit model);

        //查u
        [WebGet(UriTemplate = "CertificateAuditFindAll", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<CertificateAudit> CertificateAuditFindAll();
        #endregion

        #region smsmessage的CURD   Yanming2017.5.9
        //增
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddSMSMessage", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int AddSMSMessage(SMSMessage model);
        //删
        [OperationContract]
        [WebGet(UriTemplate = "DeleteSMSMessage?AuditID={SMSID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        int DeleteSMSMessage(string SMSID);

        //改
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateSMSMessage", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int UpdateSMSMessage(SMSMessage model);

        //查u
        [WebGet(UriTemplate = "SMSMessageFindAll", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<SMSMessage> SMSMessageFindAll();
        #endregion

        #region CourseDistribute的CURD   Yanming2017.5.9
        //增
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AddCourseDistribute", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int AddCourseDistribute(CourseDistribute model);
        //删
        [OperationContract]
        [WebGet(UriTemplate = "DeleteCourseDistribute?DistributeID={DistributeID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        int DeleteCourseDistribute(string DistributeID);

        //改
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "UpdateCourseDistribute", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        int UpdateCourseDistribute(CourseDistribute model);

        //查u
        [WebGet(UriTemplate = "CourseDistributeFindAll", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<CourseDistribute> CourseDistributeFindAll();
        #endregion

        #region  查询该课程是否被用户收藏  YM 2017.5.10
        [OperationContract]
        [WebGet(UriTemplate = "CheckHasCollect?UserID={UserID}&CourseID={CourseID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string CheckHasCollect(string UserID, string CourseID);
        #endregion

        #region  删除笔记功能 YM 2017.5.10
        [OperationContract]
        [WebGet(UriTemplate = "DeleteNote?NoteID={NoteID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string DeleteNote(string NoteID);
        #endregion 

        #region 取消点赞 YM 2017.5.10
        [OperationContract]
        [WebGet(UriTemplate = "CancelFavor?TypeID={TypeID}&UserID={UserID}&MainID={MainID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string CancelFavor(int TypeID, string UserID, string MainID);
        #endregion

        #region 认证信息   实名认证职业认证2017.5.17
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "AuthenticationInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string AuthenticationInfo(CertificateAudit model);
        #endregion

        #region 通知公告  YM 2017.5.11
        [OperationContract]
        [WebGet(UriTemplate = "MessageFind?MessageType={MessageType}&ReceiverID={ReceiverID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<Message> MessageFind(int MessageType, string ReceiverID);
        #endregion

        #region 查询审核状态  YM2017.5.19
        [OperationContract]
        [WebGet(UriTemplate = "GetAuditStatus?ReguserID={ReguserID}&CertificateType={CertificateType}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string  GetAuditStatus(string ReguserID,int CertificateType);


        #endregion

        #region 查询用户点赞  YM2017.5.19   2017.6.20 改成post，并且返回传过来的model
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "GetFavour", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string  GetFavour(IList<Favour> model);

        //[OperationContract]
        //[WebGet(UriTemplate = "GetFavour?UserID={UserID}&MainID={MainID}&TypeID={TypeID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        //IList<Favour> GetFavour(string UserID, string  MainID,int TypeID);
        #endregion

        #region 短信验证码  YM 2017.5.11  
        [OperationContract]
        [WebGet(UriTemplate = "SMSCodeSend?ReguserID={ReguserID}&PhoneNumber={PhoneNumber}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string SMSCodeSend(string ReguserID, string PhoneNumber);
        #endregion

        #region 短信验证  
        [OperationContract]
        [WebGet(UriTemplate = "SMSVerification?ReguserID={ReguserID}&PhoneNumber={PhoneNumber}&code={code}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string SMSVerification(string ReguserID, string PhoneNumber, string code);
        #endregion

        #region 新课程推送 YM 2017.5.11  
        [OperationContract]
        [WebGet(UriTemplate = "NewCourseDistribute?DistributeType={DistributeType}&ReceiverID={ReceiverID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<CourseDistribute> NewCourseDistribute(int DistributeType, string ReceiverID);
        #endregion

        #region 个人主页（笔记） YM 2017.5.11
        [OperationContract]
        [WebGet(UriTemplate = "GetNotes?ReguserId={ReguserId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<Notes> GetNotes(string ReguserId);
        #endregion

        #region 根据课程CourseID,userid得到这个人当前课程章节的视频PlayTime和LastPlayChapterID
        [OperationContract]
        [WebGet(UriTemplate = "GetPlayTimeByCourseIdUserId?CourseId={CourseId}&UserId={UserId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        CourseRecord GetPlayTimeByCourseIdUserId(string CourseId,string UserId);
        #endregion

        #region 根据科室id获取医院信息
        // FindHospByDepId(string DepId)

        [OperationContract]
        [WebGet(UriTemplate = "FindHospByDepId?DepId={DepId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        aers_tbl_hospital FindHospByDepId(string DepId);
        #endregion

        #region 2017-6-5 获取积分


        [OperationContract]
        [WebGet(UriTemplate = "IntegralFindByUserID?UserId={UserId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<Integral> IntegralFindByUserID(string UserID);

        [OperationContract]
        [WebGet(UriTemplate = "IntegralTotalFindByUserID?UserId={UserId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string IntegralTotalFindByUserID(string UserID);

        #endregion

        #region 2017-6-5 获取学分

        [OperationContract]
        [WebGet(UriTemplate = "CreditRecordFindByUserID?UserId={UserId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        IList<CreditScore> CreditRecordFindByUserID(string UserID);

        [OperationContract]
        [WebGet(UriTemplate = "CreditRecordTotalByUserID?UserId={UserId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string CreditRecordTotalByUserID(string UserID);


        #endregion

        #region 2017.6.15 YM消息标记已读
        [OperationContract]
        [WebGet(UriTemplate = "MsgReaded?MessageID={MessageID}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string MsgReaded(string MessageID);
        #endregion

        #region 2017.6.26查询用户是否已经注册过
        
        [OperationContract]
        [WebGet(UriTemplate = "IsHasPhoneNumber?LoginName={LoginName}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string IsHasPhoneNumber(string LoginName);
        #endregion


        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Register", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        string Register(RegSMS model);
        #endregion







    }





}
