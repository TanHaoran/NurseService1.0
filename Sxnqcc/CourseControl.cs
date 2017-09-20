using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraBars;
using Aersysm;
using Aersysm.Domain;
using Aersysm.Persistence;

namespace Sxnqcc
{
    public partial class CourseControl : UserControl, IBusinessControl
    {
        public CourseControl()
        {
            InitializeComponent();
            SetCol();
            IsRefresh = true;
        }

       

        public void SetCol()
        {
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

            gridView1.RowHeight = 20;

            AddColumn("课程编号", "CourseID", 100);
            AddColumn("课程类型", "CourseType", 100);
            AddColumn("课程名称", "CourseName", 200);
            AddColumn("授课教员", "CourseTeacher", 100);
            AddColumn("课程简介信息", "CourseIntro", 100);
            AddColumn("课程持续时长", "CourseDuration", 100);
            AddColumn("课程等级", "CourseLevel", 100);
           // AddColumn("标签", "CourseTag", 100);
            AddColumn("课程缩略图", "CourseThumb", 100);
            AddColumn("适用职称", "SuitableDuty", 100);
           // AddColumn("适用一年的上升", "SuitableYearUp", 100);
           // AddColumn("适用一年的下降", "SuitableYearDown", 100);
            AddColumn("热度（学习人数）", "CourseHot", 100);
            AddColumn("排序序列标识", "CourseSort", 100);
           // AddColumn("创始人编号", "AuthorID", 100);
            AddColumn("推荐", "Recommend", 50);
            AddColumn("教师简介", "TeacherIntroduction", 100);
            //AddColumn("ChapterPoints", "ChapterPoints", 100);
            AddColumn("操作员", "OperatorID", 100);
            AddColumn("操作时间", "OperateTime", 100);

        }

        public bool IsRefresh { get; set; }

        private void picRefresh_Click(object sender, EventArgs e)
        {
            DataBind();
        }
        protected void AddColumn(string caption, string fieldName, int size)
        {

            int index = this.gridView1.Columns.Count;
            GridColumn column = new GridColumn();
            column.AppearanceCell.Options.UseFont = true;
            column.Caption = caption;
            column.FieldName = fieldName;
            column.OptionsColumn.AllowEdit = false;
            column.Visible = true;
            column.VisibleIndex = index;
            column.Width = size;
            this.gridView1.Columns.Add(column);

        }

        protected void AddColumnDateTime(string caption, string fieldName)
        {
            int index = this.gridView1.Columns.Count;
            GridColumn column = new GridColumn();
            column.Caption = caption;
            column.FieldName = fieldName;
            column.OptionsColumn.AllowEdit = false;
            column.Visible = true;
            column.VisibleIndex = index;
            column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            column.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

            this.gridView1.Columns.Add(column);
        }

        CourseSqlMapDao dal = new CourseSqlMapDao();

        public void DataBind()
        {
       

            IList<Course> list=dal.CourseFindAll();

            string Pram = txtfind.Text.Trim();

            if (!string.IsNullOrEmpty(Pram))
            {
                list = list.Where(o => o.CourseName.Contains(Pram)).ToList();
            }

            this.gridControl1.DataSource = new BindingList<Course>(list); ;
            lblcount.Text = "共 " + list.Count + " 条";
        }
    

        private void hospitalControl_Load(object sender, EventArgs e)
        {
            
        }

        private void btn_dc_Click_Click(object sender, EventArgs e)
        {
            DataBind();
        }

        private void picAdd_Click(object sender, EventArgs e)
        {
           // hospdepForm form = new hospdepForm();
            CourseForm form = new CourseForm();
            form.DataBind();
            if (form.ShowDialog() == DialogResult.OK)
            {
                DataBind();
            };

        }

        private void picdelete_Click(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                Course data = this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Course;

                if ((MessageBox.Show("确定要删除吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                {
                    int i = dal.Delete(data.CourseID);
                    if (i > 0)
                    {
                        DataBind();
                    }
                    else
                    {
                        MessageBox.Show("删除失败！");
                    }
                }
            }
        }

        private void picupdate_Click(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                object data = this.gridView1.GetRow(this.gridView1.FocusedRowHandle);
                CourseForm form = new CourseForm();
                form.DataBind(data as Course);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    DataBind();
                };

            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            picupdate_Click(null,null);
        }
    }

 
}
