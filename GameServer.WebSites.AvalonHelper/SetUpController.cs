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
            return new AvalonHelperModel(game, Request);
        }

        [HttpMethod("GET")]
        [AuthorizedRole("Player")]
        public AvalonHelperModel Player()
        {
            return new AvalonHelperModel(game, Request);
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

        [HttpMethod("POST")]
        [AuthorizedRole("GM_Admin")]
        public AvalonHelperModel StartGame()
        {
            game.StartGame();

            Request.ViewTemplateName = "GamesMaster";
            return GamesMaster();
        }
    }
}
