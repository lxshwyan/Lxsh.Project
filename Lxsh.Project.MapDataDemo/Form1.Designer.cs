namespace Lxsh.Project.MapDataDemo
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
            this.txtShengChepai = new System.Windows.Forms.TextBox();
            this.txtShengshengFfen = new System.Windows.Forms.TextBox();
            this.txtShishengFfen = new System.Windows.Forms.TextBox();
            this.txtShiChepai = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.txtShi = new System.Windows.Forms.TextBox();
            this.txtSheng = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtShengChepai
            // 
            this.txtShengChepai.Location = new System.Drawing.Point(614, 128);
            this.txtShengChepai.Name = "txtShengChepai";
            this.txtShengChepai.Size = new System.Drawing.Size(279, 21);
            this.txtShengChepai.TabIndex = 3;
            this.txtShengChepai.Text = "车牌";
            // 
            // txtShengshengFfen
            // 
            this.txtShengshengFfen.Location = new System.Drawing.Point(614, 173);
            this.txtShengshengFfen.Name = "txtShengshengFfen";
            this.txtShengshengFfen.Size = new System.Drawing.Size(295, 21);
            this.txtShengshengFfen.TabIndex = 4;
            this.txtShengshengFfen.Text = "身份证";
            // 
            // txtShishengFfen
            // 
            this.txtShishengFfen.Location = new System.Drawing.Point(614, 371);
            this.txtShishengFfen.Name = "txtShishengFfen";
            this.txtShishengFfen.Size = new System.Drawing.Size(294, 21);
            this.txtShishengFfen.TabIndex = 6;
            this.txtShishengFfen.Text = "身份证";
            // 
            // txtShiChepai
            // 
            this.txtShiChepai.Location = new System.Drawing.Point(614, 312);
            this.txtShiChepai.Name = "txtShiChepai";
            this.txtShiChepai.Size = new System.Drawing.Size(294, 21);
            this.txtShiChepai.TabIndex = 5;
            this.txtShiChepai.Text = "车牌";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(612, 482);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "保存数据";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(13, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(578, 710);
            this.treeView1.TabIndex = 8;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // txtShi
            // 
            this.txtShi.Location = new System.Drawing.Point(614, 262);
            this.txtShi.Name = "txtShi";
            this.txtShi.Size = new System.Drawing.Size(294, 21);
            this.txtShi.TabIndex = 10;
            this.txtShi.Text = "名称";
            // 
            // txtSheng
            // 
            this.txtSheng.Location = new System.Drawing.Point(614, 76);
            this.txtSheng.Name = "txtSheng";
            this.txtSheng.Size = new System.Drawing.Size(294, 21);
            this.txtSheng.TabIndex = 11;
            this.txtSheng.Text = "省名称";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(614, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "读取数据";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(310, 310);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 12;
            this.button3.Text = "刷新数据";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 713);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.txtSheng);
            this.Controls.Add(this.txtShi);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtShishengFfen);
            this.Controls.Add(this.txtShiChepai);
            this.Controls.Add(this.txtShengshengFfen);
            this.Controls.Add(this.txtShengChepai);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtShengChepai;
        private System.Windows.Forms.TextBox txtShengshengFfen;
        private System.Windows.Forms.TextBox txtShishengFfen;
        private System.Windows.Forms.TextBox txtShiChepai;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TextBox txtShi;
        private System.Windows.Forms.TextBox txtSheng;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
    }
}

