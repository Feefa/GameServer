using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thingy.WebServerLite.Api;

namespace GameServer.Test
{
    public class TestPlayer : IUser
    {
        public TestPlayer(string name)
        {
            UserId = name;
            Roles = new string[] { "Player" };

        }

        public string[] Roles { get; private set; }

        public string UserId { get; private set; }
    }
}
