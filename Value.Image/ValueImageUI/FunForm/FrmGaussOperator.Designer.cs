namespace ValueImageUI.FuncForm
{
    partial class FrmGaussOperator
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
            this.BtnConfirm = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RdoDoG = new System.Windows.Forms.RadioButton();
            this.RdoLoG = new System.Windows.Forms.RadioButton();
            this.LblThresholding = new System.Windows.Forms.Label();
            this.LblSigma = new System.Windows.Forms.Label();
            this.TxtSigma = new System.Windows.Forms.TextBox();
            this.TxtThresholding = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnConfirm
            // 
            this.BtnConfirm.Location = new System.Drawing.Point(86, 201);
            this.BtnConfirm.Name = "BtnConfirm";
            this.BtnConfirm.Size = new System.Drawing.Size(75, 23);
            this.BtnConfirm.TabIndex = 0;
            this.BtnConfirm.Text = "确定";
            this.BtnConfirm.UseVisualStyleBackColor = true;
            this.BtnConfirm.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RdoDoG);
            this.panel1.Controls.Add(this.RdoLoG);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(168, 85);
            this.panel1.TabIndex = 1;
            // 
            // RdoDoG
            // 
            this.RdoDoG.AutoSize = true;
            this.RdoDoG.Location = new System.Drawing.Point(24, 54);
            this.RdoDoG.Name = "RdoDoG";
            this.RdoDoG.Size = new System.Drawing.Size(95, 16);
            this.RdoDoG.TabIndex = 3;
            this.RdoDoG.TabStop = true;
            this.RdoDoG.Text = "差分高斯算子";
            this.RdoDoG.UseVisualStyleBackColor = true;
            // 
            // RdoLoG
            // 
            this.RdoLoG.AutoSize = true;
            this.RdoLoG.Location = new System.Drawing.Point(24, 17);
            this.RdoLoG.Name = "RdoLoG";
            this.RdoLoG.Size = new System.Drawing.Size(125, 16);
            this.RdoLoG.TabIndex = 2;
            this.RdoLoG.TabStop = true;
            this.RdoLoG.Text = "拉普拉斯-高斯算子";
            this.RdoLoG.UseVisualStyleBackColor = true;
            // 
            // LblThresholding
            // 
            this.LblThresholding.AutoSize = true;
            this.LblThresholding.Location = new System.Drawing.Point(22, 162);
            this.LblThresholding.Name = "LblThresholding";
            this.LblThresholding.Size = new System.Drawing.Size(35, 12);
            this.LblThresholding.TabIndex = 2;
            this.LblThresholding.Text = "阈值:";
            // 
            // LblSigma
            // 
            this.LblSigma.AutoSize = true;
            this.LblSigma.Location = new System.Drawing.Point(22, 125);
            this.LblSigma.Name = "LblSigma";
            this.LblSigma.Size = new System.Drawing.Size(47, 12);
            this.LblSigma.TabIndex = 3;
            this.LblSigma.Text = "均方差:";
            // 
            // TxtSigma
            // 
            this.TxtSigma.Location = new System.Drawing.Point(75, 122);
            this.TxtSigma.Name = "TxtSigma";
            this.TxtSigma.Size = new System.Drawing.Size(100, 21);
            this.TxtSigma.TabIndex = 4;
            this.TxtSigma.Text = "1.5";
            // 
            // TxtThresholding
            // 
            this.TxtThresholding.Location = new System.Drawing.Point(75, 159);
            this.TxtThresholding.Name = "TxtThresholding";
            this.TxtThresholding.Size = new System.Drawing.Size(100, 21);
            this.TxtThresholding.TabIndex = 5;
            this.TxtThresholding.Text = "40";
            // 
            // FrmGaussOperator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(196, 237);
            this.Controls.Add(this.TxtThresholding);
            this.Controls.Add(this.TxtSigma);
            this.Controls.Add(this.LblSigma);
            this.Controls.Add(this.LblThresholding);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.BtnConfirm);
            this.Name = "FrmGaussOperator";
            this.Text = "FrmGaussOperator";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnConfirm;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton RdoDoG;
        private System.Windows.Forms.RadioButton RdoLoG;
        private System.Windows.Forms.Label LblThresholding;
        private System.Windows.Forms.Label LblSigma;
        private System.Windows.Forms.TextBox TxtSigma;
        private System.Windows.Forms.TextBox TxtThresholding;
    }
}