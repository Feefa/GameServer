using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.WebSites.AvalonHelper
{
    public class AvalonHelperModel
    {
        public UserStatusModel UserStatus { get; set; }
        public PlayerStatusModel[] Players { get; set; }
    }
}
