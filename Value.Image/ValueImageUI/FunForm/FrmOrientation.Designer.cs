namespace ValueImageUI.FunForm
{
    partial class FrmOrientation
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
            this.PnlBack = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // PnlBack
            // 
            this.PnlBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlBack.Location = new System.Drawing.Point(12, 12);
            this.PnlBack.Name = "PnlBack";
            this.PnlBack.Size = new System.Drawing.Size(255, 255);
            this.PnlBack.TabIndex = 0;
            // 
            // FrmOrientation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 279);
            this.Controls.Add(this.PnlBack);
            this.Name = "FrmOrientation";
            this.Text = "FrmOrientation";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlBack;
    }
}