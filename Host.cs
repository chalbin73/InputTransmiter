using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InputTransmiter.Core;
using Albin.Commons.Logging;
using WindowsInput;

namespace InputTransmiter
{
    public partial class Host : Form
    {
        NetworksUtilHost host;
        public Host()
        {
            InitializeComponent();

            this.FormClosing += this_close;

            host = new NetworksUtilHost();
            Program.log.addLoggingElement(host.log);
            host.keyPessed += host_keyPressed;
        }

        private void host_keyPressed(object sender, Core.KeyEventArgs e)
        {
            richTextBox1.AppendText(e.virtualKeyCode.ToString() + " " + e.press.ToString());
            if (e.press)
            {
                InputSimulator.SimulateKeyDown(e.virtualKeyCode);
            }
            else
            {
                InputSimulator.SimulateKeyUp(e.virtualKeyCode);
            }

            
        }

        private void this_close(object sender, FormClosingEventArgs e)
        {
            if (host.server.IsStarted)
                host.server.Stop();
            (new Main()).Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!host.start((int)numericUpDown1.Value))
            {
                MessageBox.Show("Error while starting server :/");
                return;
            }
        }

        private void Host_Load(object sender, EventArgs e)
        {

        }
    }
}
