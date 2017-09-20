namespace Sxnqcc
{
    partial class CourseCatalogForm
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
            this.labCatalogName = new System.Windows.Forms.Label();
            this.txtCatalogName = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCatalogSort = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtCatalogSort);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.txtCatalogName);
            this.panel1.Controls.Add(this.labCatalogName);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(521, 480);
            this.panel1.TabIndex = 0;
            // 
            // labCatalogName
            // 
            this.labCatalogName.AutoSize = true;
            this.labCatalogName.Location = new System.Drawing.Point(26, 74);
            this.labCatalogName.Name = "labCatalogName";
            this.labCatalogName.Size = new System.Drawing.Size(53, 12);
            this.labCatalogName.TabIndex = 0;
            this.labCatalogName.Text = "目录名称";
            // 
            // txtCatalogName
            // 
            this.txtCatalogName.Location = new System.Drawing.Point(96, 71);
            this.txtCatalogName.Name = "txtCatalogName";
            this.txtCatalogName.Size = new System.Drawing.Size(376, 21);
            this.txtCatalogName.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(162, 350);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(294, 350);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "排序标识";
            // 
            // txtCatalogSort
            // 
            this.txtCatalogSort.Location = new System.Drawing.Point(96, 126);
            this.txtCatalogSort.Name = "txtCatalogSort";
            this.txtCatalogSort.Size = new System.Drawing.Size(376, 21);
            this.txtCatalogSort.TabIndex = 11;
            // 
            // CourseCatalogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Sxnqcc.Properties.Resources.background_mainwnd;
            this.ClientSize = new System.Drawing.Size(551, 505);
            this.Controls.Add(this.panel1);
            this.Name = "CourseCatalogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "课程目录";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtCatalogName;
        private System.Windows.Forms.Label labCatalogName;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtCatalogSort;
        private System.Windows.Forms.Label label1;
    }
}