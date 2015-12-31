using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LegionLauncher
{
    static class Program
    {
        static string armaPath;
        /// <summary>
        /// The main entry point for the launcher.
        /// </summary>
        [STAThread]
        static void Main()
        {
            armaPath = getSteamPath();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(armaPath, getMods(armaPath)));
        }

        /// <summary>
        /// Method to get the steam path
        /// </summary>
        static string getSteamPath()
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey(@"Software\Valve\Steam");
            string fixed_path;
            if (regKey != null)
            {
                string raw_path = @regKey.GetValue("SourceModInstallPath").ToString();
                fixed_path = raw_path.Replace("sourcemods", "");
            }
            else {
                return "Steam directory not found!";
            }
            string arma_path = string.Concat(fixed_path, "common\\Arma 3\\");
            if (!Directory.Exists(arma_path))
            {
                return "Could not find ARMA directory";
            }
            return arma_path;
            //Process the ARMA directory
            
        }
        static List<string> getMods(string arma_path)
        {
            string[] raw_mods = Directory.GetDirectories(arma_path);
            List<string> mods = new List<string>();
            Console.WriteLine("tewstin");
            foreach (string directoryName in raw_mods)
            {
                Console.WriteLine(directoryName);
                if (directoryName.Contains("\\@"))
                {
                    Console.WriteLine(directoryName);
                    mods.Add(directoryName.Replace(arma_path+"@", ""));
                }
            }
            return mods;
        }
    }
}
