using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.WebSites.AvalonHelper
{
    public class AvalonHelperException : Exception
    {
        public AvalonHelperException(string msg) : base(msg) { }
    }
}
