/**
  * Name:Aersysm.SqlMapDao-aers_tbl_eventsresume
  * Author: banshine
  * Description: aers_tbl_eventsresume Dao层 
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
    public partial class aers_tbl_eventsresumeSqlMapDao : BaseSqlMapDao
    {
        /// <summary>
        /// 检查当前月份的上月是否已经存上报过无事件
        /// </summary>
        /// <returns></returns>
        public bool checkevents()
        {
            bool res = false;
            String stmtId = "aers_tbl_eventsresume_FindByDate";
            DateTime dt = DateTime.Now.AddMonths(-1);
            Hashtable ht = new Hashtable();
            ht.Add("HappenedDate", dt.Year + "-" + dt.Month + "-01 00:00:00");
            aers_tbl_eventsresume obj = ExecuteQueryForObject<aers_tbl_eventsresume>(stmtId, ht);
            if (obj != null)
                res = true;
            return res;
        }

        public bool checkonekey(string uid,string yue)
        {
            bool res = false;
            String stmtId = "aers_tbl_eventsresume_FindOnekeyByDate";
            DateTime dt = DateTime.Now.AddMonths(-1);
            Hashtable ht = new Hashtable();
            ht.Add("HospDepId", uid);


            DateTime time = DateTime.Now.AddMonths(-1);
            if (!string.IsNullOrEmpty(yue))
            {
                time = Convert.ToDateTime(yue);
            }
 
            DateTime time1 = new DateTime(time.Year, time.Month, 1);
            DateTime time2 = new DateTime(time.Year, time.Month, 1).AddMonths(1);

            ht.Add("HappenedDate", time1);
            IList<aers_tbl_eventsresume> list = ExecuteQueryForList<aers_tbl_eventsresume>(stmtId, ht);

            list = list.Where(o => o.HappenedDate < time2).ToList();

            if (list.Count>0)
                res = true;
            return res;
        }

        public string  checkonekeynew(string uid, string yue)
        {
            bool res = false;
            String stmtId = "aers_tbl_eventsresume_FindOnekeyByDate";
            DateTime dt = DateTime.Now.AddMonths(-1);
            Hashtable ht = new Hashtable();
            ht.Add("HospDepId", uid);


            DateTime time = DateTime.Now.AddMonths(-1);
            if (!string.IsNullOrEmpty(yue))
            {
                time = Convert.ToDateTime(yue);
            }

            DateTime time1 = new DateTime(time.Year, time.Month, 1);
            DateTime time2 = new DateTime(time.Year, time.Month, 1).AddMonths(1);

            ht.Add("HappenedDate", time1);
            IList<aers_tbl_eventsresume> list = ExecuteQueryForList<aers_tbl_eventsresume>(stmtId, ht);

            list = list.Where(o => o.HappenedDate < time2).ToList();

            return "";
            //if (list.Count > 0)
            //    res = true;
            //return res;
        }
        public IList<aers_tbl_eventsresume> GetEventsresumeList()
        {

            String stmtId = "aers_tbl_eventsresume_FindAll";
            Hashtable ht = new Hashtable();
            IList<aers_tbl_eventsresume> list = ExecuteQueryForList<aers_tbl_eventsresume>(stmtId, ht);
            return list;
        }

        public string instertbyonekey(aers_tbl_eventsresume model)
        {
            try
            {
                //if (!checkevents())
                //{
                    model.EveresId = new aers_sys_seedSqlMapDao().GetMaxID("eventsresume");
                    DateTime dt = DateTime.Now.AddMonths(-1);
                    model.OperatorDate = dt;
                    model.HappenedDate = Convert.ToDateTime(dt.Year+"-"+dt.Month);
                    model.SendtoDate = DateTime.Now;
                    model.OperatorDate = DateTime.Now;
                    model.Remark = "本月无不良事件（来源：一键上报）";
                    model.IsFlag = 0;
                    model.ExamineState = "0";//状态标识为 0 未审核
                    model.FeedbackState = 0;
                    model.EveresLevel = "--";
                    model.EveresName = "151";
                    String stmtId = "aers_tbl_eventsresume_Insert";
                    ExecuteInsert(stmtId, model);
                    return model.EveresId;
                //  }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string instert(aers_tbl_eventsresume model)
        {
            try
            {
                model.EveresId = new aers_sys_seedSqlMapDao().GetMaxID("eventsresume");
                DateTime dt = DateTime.Now;
                model.OperatorDate = dt;
                model.IsFlag = 0;


                model.FeedbackState = 0;
                String stmtId = "aers_tbl_eventsresume_Insert";
                ExecuteInsert(stmtId, model);


                return model.EveresId;
            }
            catch (Exception ex)
            {

                throw;
            }
 
        }

        public string Update(aers_tbl_eventsresume model)
        {

            DateTime dt = DateTime.Now;
            model.OperatorDate = dt;
            model.IsFlag = 0;
            model.FeedbackState = 0;
            String stmtId = "aers_tbl_eventsresume_Update";
            ExecuteUpdate(stmtId, model);
            return model.EveresId;
        }

        public string Delete(string eid)
        {

            Hashtable ht = new Hashtable();
            ht.Add("EveresId", eid);
            String stmtId = "aers_tbl_eventsresume_Delete";
            return ExecuteDelete(stmtId, ht).ToString();

        }

        public IList<aers_tbl_eventsresume> FindDepByRud(string uid, string state)
        {
            try
            {
                String stmtId = "aers_tbl_eventsresume_FindDepByRud";
                Hashtable ht = new Hashtable();
                ht.Add("ReguserId", uid);
                ht.Add("ExamineState", state);
                return ExecuteQueryForList<aers_tbl_eventsresume>(stmtId, ht);
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }


        public IList<aers_tbl_eventsresume> FindDepByData(string uid)
        {
            try
            {
                String stmtId = "aers_tbl_eventsresume_FindDepByRudData";
                Hashtable ht = new Hashtable();
                ht.Add("ReguserId", uid);
                return ExecuteQueryForList<aers_tbl_eventsresume>(stmtId, ht);
       
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public IList<aers_tbl_eventsresume> FindDepByDataZ(string uid)
        {
            try
            {
                String stmtId = "aers_tbl_eventsresume_FindDepByRudDataZ";
                Hashtable ht = new Hashtable();
                ht.Add("ReguserId", uid);
                return ExecuteQueryForList<aers_tbl_eventsresume>(stmtId, ht);

            }
            catch (Exception ex)
            {

                return null;
            }

        }



        public IList<aers_tbl_eventsresume> FindHospByRud(string uid, string state)
        {
            String stmtId = "aers_tbl_eventsresume_FindHospByRud";
            Hashtable ht = new Hashtable();
            ht.Add("ReguserId", uid);
            ht.Add("ExamineState", state);
            return ExecuteQueryForList<aers_tbl_eventsresume>(stmtId, ht);
        }

        public IList<aers_tbl_eventsresume> FindHospByData(string uid)
        {
            String stmtId = "aers_tbl_eventsresume_FindHospByData";
            Hashtable ht = new Hashtable();
            ht.Add("ReguserId", uid);
            return ExecuteQueryForList<aers_tbl_eventsresume>(stmtId, ht);
        }

        public IList<aers_tbl_eventsresume> FindHospByHospId(string HospId, string state)
        {
            String stmtId = "aers_tbl_eventsresume_FindHospByHospId";
            Hashtable ht = new Hashtable();
            ht.Add("HospId", HospId);
            ht.Add("ExamineState", state);
            return ExecuteQueryForList<aers_tbl_eventsresume>(stmtId, ht);
        }

        


        public IList<aers_tbl_eventsresume> FindAllByRud(string state, DateTime StaTime)
        {
            String stmtId = "aers_tbl_eventsresume_FindAllByRud";
            Hashtable ht = new Hashtable();
            ht.Add("ExamineState", state);
            ht.Add("HappenedDate", StaTime);
            return ExecuteQueryForList<aers_tbl_eventsresume>(stmtId, ht);//
        }

        public IList<aers_tbl_eventsresume> FindAllByRudHosp(string state,string hid,DateTime StaTime)
        {
            String stmtId = "aers_tbl_eventsresume_FindAllByRudHosp";
            Hashtable ht = new Hashtable();
            ht.Add("ExamineState", state);
            ht.Add("HappenedDate", StaTime); 
            ht.Add("HospId", hid);
            return ExecuteQueryForList<aers_tbl_eventsresume>(stmtId, ht);//
        }

        public DataSet FindAllByRudCount(string roles,string state, string uid)
        {
            String stmtId = "";
            Hashtable ht = new Hashtable();
            switch (roles)
            {
                case "145":
                    //护理部
                    stmtId = "aers_tbl_eventsresume_FindAllByRudHosp";
                    ht.Add("HospId", uid);
                    break;
                case "146":
                    //护士长
                    stmtId = "aers_tbl_eventsresume_FindAllByRudDep";
                    ht.Add("HospdepId", uid);
                    break;
                case "147":
                    //省厅
                    stmtId = "aers_tbl_eventsresume_FindAllByRud";
                    break;
                case "148":
                    //区域
                    stmtId = "aers_tbl_eventsresume_FindAllByRud";
                    break;
                default:
                    break;
            }
            ht.Add("HappenedDate", new DateTime(DateTime.Now.Year, 1, 1));
            ht.Add("ExamineState", state);
            return ExecutQueryForDataSet(stmtId, ht);
        }

        public void UpdateState(string feedback, string examine, string uid)
        {
            String stmtId = "aers_tbl_eventsresume_Update_State";
            Hashtable ht = new Hashtable();
            ht.Add("EveresId", uid);
            ht.Add("FeedbackState", feedback);
            ht.Add("ExamineState", examine);
            ExecuteUpdate(stmtId, ht);
        }

        public aers_tbl_eventsresume Find(string id)
        {
            String stmtId = "aers_tbl_eventsresume_Find";
            Hashtable ht = new Hashtable();
            ht.Add("EveresId", id);
            return ExecuteQueryForObject<aers_tbl_eventsresume>(stmtId, ht);
        }

        public DataSet Findonkeyinfos(string id)
        {
            String stmtId = "aers_tbl_eventsresume_FindOnekeyInfo";
            Hashtable ht = new Hashtable();
            ht.Add("EveresId", id);
            return ExecutQueryForDataSet(stmtId, ht);
        }


 
    }

}

