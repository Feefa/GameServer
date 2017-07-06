using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.WebSites.AvalonHelper
{
    public class UserStatusModel
    {
        public int RoleId { get; set; }

        public string[] Secrets { get; set; }
    }
}
