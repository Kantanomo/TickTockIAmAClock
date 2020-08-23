using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TickTockIAmAClock
{
    public static class Program
    {
        public static MemoryHandler Memory;
        public static int TickRate = 60;
        public static int DeltaRickRate = 60;
        public static bool TrueSixty = false;
        public static bool advancedMode = true;
        public static Form1 Form;
        public static bool MinimalMode = false;
        public static ContextMenu trayMenu;
        public static MenuItem trayItem;
        public static MenuItem trayItem2;
        public static NotifyIcon trayIcon;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if !DEBUG
            MinimalMode = true;
#endif
            if (!MinimalMode)
            {
                Application.Run(Form = new Form1());
            }
            else
            {
                Thread trayThread = new Thread(
                    delegate()
                    {
                        trayMenu = new ContextMenu();
                        trayItem = new MenuItem("Exit");
                        trayItem2 = new MenuItem("60");
                        trayMenu.MenuItems.Add(0, trayItem);
                        trayMenu.MenuItems.Add(1, trayItem2);
                        trayIcon = new NotifyIcon()
                        {
                            Icon = Properties.Resources.dur,
                            ContextMenu = trayMenu,
                            Text = "Halo 2 Uncapped Assistant"
                        };
                        trayItem.Click += new EventHandler(trayItemClick);
                        trayItem2.Click += new EventHandler(trayItem2Click);
                        trayIcon.Visible = true;
                        trayIcon.ShowBalloonTip(1000, "Uncapped Assistant", "Is now running", ToolTipIcon.None);
                        Application.Run();
                    });
                trayThread.Start();
                Thread.Sleep(2000);
                WatcherThread.Run();
            }
        }

        static void trayItemClick(object sender, EventArgs e)
        {
            trayIcon.Dispose();
            Process.GetCurrentProcess().Kill();
        }

        static void trayItem2Click(object sender, EventArgs e)
        {
            if (trayItem2.Text == "120")
            {
                trayItem2.Text = "60";
                TickRate = 60;
            }
            else
            {
                trayItem2.Text = "120";
                TickRate = 120;
            }
        }
    }
}
