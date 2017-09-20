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
    public partial class CourseForm : Form
    {
        public CourseForm()
        {
            InitializeComponent();
        }

        FormState state;
        // aers_tbl_hospdep hospdep;
        Course course;
        CourseSqlMapDao dal=new CourseSqlMapDao();

        private void btnOK_Click(object sender, EventArgs e)
        {
            course.CourseType = combCourseType.SelectedItem.ToString();
            course.CourseName = txtCourseName.Text;
            course.CourseTeacher = txtCourseTeacher.Text;
            course.CourseIntro = txtCourseInfo.Text;
            course.CourseDuration =Convert .ToInt32(txtCourseDur.Text);
            course.CourseLevel =Convert .ToInt32 ( combCourseLevel.SelectedItem);
            course.SuitableDuty = combSuitableDuty.SelectedItem.ToString();
            course.CourseHot = Convert.ToInt32(txtCourseHot.Text);
            course.Recommend = Convert.ToInt32( txtRecommand.Text);
            course.TeacherIntroduction = txtTeacherIntroduction.Text;
            course .OperatorID = MainForm.user.ReUId;
            course.OperateTime = DateTime.Now;
            if (state == FormState.Add)
            {
                try
                {
                    dal.CourseInsert(course);
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
                    dal.CourseUpdate(course);
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("修改成功！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("修改失败！" +ex );
                }
               
                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CourseForm_Load(object sender, EventArgs e)
        {
           
        }

        public void DataBind(Course data = null)
        {
            if (data == null)
            {
                course = new Course();
                state = FormState.Add;

            }
            else
            {
                course = data;
                state = FormState.Modify;

                combCourseType.Text = course.CourseType;
                txtCourseName.Text = course.CourseName;
                txtCourseTeacher.Text = course.CourseTeacher;
                txtCourseInfo.Text = course.CourseIntro;
                txtCourseDur.Text = course.CourseDuration.ToString();
                combCourseLevel.Text = course.CourseLevel.ToString();
                combSuitableDuty.Text = course.SuitableDuty;
                txtCourseHot.Text = course.CourseHot.ToString();
                txtRecommand.Text = course.Recommend.ToString();
                txtTeacherIntroduction.Text = course.TeacherIntroduction;

            }
        }
    }
}
