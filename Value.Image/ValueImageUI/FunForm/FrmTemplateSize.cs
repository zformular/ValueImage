using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ValueImageUI.FuncForm
{
    public partial class FrmTemplateSize : Form
    {
        private Action<Byte[]> act;

        private static FrmTemplateSize instance;
        public static FrmTemplateSize GetInstance()
        {
            if (instance == null || instance.IsDisposed)
                instance = new FrmTemplateSize();

            return instance;
        }

        public void SetAction(Action<Byte[]> act)
        {
            this.act = act;
        }

        private FrmTemplateSize()
        {
            InitializeComponent();
            this.TopMost = true;

            this.RdoTemplate3x3.CheckedChanged += delegate(Object sender, EventArgs e)
            {
                if (((RadioButton)sender).Checked)
                {
                    this.Pnl3x3.Parent = this.PnlMain;
                    this.Pnl3x3.Visible = true;
                    this.Pnl3x3.Dock = DockStyle.Fill;
                    this.Pnl5x5.Visible = false;
                    this.Pnl7x7.Visible = false;
                }
            };

            this.RdoTemplate5x5.CheckedChanged += delegate(Object sender, EventArgs e)
            {
                if (((RadioButton)sender).Checked)
                {
                    this.Pnl5x5.Parent = this.PnlMain;
                    this.Pnl5x5.Visible = true;
                    this.Pnl5x5.Dock = DockStyle.Fill;
                    this.Pnl3x3.Visible = false;
                    this.Pnl7x7.Visible = false;
                }
            };

            this.RdoTemplate7x7.CheckedChanged += delegate(Object sender, EventArgs e)
            {
                if (((RadioButton)sender).Checked)
                {
                    this.Pnl7x7.Parent = this.PnlMain;
                    this.Pnl7x7.Visible = true;
                    this.Pnl7x7.Dock = DockStyle.Fill;
                    this.Pnl3x3.Visible = false;
                    this.Pnl5x5.Visible = false;
                }
            };

            SetButtonEvent(this.Pnl3x3);
            SetButtonEvent(this.Pnl5x5);
            SetButtonEvent(this.Pnl7x7);
        }

        private void SetButtonEvent(Panel pnl)
        {
            var btns = pnl.Controls;
            for (int i = 0; i < btns.Count; i++)
            {
                Button btn = (Button)btns[i];
                btn.Click += delegate(Object sender, EventArgs e)
                {
                    Button bt = (Button)sender;
                    if (bt.BackColor == SystemColors.Control)
                    {
                        bt.BackColor = Color.LightBlue;
                    }
                    else
                    {
                        bt.BackColor = SystemColors.Control;

                    }
                };
            }
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            Byte[] template = null;
            if (this.RdoTemplate3x3.Checked)
            {
                template = new Byte[9];
                var btns = this.Pnl3x3.Controls;
                for (int i = btns.Count - 1; i >= 0; i--)
                {
                    if (((Button)btns[i]).BackColor == Color.LightBlue)
                        template[8 - i] = 1;
                    else
                        template[8 - i] = 0;
                }
                act(template);
            }
            else if (this.RdoTemplate5x5.Checked)
            {
                template = new Byte[25];
                var btns = this.Pnl5x5.Controls;
                for (int i = btns.Count - 1; i >= 0; i--)
                {
                    if (((Button)btns[i]).BackColor == Color.LightBlue)
                        template[24 - i] = 1;
                    else
                        template[24 - i] = 0;
                }
                act(template);
            }
            else if (this.RdoTemplate7x7.Checked)
            {
                template = new Byte[49];
                var btns = this.Pnl7x7.Controls;
                for (int i = btns.Count - 1; i >= 0; i--)
                {
                    if (((Button)btns[i]).BackColor == Color.LightBlue)
                        template[48 - i] = 1;
                    else
                        template[48 - i] = 0;
                }
                act(template);
            }
        }
    }
}
