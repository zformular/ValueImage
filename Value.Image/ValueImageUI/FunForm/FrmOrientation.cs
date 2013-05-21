using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ValueImage.Infrastructure;

namespace ValueImageUI.FunForm
{
    public partial class FrmOrientation : Form
    {
        public FrmOrientation(Func<OrientationType, Int32[]> act, OrientationType orientType, Int32 width, Int32 height)
        {
            InitializeComponent();
            this.TopMost = true;
            this.PnlBack.Width = width;
            this.PnlBack.Height = height;
            if (orientType == OrientationType.Horizontal)
            {
                this.PnlBack.Width = 255;
            }
            else
            {
                this.PnlBack.Height = 255;
            }

            this.Width = this.PnlBack.Width + 40;
            this.Height = this.PnlBack.Height + 60;

            Int32[] result = act(orientType);
            Pen pen = new Pen(Color.LightBlue);
            this.PnlBack.Paint += delegate(Object sender, PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                for (int i = 0; i < result.Length; i++)
                {
                    if (orientType == OrientationType.Horizontal)
                    {
                        g.DrawLine(pen, 0, i, result[i], i);
                    }
                    else if (orientType == OrientationType.Vertical)
                    {
                        g.DrawLine(pen, i, PnlBack.Height - result[i], i, PnlBack.Height);
                    }
                }
                g.Dispose();
            };
        }
    }
}
