using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InputTransmiter.Language;
using InputTransmiter.Core;
using WindowsInput;
using IniParser;
using IniParser.Model;

namespace InputTransmiter
{
    public partial class Main : Form
    {

        public Lang Lang = new Lang();
        
        public Main()
        {
            InitializeComponent();

            this.FormClosing += this_close;

            Lang = new Lang();
            Program.log.addLoggingElement(Lang.log);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            FileIniDataParser settingsParse = new FileIniDataParser();
            IniData settings = settingsParse.ReadFile("settings.ini");
            comboBox1.SelectedItem = settings["settings"]["lang"];
            Program.Language = settings["settings"]["lang"];
            Lang.LoadLang(comboBox1.Text);
            loadNames();
        }

        private void this_close(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            this.Close();
            Application.ExitThread();
        }

        private void Main_Load(object sender, EventArgs e)
        {
           
        }

        private void loadNames()
        {
            button1.Text = Lang.strings["butHost"];
            button2.Text = Lang.strings["butClient"];
            button3.Text = Lang.strings["butHelp"];
            label1.Text = Lang.strings["comboLang"] + " :";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            FileIniDataParser iniFile = new FileIniDataParser();
            IniData iniData = iniFile.ReadFile("settings.ini");

            iniData["settings"]["lang"] = comboBox1.SelectedItem.ToString();
            iniFile.WriteFile("settings.ini", iniData);
            Lang.LoadLang(comboBox1.Text);
            loadNames();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Host host = new Host();
            host.ShowDialog();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Client client = new Client();
            client.ShowDialog();
            
        }
    }
}
