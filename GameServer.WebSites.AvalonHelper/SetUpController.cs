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
        [HttpMethod("GET")]
        [AuthorizedRole("GM_Admin")]
        public string GamesMaster()
        {
            return string.Empty;
        }
    }
}
