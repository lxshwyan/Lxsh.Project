namespace Lxsh.Project.CameraAforge
{
    partial class Form1
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
            this.vispShoot = new AForge.Controls.VideoSourcePlayer();
            this.cmbCamera = new System.Windows.Forms.ComboBox();
            this.cmbResolution = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnShoot = new System.Windows.Forms.Button();
            this.picbPreview = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picbPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // vispShoot
            // 
            this.vispShoot.Location = new System.Drawing.Point(12, 89);
            this.vispShoot.Name = "vispShoot";
            this.vispShoot.Size = new System.Drawing.Size(669, 321);
            this.vispShoot.TabIndex = 0;
            this.vispShoot.Text = "videoSourcePlayer1";
            this.vispShoot.VideoSource = null;
            // 
            // cmbCamera
            // 
            this.cmbCamera.FormattingEnabled = true;
            this.cmbCamera.Location = new System.Drawing.Point(80, 25);
            this.cmbCamera.Name = "cmbCamera";
            this.cmbCamera.Size = new System.Drawing.Size(140, 20);
            this.cmbCamera.TabIndex = 1;
            this.cmbCamera.SelectedIndexChanged += new System.EventHandler(this.cmbCamera_SelectedIndexChanged);
            // 
            // cmbResolution
            // 
            this.cmbResolution.FormattingEnabled = true;
            this.cmbResolution.Items.AddRange(new object[] {
            "640×480",
            "669, 321"});
            this.cmbResolution.Location = new System.Drawing.Point(80, 63);
            this.cmbResolution.Name = "cmbResolution";
            this.cmbResolution.Size = new System.Drawing.Size(140, 20);
            this.cmbResolution.TabIndex = 2;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(248, 53);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(367, 53);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 4;
            this.btnDisconnect.Text = "断开";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnShoot
            // 
            this.btnShoot.Location = new System.Drawing.Point(495, 53);
            this.btnShoot.Name = "btnShoot";
            this.btnShoot.Size = new System.Drawing.Size(75, 23);
            this.btnShoot.TabIndex = 5;
            this.btnShoot.Text = "拍照";
            this.btnShoot.UseVisualStyleBackColor = true;
            this.btnShoot.Click += new System.EventHandler(this.btnShoot_Click);
            // 
            // picbPreview
            // 
            this.picbPreview.Location = new System.Drawing.Point(12, 416);
            this.picbPreview.Name = "picbPreview";
            this.picbPreview.Size = new System.Drawing.Size(669, 313);
            this.picbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbPreview.TabIndex = 6;
            this.picbPreview.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "分辨率：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "摄像头：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 734);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picbPreview);
            this.Controls.Add(this.btnShoot);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.cmbResolution);
            this.Controls.Add(this.cmbCamera);
            this.Controls.Add(this.vispShoot);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picbPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AForge.Controls.VideoSourcePlayer vispShoot;
        private System.Windows.Forms.ComboBox cmbCamera;
        private System.Windows.Forms.ComboBox cmbResolution;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnShoot;
        private System.Windows.Forms.PictureBox picbPreview;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

