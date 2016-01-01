using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LegionLauncher
{
    public partial class MainForm : Form
    {
        delegate void SetTextCallback(string text);
        private delegate void AddItemCallback(Boolean verified, int index);
        string MAINARMAPATH;
        Thread verification;
        string[] serverModHashes;
        List<string> modList;
        List<string> modHashes;
        Thread downloadHashes;
        Thread modHashChecker;
        string MOD_HASH_DOWNLOAD_LINK = "https://drive.google.com/uc?export=download&id=0BzpVa7TGxQTBLXJ0S19ZR21nVUU";
        public MainForm(String arma_Path, List<string> currentInstalledMods, int maxPlayers, int numPlayers)
        {
            InitializeComponent();
            MAINARMAPATH = arma_Path;
            this.armaPath.Text = arma_Path;
            currentModsListBox.View = View.Details;
            currentModsListBox.Columns.Add("Mod");
            modList = new List<string>();
            modHashes = currentInstalledMods;
            foreach (string mod in currentInstalledMods)
            {
                modList.Add(mod);
                currentModsListBox.Items.Add(mod);
            }
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

        private void verifyModsButton_Click(object sender, EventArgs e)
        {
            this.verifyModsButton.Text = "VERIFYING MODS\nPLEASE WAIT!";
            verification = new Thread(new ThreadStart(verifyWrapper));
            verification.Start();
            downloadHashes = new Thread(new ThreadStart(downloadModHashes));
            downloadHashes.Start();
            modHashChecker = new Thread(new ThreadStart(modHashCheck));
            modHashChecker.Start();
        }

        private void modHashCheck()
        {
            downloadHashes.Join();
            for(int x = 0; x < serverModHashes.Length; x++)
            {
                Console.WriteLine(modList[x]);
                Console.WriteLine(serverModHashes[x]);
                if (serverModHashes[x].Contains(modList[x])) {
                    if (serverModHashes[x].Contains(modHashes[x]))
                    {
                        Console.WriteLine("hash check match");
                        AddItem(true, x);
                    }
                    else
                    {
                        Console.WriteLine("hash check non-match");
                        AddItem(false, x);
                    }
                }
            }
        }

        private void downloadModHashes()
        {
            WebClient webClient = new WebClient();
            webClient.DownloadFile(MOD_HASH_DOWNLOAD_LINK, MAINARMAPATH+ "\\modHashes.txt");
            string[] lines = System.IO.File.ReadAllLines(MAINARMAPATH + "\\modHashes.txt");
            //Console.WriteLine("Contents of WriteLines2.txt = ");
            foreach (string line in lines)
            {
                //Console.WriteLine("\t" + line);
            }
            serverModHashes = lines;
            verification.Join();
            SetText("VERIFICATION\nCOMPLETE");
        }

        private void verifyWrapper()
        {
            for(int x = 0; x< modList.Count; x++)
            {
                //Console.WriteLine(MAINARMAPATH + "@" + modList[x]);
                modHashes[x] = verifyMod(MAINARMAPATH + "@" + modList[x]);
                //Console.WriteLine(modList[x]);
            }
        }


        private string verifyMod(string path)
        {
            // assuming you want to include nested folders
            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                                 .OrderBy(p => p).ToList();

            MD5 md5 = MD5.Create();

            for (int i = 0; i < files.Count; i++)
            {
                string file = files[i];

                // hash path
                string relativePath = file.Substring(path.Length + 1);
                byte[] pathBytes = Encoding.UTF8.GetBytes(relativePath.ToLower());
                md5.TransformBlock(pathBytes, 0, pathBytes.Length, pathBytes, 0);

                // hash contents
                byte[] contentBytes = File.ReadAllBytes(file);
                if (i == files.Count - 1)
                    md5.TransformFinalBlock(contentBytes, 0, contentBytes.Length);
                else
                    md5.TransformBlock(contentBytes, 0, contentBytes.Length, contentBytes, 0);
            }

            return BitConverter.ToString(md5.Hash).Replace("-", "").ToLower();
        }

        private void currentModsViewBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.verifyModsButton.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.verifyModsButton.Text = text;
            }
        }

        private void AddItem(Boolean verified, int index)
        {
            if (this.currentModsListBox.InvokeRequired)
            {
                AddItemCallback d = new AddItemCallback(AddItem);
                this.Invoke(d, new object[] { verified, index });
            }
            else
            {
                foreach (ListViewItem li in currentModsListBox.Items)
                {
                    if (li.Index == index && !verified)
                        li.ForeColor = Color.Red;
                    if (li.Index == index && verified)
                        li.ForeColor = Color.Green;
                }
            }

            
        }
    }
}
