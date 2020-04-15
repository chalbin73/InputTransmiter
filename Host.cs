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
using InputTransmiter.Language;
using System.Net.Sockets;

namespace InputTransmiter
{
    public partial class Host : Form
    {
        Lang lang;
        NetworksUtilHost host;
        public Host()
        {
            InitializeComponent();
            lang = new Lang();
            lang.LoadLang(Program.Language);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            statusStrip1.Items[0].ForeColor = Color.Red;
            statusStrip1.Items[0].Text = lang.strings["ncon"];

            Program.log.addLoggingElement(lang.log);

            this.FormClosing += this_close;

            host = new NetworksUtilHost();
            Program.log.addLoggingElement(host.log);
            host.keyPessed += host_keyPressed;
            checkBox1.Text = lang.strings["chkKey"];
        }

        private void host_keyPressed(object sender, Core.KeyEventArgs e)
        {
            if (checkBox1.Checked)
            {
                richTextBox1.AppendText(e.virtualKeyCode.ToString() + " " + e.press.ToString() + "\n");
                if (e.press)
                {
                    InputSimulator.SimulateKeyDown(e.virtualKeyCode);
                }
                else
                {
                    InputSimulator.SimulateKeyUp(e.virtualKeyCode);
                }
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
            host.server.Stop();
            if (!host.start((int)numericUpDown1.Value))
            {
                MessageBox.Show(lang.strings["servErr"]);
                statusStrip1.Items[0].ForeColor = Color.Red;
                statusStrip1.Items[0].Text = lang.strings["ncon"];
                host.server.ClientConnected += clientConnected;
                host.server.ClientDisconnected += clientDisconnected;
                return;
            }
            statusStrip1.Items[0].ForeColor = Color.Orange;
            statusStrip1.Items[0].Text = lang.strings["ready"];
        }

        private void clientDisconnected(object sender, TcpClient e)
        {
            host.server.Stop();
            statusStrip1.Items[0].ForeColor = Color.Red;
            statusStrip1.Items[0].Text = lang.strings["ncon"];
        }

        private void clientConnected(object sender, TcpClient e)
        {
            statusStrip1.Items[0].ForeColor = Color.Green;
            statusStrip1.Items[0].Text = lang.strings["con"];
        }

        private void Host_Load(object sender, EventArgs e)
        {
            button1.Text = lang.strings["btnPort"];
            label1.Text = lang.strings["historicLabel"];
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
