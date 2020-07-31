using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TickTockIAmAClock
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
            Task.Factory.StartNew(WatcherThread.Run);
            //foreach (var process in Process.GetProcessesByName("halo2"))
            //{
            //    KillProcessAndChildrens(process.Id);
            //}
        }
        private static void KillProcessAndChildrens(int pid)
        {
            ManagementObjectSearcher processSearcher = new ManagementObjectSearcher
                ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection processCollection = processSearcher.Get();

            try
            {
                Process proc = Process.GetProcessById(pid);
                if (!proc.HasExited) proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }

            if (processCollection != null)
            {
                foreach (ManagementObject mo in processCollection)
                {
                    KillProcessAndChildrens(Convert.ToInt32(mo["ProcessID"])); //kill child processes(also kills childrens of childrens etc.)
                }
            }
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
    }
}
