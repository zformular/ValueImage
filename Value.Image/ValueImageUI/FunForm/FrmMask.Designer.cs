namespace ValueImageUI.FuncForm
{
    partial class FrmMask
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
            this.PnlThresholding = new System.Windows.Forms.Panel();
            this.PnlType = new System.Windows.Forms.Panel();
            this.BtnConfirm = new System.Windows.Forms.Button();
            this.RdoRoberts = new System.Windows.Forms.RadioButton();
            this.RdoPrewitt = new System.Windows.Forms.RadioButton();
            this.RdoSobel = new System.Windows.Forms.RadioButton();
            this.RdoLaplacian1 = new System.Windows.Forms.RadioButton();
            this.RdoLaplacian2 = new System.Windows.Forms.RadioButton();
            this.LblThresholding = new System.Windows.Forms.Label();
            this.TxtThresholding = new System.Windows.Forms.TextBox();
            this.RdoLaplacian3 = new System.Windows.Forms.RadioButton();
            this.RdoKirsch = new System.Windows.Forms.RadioButton();
            this.PnlThresholding.SuspendLayout();
            this.PnlType.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlThresholding
            // 
            this.PnlThresholding.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlThresholding.Controls.Add(this.TxtThresholding);
            this.PnlThresholding.Controls.Add(this.LblThresholding);
            this.PnlThresholding.Location = new System.Drawing.Point(139, 17);
            this.PnlThresholding.Name = "PnlThresholding";
            this.PnlThresholding.Size = new System.Drawing.Size(127, 81);
            this.PnlThresholding.TabIndex = 0;
            // 
            // PnlType
            // 
            this.PnlType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlType.Controls.Add(this.RdoKirsch);
            this.PnlType.Controls.Add(this.RdoLaplacian3);
            this.PnlType.Controls.Add(this.RdoLaplacian2);
            this.PnlType.Controls.Add(this.RdoLaplacian1);
            this.PnlType.Controls.Add(this.RdoSobel);
            this.PnlType.Controls.Add(this.RdoPrewitt);
            this.PnlType.Controls.Add(this.RdoRoberts);
            this.PnlType.Location = new System.Drawing.Point(12, 12);
            this.PnlType.Name = "PnlType";
            this.PnlType.Size = new System.Drawing.Size(109, 166);
            this.PnlType.TabIndex = 0;
            // 
            // BtnConfirm
            // 
            this.BtnConfirm.Location = new System.Drawing.Point(165, 138);
            this.BtnConfirm.Name = "BtnConfirm";
            this.BtnConfirm.Size = new System.Drawing.Size(75, 23);
            this.BtnConfirm.TabIndex = 1;
            this.BtnConfirm.Text = "确认";
            this.BtnConfirm.UseVisualStyleBackColor = true;
            this.BtnConfirm.Click += new System.EventHandler(this.BtnConfirm_Click);
            // 
            // RdoRoberts
            // 
            this.RdoRoberts.AutoSize = true;
            this.RdoRoberts.Location = new System.Drawing.Point(12, 3);
            this.RdoRoberts.Name = "RdoRoberts";
            this.RdoRoberts.Size = new System.Drawing.Size(65, 16);
            this.RdoRoberts.TabIndex = 0;
            this.RdoRoberts.TabStop = true;
            this.RdoRoberts.Text = "Roberts";
            this.RdoRoberts.UseVisualStyleBackColor = true;
            // 
            // RdoPrewitt
            // 
            this.RdoPrewitt.AutoSize = true;
            this.RdoPrewitt.Location = new System.Drawing.Point(12, 25);
            this.RdoPrewitt.Name = "RdoPrewitt";
            this.RdoPrewitt.Size = new System.Drawing.Size(65, 16);
            this.RdoPrewitt.TabIndex = 1;
            this.RdoPrewitt.TabStop = true;
            this.RdoPrewitt.Text = "Prewitt";
            this.RdoPrewitt.UseVisualStyleBackColor = true;
            // 
            // RdoSobel
            // 
            this.RdoSobel.AutoSize = true;
            this.RdoSobel.Location = new System.Drawing.Point(12, 47);
            this.RdoSobel.Name = "RdoSobel";
            this.RdoSobel.Size = new System.Drawing.Size(53, 16);
            this.RdoSobel.TabIndex = 2;
            this.RdoSobel.TabStop = true;
            this.RdoSobel.Text = "Sobel";
            this.RdoSobel.UseVisualStyleBackColor = true;
            // 
            // RdoLaplacian1
            // 
            this.RdoLaplacian1.AutoSize = true;
            this.RdoLaplacian1.Location = new System.Drawing.Point(12, 69);
            this.RdoLaplacian1.Name = "RdoLaplacian1";
            this.RdoLaplacian1.Size = new System.Drawing.Size(83, 16);
            this.RdoLaplacian1.TabIndex = 3;
            this.RdoLaplacian1.TabStop = true;
            this.RdoLaplacian1.Text = "Laplacian1";
            this.RdoLaplacian1.UseVisualStyleBackColor = true;
            // 
            // RdoLaplacian2
            // 
            this.RdoLaplacian2.AutoSize = true;
            this.RdoLaplacian2.Location = new System.Drawing.Point(12, 91);
            this.RdoLaplacian2.Name = "RdoLaplacian2";
            this.RdoLaplacian2.Size = new System.Drawing.Size(83, 16);
            this.RdoLaplacian2.TabIndex = 4;
            this.RdoLaplacian2.TabStop = true;
            this.RdoLaplacian2.Text = "Laplacian2";
            this.RdoLaplacian2.UseVisualStyleBackColor = true;
            // 
            // LblThresholding
            // 
            this.LblThresholding.AutoSize = true;
            this.LblThresholding.Location = new System.Drawing.Point(3, 20);
            this.LblThresholding.Name = "LblThresholding";
            this.LblThresholding.Size = new System.Drawing.Size(35, 12);
            this.LblThresholding.TabIndex = 0;
            this.LblThresholding.Text = "阈值:";
            // 
            // TxtThresholding
            // 
            this.TxtThresholding.Location = new System.Drawing.Point(3, 42);
            this.TxtThresholding.Name = "TxtThresholding";
            this.TxtThresholding.Size = new System.Drawing.Size(119, 21);
            this.TxtThresholding.TabIndex = 1;
            this.TxtThresholding.Text = "0";
            // 
            // RdoLaplacian3
            // 
            this.RdoLaplacian3.AutoSize = true;
            this.RdoLaplacian3.Location = new System.Drawing.Point(12, 110);
            this.RdoLaplacian3.Name = "RdoLaplacian3";
            this.RdoLaplacian3.Size = new System.Drawing.Size(83, 16);
            this.RdoLaplacian3.TabIndex = 5;
            this.RdoLaplacian3.TabStop = true;
            this.RdoLaplacian3.Text = "Laplacian3";
            this.RdoLaplacian3.UseVisualStyleBackColor = true;
            // 
            // RdoKirsch
            // 
            this.RdoKirsch.AutoSize = true;
            this.RdoKirsch.Location = new System.Drawing.Point(12, 132);
            this.RdoKirsch.Name = "RdoKirsch";
            this.RdoKirsch.Size = new System.Drawing.Size(59, 16);
            this.RdoKirsch.TabIndex = 6;
            this.RdoKirsch.TabStop = true;
            this.RdoKirsch.Text = "Kirsch";
            this.RdoKirsch.UseVisualStyleBackColor = true;
            // 
            // FrmMask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 195);
            this.Controls.Add(this.BtnConfirm);
            this.Controls.Add(this.PnlType);
            this.Controls.Add(this.PnlThresholding);
            this.Name = "FrmMask";
            this.Text = "FrmMask";
            this.PnlThresholding.ResumeLayout(false);
            this.PnlThresholding.PerformLayout();
            this.PnlType.ResumeLayout(false);
            this.PnlType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlThresholding;
        private System.Windows.Forms.Panel PnlType;
        private System.Windows.Forms.Label LblThresholding;
        private System.Windows.Forms.RadioButton RdoLaplacian2;
        private System.Windows.Forms.RadioButton RdoLaplacian1;
        private System.Windows.Forms.RadioButton RdoSobel;
        private System.Windows.Forms.RadioButton RdoPrewitt;
        private System.Windows.Forms.RadioButton RdoRoberts;
        private System.Windows.Forms.Button BtnConfirm;
        private System.Windows.Forms.TextBox TxtThresholding;
        private System.Windows.Forms.RadioButton RdoKirsch;
        private System.Windows.Forms.RadioButton RdoLaplacian3;
    }
}