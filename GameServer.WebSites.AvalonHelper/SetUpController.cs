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
        public AvalonHelperModel GamesMaster()
        {
            return CreateAvalonHelperModel();
        }

        [HttpMethod("GET")]
        [AuthorizedRole("Player")]
        public AvalonHelperModel Player()
        {
            return CreateAvalonHelperModel();
        }

        [HttpMethod("POST")]
        [AuthorizedRole("Player")]
        public AvalonHelperModel SelectRole(int roleId)
        {
            if (game.GetUserStatus(Request.User).RoleId == 0)
            {
                game.SelectRole(Request.User, roleId);
            }

            Request.ViewTemplateName = "Player";
            return Player();
        }

        private AvalonHelperModel CreateAvalonHelperModel()
        {
            return new AvalonHelperModel
            {
                GameStatus = game.GetGameStatus(),
                UserStatus = game.GetUserStatus(Request.User),
                Players = game.GetPlayerList()
            };
        }
    }
}
