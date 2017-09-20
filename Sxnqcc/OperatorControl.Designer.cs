namespace Sxnqcc
{
    partial class OperatorControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblcount = new System.Windows.Forms.Label();
            this.txtfind = new System.Windows.Forms.TextBox();
            this.btn_dc_Click = new System.Windows.Forms.PictureBox();
            this.picRefresh = new System.Windows.Forms.PictureBox();
            this.picupdate = new System.Windows.Forms.PictureBox();
            this.picdelete = new System.Windows.Forms.PictureBox();
            this.picAdd = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.lblTopname = new System.Windows.Forms.Label();
            this.ddlRoleState = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_dc_Click)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picupdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picdelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 111);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(838, 407);
            this.gridControl1.TabIndex = 19;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            this.gridControl1.DoubleClick += new System.EventHandler(this.gridControl1_DoubleClick);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(247)))), ((int)(((byte)(249)))));
            this.panel1.Controls.Add(this.ddlRoleState);
            this.panel1.Controls.Add(this.lblcount);
            this.panel1.Controls.Add(this.txtfind);
            this.panel1.Controls.Add(this.btn_dc_Click);
            this.panel1.Controls.Add(this.picRefresh);
            this.panel1.Controls.Add(this.picupdate);
            this.panel1.Controls.Add(this.picdelete);
            this.panel1.Controls.Add(this.picAdd);
            this.panel1.Controls.Add(this.pictureBox7);
            this.panel1.Controls.Add(this.lblTopname);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(838, 111);
            this.panel1.TabIndex = 18;
            // 
            // lblcount
            // 
            this.lblcount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblcount.AutoSize = true;
            this.lblcount.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblcount.Location = new System.Drawing.Point(727, 69);
            this.lblcount.Name = "lblcount";
            this.lblcount.Size = new System.Drawing.Size(59, 20);
            this.lblcount.TabIndex = 42;
            this.lblcount.Text = "共0条";
            // 
            // txtfind
            // 
            this.txtfind.Location = new System.Drawing.Point(340, 72);
            this.txtfind.Name = "txtfind";
            this.txtfind.Size = new System.Drawing.Size(186, 21);
            this.txtfind.TabIndex = 41;
            // 
            // btn_dc_Click
            // 
            this.btn_dc_Click.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_dc_Click.Image = global::Sxnqcc.Properties.Resources.magnifying_glass_32x32;
            this.btn_dc_Click.Location = new System.Drawing.Point(544, 69);
            this.btn_dc_Click.Name = "btn_dc_Click";
            this.btn_dc_Click.Size = new System.Drawing.Size(64, 25);
            this.btn_dc_Click.TabIndex = 40;
            this.btn_dc_Click.TabStop = false;
            this.btn_dc_Click.Click += new System.EventHandler(this.btn_dc_Click_Click);
            // 
            // picRefresh
            // 
            this.picRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picRefresh.Image = global::Sxnqcc.Properties.Resources.刷新;
            this.picRefresh.Location = new System.Drawing.Point(252, 69);
            this.picRefresh.Name = "picRefresh";
            this.picRefresh.Size = new System.Drawing.Size(64, 25);
            this.picRefresh.TabIndex = 37;
            this.picRefresh.TabStop = false;
            this.picRefresh.Click += new System.EventHandler(this.picRefresh_Click_1);
            // 
            // picupdate
            // 
            this.picupdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picupdate.Image = global::Sxnqcc.Properties.Resources.pen_32x32;
            this.picupdate.Location = new System.Drawing.Point(171, 69);
            this.picupdate.Name = "picupdate";
            this.picupdate.Size = new System.Drawing.Size(64, 25);
            this.picupdate.TabIndex = 36;
            this.picupdate.TabStop = false;
            this.picupdate.Click += new System.EventHandler(this.picupdate_Click);
            // 
            // picdelete
            // 
            this.picdelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picdelete.Image = global::Sxnqcc.Properties.Resources.x_alt_32x32;
            this.picdelete.Location = new System.Drawing.Point(92, 69);
            this.picdelete.Name = "picdelete";
            this.picdelete.Size = new System.Drawing.Size(64, 25);
            this.picdelete.TabIndex = 35;
            this.picdelete.TabStop = false;
            this.picdelete.Click += new System.EventHandler(this.picdelete_Click);
            // 
            // picAdd
            // 
            this.picAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picAdd.Image = global::Sxnqcc.Properties.Resources.plus_alt_32x32;
            this.picAdd.Location = new System.Drawing.Point(12, 69);
            this.picAdd.Name = "picAdd";
            this.picAdd.Size = new System.Drawing.Size(64, 25);
            this.picAdd.TabIndex = 34;
            this.picAdd.TabStop = false;
            this.picAdd.Click += new System.EventHandler(this.picAdd_Click);
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(102)))), ((int)(((byte)(102)))));
            this.pictureBox7.Location = new System.Drawing.Point(17, 25);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(10, 10);
            this.pictureBox7.TabIndex = 28;
            this.pictureBox7.TabStop = false;
            // 
            // lblTopname
            // 
            this.lblTopname.AutoSize = true;
            this.lblTopname.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTopname.Location = new System.Drawing.Point(36, 12);
            this.lblTopname.Name = "lblTopname";
            this.lblTopname.Size = new System.Drawing.Size(107, 25);
            this.lblTopname.TabIndex = 0;
            this.lblTopname.Text = "操作员管理";
            // 
            // ddlRoleState
            // 
            this.ddlRoleState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlRoleState.FormattingEnabled = true;
            this.ddlRoleState.Items.AddRange(new object[] {
            "全部",
            "护士",
            "省厅",
            "区域",
            "护理部",
            "护士长",
            "未知状态"});
            this.ddlRoleState.Location = new System.Drawing.Point(252, 17);
            this.ddlRoleState.Name = "ddlRoleState";
            this.ddlRoleState.Size = new System.Drawing.Size(112, 20);
            this.ddlRoleState.TabIndex = 43;
            this.ddlRoleState.SelectedIndexChanged += new System.EventHandler(this.ddlRoleState_SelectedIndexChanged);
            // 
            // OperatorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panel1);
            this.Name = "OperatorControl";
            this.Size = new System.Drawing.Size(838, 518);
            this.Load += new System.EventHandler(this.OperatorControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_dc_Click)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picupdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picdelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.PictureBox btn_dc_Click;
        private System.Windows.Forms.PictureBox picRefresh;
        public System.Windows.Forms.PictureBox picupdate;
        private System.Windows.Forms.PictureBox picdelete;
        private System.Windows.Forms.PictureBox picAdd;
        private System.Windows.Forms.PictureBox pictureBox7;
        public System.Windows.Forms.Label lblTopname;
        private System.Windows.Forms.TextBox txtfind;
        private System.Windows.Forms.Label lblcount;
        private System.Windows.Forms.ComboBox ddlRoleState;
    }
}
