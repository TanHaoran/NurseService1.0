using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using Aersysm;
using System.ServiceModel;

namespace CreditWeb
{
    public partial class adminSiteMaster : System.Web.UI.MasterPage
    {

        public static void Main(string[] args)
        {
            try
            {
                //  MMIPService service = new MMIPService();
                Aers service = new Aers();
                IAers iaers = new Aers();
                


                //test1(service);

                Console.WriteLine("服务启动中。。。");
                ////  查询所有注册的科室

                //  启动服务
                ServiceHost host = new ServiceHost(iaers.GetType());
                host.Opened += delegate
                {
                    Console.WriteLine("服务启动成功!");
                };
                host.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
            protected void Page_Load(object sender, EventArgs e)
        {

            lblname.Text = ConfigurationManager.AppSettings["HISName"].ToString();

            if (!Page.IsPostBack)
            {
                if (ConfigurationManager.AppSettings["Login"].ToString() == "1")
                {
                    if (Session["user"] == null)
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                }
            }


        }
    }
}
