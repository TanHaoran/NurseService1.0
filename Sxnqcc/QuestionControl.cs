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
    public partial class QuestionControl : UserControl,IBusinessControl
    {
        public bool IsRefresh { get; set; }

        private void picRefresh_Click(object sender, EventArgs e)
        {
            DataBind();
        }

        public QuestionControl()
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

            AddColumn("题目编号", "Qid", 100);
            AddColumn("章节编号", "ChapterID", 100);
            AddColumn("题目类型", "TypeName", 100);
            AddColumn("题目", "TitleName", 100);
            AddColumn("答案A", "A", 100);
            AddColumn("答案B", "B", 100);
            AddColumn("答案C", "C", 100);
            AddColumn("答案D", "D", 100);
            AddColumn("答案E", "E", 100);
            AddColumn("答案F", "F", 100);
            AddColumn("分值", "Score", 100);
            AddColumn("正确答案", "Result", 100);
            AddColumn("操作员", "OperatorID", 100);
            AddColumn("修改时间", "ModifyDate", 100);
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
        private void picAdd_Click(object sender, EventArgs e)
        {
            QuestionForm form = new QuestionForm();
            form.DataBind();
            if (form.ShowDialog() == DialogResult.OK)
            {
                DataBind();
            };
        }
      //  aers_tbl_registereduserSqlMapDao dal = new aers_tbl_registereduserSqlMapDao();
        QuestionsSqlmapDao dal = new QuestionsSqlmapDao();
        private void picdelete_Click(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                Questions data = this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as Questions;

                if ((MessageBox.Show("确定要删除吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                {
                    int i = dal.Delete(data.Qid);
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
                QuestionForm form = new QuestionForm();
                form.DataBind(data as Questions );
                if (form.ShowDialog() == DialogResult.OK)
                {
                    DataBind();
                };

            }
        }



        private void btn_dc_Click_Click(object sender, EventArgs e)
        {

        }

        public void DataBind()
        {
            IList<Questions> list = dal.QuestionsFindAll();

            string Pram = txtfind.Text.Trim();

            if (!string.IsNullOrEmpty(Pram))
            {
                list = list.Where(o => o.TitleName .Contains(Pram) || o.TitleName .Contains(Pram) || o.TypeName .Contains(Pram)).ToList();
            }

            this.gridControl1.DataSource = new BindingList<Questions>(list);
            lblcount.Text = "共 " + list.Count + " 条";
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            picupdate_Click(null, null);
        }
    }
}
