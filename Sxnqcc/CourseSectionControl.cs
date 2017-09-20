using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aersysm.Domain;
using Aersysm.Persistence;
using DevExpress.XtraGrid.Columns;

namespace Sxnqcc
{
    public partial class CourseSectionControl : UserControl, IBusinessControl
    {
        public CourseSectionControl()
        {
            InitializeComponent();
            SetCol();
        }

        public void SetCol()
        {
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

            gridView1.RowHeight = 20;

            AddColumn("章节编号", "ChapterID", 100);
            AddColumn("目录编号", "CatalogID", 100);
            AddColumn("课程编号", "CourseID", 100);
            AddColumn("章节名称", "ChapterName", 100);
            AddColumn("章节时长", "ChapterDuration", 100);
            //AddColumn("章节类型", "ChapterType", 100);
            //AddColumn("授权类型", "AuthorizationType", 100);
            //AddColumn("排序标识", "ChapterSort", 100);
            AddColumn("内容URL地址", "ThumbUrl", 100);
            AddColumn("积分", "ChapterPoints", 100);
            //AddColumn("分值", "Score", 100);
          //  AddColumn("章节结束端", "IsEndLevel", 100);
            AddColumn("操作员", "OperatorID", 100);
            AddColumn("操作时间", "OperatorTime", 100);
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
        CourseSection courseSection;
        CourseSectionSqlMapDao dal=new CourseSectionSqlMapDao();

        public bool IsRefresh { get; set; }

        public void DataBind()
        {
            IList<CourseSection> list = dal.CourseSectionFindAll();

            string Pram = txtfind.Text.Trim();

            if (!string.IsNullOrEmpty(Pram))
            {
                list = list.Where(o => o.ChapterName.Contains(Pram)).ToList();
            }

            this.gridControl1.DataSource = new BindingList<CourseSection>(list); ;
            lblcount.Text = "共 " + list.Count + " 条";
        }

        private void picAdd_Click(object sender, EventArgs e)
        {
            CourseSectionForm form = new CourseSectionForm();
            form.DataBind();
            if (form.ShowDialog() == DialogResult.OK)
            {
                DataBind();
            };
        }
    }
}
