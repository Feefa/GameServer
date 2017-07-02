using GameServer.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.WebSites.AvalonHelper
{
    public class AvalonHelperGamesMasterGameInfo : GameInfoBase
    {
        public AvalonHelperGamesMasterGameInfo(int portNumber, string pathAndQuery, string title, string description, string role) : 
            base(portNumber, pathAndQuery, title, description, role)
        {
        }
    }
}
