namespace Sxnqcc
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.txtname = new System.Windows.Forms.TextBox();
            this.txtpwd = new System.Windows.Forms.TextBox();
            this.btnkogin = new System.Windows.Forms.Button();
            this.btnexit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtname
            // 
            this.txtname.Location = new System.Drawing.Point(126, 68);
            this.txtname.Name = "txtname";
            this.txtname.Size = new System.Drawing.Size(149, 21);
            this.txtname.TabIndex = 0;
            // 
            // txtpwd
            // 
            this.txtpwd.Location = new System.Drawing.Point(126, 126);
            this.txtpwd.Name = "txtpwd";
            this.txtpwd.PasswordChar = '*';
            this.txtpwd.Size = new System.Drawing.Size(149, 21);
            this.txtpwd.TabIndex = 1;
            this.txtpwd.Text = "123456";
            // 
            // btnkogin
            // 
            this.btnkogin.Location = new System.Drawing.Point(75, 172);
            this.btnkogin.Name = "btnkogin";
            this.btnkogin.Size = new System.Drawing.Size(75, 23);
            this.btnkogin.TabIndex = 2;
            this.btnkogin.Text = "登录";
            this.btnkogin.UseVisualStyleBackColor = true;
            this.btnkogin.Click += new System.EventHandler(this.btnkogin_Click);
            // 
            // btnexit
            // 
            this.btnexit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnexit.Location = new System.Drawing.Point(168, 172);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(75, 23);
            this.btnexit.TabIndex = 3;
            this.btnexit.Text = "退出";
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "用户名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "密  码";
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnkogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnexit;
            this.ClientSize = new System.Drawing.Size(360, 262);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnexit);
            this.Controls.Add(this.btnkogin);
            this.Controls.Add(this.txtpwd);
            this.Controls.Add(this.txtname);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "不良事件后台管理登录";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtname;
        private System.Windows.Forms.TextBox txtpwd;
        private System.Windows.Forms.Button btnkogin;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}