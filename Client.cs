using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using WindowsInput;
using Albin.Commons.Logging;
using InputTransmiter.Core;
using InputTransmiter.Language;

namespace InputTransmiter
{
    public partial class Client : Form
    {
        NetworksUtilsClient client;
        Lang lang;
        bool terminated = false;
        Dictionary<VirtualKeyCode, bool> pressed;
        bool connect = false;
        public Client()
        {
            InitializeComponent();
            
            this.FormClosing += this_close;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            statusStrip1.Items[0].ForeColor = Color.Red;
            statusStrip1.Items[0].Text = lang.strings["ncon"];

            lang = new Lang();
            client = new NetworksUtilsClient();
            Program.log.addLoggingElement(client.log);
            Program.log.addLoggingElement(lang.log);

            lang.LoadLang(Program.Language);
            button1.Text = lang.strings["butCon"];
            button2.Text = lang.strings["butFoc"];
            button3.Text = lang.strings["butStop"];

            button2.Enabled = button3.Enabled = false;

        }

        private void this_close(object sender, FormClosingEventArgs e)
        {
            if(client.client.TcpClient != null) 
                if (client.client.TcpClient.Connected)
                    client.client.Disconnect();
            (new Main()).Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!client.connect(textBox1.Text, (int)numericUpDown1.Value)){
                MessageBox.Show(lang.strings["conMsg"]);
                return;
            }
            statusStrip1.Items[0].ForeColor = Color.Green;
            statusStrip1.Items[0].Text = lang.strings["con"];

            connect = true;
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pressed = new Dictionary<VirtualKeyCode, bool>();
            foreach(VirtualKeyCode key in Enum.GetValues(typeof(VirtualKeyCode)))
            {
                pressed[key] = false;
            }
            this.Focus();
            Program.log.log("Pressed");
            terminated = false;
            this.KeyDown += this_keyDown;
            this.KeyUp += this_keyUp;
            this.ResumeLayout(false);
            this.KeyPreview = true;

            button3.Enabled = true;
            button2.Enabled = false;
            groupBox1.Enabled = false;
        }

        private void this_keyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            VirtualKeyCode key = (VirtualKeyCode)e.KeyValue;
            Program.log.log("Key press down : " + key.ToString());
            e.Handled = true;
            pressed[key] = false;
            client.sendKey(key, true);
        }

        private void this_keyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            VirtualKeyCode key = (VirtualKeyCode)e.KeyValue;
            if (!pressed[key])
            {
                Program.log.log("Pressed : " + key.ToString());
                client.sendKey(key, false);
                pressed[key] = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (connect)
                button2.Enabled = true;
            else
                button2.Enabled = false;
            button3.Enabled = false;
            groupBox1.Enabled = true;
            this.KeyDown -= this_keyDown;
            this.KeyUp -= this_keyUp;
        }

        private void Client_Load(object sender, EventArgs e)
        {

        }
    }
}
