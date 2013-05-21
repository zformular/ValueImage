using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ValueImage.Infrastructure;

namespace ValueImageUI.FuncForm
{
    public partial class FrmGaussOperator : Form
    {
        private Action<GaussFilterType, Double, Double> act;

        private static FrmGaussOperator instance;
        public static FrmGaussOperator GetInstance()
        {
            if (instance == null || instance.IsDisposed)
                instance = new FrmGaussOperator();
            return instance;
        }

        public void SetAction(Action<GaussFilterType, Double, Double> act)
        {
            this.act = act;
        }

        private FrmGaussOperator()
        {
            InitializeComponent();

            this.RdoLoG.Checked = true;
            this.TopMost = true;
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            Double sigma = Convert.ToDouble(this.TxtSigma.Text);
            Double thresholding = Convert.ToDouble(this.TxtThresholding.Text);

            if (this.RdoLoG.Checked)
                act(GaussFilterType.LoG, sigma, thresholding);
            else if (this.RdoDoG.Checked)
                act(GaussFilterType.DoG, sigma, thresholding);
        }
    }
}
