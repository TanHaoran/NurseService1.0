using Aersysm.Domain;
using Aersysm.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Services.admin
{
    public partial class EveresList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SetPageData();
            }
        }


        public void SetPageData()
        {
            
            IList<aers_tbl_hospital> listhosp = new aers_tbl_events_ycSqlMapDao().hospitalFindAll();

            IList<aers_tbl_hospdep> listdep = new aers_tbl_hospdepSqlMapDao().hospdepFindAll();

            IList<aers_tbl_eventsresume> list = new aers_tbl_eventsresumeSqlMapDao().GetEventsresumeList().OrderByDescending(o => o.EveresId).ToList();

            IList<aers_sys_statecode> listcode = new aers_sys_statecodeSqlMapDao().FindAll();

            foreach (aers_tbl_eventsresume item in list)
            {


                aers_sys_statecode code = listcode.FirstOrDefault(o => o.ECodeValue == item.EveresName);
                if (code != null)
                {
                    item.EveresName = code.ECodeTag;
                }

                aers_tbl_hospital hosp = listhosp.FirstOrDefault(o => o.HospId == item.HospId);
                if (hosp != null)
                {
                    item.HospId = hosp.HospName;
                }
                aers_tbl_hospdep dep = listdep.FirstOrDefault(o => o.HospdepId == item.HospDepId);
                if (dep != null)
                {
                    item.HospDepId = dep.HospdepName;
                }

                switch (item.ExamineState.ToString())
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

            GridView1.DataSource = list;
            GridView1.DataBind();
        }



        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentColor= this.style.backgroundColor;this.style.backgroundColor='#33CCFF'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentColor");

            }
        }
    }


   
}