using Aersysm.Domain;
using Aersysm.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sxnqcc
{
    public partial class CourseCatalogForm : Form
    {
        public CourseCatalogForm()
        {
            InitializeComponent();
        }
        FormState state;
        CourseCatalog courseCatalog;
        CourseCatalogSqlMapDao dal=new CourseCatalogSqlMapDao ();
        object o;
        public void DataBind(CourseCatalog data = null)
        {
            if (data == null)
            {
                courseCatalog = new CourseCatalog();
                state = FormState.Add;
            }
            else
            {
                state = FormState.Modify;
                txtCatalogName.Text = data.CatalogName;
                txtCatalogSort.Text = data.CatalogSort.ToString ();
                o = data.CourseID;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            courseCatalog.CourseID = o.ToString();
            courseCatalog.CatalogName = txtCatalogName.Text;
            courseCatalog.CourseID = txtCatalogSort.Text;


            if (state == FormState.Add)
            {
                try
                {
                    dal.CourseCatalogInsert(courseCatalog);
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("增加成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("增加失败！" +ex);
                }
            }
            else
            {
                try
                {
                    dal.CourseCatalogUpdate(courseCatalog);
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("修改成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("修改失败！");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
