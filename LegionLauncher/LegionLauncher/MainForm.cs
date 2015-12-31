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
        public MainForm(String arma_Path, List<string> currentInstalledMods)
        {
            InitializeComponent();
            this.armaPath.Text = arma_Path;
            this.currentModsViewBox.DataSource = currentInstalledMods;
        }

        private void mods_Click(object sender, EventArgs e)
        {

        }
        
    }
}
