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
    public partial class FrmMask : Form
    {
        private static FrmMask instance;
        public static FrmMask GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new FrmMask();
            }
            return instance;
        }

        private Action<MaskType, Int32> act;
        public void SetAction(Action<MaskType, Int32> act)
        {
            this.act = act;
        }


        public FrmMask()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            if (this.RdoRoberts.Checked)
                act(MaskType.Roberts, Int32.Parse(this.TxtThresholding.Text));
            else if (this.RdoPrewitt.Checked)
                act(MaskType.Prewitt, Int32.Parse(this.TxtThresholding.Text));
            else if (this.RdoSobel.Checked)
                act(MaskType.Sobel, Int32.Parse(this.TxtThresholding.Text));
            else if (this.RdoLaplacian1.Checked)
                act(MaskType.Laplacian1, Int32.Parse(this.TxtThresholding.Text));
            else if (this.RdoLaplacian2.Checked)
                act(MaskType.Laplacian2, Int32.Parse(this.TxtThresholding.Text));
            else if (this.RdoLaplacian3.Checked)
                act(MaskType.Laplacian3, Int32.Parse(this.TxtThresholding.Text));
            else if (this.RdoKirsch.Checked)
                act(MaskType.Kirsch, Int32.Parse(this.TxtThresholding.Text));
        }
    }
}
