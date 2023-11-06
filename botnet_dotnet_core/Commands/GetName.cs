using BotNet.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Godfather.Commands
{
    class GetName : Command
    {
        public GetName(string name) : base(name) { }

        public override string execute(string[] args)
        {
            if (args.Length != 0) return "Invalid arguments for GetName, expected 0";

            return Environment.UserName;
        }
    }
}
