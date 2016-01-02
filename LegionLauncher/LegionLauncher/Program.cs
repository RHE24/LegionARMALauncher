using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
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
            ArmA3ServerInfo taviServer1 = getServerInfo("158.69.118.212", 2319);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(armaPath, taviServer1.ServerInfo.MaxPlayers
                , taviServer1.ServerInfo.NumPlayers));
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
        }
        
        static ArmA3ServerInfo getServerInfo(string ipAddr, int port)
        {
            ArmA3ServerInfo taviServer1 = new ArmA3ServerInfo(ipAddr, port);
            taviServer1.Update();
            return taviServer1;
        }
    }

    public class ArmA3ServerInfo
    {
        public ServerInfo ServerInfo { get; protected set; }
        public PlayerCollection Players { get; protected set; }
        public string host;
        public int port;
        public long ping;
        public Stopwatch Stopwatch = new Stopwatch();

        public ArmA3ServerInfo(string host, int port)
        {
            this.host = host;
            this.port = port;
        }
        public void Update()
        {
            try
            {
                using (UdpClient client = new UdpClient(56800))
                {
                    Stopwatch.Reset();
                    byte[] request, response;

                    IPEndPoint remoteIpEndpoint = new IPEndPoint(IPAddress.Parse(host), port);
                    client.Client.ReceiveTimeout = 10000;
                    client.Connect(remoteIpEndpoint);
                    //Server Info
                    request = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x54, 0x53, 0x6F, 0x75, 0x72, 0x63, 0x65, 0x20, 0x45, 0x6E, 0x67, 0x69, 0x6E, 0x65, 0x20, 0x51, 0x75, 0x65, 0x72, 0x79, 0x00 };
                    client.Send(request, request.Length);
                    Stopwatch.Start();
                    response = client.Receive(ref remoteIpEndpoint);
                    Stopwatch.Stop();
                    this.ping = Stopwatch.ElapsedMilliseconds;
                    //string dataServer = Encoding.ASCII.GetString(response).Remove(0, 5);
                    this.ServerInfo = ServerInfo.Parse(response);

                    //request = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0x69 };
                    //client.Send(request, request.Length);
                    //response = client.Receive(ref remoteIpEndpoint);

                    //Console.WriteLine(response);
                    //this.ping =
                    //Player Info
                    //request = new byte[] { 0xfe, 0xfd, 0x00, 0x43, 0x4f, 0x52, 0x59, 0x00, 0xff, 0xff };
                    //client.Send(request, request.Length);
                    //response = client.Receive(ref remoteIpEndpoint);
                    //string dataPlayer = Encoding.ASCII.GetString(response).Remove(0, 5);
                    //this.Players = PlayerCollection.Parse(dataPlayer.Remove(0, 32));
                }
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode != SocketError.TimedOut)
                {
                    Console.WriteLine(String.Format(("Socketerror while retrievining server information."), ex));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format(("Unknown error while retrievining server information."), ex));
            }
            Console.WriteLine("updated");
        }
    }

    public class ServerInfo
    {
        public string GameVersion { get; set; }
        public string HostName { get; set; }
        public string MapName { get; set; }
        public string GameType { get; set; }
        public int NumPlayers { get; set; }
        public int NumTeam { get; set; }
        public int MaxPlayers { get; set; }
        public string GameMode { get; set; }
        public string TimeLimit { get; set; }
        public bool Password { get; set; }
        public string CurrentVersion { get; set; }
        public string RequiredVersion { get; set; }
        public string Mod { get; set; }
        public bool BattleEye { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public List<string> Players { get; set; }
        public string Mission { get; set; }

        public static ServerInfo Parse(string data)
        {
            ServerInfo info = new ServerInfo();
            string[] parts = data.Split('\0');
            Dictionary<string, string> values = new Dictionary<string, string>();
            for (int i = 0; i < parts.Length; i++)
            {
                if ((i & 1) == 0 && !values.ContainsKey(parts[i]) && (i + 1) < parts.Length)
                    values.Add(parts[i], parts[i + 1]);
            }

            info.GameVersion = GetValueByKey("gamever", values);
            info.HostName = GetValueByKey("hostname", values);
            info.MapName = GetValueByKey("mapname", values);
            info.GameType = GetValueByKey("gametype", values);
            info.NumPlayers = ParseInt(GetValueByKey("numplayers", values));
            info.NumTeam = ParseInt(GetValueByKey("numteams", values));
            info.MaxPlayers = ParseInt(GetValueByKey("maxplayers", values));
            info.GameMode = GetValueByKey("gamemode", values);
            info.TimeLimit = GetValueByKey("timelimit", values);
            info.Password = ParseBoolean(GetValueByKey("password", values));
            info.CurrentVersion = GetValueByKey("currentVersion", values);
            info.RequiredVersion = GetValueByKey("requiredVersion", values);
            info.Mod = GetValueByKey("mod", values);
            info.BattleEye = ParseBoolean(GetValueByKey("sv_battleye", values));
            info.Longitude = ParseDouble(GetValueByKey("lng", values));
            info.Latitude = ParseDouble(GetValueByKey("lat", values));
            info.Mission = GetValueByKey("mission", values);

            return info;
        }


        internal static ServerInfo Parse(byte[] response)
        {
            ServerInfo info = new ServerInfo();
            int pos = 5;

            info.HostName = GetNextPartString(ref pos, response);
            info.MapName = GetNextPartString(ref pos, response);
            info.GameType = GetNextPartString(ref pos, response);
            info.GameMode = GetNextPartString(ref pos, response);
            GetNextPart(ref pos, response);//Empty
            GetNextPart(ref pos, response);//Empty
            byte[] pInfo = GetNextPart(ref pos, response);

            if (pInfo.Length > 0)
            {
                info.NumPlayers = pInfo[0];
                info.MaxPlayers = pInfo[1];
            }
            else
            {
                info.NumPlayers = 0;
                info.MaxPlayers = GetNextPart(ref pos, response)[0];
            }

            GetNextPart(ref pos, response);//Enviroment. dw??
            GetNextPart(ref pos, response);//Visibility & vac?
            info.GameVersion = GetNextPartString(ref pos, response);
            //Extra data flag
            //for (int i = 0; i < 25; i++)
            //{
            //    Console.WriteLine(GetNextPart(ref pos, response));
            //}
            return info;
        }

        private static byte[] GetNextPart(ref int pos, byte[] response)
        {
            byte[] part = new byte[0];

            for (int i = 0; i <= response.Length; i++)
            {
                if (response[i + pos] == 0x0)
                {
                    pos = i + pos + 1;
                    return part; ;
                }
                Array.Resize(ref part, part.Length + 1);
                part[i] = response[i + pos];
            }

            return new byte[0];
        }

        private static string GetNextPartString(ref int pos, byte[] response)
        {
            byte[] val = GetNextPart(ref pos, response);
            return Encoding.ASCII.GetString(val);
        }

        private static int ParseInt(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return 0;
            int parsedValue = 0;
            Int32.TryParse(value, out parsedValue);
            return parsedValue;
        }
        private static double ParseDouble(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return 0;
            double parsedValue = 0;
            Double.TryParse(value, out parsedValue);
            return parsedValue;
        }
        private static bool ParseBoolean(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return false;
            if (value == "1" || value.ToLowerInvariant() == "true")
                return true;
            return false;
        }
        private static string GetValueByKey(string key, Dictionary<string, string> values)
        {
            if (values.ContainsKey(key))
                return values[key];
            return null;
        }

    }

    public class Player
    {
        public string Name { get; set; }
        public Player()
            : this(null)
        { }
        public Player(string name)
        {
            this.Name = name;
        }
    }

    public class PlayerCollection : List<Player>
    {
        public PlayerCollection()
            : base()
        { }
        public PlayerCollection(IEnumerable<Player> collection)
            : base(collection)
        { }
        public static PlayerCollection Parse(string data)
        {
            string final = "";
            var players = data.Split('\0');
            int lel = 0;
            foreach (var p in players)
            {
                if (lel == 0)
                {
                    if (p != "")
                    {
                        final += p + "\0";
                    }
                }
                lel++;
                if (lel == 4)
                    lel = 0;
            }
            players = final.Split('\0');

            PlayerCollection collection = new PlayerCollection(
                from s in players
                select new Player(s)
            );
            return collection;
        }
    }
}
