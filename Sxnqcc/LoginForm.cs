using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aersysm;
using Aersysm.Domain;
using Aersysm.Persistence;

namespace Sxnqcc
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            txtname.Text = "test_st";
        }


        private void btnkogin_Click(object sender, EventArgs e)
        {
            
            

            string name = txtname.Text.Trim();
            string pwd = txtpwd.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("用户名不能为空！");
                return;
            }

            if (string.IsNullOrEmpty(pwd))
            {
                MessageBox.Show("密码不能为空！");
                return;
            }


            Aers service = new Aers();

            //2017.6.12 YM 
            view_Login model = new view_Login();
            model.Name = name;
            model.Pwd = pwd;


            //  ResModel<view_tbl_registereduser> aaa = service.UserLogin(name, pwd);
            //ResModel<view_tbl_registereduser> aaa = service.UserLogin(model);
            //if (aaa.restag == "103")
            //{
            //    if (aaa.model.RoleState.Contains("147"))
            //    {
            //        MainForm.user = aaa.model;

            //        this.DialogResult = DialogResult.OK;
            //    }
            //    else
            //    {
            //        MessageBox.Show("您的权限不能登录该系统！");
            //    }

            //}
            //else
            //{
            //    MessageBox.Show("用户名密码错误！");
            //}



        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     
    }
}
