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
    public partial class StaffControl : UserControl, IBusinessControl
    {
        public StaffControl()
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

            AddColumn("人员编码", "StaffId", 100);
            AddColumn("注册编码", "ReguserId", 100);
            AddColumn("科室编码", "DepId", 100);
            AddColumn("姓名", "Name", 100);
            AddColumn("角色标识", "StringRoleState", 100);
            AddColumn("性别", "Sex", 100);
            //AddColumn("职称", "Position", 100);
            AddColumn("联系电话", "Phone", 100);
            //AddColumn("住址", "Address", 100);
            //AddColumn("证件号", "IDNumber", 100);
            AddColumn("停用标识", "IsFlag", 100);
            AddColumn("备注", "Remarks", 100);
            AddColumn("操作员", "OperatorId", 100);
            AddColumn("操作时间", "OperatorDate", 100);
        }

        public bool IsRefresh { get; set; }

        private void picRefresh_Click(object sender, EventArgs e)
        {
         
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
        aers_tbl_staffSqlMapDao dal = new aers_tbl_staffSqlMapDao();

        public void DataBind()
        {
        

            IList<aers_tbl_staff> list = dal.staffFindAll();


            foreach (aers_tbl_staff item in list)
            {


                if (item.RoleState.Contains("147"))
                {
                    item.StringRoleState = "省厅";
                }
                else if (item.RoleState.Contains("148"))
                {
                    item.StringRoleState = "区域";
                }
                else if(item.RoleState.Contains("145"))
                {
                    item.StringRoleState = "护理部";
                }
                else if (item.RoleState.Contains("146"))
                {
                    item.StringRoleState = "护士长";
                }
                else
                {
                    item.StringRoleState = "未知状态";
                }

                if (item.Sex == "107")
                {
                    item.Sex = "女";
                }
                else if (item.Sex == "108")
                {
                    item.Sex = "男";
                }
                else
                {
                    item.Sex = "未知";
                }
            }

            string Pram = txtfind.Text.Trim();

            if (!string.IsNullOrEmpty(Pram))
            {
                list = list.Where(o => o.Name.Contains(Pram)).ToList();
            }

            this.gridControl1.DataSource = new BindingList<aers_tbl_staff>(list); ;
            lblcount.Text = "共 " + list.Count + " 条";
        }

        private void btn_dc_Click_Click(object sender, EventArgs e)
        {
            DataBind();
        }

        private void picAdd_Click(object sender, EventArgs e)
        {
            StaffForm form = new StaffForm();
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
                aers_tbl_staff data = this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as aers_tbl_staff;

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
                StaffForm form = new StaffForm();
                form.DataBind(data as aers_tbl_staff);
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

        private void picRefresh_Click_1(object sender, EventArgs e)
        {
            DataBind();
        }
    }
}
