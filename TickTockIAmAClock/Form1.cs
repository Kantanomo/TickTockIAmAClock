using System;
using System.CodeDom;
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
            if (!Program.MinimalMode)
            {
                advancedSize = this.Size;
                advancedButton_Click(null, null);
                Form1.userObjectAddress_ = this.userObjectAddress;
            }
            else
            {
                this.Load += hideForm;
            }
        }

        private void hideForm(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(0, 0);
            this.Location = new Point(-9999, -9999);
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
            this.Size = !Program.advancedMode ? new Size(193, 150) : advancedSize;
        }

        private NumericUpDown baseTicker = new NumericUpDown()
        {
            Location = new System.Drawing.Point(0, 0),
            Maximum = new decimal(new int[]{99999,0,0,0}),
            Minimum = new decimal(new int[]{0,0,0,0}),
            Size = new System.Drawing.Size(40, 20),
            Value = new decimal(new int[]{60,0,0,0})
        };

        private CheckBox baseCheck = new CheckBox()
        {
            Location = new Point(0, 0),
            UseVisualStyleBackColor = true,
            AutoSize = true
        };

        private int CurX = 0;
        private int CurY = 0;
        private int CurXa = 0;
        private int CurYa = 0;
        private int CurXb = 0;
        private int CurYb = 0;
        private int CurXc = 0;
        private int CurYc = 0;
        private int Ccount = 0;
        public void addControl(string name, string page, object value, string description, Action<object, EventArgs> event_)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate(){addControl(name, page, value, description, event_);}));
            }
            else
            {
                if (page == "get_tick_rate")
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
                    newControl.Value = (int) value;
                    newControl.Tag = description;
                    newControl.ValueChanged += (sender, args) => event_(sender, args);
                    newControl.MouseEnter += NewControlOnMouseHover;
                    getTickRatePage.Controls.Add(newControl);
                }
                else if (page == "adjust_tick_int")
                {
                    var newControl = baseTicker.Clone();
                    newControl.Name = name;
                    newControl.Location = new Point(CurXa, CurYa);
                    CurXa += 40;
                    if (CurXa == 400)
                    {
                        CurXa = 0;
                        CurYa += 20;
                    }

                    Ccount++;
                    newControl.Value = (int)value;
                    newControl.Tag = description;
                    newControl.ValueChanged += (sender, args) => event_(sender, args);
                    newControl.MouseEnter += NewControlOnMouseHover;
                    adjustTickIntPage.Controls.Add(newControl);
                }
                else if (page == "get_tick_delta")
                {
                    var newControl = baseCheck.Clone();
                    newControl.Name = name;
                    newControl.Location = new Point(CurXb, CurYb);
                    CurXb += 15;
                    if (CurXb > 400)
                    {
                        CurXb = 0;
                        CurYb += 14;
                    }

                    Ccount++;
                    newControl.Checked = (bool) value;
                    newControl.Tag = description;
                    newControl.CheckedChanged += (sender, args) => event_(sender, args);
                    newControl.MouseEnter += NewControlOnMouseHover;
                    getDeltaPage.Controls.Add(newControl);
                }
                else if (page == "get_delta_multi")
                {
                    var newControl = baseCheck.Clone();
                    newControl.Name = name;
                    newControl.Location = new Point(CurXc, CurYc);
                    CurXc += 15;
                    if (CurXc > 400)
                    {
                        CurXc = 0;
                        CurYc += 14;
                    }

                    Ccount++;
                    newControl.Checked = (bool) value;
                    newControl.Tag = description;
                    newControl.CheckedChanged += (sender, args) => event_(sender, args);
                    newControl.MouseEnter += NewControlOnMouseHover;
                    getDeltaMultiPage.Controls.Add(newControl);
                }
            }
        }

        private void NewControlOnMouseHover(object sender, EventArgs e)
        {
            descriptorLabel.Text = (string)((Control) sender).Tag;
        }

        public void removeControls()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(removeControls));
            }
            else
            {
                getTickRatePage.Controls.Clear();
                adjustTickIntPage.Controls.Clear();
                getDeltaPage.Controls.Clear();
            }
        }

        public void updateControl<T>(string name, object value)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate () { updateControl<T>(name, value); }));
            }
            else
            {
                if (typeof(T) == typeof(CheckBox))
                {
                    var c = ((CheckBox) getControl(name));
                    if (c != null)
                        c.Checked = (bool) value;
                }
                else if (typeof(T) == typeof(NumericUpDown))
                {
                    var n = ((NumericUpDown) getControl(name));
                    if (n != null)
                        n.Value = (decimal) value;
                }
            }
        }

        public Control getControl(string name) =>
            Controls.ContainsKey(name) ? Controls[name] :
            getTickRatePage.Controls.ContainsKey(name) ? getTickRatePage.Controls[name] :
            adjustTickIntPage.Controls.ContainsKey(name) ? adjustTickIntPage.Controls[name] : 
            getDeltaPage.Controls.ContainsKey(name) ? getDeltaPage.Controls[name] : null;

        private void deltaTick_ValueChanged(object sender, EventArgs e)
        {
            Program.DeltaRickRate = (int)deltaTick.Value;
        }

        private void getDeltaDeactivate_Click(object sender, EventArgs e)
        {
            foreach (Control control in getDeltaPage.Controls)
            {
                if (control.GetType() == typeof(CheckBox))
                    ((CheckBox) control).Checked = false;
            }
        }

        private void getRateReset_Click(object sender, EventArgs e)
        {
            foreach (Control control in getTickRatePage.Controls)
            {
                if(control.GetType() == typeof(NumericUpDown))
                    if (control.Name != "getRateMaster")
                        ((NumericUpDown) control).Value = 60;
            }
        }

        private void getRateMaster_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control control in getTickRatePage.Controls)
            {
                if (control.GetType() == typeof(NumericUpDown))
                    if (control.Name != "getRateMaster")
                        ((NumericUpDown)control).Value = getRateMaster.Value;
            }
        }

        private void adjustIntReset_Click(object sender, EventArgs e)
        {
            foreach (Control control in adjustTickIntPage.Controls)
            {
                if (control.GetType() == typeof(NumericUpDown))
                    if (control.Name != "adjustIntMaster")
                        ((NumericUpDown)control).Value = 60;
            }
        }

        private void adjustIntMaster_ValueChanged(object sender, EventArgs e)
        {
            foreach (Control control in adjustTickIntPage.Controls)
            {
                if (control.GetType() == typeof(NumericUpDown))
                    if (control.Name != "adjustIntMaster")
                        ((NumericUpDown)control).Value = adjustIntMaster.Value;
            }
        }
    }
}
