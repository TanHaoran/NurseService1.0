using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Aersysm;
using Aersysm.Domain;
using System.Configuration;
using Services;
using System.ServiceModel;

namespace CreditWeb
{
    public partial class _Default : System.Web.UI.Page
    {
        //Program p = new Program();
       



        protected void Page_Load(object sender, EventArgs e)
        {
           // static void Main(string[] args)
            //{
            //    // DBManager._instance.CurrentDataAccessAction = DBFactory.CreateDataBase;

            //    try
            //    {
            //        //  MMIPService service = new MMIPService();
            //        Aers service = new Aers();
            //        IAers iaers = new Aers();
            //        ;


            //        //test1(service);

            //        Console.WriteLine("服务启动中。。。");
            //        ////  查询所有注册的科室

            //        //  启动服务
            //        ServiceHost host = new ServiceHost(iaers.GetType());
            //        host.Opened += delegate
            //        {
            //            Console.WriteLine("服务启动成功!");
            //        };
            //        host.Open();
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
            //    Console.ReadLine();
            //}
            if (!Page.IsPostBack)
            {
            }
        }

        public  string UserMd5(string str)
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


        //消息提示
        public void AlertMagess(string magess)
        {
            string script = "<script>window.alert('" + magess + "');</script>";
            Page.RegisterStartupScript("", script);
            //string script = "alert('" + magess + "');";
            //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, GetType(), "", script, true);
        }


        protected void bntlogin_Click(object sender, EventArgs e)
        {
            string name = txtname.Text.Trim();
            string pwd = txtpwd.Text.Trim();


            try
            {


                Aers service = new Aers();

                view_Login lg = new view_Login();

                ResModel<view_tbl_registereduser> op = service.UserLogin(lg);

                if (op != null)
                {
                    if (op.restag == "103")
                    {
                        if (op.model.RoleState == "147")
                        {
                            Session["user"] = op.model;
                            Response.Redirect("~/Index.aspx");
                        }
                        else
                        {
                            AlertMagess("您的权限不能登录该系统！");
                            return;
                        }
                     
                       
                    }
                    else if (op.restag == "102")
                    {
                        AlertMagess("密码错误！");
                        return;
                    }
                    else
                    {
                        AlertMagess("错误:"+op.restag);
                        return;
                    }
                }
                else
                {
                    AlertMagess("用户名不存在！");
                    return;
                }

            }
            catch (Exception ex)
            {

            }


        }
    }
}
