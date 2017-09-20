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
    public partial class OperatorControl : UserControl, IBusinessControl
    {
        public OperatorControl()
        {
            InitializeComponent();  
            SetCol();
            IsRefresh = true;
        }


        private void OperatorControl_Load(object sender, EventArgs e)
        {
            ddlRoleState.SelectedIndex = 0;
        }

        public void SetCol()
        {
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

            gridView1.RowHeight = 20;

            AddColumn("用户编码", "ReguserId", 100);
            AddColumn("登录账号", "LoginName", 100);
            AddColumnDateTime("注册时间", "OperatorDate");
            AddColumn("备注", "Remarks", 100);

            AddColumn("人员编号", "StaffId", 100);
            AddColumn("姓名", "Name", 100);
            AddColumn("角色", "RoleState", 100);
            AddColumn("性别", "Sex", 100);
            AddColumn("职称", "Position", 100);
            AddColumn("电话", "Phone", 100);
            //AddColumn("地址", "Address", 100);
            //AddColumn("证件号码", "IDNumber", 100);
            AddColumn("人员备注", "StaffRemarks", 100);


            AddColumn("医院编号", "HospId", 100);
            AddColumn("医院名称", "HospName", 200);
            AddColumn("医院地址", "hospitalAddress", 100);
            AddColumn("医院等级", "Grade", 100);
          
            AddColumn("科室编号", "DepId", 100);
            AddColumn("科室名称", "HospdepName", 100);


            AddColumn("操作员", "OperatorId", 100);
        
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
            column.Width = 200;
            column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            column.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";

            this.gridView1.Columns.Add(column);
        }

        aers_tbl_registereduserSqlMapDao dal = new aers_tbl_registereduserSqlMapDao();
        aers_tbl_staffSqlMapDao dalstaff = new aers_tbl_staffSqlMapDao();
        aers_tbl_events_ycSqlMapDao dalhospital = new aers_tbl_events_ycSqlMapDao();
        aers_tbl_hospdepSqlMapDao dalhospdep = new aers_tbl_hospdepSqlMapDao();

        public void DataBind()
        {
            IList<aers_tbl_registereduser> list = dal.FindAll("").OrderBy(o=>o.ReguserId).ToList();

            string Pram = txtfind.Text.Trim();

            if (!string.IsNullOrEmpty(Pram))
            {
                list = list.Where(o=> o.Name.Contains(Pram) || o.LoginName.Contains(Pram)|| o.HospName.Contains(Pram) || o.HospdepName.Contains(Pram) || o.RoleState.Contains(Pram)).ToList();
            }


            if (!string.IsNullOrEmpty(ddlRoleState.Text))
            {
                if(ddlRoleState.Text!="全部")
                {
                    list = list.Where(o => o.RoleState == ddlRoleState.Text).ToList();
                }
                
            }


            this.gridControl1.DataSource = new BindingList<aers_tbl_registereduser>(list);
            lblcount.Text="共 "+ list .Count+ " 条";

        }

        private void btn_dc_Click_Click(object sender, EventArgs e)
        {
            DataBind();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            picupdate_Click(null, null);
        }

        private void picAdd_Click(object sender, EventArgs e)
        {
            OperatorForm form = new OperatorForm();
            form.DataBind();
            if (form.ShowDialog() == DialogResult.OK)
            {
                DataBind();
            };
        }

        private void picRefresh_Click_1(object sender, EventArgs e)
        {
            DataBind();
        }

        private void picdelete_Click(object sender, EventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                aers_tbl_registereduser data = this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as aers_tbl_registereduser;

                if ((MessageBox.Show("确定要删除吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                {
                    int i = dal.Delete(data.ReguserId);
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
                OperatorForm form = new OperatorForm();
                form.DataBind(data as aers_tbl_registereduser);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    DataBind();
                };

            }
        }

        private void ddlRoleState_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBind();
        }
    }
}
