using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thingy.WebServerLite.Api;

namespace GameServer.WebSites.Default
{
    public class StartUpModel
    {
        public string Message { get; set; }

        public IUser User { get; set; }

        public bool UserHasRole(string role)
        {
            return User.Roles.Contains(role);
        }
    }
}
