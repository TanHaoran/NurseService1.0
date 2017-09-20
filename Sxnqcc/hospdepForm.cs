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
    public partial class hospdepForm : Form
    {
        public hospdepForm()
        {
            InitializeComponent();
        }

        FormState state;
        aers_tbl_hospdep hospdep;

        aers_tbl_hospdepSqlMapDao dal = new aers_tbl_hospdepSqlMapDao();
        aers_tbl_events_ycSqlMapDao dalhospital = new aers_tbl_events_ycSqlMapDao();

        private void hospdepForm_Load(object sender, EventArgs e)
        {
            IList<aers_tbl_hospital> listhospital = dalhospital.hospitalFindAll();


            this.txt_HospId.FieldValue = "HospName";
            this.txt_HospId.DisplayValue = "HospName";
            this.txt_HospId.DataSource = listhospital;
        }


        public void DataBind(aers_tbl_hospdep data = null)
        {
            if (data == null)
            {
                hospdep = new aers_tbl_hospdep();
                txt_DisplayOrder.Text ="0";
                state = FormState.Add;
            }
            else
            {
                hospdep = data;
                state = FormState.Modify;

                txt_HospdepName.Text = hospdep.HospdepName;
                txt_HospId.Text=hospdep.HospId;
                txt_DisplayOrder.Text=hospdep.DisplayOrder.ToString();
                txt_Remarks.Text = hospdep.Remarks;

                aers_tbl_hospital hospital = dalhospital.hospitalFindAll().FirstOrDefault(o=>o.HospId== hospdep.HospId);



                if (hospital != null)
                {
                    txt_HospId.EditValue = hospital.HospId;

                    this.txt_HospId.Text = hospital.HospName;
                    this.txt_HospId.Tag = hospital.HospId;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            if (txt_HospId.SelectedRow == null || string.IsNullOrEmpty(txt_HospId.SelectedRow["HospId"].ToString()))
            {
                if (txt_HospId.Tag != null)
                {
                    hospdep.HospId = txt_HospId.Tag.ToString();
                }
                else
                {
                    MessageBox.Show("请选择医院");
                    return;
                }


            }
            else
            {
                hospdep.HospId = txt_HospId.SelectedRow["HospId"].ToString();
            }


       
            hospdep.BasedepId = "0000000001";
            hospdep.HospdepName = txt_HospdepName.Text.Trim();

            int DisplayOrder = 0;
            try
            {
                DisplayOrder=Convert.ToInt32(txt_DisplayOrder.Text.Trim());
            }
            catch (Exception)
            {

                MessageBox.Show("排序编号输入错误");
            }


            hospdep.DisplayOrder = DisplayOrder;
            hospdep.Remarks=txt_Remarks.Text.Trim();
            hospdep.OperatorId = MainForm.user.ReUId;
            hospdep.OperatorDate = DateTime.Now;

         

            if (string.IsNullOrEmpty(hospdep.HospdepName))
            {
                MessageBox.Show("科室名称不能为空！");
                return;
            }


    

            if (state == FormState.Add)
            {
                int i = dal.Insert(hospdep);
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
                int i = dal.Update(hospdep);
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
