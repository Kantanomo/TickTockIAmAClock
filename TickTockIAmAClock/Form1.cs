using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Management;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TickTockIAmAClock
{
    public partial class Form1 : Form
    {
        private Size advancedSize;
        public static Control userObjectAddress_;
        public static Panel tPanel;
        public Form1()
        {

            InitializeComponent();

            Task.Factory.StartNew(WatcherThread.Run);
            advancedSize = this.Size;
            advancedButton_Click(null, null);
            Form1.userObjectAddress_ = this.userObjectAddress;
            Form1.tPanel = this.tickPanel;
        }

        private void TickRateNum_ValueChanged(object sender, EventArgs e)
        {
            Program.TickRate = (int) TickRateNum.Value;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            TickRateNum.Value = 60;
            Program.TickRate = 60;
            Program.TrueSixty = checkBox1.Checked;
            TickRateNum.ReadOnly = checkBox1.Checked;
        }

        private void advancedButton_Click(object sender, EventArgs e)
        {
            Program.advancedMode = !Program.advancedMode;
            this.Size = !Program.advancedMode ? new Size(193, 130) : advancedSize;
        }

        private NumericUpDown baseTicker = new NumericUpDown()
        {
            Location = new System.Drawing.Point(0, 0),
            Maximum = new decimal(new int[]
            {
                99999,
                0,
                0,
                0
            }),
            Minimum = new decimal(new int[]
            {
                30,
                0,
                0,
                0
            }),
            Size = new System.Drawing.Size(40, 20),
            Value = new decimal(new int[]
            {
                60,
                0,
                0,
                0
            })
        };

        private int CurX = 0;
        private int CurY = 0;
        private int Ccount = 0;
        public void addControl(string name, Action<object, EventArgs> event_)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate(){addControl(name, event_);}));
            }
            else
            {
                var newControl = baseTicker.Clone();
                newControl.Name = name;
                newControl.Location = new Point(CurX, CurY);
                CurX += 40;
                if (CurX == 400)
                {
                    CurX = 0;
                    CurY += 20;
                }
                Ccount++;
                newControl.ValueChanged += delegate(object sender, EventArgs args) { event_(sender, args); };
                tickPanel.Controls.Add(newControl);
            }
        }
    }
}
