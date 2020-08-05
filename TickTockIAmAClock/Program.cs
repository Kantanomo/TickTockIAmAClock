using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TickTockIAmAClock
{
    public static class Program
    {
        public static MemoryHandler Memory;
        public static int TickRate = 60;
        public static bool TrueSixty = false;
        public static bool advancedMode = true;
        public static Form1 Form;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Application.Run(Form = new Form1());
        }
    }
}
