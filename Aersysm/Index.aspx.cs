using Aersysm.Domain;
using Aersysm.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Services.admin
{
    public partial class Index : System.Web.UI.Page
    {

        aers_tbl_eventsresumeDAL dal = new aers_tbl_eventsresumeDAL();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {


           
                DataSet ds = dal.GetEventsresumeByhospital();

                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentColor= this.style.backgroundColor;this.style.backgroundColor='#33CCFF'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentColor");
       
            }
        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            DataSet ds = dal.GetEventsresumeByhospital();

            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }

        protected void btn2_Click(object sender, EventArgs e)
        {
            aers_tbl_events_ycSqlMapDao dal = new aers_tbl_events_ycSqlMapDao();
            IList<aers_tbl_hospital> listhosp = dal.hospitalFindAll();



            aers_tbl_eventsresumeSqlMapDao dao = new aers_tbl_eventsresumeSqlMapDao();
            IList<aers_tbl_eventsresume> list = dao.GetEventsresumeList().OrderByDescending(o=>o.EveresId).ToList();


            foreach (aers_tbl_eventsresume item in list)
            {
                aers_tbl_hospital hosp = listhosp.FirstOrDefault(o => o.HospId == item.HospId);
                if (hosp != null)
                {
                    item.HospId = hosp.HospName;
                }
            
            }

            GridView1.DataSource = list;
            GridView1.DataBind();

        }
    }
}