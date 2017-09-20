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
    public partial class QuestionForm : Form
    {
        public QuestionForm()
        {
            InitializeComponent();
        }
        Questions questions;
        CourseSectionSqlMapDao courSection = new CourseSectionSqlMapDao();
        QuestionsSqlmapDao dal = new QuestionsSqlmapDao();
        FormState state;

        #region 绑定加载
        public void DataBind(Questions data = null)
        {
            if (data == null)
            {
                questions = new Questions();
                state = FormState.Add;
            }
            else
            {
                questions = data;
                state = FormState.Modify;

                combTypeName.Text = data.TypeName;
                txtBoxTitleName.Text = data.TitleName;

                txtboxAnswerA.Text = data.A;
                txtboxAnswerB.Text = data.B;
                txtboxAnswerC.Text = data.C;
                txtboxAnswerD.Text = data.D;
                txtboxAnswerE.Text = data.E;
                txtboxAnswerF.Text = data.F;

                txtboxCorrentAnswer.Text  = data.Result;
                combScore.Text = data.Score.ToString ();
  
            }
        }
         #endregion

        private void QuestionForm_Load(object sender, EventArgs e)
        {
            IList<CourseSection> listhospital = courSection.CourseSectionFindAll();

            this.combChapter.FieldValue = "ChapterID";
            this.combChapter.DisplayValue = "ChapterName";
            this.combChapter.DataSource = listhospital;
        }
        
        #region 添加修改
        private void btnOK_Click(object sender, EventArgs e)
        {
            questions.TypeName = combTypeName.SelectedItem.ToString();
            questions.TitleName = txtBoxTitleName.Text;

            questions.A = txtboxAnswerA.Text;
            questions.B = txtboxAnswerB.Text;
            questions.C = txtboxAnswerC.Text;
            questions.D = txtboxAnswerD.Text;
            questions.E = txtboxAnswerE.Text;
            questions.F = txtboxAnswerF.Text;

            questions.Result = txtboxCorrentAnswer.Text;
            questions.Score =Convert.ToInt32(combScore.SelectedItem);

            questions.ModifyDate = DateTime.Now;
            questions .OperatorID = MainForm.user.ReUId;


            if (state == FormState.Add)
            {
                int QuestionsId = dal.Insert(questions);
                if (QuestionsId >0)
                {
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("增加成功！");
                }
                else
                {
                    MessageBox.Show("增加失败！");
                }
            }
            else
            {
                int QuestionsId = dal.Update(questions);
                if (QuestionsId > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("修改成功！");
                }
                else
                {
                    MessageBox.Show("修改失败！");
                }
            }
        }
        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
