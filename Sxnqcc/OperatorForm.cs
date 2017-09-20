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
    public partial class OperatorForm : Form
    {
        public OperatorForm()
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

            txt_Sex.DisplayMember = "ECodeTag";
            txt_Sex.ValueMember = "ECodeValue";
            txt_Sex.DataSource = liststate.Where(o => o.Remarks == "性别").ToList();
        }


        FormState state;
        aers_tbl_registereduser registereduser;
        aers_tbl_staff staff;

        aers_tbl_registereduserSqlMapDao dal = new aers_tbl_registereduserSqlMapDao();
        aers_tbl_staffSqlMapDao dalstaff = new aers_tbl_staffSqlMapDao();
        aers_tbl_events_ycSqlMapDao dalhospital = new aers_tbl_events_ycSqlMapDao();
        aers_tbl_hospdepSqlMapDao dalhospdep = new aers_tbl_hospdepSqlMapDao();
        aers_sys_statecodeSqlMapDao dalstatecode = new aers_sys_statecodeSqlMapDao();
        private void OperatorForm_Load(object sender, EventArgs e)
        {
        
        }

        private void txt_HospId_EditValueChanged(object sender, EventArgs e)
        {
            string HospId = txt_HospId.EditValue.ToString();

            IList<aers_tbl_hospdep> listhospdep = dalhospdep.hospdepFindAll();


            IList<aers_tbl_staff> liststaff =  dalstaff.staffFindAll();

            foreach (aers_tbl_staff item in liststaff)
            {
                aers_tbl_hospdep hospdep = listhospdep.FirstOrDefault(o => o.HospdepId == item.DepId);
                if (hospdep != null)
                {
                    item.HospId = hospdep.HospId;
                    item.HospdepName = hospdep.HospdepName;
                }
            }
            this.txt_HospdepId.FieldValue = "HospdepId";
            this.txt_HospdepId.DisplayValue = "HospdepName";
            this.txt_HospdepId.DataSource = listhospdep.Where(o => o.HospId == HospId).ToList();
         }

        public void DataBind(aers_tbl_registereduser data = null)
        {
            if (data == null)
            {
                registereduser = new aers_tbl_registereduser();

                state = FormState.Add;
            }
            else
            {
                registereduser = data;
                state = FormState.Modify;

                txt_LoginName.Text = data.LoginName;
                txt_Remarks.Text = data.Remarks;

             

                staff = dalstaff.FindByRUid(data.ReguserId);

                if (staff != null)
                {
                    aers_tbl_hospdep hospdep=dalhospdep.FindhospdepByDepId(staff.DepId);

                    if (hospdep != null)
                    {
                        aers_tbl_hospital listhosp = dalhospital.hospitalFindAll().FirstOrDefault(o => o.HospId == hospdep.HospId);

                        txt_HospId.EditValue = listhosp.HospId;

                        this.txt_HospdepId.Text = hospdep.HospdepName;
                        this.txt_HospdepId.Tag = hospdep.HospdepId;
                    }


                    txt_Name.Tag = staff.StaffId;
                    txt_Name.Text = staff.Name;

                    txt_RoleState.Text = staff.RoleState.ToString();
                    txt_Sex.SelectedValue = staff.Sex;
                    txt_Phone.Text = staff.Phone;
                    txt_StaffRemarks.Text = staff.Remarks;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {


           
            registereduser.LoginName = txt_LoginName.Text.Trim();
            if (!string.IsNullOrEmpty(txt_Password.Text.Trim()))
            {
                registereduser.Password = txt_Password.Text.Trim();
            }
            registereduser.Remarks = txt_Remarks.Text.Trim();
            registereduser.OperatorId = MainForm.user.ReUId;
            registereduser.OperatorDate = DateTime.Now;

            if (string.IsNullOrEmpty(registereduser.LoginName))
            {
                MessageBox.Show("用户名不能为空！");
                return;
            }

            aers_tbl_registereduser check =dal.Uniquenessverification(registereduser.LoginName);

            if (state == FormState.Add)
            {
                if (string.IsNullOrEmpty(registereduser.Password))
                {
                    MessageBox.Show("密码不能为空！");
                    return;
                }
                if (check != null)
                {
                    MessageBox.Show("用户名已存在！");
                    return;
                }
            }

            if (staff == null)
            {
                staff = new aers_tbl_staff();
            }

   
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

            

            staff.Name = txt_Name.Text;
            staff.RoleState= txt_RoleState.Text;
            staff.IDNumber = "";
            staff.Position = "";
            staff.Address = "";
            staff.Sex= txt_Sex.SelectedValue.ToString();

            if (!string.IsNullOrEmpty(txt_Phone.Text.Trim()))
            {
                staff.Phone = txt_Phone.Text;
            }

            if (!string.IsNullOrEmpty(txt_StaffRemarks.Text.Trim()))
            {
                staff.Remarks = txt_StaffRemarks.Text;
            }
         
            staff.OperatorId = MainForm.user.ReUId;
            staff.OperatorDate = DateTime.Now;



            if (string.IsNullOrEmpty(staff.Name))
            {
                MessageBox.Show("姓名不能为空！");
                return;
            }

            if (staff.RoleState == "")
            {
                MessageBox.Show("请选择权限！");
                return;
            }
            







            if (state == FormState.Add)
            {
                string ReguserId = dal.Insert(registereduser);
                if (!string.IsNullOrEmpty(ReguserId))
                {
                    check=dal.Uniquenessverification(registereduser.LoginName);
                    staff.ReguserId = check.ReguserId;
                    int i;
                    if (dalstaff.FindByRUid(check.ReguserId)==null)
                    {
                        i = dalstaff.Insert(staff);
                    }
                    else
                    {
                        i = dalstaff.Update(staff);
                    }
             
                    if (i > 0)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("人员信息增加失败！");
                    }
                 
                }
                else
                {
                    MessageBox.Show("增加失败！");
                }
            }
            else
            {
                int i = dal.Update(registereduser);
                if (i > 0)
                {
                    staff.ReguserId = registereduser.ReguserId;
                    i = dalstaff.Update(staff);
                    if (i > 0)
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        MessageBox.Show("人员信息修改失败！");
                    }
               
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

        private void txt_Name_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txt_Sex_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
