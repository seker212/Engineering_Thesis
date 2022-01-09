using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comex.Server.AdminCli
{
    public class UnknownCommandException : Exception
    {
        public UnknownCommandException() : base("Unknown Command") { }
    }
}
