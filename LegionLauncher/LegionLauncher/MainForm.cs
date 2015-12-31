using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegionLauncher
{
    public partial class MainForm : Form
    {
        string MAINARMAPATH;
        public MainForm(String arma_Path, List<string> currentInstalledMods, int maxPlayers, int numPlayers)
        {
            InitializeComponent();
            MAINARMAPATH = arma_Path;
            this.armaPath.Text = arma_Path;
            this.currentModsViewBox.DataSource = currentInstalledMods;
            this.serverNamePlaceholder.Text = "LEGION Taviana Server 1";
            this.playersPlaceholder.Text =  numPlayers + "\\" + maxPlayers + " players online";
        }

        private void mods_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void serverOneLaunchButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C \"" + MAINARMAPATH + "arma3.exe\" -nosplash -mod=AllInArmaTerrainPack;Exile;RHSAFRF;RHSUSAF;TavianaA3 -connect=158.69.118.212 -port=2318";
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
