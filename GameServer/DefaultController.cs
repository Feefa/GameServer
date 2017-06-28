using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thingy.WebServerLite.Api;

namespace GameServer
{
    public class DefaultController : ControllerBase
    {
        [HttpMethod("GET")]
        public string Test()
        {
            return Request.ControllerName;
        }
    }
}
