using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Thingy.WebServerLite.Api;

namespace GameServer
{
    public partial class MainForm : Form, IMainForm
    {
        private readonly IWebServer webServer;

        public MainForm(IWebServer webServer)
        {
            this.webServer = webServer;
            InitializeComponent();
            Application.Idle += Application_Idle;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            if (StartStopPanel.BackColor != Color.Red && !webServer.IsStarted)
            {
                StartStopPanel.BackColor = Color.Red;
                StartStopButton.Text = "Start";
            }

            if (StartStopPanel.BackColor != Color.Green && webServer.IsStarted)
            {
                StartStopPanel.BackColor = Color.Green;
                StartStopButton.Text = "Stop";
            }
        }

        public Form GetForm()
        {
            return this;
        }

        private void StartStopButton_Click(object sender, EventArgs e)
        {
            if (webServer.IsStarted)
            {
                webServer.Stop();
            }
            else
            {
                webServer.Start();
            }
        }
    }
}
