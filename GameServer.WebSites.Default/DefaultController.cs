using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thingy.WebServerLite.Api;

namespace GameServer.WebSites.Default
{
    public class DefaultController : ControllerBase
    {
        [HttpMethod("GET")]
        public StartUpModel Index()
        {
            if (!Request.User.Roles.Any())
            {
                Request.ViewTemplateName = "Login";
            }

            return new StartUpModel
            {
                User = Request.User
            };
        }

        [HttpMethod("POST")]
        public StartUpModel Login()
        {
            if (Request.User.Roles.Any())
            {
                Request.ViewTemplateName = "Index";
                return Index();
            }

            return new StartUpModel
            {
                Message = "There was a problem with your login details. Please try again.",
                User = Request.User
            };
        }
    }
}
