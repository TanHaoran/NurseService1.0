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
    public partial class hospitalForm : Form
    {
        public hospitalForm()
        {
            InitializeComponent();
        }

        FormState state;
        aers_tbl_hospital hospital;

        aers_tbl_events_ycSqlMapDao dal = new aers_tbl_events_ycSqlMapDao();

        public void DataBind(aers_tbl_hospital data = null)
        {
            if (data == null)
            {
                hospital = new aers_tbl_hospital();

                state = FormState.Add;
            }
            else
            {
                hospital = data;
                state = FormState.Modify;

                txt_HospName.Text = hospital.HospName;
                txt_Grade.Text = hospital.Grade;
                txt_Address.Text = hospital.Address;
                txt_Phone.Text = hospital.Phone;
                txt_Contact.Text = hospital.Contact;
                txt_Remarks.Text = hospital.Remarks;
                txt_Validitytime.DateTime = hospital.Validitytime;

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            hospital.HospName=txt_HospName.Text.Trim();
            hospital.Grade=txt_Grade.Text.Trim();
            hospital.Address=txt_Address.Text.Trim();
            hospital.Phone=txt_Phone.Text.Trim();
            hospital.Contact=txt_Contact.Text.Trim();
            hospital.Remarks=txt_Remarks.Text.Trim();
            hospital.Validitytime=txt_Validitytime.DateTime;
            hospital.OperatorId = MainForm.user.ReUId;
            hospital.OperatorDate = DateTime.Now;

            if (hospital.Validitytime == DateTime.MinValue)
            {
                hospital.Validitytime = DateTime.Now;
            }

            if (string.IsNullOrEmpty(hospital.HospName))
            {
                MessageBox.Show("医院名称不能为空！");
                return;
            }


    

            if (state == FormState.Add)
            {
                int i = dal.Insert(hospital);
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
                int i = dal.Update(hospital);
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
    }
}
