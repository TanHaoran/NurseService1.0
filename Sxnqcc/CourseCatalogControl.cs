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
    public partial class CourseCatalogControl : UserControl, IBusinessControl
    {
        public CourseCatalogControl()
        {
            InitializeComponent();
            SetCol();
            IsRefresh = true;

            CourseSqlMapDao dal = new CourseSqlMapDao();

   
            IList<Course> list = dal.CourseFindAll();

       
            txt_CourseID.Properties.ValueMember = "CourseID";
            txt_CourseID.Properties.DisplayMember = "CourseName";
            txt_CourseID.Properties.DataSource = list.ToList();


            txt_CourseID.EditValue = list[0].CourseID;

        }

       

        public void SetCol()
        {
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

            gridView1.RowHeight = 20;

            AddColumn("目录编号", "CatalogID", 100);
            AddColumn("课程编号", "CourseID", 100);
            AddColumn("目录名称", "CatalogName", 200);
            AddColumn("排序标识", "CatalogSort", 100);

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

        CourseCatalogSqlMapDao dal = new CourseCatalogSqlMapDao();

        public void DataBind()
        {
       

            IList<CourseCatalog> list=dal.CourseCatalog_FindByCourseID(txt_CourseID.EditValue.ToString());

            string Pram = txtfind.Text.Trim();

            if (!string.IsNullOrEmpty(Pram))
            {
                list = list.Where(o => o.CatalogName.Contains(Pram)).ToList();
            }

            this.gridControl1.DataSource = new BindingList<CourseCatalog>(list); 
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
            CourseCatalogForm form = new CourseCatalogForm();
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
                CourseCatalog data = this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as CourseCatalog;

                if ((MessageBox.Show("确定要删除吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                {
                    int i = dal.Delete(data.CatalogID);
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
                CourseCatalogForm form = new CourseCatalogForm();
                form.DataBind(data as CourseCatalog);
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

        private void txt_CourseID_EditValueChanged(object sender, EventArgs e)
        {
            DataBind();
        }
    }

 
}
