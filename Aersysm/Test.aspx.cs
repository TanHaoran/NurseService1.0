using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aersysm.Domain;
using Aersysm.Persistence;
using System.Data;
using Services;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Net;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;

namespace Aersysm

{
  
    public partial class Test : System.Web.UI.Page
    {
        #region 导入以前的医院信息
        private IList<aers_tbl_hospital>  GetHospital()
        {
            aers_tbl_events_ycSqlMapDao atdao = new aers_tbl_events_ycSqlMapDao();  //这命名。。。
            IList<aers_tbl_hospital> codelist = atdao.hospitalFindAll();
            return codelist;
        }

        private void AddHospitalInfo()
        {
            var lidt= GetHospital();
            HospitalSqlMapDao hdao = new HospitalSqlMapDao();
            foreach (var item in lidt)
            {
               
                Hospital h = new Hospital();
               // h.HospitalId = new aers_sys_seedSqlMapDao().GetMaxID("Hospital");
                h.HospitalId = item.HospId;
                h.Name = item.HospName;
                h.Phone =item.Phone;
                h.Contact = item.Contact;
                h.Grade =item.Grade;
              
                hdao.Addhospital(h);
            }
        }
        #endregion

        #region 导入以前的科室信息
        private IList<aers_tbl_hospdep> GetDepartment()
        {
            aers_tbl_hospdepSqlMapDao ad = new aers_tbl_hospdepSqlMapDao();
            IList<aers_tbl_hospdep> codelist = ad.hospdepFindAll();
            return codelist;
        }
        private void AddDepartment()
        {
            var listd = GetDepartment();
            DepartmentSqlMapDao ddao = new DepartmentSqlMapDao();
            foreach (var item in listd)
            {
                Department d = new Department();
                d.DepartmentId = item.HospdepId;
                d.HospitalId = item.HospId;
                d.Name = item.HospdepName;
                ddao.Adddepartment(d);
            }
        }
        #endregion

        #region 导入以前的账号信息   用户名，密码注册Id
        private IList<aers_tbl_registereduser> GetUser()
        {
            aers_tbl_registereduserSqlMapDao rdao = new aers_tbl_registereduserSqlMapDao();
            IList<aers_tbl_registereduser> userList = rdao.FindAllUser();
            return userList;
        }

        
        private void AddHospitalUserInfo()
        {
            UserService userservice = new UserService();
            var listUser = GetUser();
           
            
            foreach (var item in listUser)
            {
//------------------------------------------------------------------------------------------------------------------------------------------------------
                //以前的注册表信息导入到授权表里面
                var registerdata = userservice.GetUserregiserId();
                UserauthsSqlMapDao urd = new UserauthsSqlMapDao();    //授权表导入数据   登陆名，密码，以前的注册Id
                Userauths ur = new Userauths();
                ur.AuthsId = new aers_sys_seedSqlMapDao().GetMaxID("Userauths");
                ur.LoginLastTime = Common.StrToDateTime();
                ur.LoginNumber = item.LoginName;
                ur.LoginType = 0; //院内账号类型是0；
                ur.Password = item.Password;
                ur.RegisterId = registerdata;
                ur.Verified = 0;
                ur.ReguserId = item.ReguserId;
                urd.Adduserauths(ur);


                //------------------------------------------------------------------------------------------------------------------------------------------------------
                aers_tbl_staffSqlMapDao sdao = new aers_tbl_staffSqlMapDao();
                aers_tbl_staff s = new aers_tbl_staff();
                var sdata = sdao.FindByRUid(item .ReguserId);
                //根据注册ID取出以前用户表里面的数据  科室id,姓名，性别，角色


                userregisterSqlMapDao urdao = new userregisterSqlMapDao();  //注册表导入姓名
                userregister urr = new userregister();
                urr.RegisterId = registerdata;  //注册Id
                urr.Name = sdata.Name;  //添加姓名
                urdao.Updateuserregister(urr);

                userbasicinfoSqlMapDao ubdao = new userbasicinfoSqlMapDao();  //基本信息表导入性别
                UserBasicInfo ub = new UserBasicInfo();
                ub.RegisterId = registerdata;
                if (sdata.Sex == "107")
                {
                    ub.Sex = "女";
                }
                else if (sdata.Sex == "108")
                {
                    ub.Sex = "男";
                }
                else
                {
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

                //h.HospitalId = item.HospId;
                //h.Name = item.HospName;

                urrrdao.Updateuserrelrecord(urrr);
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //AddHospitalUserInfo();     
            //var ds = Common.DESEncrypt("wajb","12345678","12345678");
            //var da = ds;
            //var dsa = Common.DESDecrypt(ds ,"12345678","12345678");
            //var dfsad = dsa;
            //Aers service = new Aers();
            UserService cuserservice = new UserService();
                
            //var result = cuserservice.GetAllToken("123");
            //var d = result;

            var dsa = cuserservice.GetCountryCodeAll();
            var sd = dsa;
            //Emuser eu = new Emuser();
            //eu.EmNickName = "001";
            //eu.EmRegisterId = "001";
            //eu.EmPassword = "123456";
            //var ds= userservice.HXregiste(eu);
            //var dsa = ds;
            //var das = userservice.GetHospitalNameAll();
            //var d = das;
            ////string[] ss = { "111" };
            ////var dsa = Common.PushMsgByAliasId("rrrrrrrrrrrrrrddddd", ss, "170976fa8ab5fc3cda2");
            //Qq qq = new Qq();
            //qq.Id = "0000000119";
            //qq.NickName = "😀🐷";
            //qq.City = "111";
            //qq.Phone = "111";
            //qq.Province = "111";
            //var dsa = userservice.emojiTest(qq);

            //var da = userservice.GetEmoji("0000000154");
            //var das = da;
            //  var dsa= userservice.LoginStatus("170976fa8ab5fc3cda2", "0000001279",1);

            // var dsa = Common.PushMsg("单个测试", "170976fa8ab5fc3cda2", "0000001225");
            //var dsa = Common.PushMsgReg("单个测试", "0000001225");
            //var ds = dsa;
            // var ds = userservice.GetUserBindInfo("0000001158");
            // var das = userservice.jPush("111");
            //var dfsa= Common.GetEnPassword("0000000430");  //加密

            //var d = dfsa;

            //var w = Common.GetDePassword(d);
            //var fdas = w;
            //  var dsa = userservice.GetXHGroupInfoList("0000000430");
            //var da = userservice.SendMsgToGroupmember("测试", "0000000430", "24344015208450");
            //清空数据库
            //  var clearData = userservice.ClearData();

            //导入以前数据
            // AddHospitalInfo();  //导入以前的医院信息

            //AddDepartment();//导入科室信息

            //  AddHospitalUserInfo();//  导入人员信息


            //  var dfsa=  userservice.GetEMFriend("0000000429");
            //// 获取全部国家编码
            //var GetCountryCodeAll = userservice.GetCountryCodeAll();

            //var dd = Common.GetRSA();
            //var ddd = dd;
            //  var dsaf = userservice.GetUserFirstInfoByPhone("13002909620"); 

            // var da = userservice.GetNotice(1,1,"1","2");
            //  var ras = Common.RSA();
            //  var dd = userservice.GetQQNickNameById("0000000024");



            //   var DSA = userservice.CreatHXUser();

            //Qq q = new Qq();
            //q.OpenId = "1111";
            //var fd = userservice.ThirdPartLoginQQ(q);


            // var dfsa=  userservice.GetQRCodeById("0000000809");
            // var dsa = Common.SaveCodeFile("浩然加班");
            //var FDSA = userservice.GetDepartmentList("0000000152");
            //var DSA = FDSA;

            //  108°56′32.60
            //    N34°15′39.59
            //string lng = "34.274";
            //string lat = "108.943";
            //// 39.915,116.404
            //var dfsa = userservice.GetAddressByLngLat(lat, lng);
            //var fdsa = dfsa;
            //var dfsa= userservice.GetContactByHopDepId("0000000152", "0000000002");
            //var df = dfsa;


            //ViewSMS dd = new ViewSMS();
            //dd.RegisterId = "13002909620";
            //dd .Phone=""

            //var fdsa = userservice.IsOKSMSCode();
            //var dd = GetHospital();
            //var dfsa = dd;
            //    userservice.AddHospital();

            #region 以前
            // var fds= userservice.GetAddressByLngLat();
            //view_Login lg = new view_Login();
            //lg.Name = "test_hsz";
            //lg.Pwd = "123456";

            //var dsa = service.UserLogin(lg);
            //var jkls = service.ForgetPwd("18625219900", "688861","123456");

            //var fdsa = jkls;


            //var dfsa = service.MsgReaded("0000000013");
            //var dfsad = dfsa;
            //var dsad = service.GetNotes("ru00000001");
            //var dfsafa = dsad;
            //ResModel<view_tbl_registereduser> dfs = service.UserLogin(lg);


            //var dsafd = service.findeventsyhByEid("0000004802", dfs.model.ReUId, dfs.model.LoginKey);

            //var  dasdasdasdas = service.GetAuditStatus("ru00000509",2);

            //var fdsfd = dasdasdasdas;

            //IList<Answer> list = service.GetAnswerByUserCourseID("ru00000507", "0000000001");

            //ResList <Answer> aaa = service.AddAnswerList(list);

            //  var fd= Aers.ChangePwd();


            // string aaa = service.SaveHistoryCourse("ru00000507", "0000000001", "0000000001", "50");

            //Integral jf = new Integral();


            //jf.UserID = "ru00000001";
            //jf.TrainingID = "0000000001";

            //jf.Grade = "2";
            //jf.ModifyDate = DateTime.Now;
            //jf.OperatorID = "ru00000001";
            //jf.HID = "hp00000001";

            //string aaa=service.IntegralInsert(jf);


            // IList<StudySchedule> list = service.StudyScheduleFindByHospdepId("0000000001");
            //  IList<Course> list = service.CourseFindOrderByCourseType("0000000001");

            //DataSet ds1 = service.GetEveCountByRegionJidu(2016);  



            //  var ddd = service.CourseFindOrderByCourseType("0000000002");


            // string adsad5056456sss = service.GetEventddzcReport("","","2016-1-1", "2017-1-1");
            //adsad5056456sss = service.GetEventZcReport("2016-1-1", "2017-1-1", "", "");
            //var  adsad5056456sss = service.GetEventGlReport("2016-1-1", "2017-1-1", "", "");
            //adsad5056456sss = service.GetEventGyReport("2016-1-1", "2017-1-1", "", "");


            // var  adsad5056456sss = service.GetEventYcReport("2016-1-1", "2017-1-1", "", "");
            //  var   adsad5056456sss = service.GetEventZyblReport("2016-1-1", "2017-1-1", "", "");



            //DataSet ds = service.GetEveresLevelCountByRegionJidu();

            //  ResModel<view_tbl_registereduser> op = service.UserLogin("13892353633", "2168713");

            //IList<aers_tbl_eventsresume> lilsilsi = service.findndepreport(op.model.ReUId, "2016-1-1", "2017-1-1", "", "");




            //string dsadsa11111 = service.FindGroupByName("2016-1-1");


            //aers_tbl_events_ddzc ddd = service.findeventsddzcByEid("0000001602");
            //ddd.EveresId = "";
            //ddd.FillStaff = "ru00000001";
            //ddd.HospId = "hp00000001";
            //ddd.OperatorID= "ru00000001";
            //string cccc = service.EventDdzcReport(ddd);


            //string dasdasdasdasdas = service.GetEventQtByCount("2016-1-1", "", "", "", "", "129");
            // string dsadsa111114343 = service.findnurswaitData("ru00000002","2016-11-1");





            //////按区域按月份统计事件上报数量  
            //string aaadsadsa = service.GetEventsresumeByCount("2016-1-1", "", "", "1", "","");






            //string dasdasdasdas = service.GetEventQtByCount("2016-1-1", "", "hp00000001", "1", "");



            //aers_tbl_events_yc ddd = service.findeventsycByEid("0000001582");
            //ddd.EveresId = "";
            //ddd.FillStaff = "ru00000001";
            //ddd.HospId = "hp00000001";
            //ddd.OperatorID= "ru00000001";
            //string cccc = service.EventYcReport(ddd);


            //
            // IList<aers_tbl_eventsresume> list = service.findndepevent(op.model.ReUId, "2016-6-1", "2017-1-1", "", "");//护理部权限 - 查询 通过审核事件
            // var  list = service.findndepnoevent("ru00000001", "2016-6-1", "2017-1-1", "129", "", "ru00000001", "face5299-ea06-49a7-942e-4c0475597ed2");//护理部权限 - 查询 未通过审核事件
            // list = service.findndepwait(op.model.ReUId, "2016-6-1", "2017-1-1", "", ""); //护理部权限 - 查询 正在审核事件
            //list = service.findndepreport(op.model.ReUId, "2016-6-1", "2017-1-1", "", "");
            //  var dfsd = list;

            //string asdasxsxas = service.GetEventddzcReport("2016-11-1", "2016-11-30", "", "");

            //bool dasdsadasdsadasdas = service.CheckOnekey("ru00000291","2016-11-1");
            //string dasdsadas = service.findnurswaitData("ru00000291", "2016-11-1");




            //string adasdasdasda =service.GetEventsresumeByhospdepCount("hp00000001", "", "2016-11-1", "2016-11-11");

            //aers_tbl_events_yc aaasdsad = service.findeventsycByEid("0000001581"); 






            //string dadsads1 = service.GetEveCountByRegion("2016-1-1");









            //IList<aers_tbl_hospdep> list = service.GetEveCountByhospdep("hp00000002", "2", "2016-10-1");

            //DataSet ds = service.GetEveCountByRegionJidu00000(0);


            //string a0001=service.GetEventsresumeByhospdepCount("hp00000002","131", "2016-1-1", "2016-9-1");





            //string cccc = service.exevent("0000000566","");

            // aers_tbl_events_ddzc ddd = service.findeventsddzcByEid("0000001296")   ;
            // ddd.EveresId = "";
            // ddd.FillStaff = "ru00000001";
            // ddd.HospId = "hp00000001";
            // string cccc = service.EventDdzcReport(ddd);









            //   var  op = service.UserLogin("1632", "123456");

            //string sasasaist = service.findnursnoevent(op.model.ReUId);//护士长权限 - 查询 未通过审核事件
            //sasasaist = service.findnursevent(op.model.ReUId);//护士长权限 - 查询 通过审核事件
            //sasasaist = service.findnurswait(op.model.ReUId);//护士长权限 - 查询 正在审核事件

            //Problem data = new Problem();

            //data.CourseID = "0000000001";
            //data.Title = "评论11111111111";
            //data.UserID = "ru00000001";
            //data.ModifyDate = DateTime.Now;
            //data.FavorCount = 0;
            //string aaaaaa = service.AddProblem(data);

            //注册
            //aers_tbl_staff staff = new aers_tbl_staff();
            //staff.LoginName = "aoteman";
            //staff.pwd = "123456";
            //string create = service.Registeruser(staff);

            //登陆
            //  ResModel<view_tbl_registereduser> op = service.UserLogin("18625210000", "1234567");

            //20170323
            //保存课程观看历史
            // var s = service.SaveHistoryCourse("ru00000507", "0000000001", "0000000001", "445");

            //获取课程观看历史列表
            // var dd = service.CourseHistoryFindByUserID("ru00000507");

            //IList<Course> cclis = service.CourseFindByUserID("ru00000001");

            //string acscscs = service.AddCourseUser("ru00000001", "0000000004");
            //acscscs = service.DeleteCourseUser("ru00000001", "0000000004");

            //  var dsafdsafd = service.GetAuditStatus("");

            //string dsadadadadasda = service.UpdatePwd("ru00000001", "123456", "123456");

            //string acsadsaddsds=service.GetEventsresumeByCount(0,"1");


            //string dasdsadsadsa = service.DeleteHospdep("0000000335");

            //IList<aers_tbl_registereduser> list = service.GetRegistereduserByHospId("hp00000002");

            //aers_tbl_hospdep mod = service.hospdepFindByHospId("hp00000002")[0];

            //mod.HospdepId = "";
            //string sacsadsadasdas=service.Addhospdep(mod);

            // aers_tbl_staff sta = service.FindByRUid("1000");
            //  DataTable dt = new DataTable("USER");
            //dt.Columns.Add("userNo", typeof(string));
            //dt.Columns.Add("usernAME", typeof(string));
            //DataRow dr = dt.NewRow();
            //for (int i = 0; i < 3; i++)
            //{
            //    dr["userNo"] = "1001" + i.ToString();
            //    dr["userName"] = "DataTable" + i.ToString ();
            //}
            //dt.Rows.Add(dr);
            //  dt = null;

            //  string ddd= service.ExportToExcel(dt, "dd", null);
            // string dse = service.ExportToExcel(dt ,"dd");

            //数据分析-事件明细
            // string sjmxexcel = service.findproeventexcel("","","2017-03-02","2017-03-02","","");
            // //var sjmxexcel = service.findproevent("", "", "2017-03-02", "2017-03-02", "", "");

            // //数据分析-按医院统计
            // var S = service.GetEveCountByhospitalexcel("2", "", "2017-03-01");


            //var dsfsfs = Aers.MD5Encrypt64("123");

            //var f = dsfsfs;
            // //数据分析-按事件数量统计
            //  var ddfd = service.GetEventsresumeByCountexcel("2017-01-01","","","","","");

            //  var ddfdr = service.GetEventsresumeByCountexcel("2017-01-01", "", "", "2", "", "");

            // //数据分析-其他事件统计
            // var ddfde = service.GetEventQtByCountexcel("2017-01-01", "", "", "", "", "");

            // //数据分析-暂未列出的其他事件统计
            // var ddddddd = service.FindGroupByNameexcel("2015-07-01");

            // //数据分析-各事件占比统计
            // var dfdfdf = service.GetEventReportexcel("2017-03-01", "2017-03-31", "", "");


            //// 按事件统计 - 跌倒事件
            // var gfdgfsd = service.GetEventddzcReportexcel("2017-03-01", "2017-03-31", "", "");


            // //按事件统计-坠床事件
            // var gfdgfsd1 = service.GetEventZcReportexcel("2017-03-01", "2017-03-31", "", "");

            // //按事件统计-管路事件
            // var gfdgfsd2 = service.GetEventGlReportexcel("2017-03-01", "2017-03-31", "", "");

            // ////按事件统计-给药事件
            // var gfdgfsd3 = service.GetEventGyReportexcel("2017-03-01", "2017-03-31", "", "");

            // ////按事件统计-压疮事件
            //var gfdgfsd4 = service.GetEventYcReportexcel("2016-01-03", "2017-04-30", "", "");

            // //按事件统计-职业暴露
            // var gfdgfsd5 = service.GetEventZyblReportexcel("2017-03-01", "2017-03-31", "", "");

            // //数据分析-科室上报明细
            // var dfdfs = service.GetEventsresumeByhospdepCountexcel("hp00000001","", "2017-03-01", "2017-03-31");

            // //数据分析-已上报未上报科室
            // var dfdfsds = service.GetEveCountByhospdepexcel("hp00000001", "2", "2017-03-01");

            // //首页
            // var fdsafasdfasd = service.GetEventsresumeByCountSYexcel("2017-01-01", "");

            //事件审核-待审核
            // var fdsafasdf = service.findndepwaitexcel("ru00000002", "2017-01-01", "2017-03-30","","");

            // 事件审核 - 已通过
            // var fdsafasfdf = service.findndepeventexcel("ru00000002", "2017-01-01", "2017-03-30", "", "");

            //事件审核-未通过
            // var fdsafsdfasfdf = service.findndepnoeventexcel("ru00000002", "2017-01-01", "2017-03-30", "", "");
            //  string excel = service.ExportExcel("sssssssssss","");
            // ExportToExcel(DataTable dt, string fileName)
            // ToExcel.ExportToExcel(dt ,"dd",null);

            //string axcaxaxaxa = service.GetEventsCounts(op.model.RoleState.ToString(), op.model.ReUId);


            //IList<aers_tbl_hospital> listaaaaaaaaaaa = service.Gethospital("0");

            //  IList<aers_tbl_hospital> listaaaaaaaa = service.GetEveCountByhospital("2","1","8");

            //IList<aers_tbl_hospdep> listaaaaa = service.hospdepFindByHospId("hp00000002");




            //IList<Answer> list = service.GetAnswerByUserCourseID("ru00000001", "0000000001");

            //string acaca = service.AddAnswerList(list);


            //string aaaccaa=service.StaffFindAllByGrade(4);

            //var dsfd = aaaccaa;
            // string aaccvv =service.AddFavor(4, "ru00000507", "0000000409");
            //IList<CourseSection> listaaccc = service.CourseSectionFindByCourseID("0000000001");


            //Notes data1 = new Notes();


            //data1.CourseID = "0000000001";
            //data1.Content = "笔记";
            //data1.UserID = "ru00000001";
            //data1.ModifyDate = DateTime.Now;
            //data1.IsShare = 1;

            //aaaaaa = service.AddNotes(data1);





            // IList<Course> Courseaaaaaa = service.CourseFindOrderBySortField("Recommend", 10);

            // IList<CourseCatalog> listccc = service.CourseCatalog_FindByCourseID("0000000001");

            //IList<CourseSection> listddd = service.CourseSectionFindByCourseID("0000000001");

            //   var fdsad = service.Questions_FindByChapterID("0000000001");

            //IList<Notes> Notesaaaaaa = service.NotesFindByCourseID("0000000001");
            //IList<Problem> Problemaaaaaa = service.ProblemFindByCourseID("0000000001");


            //aers_tbl_staff aaa = service.FindByRUid("ru00000001");

            //aaaaaa = service.GetHospState("ru00000007");  

            //aers_tbl_events_zybl ddd = service.findeventszyblByEid("0000001249");

            //ddd.EventLevel = "333";
            //ddd.FillStaff = "ru00000001";
            //ddd.HospId = "hp00000001";
            //string cccc = service.EventZyblReport(ddd);




            //string aaaaaaa=service.GetEventsCounts(op.model.RoleState.ToString(), op.model.ReUId);



            //IList<Course> Courseaaaaaa = service.CourseFindAll();

            //IList<CourseCatalog> listccc = service.CourseCatalogFindAll();


            //IList<CourseSection> listddd = service.CourseSectionFindAll();

            //int count = 0;
            //foreach (CourseSection item in listddd)
            //{
            //    Course co = Courseaaaaaa.FirstOrDefault(o => o.CourseID == item.CourseID);
            //    if (co != null)
            //    {
            //        string sql = "update CourseSection set ChapterName ='" + co .CourseName+ "'  where  CourseID='"+co.CourseID+"'";

            //        count += new TrainDAL().ExecSQL(sql);
            //    }


            //}





            //修改用户信息
            //aers_tbl_staff staff = new aers_tbl_staff();
            //staff.ReguserId = "ru00000350";
            //staff.StaffId = "0000000355";
            //staff.Address = "西安市";
            //staff.DepId = "0000000009";
            //staff.Name = "房夏玲";
            //staff.OperatorId = "ru00000001";
            //staff.OperatorDate = DateTime.Now;
            //staff.Remarks = "护理部";
            //staff.Phone = "13999999999";
            //staff.Sex = "108";
            //staff.RoleState = 145;

            // string update = service.UpdateUser(staff);




            //修改用户名
            //string UpdateName = service.UpdateLoginName("ru00000350", "18992809673");



            // 修改密码
            //string UpdatePassword = service.UpdatePwd("ru00000350","666666");



            //压疮事件上报
            //aers_tbl_events_yc yc = new aers_tbl_events_yc();
            ////--------修改--------
            //yc.EveresId = "0000000866";
            //yc.EveycId = "0000000124";
            ////--------------------
            //yc.FillStaff = "ru00000001";
            //yc.HospId = "hp00000001";
            //yc.HospDepId = "0000000001";
            //yc.IsFlag = 0;
            //yc.OperatorDate = DateTime.Now;
            //yc.OperatorID = "ru00000001";

            //List<aers_tbl_events_yc_parts> events_ycparts = new List<aers_tbl_events_yc_parts>();
            //aers_tbl_events_yc_parts events_yc = new aers_tbl_events_yc_parts();
            //events_yc.PartsId = "0000000038";
            //events_yc.IsFlag = 0;
            //events_yc.OperatorDate = DateTime.Now;
            //events_yc.OperatorID = "ru00000001";
            //events_ycparts.Add(events_yc);

            //yc.Parts = events_ycparts;

            //string ycEvents = service.EventYcReport(yc);



            // 跌倒坠床事件上报
            //aers_tbl_events_ddzc ddzc = new aers_tbl_events_ddzc();

            ////--------修改-------
            //ddzc.EveresId = "";
            //ddzc.EveddzcId = "";
            ////-------------------

            //ddzc.IsFlag = 0;
            //ddzc.HospId = "hp00000001";
            //ddzc.HospDepId = "0000000001";
            //ddzc.FillStaff = "ru00000001";
            //ddzc.EventLevel = "129";
            //ddzc.OperatorDate = DateTime.Now;
            //ddzc.OperatorID = "ru00000001";

            //List<aers_tbl_events_yc_parts> events_ycparts = new List<aers_tbl_events_yc_parts>();
            //aers_tbl_events_yc_parts events_yc = new aers_tbl_events_yc_parts();
            //events_yc.PartsId = "0000000038";
            //events_yc.IsFlag = 0;
            //events_yc.OperatorDate = DateTime.Now;
            //events_yc.OperatorID = "ru00000001";
            //events_ycparts.Add(events_yc);

            //ddzc.Parts = events_ycparts;

            //string ddzcEvents = service.EventDdzcReport(ddzc);



            //跌倒坠床
            //aers_tbl_events_ddzc ddzc = new aers_tbl_events_ddzc();
            //ddzc.IsFlag = 0;
            //ddzc.OperatorDate = DateTime.Now;
            //ddzc.OperatorID = "ru00000001";
            //string ddzcEvents = service.EventDdzcReport(ddzc);



            //Response.Write(aaaaaaa);

            //bool bl= service.CheckOnekey("ru00000001");

            //op = service.UserLogin("test_hsz", "123456");


            //aers_tbl_events_qt aaaa=service.findeventsqtByEid("0000000738");
            //aaaa.DetailTypeQt = "111";
            //aaaa.EveresId = "";
            //string cccc=service.EventQtReport(aaaa);

            //aers_tbl_eventsresumeDAL dal = new aers_tbl_eventsresumeDAL();

            //string list = "";

            //aers_tbl_events_ddzc ccc = service.findeventsddzcByEid("0000000424");

            //ccc.EveresId = "";
            // var fdsfasd = service.UserLogin("' or 1=1#", "");
            //service.EventDdzcReport(ccc);
            //草稿
            //  var fdsafasfdf = service.findndepeventexcel("ru00000002", "2017-01-01", "2017-03-30", "", "");
            // var fdsafayhtrsfdf = service.findndepreportexcel("ru00000002", "2017-01-01", "2017-03-30", "", "");

            //2017.5.5 Yanming 获取所有科室
            //var fgds= service.BasicsdepFindAll();

            //var dfs = service.GetLoginKey();

            //var d2fs = service.GetLoginKey();

            #region 查询认证状态
            //   var dsfddfsad= service.GetAuditStatus(1,"ru00000513");
            #endregion
            //Favour f = new Favour();
            //f.MainID = "0000000173";
            //f.TypeID = 4;
            //f.UserID = "ru00000510";

            //Favour f1 = new Favour();
            //f1.MainID = "0000000169";
            //f1.TypeID = 4;
            //f1.UserID = "ru000005120";
            //IList<Favour> list = new[] { f,f1 };

            //var dsdfew = service.GetFavour(list);

            #region 根据章节ID查题库
            //  var zjhqwt = service.Questions_FindByChapterID("0000000001");
            #endregion

            #region 新增4张表
            //2017.5.5
            // Message msg=new Message();
            // msg.MessageType = 1;
            // msg.ReceiverID = "2";
            // msg.MessageTitle = "测试增加标题hgf";
            // msg.MessageContent = "测试增加内容gfh";
            // msg.MessageDate = DateTime.Now;
            // msg.MessageSenderID = "200000";
            // //var dsd = service.AddMessage(msg);
            // //msg.MessageID = "0000000010";
            // //var ds = service.UpdateMessage(msg);
            //// var dsfa = service.DeleteMessage("9");
            // var fdsafds = service.MessageFindAll();

            //2017.5.9
            //CertificateAudit ctfa = new CertificateAudit();
            //ctfa.AuditID = "";
            //ctfa.ReguserID = "11";
            //ctfa.CertificateID = "1";
            //ctfa.CertificateType = 1;
            //ctfa.CertificateDate = DateTime.Now;
            //ctfa.CertificatePhoto = "fa";
            //ctfa.CertificatePhotos = "fas";
            //ctfa.SubmitDate = DateTime.Now;
            //ctfa.AuditID = "d";
            //ctfa.AuditDate = DateTime.Now;
            //ctfa.AuditStatus = 1;
            //var fdf = service.AddCertificateAudit(ctfa);
            //var fdsaf = service.DeleteCertificateAudit("");
            //var fdgd = service.UpdateCertificateAudit(ctfa);
            //var tr = service.CertificateAuditFindAll();


            //  SMSMessage smsg = new SMSMessage();
            //smsg.SMSID = "";
            //smsg.ReguserID = "1";
            //smsg.PhoneNumber = "110";
            //smsg.VerificationCode = "000";
            //smsg.SMSSendTime = DateTime.Now;
            //smsg.SendStatus = 1;
            //var fdsf = service.AddSMSMessage(smsg);
            //var gf = service.DeleteSMSMessage("");
            //var gfgh = service.UpdateSMSMessage(smsg);
            //var gfdg = service.SMSMessageFindAll();


            //CourseDistribute cdb = new CourseDistribute();
            //cdb.DistributeID = "";
            //cdb.CourseID = "11";
            //cdb.DistributeType = 1;
            //cdb.ReceiverID = "11";
            //cdb.StartTime = DateTime.Now;
            //cdb.EndTime = DateTime.Now;
            //cdb.DistributorID = "12";
            //cdb.DistributeTime = DateTime.Now;

            //var fgdgfd = service.AddCourseDistribute(cdb);
            //var gfdgfgh = service.DeleteCourseDistribute("");
            //var gfdgfdsgr = service.UpdateCourseDistribute(cdb);
            //var gfyhtr = service.CourseDistributeFindAll();
            #endregion

            #region 查询该课程是否被用户收藏
            // var ddd = service.CheckHasCollect("ru00000319", "ru00000319");
            #endregion

            #region 删除笔记功能
            // var dgfdsg = service.DeleteNote("0000000193");
            #endregion

            #region 取消点赞
            // var fdsae = service.CancelFavor(1,"","");
            #endregion

            #region 认证信息
            //  var fdsfdae = service.RealNameAuthentication("", 1, "", "");
            //CertificateAudit ca = new CertificateAudit();
            //ca.ReguserID = "123";
            //ca.CertificateID = "1234";
            //ca.CertificateType = 1;
            //var smrz = service.AuthenticationInfo(ca);
            #endregion

            #region 通知公告  YM2017.5.11
            //var fdsfdase = service.MessageFind(1, "2");
            //var dd = fdsfdase;
            #endregion

            #region 短信验证码  YM 2017.5.11
            //  var fdsfddsae = service.SMSCodeSend("ru00000510","18625219900");
            #endregion

            #region 短信验证  
            // var fdsfddewesae = service.SMSVerification("","","");
            #endregion

            #region 新课程推送 YM 2017.5.11  
            //  var fdsfdddssae = service.NewCourseDistribute(1,"11");
            //var ds = fdsfdddssae;
            #endregion

            #region 个人主页（笔记） YM 2017.5.11
            // var fdsdfddsae = service.GetNotes("");
            #endregion

            #region YM 2017.5.17根据科室ID获取医院信息
            //  var dddd = service.FindHospByDepId("0000000001");
            #endregion


            //HttpContext DDD = new HttpContext();
            //ImgHandler.ProcessRequest(DDD);


            #endregion



            //#region 现在

            //根据手机号获取验证码
            //  var GetSMSCodeByPhone = userservice.GetSMSCodeByPhone("18709269196");


            //验证验证码
            //ViewSMS v = new ViewSMS();
            //v.Code = "9923";
            //v.Phone = "110";
            //var ddd = userservice.IsOKSMSCode(v);


            //登录
            //ViewRegister v = new ViewRegister();
            //v.Phone = "13002909620";
            //v.Password = "123456";
            //var ddfs = userservice.Login(v);


            //根据注册id获取用户信息
            //var aa = userservice.GetUserReginfoById("0000000014");

            //设置昵称
            //  userregister u = new userregister();
            //  u.RegisterId = "0000000014";
            //  u.Avatar = "avgfdg";
            //  u.Name = "ngsdfg";
            ////  u.NickName = "na";
            //  u.Password = "123";
            //  u.Phone = "123456789"; 
            //  var fdsfd = userservice.SetNickNameById(u);



            //UserBasicInfo ubf = new UserBasicInfo();
            //ubf.Address = "11";
            //ubf.Age = 1;
            //ubf.Birthday = DateTime.Now;
            //ubf.City = "11";
            //ubf.Education = "11";
            //ubf.EMail = "11";
            //ubf.IDCard = "11";
            //ubf.MeritalStatus = "11";
            //ubf.Name = "11";
            //ubf.Nation = "11";
            //ubf.Phone = "11";
            //ubf.Province = "11";
            //ubf.QQ = "11";
            //ubf.Region = "11";
            //ubf.RegisterId = "0000000016";

            //var dfsad= userservice.Updateuserbasicinfo(ubf);

            //ViewResetPassword vp = new ViewResetPassword();
            //vp.PasswordNew = "147741";
            //vp.RegisterId = "0000000016";
            //vp.PasswordOld = "123456";
            //var df = userservice.ResetPassword(vp);

            //  Feedback f = new Feedback();
            //  f.Content = "测试";
            //  f.RegisterId = "0000000016";
            ////  f.ServiceTime

            //  var dfds = userservice.AddfeedbackInfo(f);

            //var fdsafd = userservice.GetReleaseversionInfo();

            //var d = DateTime.Now.Date;



            //var df = ConvertDateTimeInt(d);

            //var fdsa = GetTime(df.ToString());



            // DateTime d = new DateTime();


            //  var fdsa = DateTime.Today;



            // var g = ConvertDateTimeInt(fdsa);
            //  var tr = GetTime(g);



            //var r = fdsa.ToOADate();
            //var rr = fdsa.ToShortDateString();
            //var eew = fdsa.ToLongDateString();
            //var ew = fdsa.Ticks;
            //var ds = d.ToFileTimeUtc();
            //var fds = GetTime(Date(1498867200000 + 0800));

            //  var dfsa = (DateTime.Today.AddDays(-1) - DateTime.Parse("1970/1/1")).TotalMilliseconds;

        }
    }
}