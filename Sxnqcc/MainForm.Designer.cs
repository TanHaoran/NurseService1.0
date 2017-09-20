namespace Sxnqcc
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainPanel = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.账号管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.医院管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.科室管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.课程管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.课程管理ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.课程目录管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.课程章节管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.题库管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanel
            // 
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 25);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1229, 713);
            this.MainPanel.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.账号管理ToolStripMenuItem,
            this.医院管理ToolStripMenuItem,
            this.科室管理ToolStripMenuItem,
            this.课程管理ToolStripMenuItem,
            this.题库管理ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1229, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 账号管理ToolStripMenuItem
            // 
            this.账号管理ToolStripMenuItem.Name = "账号管理ToolStripMenuItem";
            this.账号管理ToolStripMenuItem.Size = new System.Drawing.Size(80, 21);
            this.账号管理ToolStripMenuItem.Text = "操作员管理";
            this.账号管理ToolStripMenuItem.Click += new System.EventHandler(this.账号管理ToolStripMenuItem_Click);
            // 
            // 医院管理ToolStripMenuItem
            // 
            this.医院管理ToolStripMenuItem.Name = "医院管理ToolStripMenuItem";
            this.医院管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.医院管理ToolStripMenuItem.Text = "医院管理";
            this.医院管理ToolStripMenuItem.Click += new System.EventHandler(this.医院管理ToolStripMenuItem_Click);
            // 
            // 科室管理ToolStripMenuItem
            // 
            this.科室管理ToolStripMenuItem.Name = "科室管理ToolStripMenuItem";
            this.科室管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.科室管理ToolStripMenuItem.Text = "科室管理";
            this.科室管理ToolStripMenuItem.Click += new System.EventHandler(this.科室管理ToolStripMenuItem_Click);
            // 
            // 课程管理ToolStripMenuItem
            // 
            this.课程管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.课程管理ToolStripMenuItem1,
            this.课程目录管理ToolStripMenuItem,
            this.课程章节管理ToolStripMenuItem});
            this.课程管理ToolStripMenuItem.Name = "课程管理ToolStripMenuItem";
            this.课程管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.课程管理ToolStripMenuItem.Text = "课程管理";
            // 
            // 课程管理ToolStripMenuItem1
            // 
            this.课程管理ToolStripMenuItem1.Name = "课程管理ToolStripMenuItem1";
            this.课程管理ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.课程管理ToolStripMenuItem1.Text = "课程管理";
            this.课程管理ToolStripMenuItem1.Click += new System.EventHandler(this.课程管理ToolStripMenuItem1_Click);
            // 
            // 课程目录管理ToolStripMenuItem
            // 
            this.课程目录管理ToolStripMenuItem.Name = "课程目录管理ToolStripMenuItem";
            this.课程目录管理ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.课程目录管理ToolStripMenuItem.Text = "课程目录管理";
            this.课程目录管理ToolStripMenuItem.Click += new System.EventHandler(this.课程目录管理ToolStripMenuItem_Click);
            // 
            // 课程章节管理ToolStripMenuItem
            // 
            this.课程章节管理ToolStripMenuItem.Name = "课程章节管理ToolStripMenuItem";
            this.课程章节管理ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.课程章节管理ToolStripMenuItem.Text = "课程章节管理";
            this.课程章节管理ToolStripMenuItem.Click += new System.EventHandler(this.课程章节管理ToolStripMenuItem_Click);
            // 
            // 题库管理ToolStripMenuItem
            // 
            this.题库管理ToolStripMenuItem.Name = "题库管理ToolStripMenuItem";
            this.题库管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.题库管理ToolStripMenuItem.Text = "题库管理";
            this.题库管理ToolStripMenuItem.Click += new System.EventHandler(this.题库管理ToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1229, 738);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "陕西省护理质量控制中心安全（不良）事件上报系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 医院管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 账号管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 科室管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 课程管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 课程管理ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 课程目录管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 课程章节管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 题库管理ToolStripMenuItem;
    }
}

