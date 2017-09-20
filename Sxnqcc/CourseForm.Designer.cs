namespace Sxnqcc
{
    partial class CourseForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.combCourseType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCourseName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.combCourseLevel = new System.Windows.Forms.ComboBox();
            this.combSuitableDuty = new System.Windows.Forms.ComboBox();
            this.txtCourseTeacher = new System.Windows.Forms.TextBox();
            this.txtCourseInfo = new System.Windows.Forms.TextBox();
            this.txtCourseDur = new System.Windows.Forms.TextBox();
            this.txtCourseHot = new System.Windows.Forms.TextBox();
            this.txtRecommand = new System.Windows.Forms.TextBox();
            this.txtTeacherIntroduction = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtTeacherIntroduction);
            this.panel1.Controls.Add(this.txtRecommand);
            this.panel1.Controls.Add(this.txtCourseHot);
            this.panel1.Controls.Add(this.txtCourseDur);
            this.panel1.Controls.Add(this.txtCourseInfo);
            this.panel1.Controls.Add(this.txtCourseTeacher);
            this.panel1.Controls.Add(this.combSuitableDuty);
            this.panel1.Controls.Add(this.combCourseLevel);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtCourseName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.combCourseType);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 640);
            this.panel1.TabIndex = 0;
            // 
            // combCourseType
            // 
            this.combCourseType.FormattingEnabled = true;
            this.combCourseType.Items.AddRange(new object[] {
            "内科课程",
            "外科课程",
            "儿科课程"});
            this.combCourseType.Location = new System.Drawing.Point(109, 22);
            this.combCourseType.Name = "combCourseType";
            this.combCourseType.Size = new System.Drawing.Size(380, 20);
            this.combCourseType.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "课程类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "课程名称";
            // 
            // txtCourseName
            // 
            this.txtCourseName.Location = new System.Drawing.Point(109, 61);
            this.txtCourseName.Name = "txtCourseName";
            this.txtCourseName.Size = new System.Drawing.Size(380, 21);
            this.txtCourseName.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Location = new System.Drawing.Point(43, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 14);
            this.label3.TabIndex = 14;
            this.label3.Text = "授课教员";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "课程简介";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(41, 210);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "课程时长";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(41, 259);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 17;
            this.label6.Text = "课程等级";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(41, 311);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 18;
            this.label7.Text = "适用职称";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(43, 356);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 21;
            this.label10.Text = "热度";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(43, 402);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 22;
            this.label11.Text = "推荐";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(41, 450);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 23;
            this.label12.Text = "教师简介";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(170, 533);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(289, 533);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 24;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // combCourseLevel
            // 
            this.combCourseLevel.FormattingEnabled = true;
            this.combCourseLevel.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.combCourseLevel.Location = new System.Drawing.Point(109, 258);
            this.combCourseLevel.Name = "combCourseLevel";
            this.combCourseLevel.Size = new System.Drawing.Size(380, 20);
            this.combCourseLevel.TabIndex = 25;
            // 
            // combSuitableDuty
            // 
            this.combSuitableDuty.FormattingEnabled = true;
            this.combSuitableDuty.Items.AddRange(new object[] {
            "医生",
            "医师",
            "护士",
            "护师"});
            this.combSuitableDuty.Location = new System.Drawing.Point(109, 308);
            this.combSuitableDuty.Name = "combSuitableDuty";
            this.combSuitableDuty.Size = new System.Drawing.Size(380, 20);
            this.combSuitableDuty.TabIndex = 26;
            // 
            // txtCourseTeacher
            // 
            this.txtCourseTeacher.Location = new System.Drawing.Point(109, 108);
            this.txtCourseTeacher.Name = "txtCourseTeacher";
            this.txtCourseTeacher.Size = new System.Drawing.Size(380, 21);
            this.txtCourseTeacher.TabIndex = 27;
            // 
            // txtCourseInfo
            // 
            this.txtCourseInfo.Location = new System.Drawing.Point(109, 157);
            this.txtCourseInfo.Name = "txtCourseInfo";
            this.txtCourseInfo.Size = new System.Drawing.Size(380, 21);
            this.txtCourseInfo.TabIndex = 28;
            // 
            // txtCourseDur
            // 
            this.txtCourseDur.Location = new System.Drawing.Point(109, 207);
            this.txtCourseDur.Name = "txtCourseDur";
            this.txtCourseDur.Size = new System.Drawing.Size(380, 21);
            this.txtCourseDur.TabIndex = 29;
            // 
            // txtCourseHot
            // 
            this.txtCourseHot.Location = new System.Drawing.Point(109, 353);
            this.txtCourseHot.Name = "txtCourseHot";
            this.txtCourseHot.Size = new System.Drawing.Size(380, 21);
            this.txtCourseHot.TabIndex = 30;
            // 
            // txtRecommand
            // 
            this.txtRecommand.Location = new System.Drawing.Point(109, 399);
            this.txtRecommand.Name = "txtRecommand";
            this.txtRecommand.Size = new System.Drawing.Size(380, 21);
            this.txtRecommand.TabIndex = 31;
            // 
            // txtTeacherIntroduction
            // 
            this.txtTeacherIntroduction.Location = new System.Drawing.Point(109, 445);
            this.txtTeacherIntroduction.Name = "txtTeacherIntroduction";
            this.txtTeacherIntroduction.Size = new System.Drawing.Size(380, 21);
            this.txtTeacherIntroduction.TabIndex = 32;
            // 
            // CourseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Sxnqcc.Properties.Resources.background_mainwnd;
            this.ClientSize = new System.Drawing.Size(584, 661);
            this.Controls.Add(this.panel1);
            this.Name = "CourseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "课程管理";
            this.Load += new System.EventHandler(this.CourseForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCourseName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combCourseType;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtTeacherIntroduction;
        private System.Windows.Forms.TextBox txtRecommand;
        private System.Windows.Forms.TextBox txtCourseHot;
        private System.Windows.Forms.TextBox txtCourseDur;
        private System.Windows.Forms.TextBox txtCourseInfo;
        private System.Windows.Forms.TextBox txtCourseTeacher;
        private System.Windows.Forms.ComboBox combSuitableDuty;
        private System.Windows.Forms.ComboBox combCourseLevel;
    }
}