using Aersysm.Domain;
using Aersysm.Persistence;
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
    public partial class StaffForm : Form
    {
        public StaffForm()
        {  
            InitializeComponent();
            setCmb();


        }


        public void setCmb()
        {
            IList<aers_tbl_hospital> listhospital = dalhospital.hospitalFindAll();


            this.txt_HospId.Properties.DisplayMember = "HospName";
            this.txt_HospId.Properties.ValueMember = "HospId";
            this.txt_HospId.Properties.DataSource = listhospital;


            IList<aers_sys_statecode> liststate = dalstatecode.FindAll();

            txt_RoleState.DisplayMember = "ECodeTag";
            txt_RoleState.ValueMember = "ECodeValue";
            txt_RoleState.DataSource = liststate.Where(o => o.Remarks == "角色权限").ToList();




            txt_Sex.DisplayMember = "ECodeTag";
            txt_Sex.ValueMember = "ECodeValue";
            txt_Sex.DataSource = liststate.Where(o => o.Remarks == "性别").ToList();
        }


        FormState state;
        aers_tbl_staff staff;

        aers_tbl_registereduserSqlMapDao dal = new aers_tbl_registereduserSqlMapDao();
        aers_tbl_staffSqlMapDao dalstaff = new aers_tbl_staffSqlMapDao();
        aers_tbl_events_ycSqlMapDao dalhospital = new aers_tbl_events_ycSqlMapDao();
        aers_tbl_hospdepSqlMapDao dalhospdep = new aers_tbl_hospdepSqlMapDao();
        aers_sys_statecodeSqlMapDao dalstatecode = new aers_sys_statecodeSqlMapDao();
        private void OperatorForm_Load(object sender, EventArgs e)
        {
            IList<aers_tbl_hospital> listhospital = dalhospital.hospitalFindAll();


            this.txt_HospId.Properties.DisplayMember = "HospName";
            this.txt_HospId.Properties.ValueMember = "HospId";
            this.txt_HospId.Properties.DataSource = listhospital;




            IList<aers_tbl_hospdep> listhospdep = dalhospdep.hospdepFindAll();

  
            this.txt_HospdepId.FieldValue = "HospdepName";
            this.txt_HospdepId.DisplayValue = "HospdepName";
            this.txt_HospdepId.DataSource = listhospdep;



        }

     


        public void DataBind(aers_tbl_staff data = null)
        {
            if (data == null)
            {
                staff = new aers_tbl_staff();

                state = FormState.Add;
            }
            else
            {
                staff = data;
                state = FormState.Modify;

                txt_Name.Text = data.Name;
                txt_RoleState.SelectedValue = data.RoleState.ToString();
                txt_Sex.SelectedText = data.Sex;
                txt_Phone.Text = data.Phone;
                txt_StaffRemarks.Text = data.Remarks;


                aers_tbl_hospdep hospdep = dalhospdep.FindhospdepByDepId(staff.DepId);



                if (hospdep != null)
                {
                    aers_tbl_hospital listhosp = dalhospital.hospitalFindAll().FirstOrDefault(o => o.HospId == hospdep.HospId);

                    txt_HospId.EditValue = listhosp.HospId;

                    this.txt_HospdepId.Text = hospdep.HospdepName;
                    this.txt_HospdepId.Tag = hospdep.HospdepId;
                }

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            staff.ReguserId = "0";
            staff.Name = txt_Name.Text;
            staff.RoleState = txt_RoleState.SelectedValue.ToString();
            staff.IDNumber = "";
            staff.Position = "";
            staff.Address = "";
            staff.Sex = txt_Sex.SelectedValue.ToString();
            staff.Phone = txt_Phone.Text;
            staff.Remarks = txt_StaffRemarks.Text;
            staff.OperatorId = MainForm.user.ReUId;
            staff.OperatorDate = DateTime.Now;



            if (txt_HospId.EditValue == null)
            {
                MessageBox.Show("请选择医院");
                return;
            }


            if (txt_HospdepId.SelectedRow == null || string.IsNullOrEmpty(txt_HospdepId.SelectedRow["HospdepId"].ToString()))
            {
                if (txt_HospdepId.Tag != null)
                {
                    staff.DepId = txt_HospdepId.Tag.ToString();
                }
                else
                {
                    MessageBox.Show("请选择科室");
                    return;
                }
            }
            else
            {
                staff.DepId = txt_HospdepId.SelectedRow["HospdepId"].ToString();
            }

     

           

            if (state == FormState.Add)
            {
                int i = dalstaff.Insert(staff);
                if (i > 0)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("增加失败！");
                }
            }
            else
            {
                int i = dalstaff.Update(staff);
                if (i > 0)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("修改失败！");
                }

            }

           
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_HospId_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txt_HospId_EditValueChanged_1(object sender, EventArgs e)
        {
            string HospId = txt_HospId.EditValue.ToString();

            IList<aers_tbl_hospdep> listhospdep = dalhospdep.hospdepFindAll().Where(o => o.HospId == HospId).ToList();

            this.txt_HospdepId.FieldValue = "HospdepId";
            this.txt_HospdepId.DisplayValue = "HospdepName";
            this.txt_HospdepId.DataSource = listhospdep;
        }
    }
}
 