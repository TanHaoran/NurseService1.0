using Aersysm.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sxnqcc
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public static view_tbl_registereduser user;

        // 关闭窗体
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((MessageBox.Show("确定要退出程序吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.No))
            {
                e.Cancel = true;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Application.Exit();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

      

        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowMenu("操作员管理");
        }


        #region 声明
        Dictionary<string, IBusinessControl> _controlDic = new Dictionary<string, IBusinessControl>();

        IBusinessControl ctrl = null;

        private void AddCtrl(IBusinessControl ctrl)
        {
            this.MainPanel.Controls.Clear();
            UserControl userctrl = ctrl as UserControl;
            if (userctrl == null)
                return;
            // 把用户控件添加到窗体里来
            this.MainPanel.Controls.Add(userctrl);
            userctrl.Dock = DockStyle.Fill;
        }
        #endregion

        // 呼出页面的设置
        private void ShowMenu(string name)
        {
            // 把内容加载到内存里
            if (_controlDic.ContainsKey(name))
            {
                ctrl = _controlDic[name];
                if (ctrl.IsRefresh)
                {
                    ctrl.DataBind();
                }
                AddCtrl(ctrl);
                return;
            }
            switch (name)
            {
                case "操作员管理":
                    ctrl = new OperatorControl();
                    break;
                case "医院管理":
                    ctrl = new hospitalControl();
                    break;
                case "科室管理":
                    ctrl = new hospdepControl();
                    break;
                case "人员管理":
                    ctrl = new StaffControl();
                    break;

                //case "课程管理":
                //    ctrl = new CourseControl();   //4.10以前的先屏蔽
                //    break;

                case "课程目录":
                    ctrl = new CourseCatalogControl();
                    break;
                    //2017.4.10 Ym护士学堂加
                case "题库管理":
                    ctrl = new QuestionControl();
                    break;
                case "课程管理":
                    ctrl = new CourseControl();
                    break;

                case "课程章节管理":
                    ctrl = new CourseSectionControl();  //Course Section
                    break;

                case "课程目录管理":
                    ctrl = new CourseCatalogControl();
                    break;

                default:
                    this.MainPanel.Controls.Clear();
                    break;
            }
            if (ctrl != null)
            {
                _controlDic.Add(name, ctrl);
                AddCtrl(ctrl);
                ctrl.DataBind();
            }
        }

        private void 医院管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMenu("医院管理");
        }

        private void 账号管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMenu("操作员管理");
        }

        private void 科室管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMenu("科室管理");
        }

        private void 人员管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMenu("人员管理");
        }

        private void 课程管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMenu("课程管理");
        }

        private void 课程目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMenu("课程目录");
        }

        private void 课程章节ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMenu("课程章节");
        }

     #region 2017.4.10Ym护士学堂新加
        //4.10YM

        private void 题库管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMenu("题库管理");
        }

        private void 课程管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowMenu("课程管理");
        }

        private void 课程目录管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMenu("课程目录管理");
        }

        private void 课程章节管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowMenu("课程章节管理");
        }
    }
#endregion 


}
