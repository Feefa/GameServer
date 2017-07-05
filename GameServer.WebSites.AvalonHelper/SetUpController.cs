using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thingy.WebServerLite.Api;

namespace GameServer.WebSites.AvalonHelper
{
    public class SetUpController : ControllerBase
    {
        private readonly IAvalonGame game;

        public SetUpController(IAvalonGame game)
        {
            this.game = game;
        }

        [HttpMethod("GET")]
        [AuthorizedRole("GM_Admin")]
        public string GamesMaster()
        {
            return string.Empty;
        }

        [HttpMethod("GET")]
        [AuthorizedRole("Player")]
        public AvalonHelperModel Player()
        {
            return new AvalonHelperModel
            {
                UserStatus = game.GetUserStatus(Request.User),
                Players = game.GetPlayerList()
            };
        }

        [HttpMethod("POST")]
        [AuthorizedRole("Player")]
        public AvalonHelperModel SelectRole(int roleId)
        {
            game.SelectRole(Request.User, roleId);
            Request.ViewTemplateName = "Player";

            return Player();
        }
    }
}
