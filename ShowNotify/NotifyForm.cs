using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ShowNotify
{
    public partial class NotifyForm : Form
    {
        private int timeout = 3000;
        private string message = "test";

        public NotifyForm()
        {
            InitializeComponent();
        }

        public NotifyForm(int _timeout, string _message)
            : this()
        {
            this.timeout = _timeout;
            this.message = _message;
        }

        private void NotifyForm_Load(object sender, EventArgs e)
        {
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.ShowBalloonTip(timeout, "提醒", message, ToolTipIcon.Info);
            Thread.Sleep(timeout  + 1000);
            this.notifyIcon1.Visible = false;
            Environment.Exit(0);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }
    }
}
