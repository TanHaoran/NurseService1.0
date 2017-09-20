namespace Sxnqcc
{
    partial class QuestionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.combTypeName = new System.Windows.Forms.ComboBox();
            this.labTypeName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.combScore = new System.Windows.Forms.ComboBox();
            this.labScore = new System.Windows.Forms.Label();
            this.txtboxCorrentAnswer = new System.Windows.Forms.TextBox();
            this.txtboxAnswerF = new System.Windows.Forms.TextBox();
            this.txtboxAnswerE = new System.Windows.Forms.TextBox();
            this.txtboxAnswerD = new System.Windows.Forms.TextBox();
            this.txtboxAnswerC = new System.Windows.Forms.TextBox();
            this.txtboxAnswerB = new System.Windows.Forms.TextBox();
            this.labCorrectAnswer = new System.Windows.Forms.Label();
            this.labAnswerF = new System.Windows.Forms.Label();
            this.labAnswerE = new System.Windows.Forms.Label();
            this.labAnswerD = new System.Windows.Forms.Label();
            this.labAnswerC = new System.Windows.Forms.Label();
            this.labAnswerB = new System.Windows.Forms.Label();
            this.txtboxAnswerA = new System.Windows.Forms.TextBox();
            this.labAnswerA = new System.Windows.Forms.Label();
            this.txtBoxTitleName = new System.Windows.Forms.TextBox();
            this.labTitleName = new System.Windows.Forms.Label();
            this.labChapter = new System.Windows.Forms.Label();
            this.combChapter = new Skynet.CustomComponent.LookUpEditEx.LookUpEditEx();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.combChapter.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(120, 451);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(217, 451);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // combTypeName
            // 
            this.combTypeName.FormattingEnabled = true;
            this.combTypeName.Items.AddRange(new object[] {
            "单选题",
            "是非题",
            "多选题"});
            this.combTypeName.Location = new System.Drawing.Point(83, 78);
            this.combTypeName.Name = "combTypeName";
            this.combTypeName.Size = new System.Drawing.Size(309, 20);
            this.combTypeName.TabIndex = 9;
            // 
            // labTypeName
            // 
            this.labTypeName.AutoSize = true;
            this.labTypeName.Location = new System.Drawing.Point(26, 82);
            this.labTypeName.Name = "labTypeName";
            this.labTypeName.Size = new System.Drawing.Size(53, 12);
            this.labTypeName.TabIndex = 10;
            this.labTypeName.Text = "题目类型";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.combChapter);
            this.panel1.Controls.Add(this.labChapter);
            this.panel1.Controls.Add(this.combScore);
            this.panel1.Controls.Add(this.labScore);
            this.panel1.Controls.Add(this.txtboxCorrentAnswer);
            this.panel1.Controls.Add(this.txtboxAnswerF);
            this.panel1.Controls.Add(this.txtboxAnswerE);
            this.panel1.Controls.Add(this.txtboxAnswerD);
            this.panel1.Controls.Add(this.txtboxAnswerC);
            this.panel1.Controls.Add(this.txtboxAnswerB);
            this.panel1.Controls.Add(this.labCorrectAnswer);
            this.panel1.Controls.Add(this.labAnswerF);
            this.panel1.Controls.Add(this.labAnswerE);
            this.panel1.Controls.Add(this.labAnswerD);
            this.panel1.Controls.Add(this.labAnswerC);
            this.panel1.Controls.Add(this.labAnswerB);
            this.panel1.Controls.Add(this.txtboxAnswerA);
            this.panel1.Controls.Add(this.labAnswerA);
            this.panel1.Controls.Add(this.txtBoxTitleName);
            this.panel1.Controls.Add(this.labTitleName);
            this.panel1.Controls.Add(this.labTypeName);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.combTypeName);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(449, 496);
            this.panel1.TabIndex = 11;
            // 
            // combScore
            // 
            this.combScore.FormattingEnabled = true;
            this.combScore.Items.AddRange(new object[] {
            "5",
            "10",
            "15",
            "20"});
            this.combScore.Location = new System.Drawing.Point(264, 404);
            this.combScore.Name = "combScore";
            this.combScore.Size = new System.Drawing.Size(128, 20);
            this.combScore.TabIndex = 28;
            // 
            // labScore
            // 
            this.labScore.AutoSize = true;
            this.labScore.Location = new System.Drawing.Point(229, 409);
            this.labScore.Name = "labScore";
            this.labScore.Size = new System.Drawing.Size(29, 12);
            this.labScore.TabIndex = 27;
            this.labScore.Text = "分值";
            // 
            // txtboxCorrentAnswer
            // 
            this.txtboxCorrentAnswer.Location = new System.Drawing.Point(86, 404);
            this.txtboxCorrentAnswer.Name = "txtboxCorrentAnswer";
            this.txtboxCorrentAnswer.Size = new System.Drawing.Size(120, 21);
            this.txtboxCorrentAnswer.TabIndex = 26;
            // 
            // txtboxAnswerF
            // 
            this.txtboxAnswerF.Location = new System.Drawing.Point(83, 361);
            this.txtboxAnswerF.Name = "txtboxAnswerF";
            this.txtboxAnswerF.Size = new System.Drawing.Size(309, 21);
            this.txtboxAnswerF.TabIndex = 25;
            // 
            // txtboxAnswerE
            // 
            this.txtboxAnswerE.Location = new System.Drawing.Point(83, 319);
            this.txtboxAnswerE.Name = "txtboxAnswerE";
            this.txtboxAnswerE.Size = new System.Drawing.Size(309, 21);
            this.txtboxAnswerE.TabIndex = 24;
            // 
            // txtboxAnswerD
            // 
            this.txtboxAnswerD.Location = new System.Drawing.Point(83, 279);
            this.txtboxAnswerD.Name = "txtboxAnswerD";
            this.txtboxAnswerD.Size = new System.Drawing.Size(309, 21);
            this.txtboxAnswerD.TabIndex = 23;
            // 
            // txtboxAnswerC
            // 
            this.txtboxAnswerC.Location = new System.Drawing.Point(83, 239);
            this.txtboxAnswerC.Name = "txtboxAnswerC";
            this.txtboxAnswerC.Size = new System.Drawing.Size(309, 21);
            this.txtboxAnswerC.TabIndex = 22;
            // 
            // txtboxAnswerB
            // 
            this.txtboxAnswerB.Location = new System.Drawing.Point(83, 200);
            this.txtboxAnswerB.Name = "txtboxAnswerB";
            this.txtboxAnswerB.Size = new System.Drawing.Size(309, 21);
            this.txtboxAnswerB.TabIndex = 21;
            // 
            // labCorrectAnswer
            // 
            this.labCorrectAnswer.AutoSize = true;
            this.labCorrectAnswer.Location = new System.Drawing.Point(27, 407);
            this.labCorrectAnswer.Name = "labCorrectAnswer";
            this.labCorrectAnswer.Size = new System.Drawing.Size(53, 12);
            this.labCorrectAnswer.TabIndex = 20;
            this.labCorrectAnswer.Text = "正确选项";
            // 
            // labAnswerF
            // 
            this.labAnswerF.AutoSize = true;
            this.labAnswerF.Location = new System.Drawing.Point(27, 365);
            this.labAnswerF.Name = "labAnswerF";
            this.labAnswerF.Size = new System.Drawing.Size(35, 12);
            this.labAnswerF.TabIndex = 19;
            this.labAnswerF.Text = "答案F";
            // 
            // labAnswerE
            // 
            this.labAnswerE.AutoSize = true;
            this.labAnswerE.Location = new System.Drawing.Point(27, 325);
            this.labAnswerE.Name = "labAnswerE";
            this.labAnswerE.Size = new System.Drawing.Size(35, 12);
            this.labAnswerE.TabIndex = 18;
            this.labAnswerE.Text = "答案E";
            // 
            // labAnswerD
            // 
            this.labAnswerD.AutoSize = true;
            this.labAnswerD.Location = new System.Drawing.Point(27, 284);
            this.labAnswerD.Name = "labAnswerD";
            this.labAnswerD.Size = new System.Drawing.Size(35, 12);
            this.labAnswerD.TabIndex = 17;
            this.labAnswerD.Text = "答案D";
            // 
            // labAnswerC
            // 
            this.labAnswerC.AutoSize = true;
            this.labAnswerC.Location = new System.Drawing.Point(27, 244);
            this.labAnswerC.Name = "labAnswerC";
            this.labAnswerC.Size = new System.Drawing.Size(35, 12);
            this.labAnswerC.TabIndex = 16;
            this.labAnswerC.Text = "答案C";
            // 
            // labAnswerB
            // 
            this.labAnswerB.AutoSize = true;
            this.labAnswerB.Location = new System.Drawing.Point(27, 203);
            this.labAnswerB.Name = "labAnswerB";
            this.labAnswerB.Size = new System.Drawing.Size(35, 12);
            this.labAnswerB.TabIndex = 15;
            this.labAnswerB.Text = "答案B";
            // 
            // txtboxAnswerA
            // 
            this.txtboxAnswerA.Location = new System.Drawing.Point(83, 164);
            this.txtboxAnswerA.Name = "txtboxAnswerA";
            this.txtboxAnswerA.Size = new System.Drawing.Size(309, 21);
            this.txtboxAnswerA.TabIndex = 14;
            // 
            // labAnswerA
            // 
            this.labAnswerA.AutoSize = true;
            this.labAnswerA.Location = new System.Drawing.Point(27, 168);
            this.labAnswerA.Name = "labAnswerA";
            this.labAnswerA.Size = new System.Drawing.Size(35, 12);
            this.labAnswerA.TabIndex = 13;
            this.labAnswerA.Text = "答案A";
            // 
            // txtBoxTitleName
            // 
            this.txtBoxTitleName.Location = new System.Drawing.Point(83, 115);
            this.txtBoxTitleName.Name = "txtBoxTitleName";
            this.txtBoxTitleName.Size = new System.Drawing.Size(309, 21);
            this.txtBoxTitleName.TabIndex = 12;
            // 
            // labTitleName
            // 
            this.labTitleName.AutoSize = true;
            this.labTitleName.Location = new System.Drawing.Point(27, 119);
            this.labTitleName.Name = "labTitleName";
            this.labTitleName.Size = new System.Drawing.Size(29, 12);
            this.labTitleName.TabIndex = 11;
            this.labTitleName.Text = "题目";
            // 
            // labChapter
            // 
            this.labChapter.AutoSize = true;
            this.labChapter.Location = new System.Drawing.Point(27, 44);
            this.labChapter.Name = "labChapter";
            this.labChapter.Size = new System.Drawing.Size(29, 12);
            this.labChapter.TabIndex = 29;
            this.labChapter.Text = "章节";
            // 
            // combChapter
            // 
            this.combChapter.DisplayValue = "";
            this.combChapter.DropDownHeight = 200;
            this.combChapter.DropDownWidth = 250;
            this.combChapter.FieldValue = "";
            this.combChapter.Location = new System.Drawing.Point(83, 41);
            this.combChapter.Name = "combChapter";
            this.combChapter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.combChapter.Properties.ShowPopupCloseButton = false;
            this.combChapter.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.combChapter.SelectedRow = null;
            this.combChapter.Size = new System.Drawing.Size(309, 21);
            this.combChapter.SpecialChar = new char[] {
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.',
        '.'};
            this.combChapter.TabIndex = 31;
            this.combChapter.Table = null;
            // 
            // QuestionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Sxnqcc.Properties.Resources.background_mainwnd;
            this.ClientSize = new System.Drawing.Size(473, 523);
            this.Controls.Add(this.panel1);
            this.Name = "QuestionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "题库管理";
            this.Load += new System.EventHandler(this.QuestionForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.combChapter.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox combTypeName;
        private System.Windows.Forms.Label labTypeName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labScore;
        private System.Windows.Forms.TextBox txtboxCorrentAnswer;
        private System.Windows.Forms.TextBox txtboxAnswerF;
        private System.Windows.Forms.TextBox txtboxAnswerE;
        private System.Windows.Forms.TextBox txtboxAnswerD;
        private System.Windows.Forms.TextBox txtboxAnswerC;
        private System.Windows.Forms.TextBox txtboxAnswerB;
        private System.Windows.Forms.Label labCorrectAnswer;
        private System.Windows.Forms.Label labAnswerF;
        private System.Windows.Forms.Label labAnswerE;
        private System.Windows.Forms.Label labAnswerD;
        private System.Windows.Forms.Label labAnswerC;
        private System.Windows.Forms.Label labAnswerB;
        private System.Windows.Forms.TextBox txtboxAnswerA;
        private System.Windows.Forms.Label labAnswerA;
        private System.Windows.Forms.TextBox txtBoxTitleName;
        private System.Windows.Forms.Label labTitleName;
        private System.Windows.Forms.ComboBox combScore;
        private System.Windows.Forms.Label labChapter;
        private Skynet.CustomComponent.LookUpEditEx.LookUpEditEx combChapter;
    }
}