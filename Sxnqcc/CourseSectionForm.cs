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
    public partial class CourseSectionForm : Form
    {
        public CourseSectionForm()
        {
            InitializeComponent();
        }
        CourseSection courseSection;
        CourseSectionSqlMapDao dal = new CourseSectionSqlMapDao();
        FormState state;
        public void DataBind(CourseSection data = null)
        {
            if (data == null)
            {
                courseSection = new CourseSection();
                state = FormState.Add;
            }
            else
            {
                state = FormState.Modify;
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            courseSection.CatalogID = combCatalog.SelectedText.ToString();
            courseSection.CourseID = combCourse.SelectedText.ToString();
            courseSection.ChapterName = txtSection.Text;
            courseSection.ChapterDuration = Convert.ToDateTime(txtDuration.Text);
            courseSection.ChapterPoints = Convert.ToInt32(txtChapterPoints.Text);
            if (state == FormState.Add)
            {
                try
                {
                    dal.CourseSectionInsert(courseSection);
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("增加成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("增加失败！" + ex);
                }
            }
            else
            {
                try
                {
                    dal.CourseSectionUpdate(courseSection);
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("修改成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("修改失败！" + ex);
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
