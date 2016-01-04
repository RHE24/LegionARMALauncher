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
using System.IO.Compression;

namespace LegionLauncher
{
    public partial class MainForm : Form
    {
        string MAINARMAPATH;
        Thread downloadInfoFromServer;
        Dictionary<string, string> modsDownloadLinks;
        string[] modListWithHashes;
        List<string> modListFromServer;
        List<string> modHashesFromServer;
        string MOD_HASH_DOWNLOAD_LINK = "https://drive.google.com/uc?export=download&id=0BzpVa7TGxQTBLXJ0S19ZR21nVUU";
        public MainForm(String arma_Path, int maxPlayers, int numPlayers)
        {
            InitializeComponent();
            modsDownloadLinks = new Dictionary<string, string>();
            makeModDictionary();
            ThreadPool.SetMaxThreads(2, 2);
            modListFromServer = new List<string>();
            modHashesFromServer = new List<string>();
            downloadInfoFromServer = new Thread(new ThreadStart(downloadModListFromServer));
            downloadInfoFromServer.Start();
            MAINARMAPATH = arma_Path;
            this.serverNamePlaceholder.Text = "LEGION Taviana Server 1";
            this.playersPlaceholder.Text = numPlayers + "\\" + maxPlayers + " players online";
        }

        private void makeModDictionary()
        {
            modsDownloadLinks.Add("aia", "https://googledrive.com/host/0BzpVa7TGxQTBXy11ZTBhejVIRlE/@AllInArmaTerrainPack.zip");
            modsDownloadLinks.Add("tavi", "https://googledrive.com/host/0BzpVa7TGxQTBXy11ZTBhejVIRlE/@TavianaA3.zip");
            modsDownloadLinks.Add("cup", "https://googledrive.com/host/0BzpVa7TGxQTBXy11ZTBhejVIRlE/@CUP_Weapons.zip");
            modsDownloadLinks.Add("usaf", "https://googledrive.com/host/0BzpVa7TGxQTBXy11ZTBhejVIRlE/@RHSUSAF.zip");
            modsDownloadLinks.Add("afrf", "https://googledrive.com/host/0BzpVa7TGxQTBXy11ZTBhejVIRlE/@RHSAFRF.zip");
            modsDownloadLinks.Add("exile", "https://googledrive.com/host/0BzpVa7TGxQTBXy11ZTBhejVIRlE/@Exile.zip");
            modsDownloadLinks.Add("asdg", "https://googledrive.com/host/0BzpVa7TGxQTBXy11ZTBhejVIRlE/@ASDG_JR.zip");
        }

        private void serverOneLaunchButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C \"" + MAINARMAPATH + "arma3launcher.exe\" 2 1 -noLauncher -useBE -nosplash -world=empty -mod=@AllInArmaTerrainPack;@Exile;@RHSAFRF;@RHSUSAF;@TavianaA3;@ASDG_JR;@CUP_Weapons -connect=158.69.118.212 -port=2318";
            process.StartInfo = startInfo;
            process.Start();
        }

        private void downloadModListFromServer()
        {
            if (File.Exists(MAINARMAPATH + "\\modListWithHashes.txt"))
                File.Delete(MAINARMAPATH + "\\modListWithHashes.txt");
            WebClient webClient = new WebClient();
            webClient.DownloadFile(MOD_HASH_DOWNLOAD_LINK, MAINARMAPATH + "\\modListWithHashes.txt");
            modListWithHashes = System.IO.File.ReadAllLines(MAINARMAPATH + "\\modListWithHashes.txt");
            formatDataFromServerList();
        }

        public void formatDataFromServerList()
        {
            foreach(string line in modListWithHashes)
            {
                int splitter = line.IndexOf(" ");
                Console.WriteLine(line.Substring(splitter + 1));
                modHashesFromServer.Add(line.Substring(splitter + 1));
                Console.WriteLine(line.Substring(0, splitter));
                modListFromServer.Add(line.Substring(0, splitter));
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

        private void verificationOfMod(string path, Label label, Button button, int hashIndex)
        {
            downloadInfoFromServer.Join();
            if (Directory.Exists(path))
            {
                //DON'T FORGET TO CHANGE THE INDEX
                if (verifyMod(path).Equals(modHashesFromServer[hashIndex]))
                {
                    Invoke(new Action(() => button.Text = "VERIFIED!"));
                    Invoke(new Action(() => label.BackColor = System.Drawing.Color.Green));
                }
                else
                {
                    Invoke(new Action(() => button.Text = "FAILED!"));
                    Invoke(new Action(() => label.BackColor = System.Drawing.Color.Red));
                }
            }
            else
            {
                Invoke(new Action(() => button.Text = "FAILED!"));
                Invoke(new Action(() => label.BackColor = System.Drawing.Color.Red));
            }
        }
        private void downloadMod(string modName, string modLink, Button button, string modDirectory)
        {
            if(File.Exists(MAINARMAPATH + "\\" + modName + ".zip"))
                File.Delete(MAINARMAPATH + "\\" + modName + ".zip");
            WebClient webClient = new WebClient();
            Console.WriteLine(modLink);
            webClient.DownloadFile(modLink, MAINARMAPATH + "\\"+modName+".zip");
            Invoke(new Action(() => button.Text = "UNZIPPING!"));
            if (Directory.Exists(MAINARMAPATH + "\\" + modDirectory))
                Directory.Delete((MAINARMAPATH + "\\" + modDirectory), true);
            ZipFile.ExtractToDirectory(MAINARMAPATH + "\\" + modName + ".zip", MAINARMAPATH);
            Directory.CreateDirectory(MAINARMAPATH + "\\" + modDirectory);
            File.Delete(MAINARMAPATH + "\\" + modName + ".zip");
            Invoke(new Action(() => button.Text = "INSTALLED!\nNOW VERIFY!"));
        }


        private void asdgVerifyButton_Click(object sender, EventArgs e)
        {
            asdgVerifyButton.Text = "VERIFING";
            ThreadPool.QueueUserWorkItem(s => { verificationOfMod(MAINARMAPATH + "\\@ASDG_JR", asdgModLabel, asdgVerifyButton, 1); });
        }

        private void cupVerifyButton_Click(object sender, EventArgs e)
        {
            cupVerifyButton.Text = "VERIFING";
            ThreadPool.QueueUserWorkItem(s => { verificationOfMod(MAINARMAPATH + "\\@CUP_Weapons", cupModLabel, cupVerifyButton, 5);});
        }

        private void taviVerifyButton_Click(object sender, EventArgs e)
        {
            taviVerifyButton.Text = "VERIFING";
            ThreadPool.QueueUserWorkItem(s => { verificationOfMod(MAINARMAPATH + "\\@TavianaA3", taviModLabel, taviVerifyButton, 4); });
        }

        private void afrfVerifyButton_Click(object sender, EventArgs e)
        {
            afrfVerifyButton.Text = "VERIFING";
            ThreadPool.QueueUserWorkItem(s => { verificationOfMod(MAINARMAPATH + "\\@RHSAFRF", afrfModLabel, afrfVerifyButton, 2); });
        }

        private void usafVerifyButton_Click(object sender, EventArgs e)
        {
            usafVerifyButton.Text = "VERIFING";
            ThreadPool.QueueUserWorkItem(s => { verificationOfMod(MAINARMAPATH + "\\@RHSUSAF", usafModLabel, usafVerifyButton, 3); });
        }

        private void aiaVerifyButton_Click(object sender, EventArgs e)
        {
            aiaVerifyButton.Text = "VERIFING";
            ThreadPool.QueueUserWorkItem(s => { verificationOfMod(MAINARMAPATH + "\\@AllinArmaTerrainPack", aiaModLabel, aiaVerifyButton, 0); });
        }

        private void exileVerifyButton_Click(object sender, EventArgs e)
        {
            exileVerifyButton.Text = "VERIFING";
            ThreadPool.QueueUserWorkItem(s => { verificationOfMod(MAINARMAPATH + "\\@Exile", exileModLabel, exileVerifyButton, 6); });
        }

        private void asdgDownloadButton_Click(object sender, EventArgs e)
        {
            asdgDownloadButton.Text = "DOWNLADING";
            Thread download = new Thread(s => downloadMod("asdg", modsDownloadLinks["asdg"], asdgDownloadButton, "@ASDG_JR"));
            download.Start();
        }

        private void cupDownloadButton_Click(object sender, EventArgs e)
        {
            cupDownloadButton.Text = "DOWNLOADING";
            Thread download = new Thread(s => downloadMod("cup", modsDownloadLinks["cup"], cupDownloadButton, "@CUP_Weapons"));
            download.Start();
        }

        private void taviDownloadButton_Click(object sender, EventArgs e)
        {
            taviDownloadButton.Text = "DOWNLOADING";
            Thread download = new Thread(s => downloadMod("tavi", modsDownloadLinks["tavi"], taviDownloadButton, "@TavianaA3"));
            download.Start();
        }

        private void afrfDownloadButton_Click(object sender, EventArgs e)
        {
            afrfDownloadButton.Text = "DOWNLOADING";
            Thread download = new Thread(s => downloadMod("afrf", modsDownloadLinks["afrf"], afrfDownloadButton, "@RHSAFRF"));
            download.Start();
        }

        private void usafDownloadButton_Click(object sender, EventArgs e)
        {
            usafDownloadButton.Text = "DOWNLOADING";
            Thread download = new Thread(s => downloadMod("usaf", modsDownloadLinks["usaf"], usafDownloadButton, "@RHSUSAF"));
            download.Start();
        }

        private void aiaDownloadButton_Click(object sender, EventArgs e)
        {
            aiaDownloadButton.Text = "DOWNLOADING";
            Thread download = new Thread(s => downloadMod("aia", modsDownloadLinks["aia"], aiaDownloadButton, "@AllInArmaTerrainPack"));
            download.Start();
        }

        private void exileDownloadButton_Click(object sender, EventArgs e)
        {
            exileDownloadButton.Text = "DOWNLOADING";
            Thread download = new Thread(s => downloadMod("exile", modsDownloadLinks["exile"], exileDownloadButton, "@Exile"));
            download.Start();
        }
    }
}
