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
    public partial class hospdepControl : UserControl, IBusinessControl
    {
        public hospdepControl()
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

            AddColumn("科室编码", "HospdepId", 100);
            AddColumn("基础科室编码", "BasedepId", 100);
            AddColumn("医院编码", "HospId", 100);
            AddColumn("医院名称", "HospName", 200);
            AddColumn("科室名称", "HospdepName", 100);
            AddColumn("检索码", "SpellNo", 100);
            //AddColumn("科室Logo", "HospdepLogo", 100);
            AddColumn("显示顺序", "DisplayOrder", 100);
            AddColumn("停用标识", "IsFlag", 100);
            AddColumn("备注", "Remarks", 100);
            AddColumn("操作员", "OperatorId", 100);
            AddColumn("操作时间", "OperatorDate", 100);
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

        aers_tbl_hospdepSqlMapDao dal = new aers_tbl_hospdepSqlMapDao();

        aers_tbl_events_ycSqlMapDao dalhospital = new aers_tbl_events_ycSqlMapDao();
        public void DataBind()
        {


            IList<aers_tbl_hospdep> list = dal.hospdepFindAll();

            IList<aers_tbl_hospital> listhospital = dalhospital.hospitalFindAll();


            foreach (aers_tbl_hospdep item in list)
            {
                aers_tbl_hospital hospital = listhospital.FirstOrDefault(o => o.HospId == item.HospId);
                if (hospital != null)
                {
                    item.HospName = hospital.HospName;
                }
               
            }


            string Pram = txtfind.Text.Trim();

            if (!string.IsNullOrEmpty(Pram))
            {
                list = list.Where(o =>o.HospName.Contains(Pram)|| o.HospdepName.Contains(Pram)).ToList();
            }

            this.gridControl1.DataSource = new BindingList<aers_tbl_hospdep>(list); ;
            lblcount.Text = "共 " + list.Count + " 条";
        }

        private void btn_dc_Click_Click(object sender, EventArgs e)
        {
            DataBind();
        }

        private void picAdd_Click(object sender, EventArgs e)
        {
            hospdepForm form = new hospdepForm();
            form.DataBind();
            if (form.ShowDialog() == DialogResult.OK)
            {
                DataBind();
            };
        }

        private void picupdate_Click(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                aers_tbl_hospdep data = this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as aers_tbl_hospdep;

                if ((MessageBox.Show("确定要删除吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                {
                    int i = dal.Delete(data.HospdepId);
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

        private void picdelete_Click(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                object data = this.gridView1.GetRow(this.gridView1.FocusedRowHandle);
                hospdepForm form = new hospdepForm();
                form.DataBind(data as aers_tbl_hospdep);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    DataBind();
                };

            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            picdelete_Click(null,null);
        }

        private void picRefresh_Click_1(object sender, EventArgs e)
        {
            DataBind();
        }
    }
}
