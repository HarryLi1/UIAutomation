using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ShowNotify
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            int timeout, default_Timeout = 1;
            string message, default_Message = "test";

            if (args != null && args.Length > 0)
            {
                timeout = args.Length > 0 ? (int.TryParse(args[0], out timeout) ? timeout : default_Timeout) : default_Timeout;
                message = args.Length > 1 ? args[1] : default_Message;
            }
            else
            {
                timeout = default_Timeout;
                message = default_Message;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NotifyForm(timeout, message));
        }
    }
}
