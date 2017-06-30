﻿using System;
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
        public string Index()
        {
            if (!Request.User.Roles.Any())
            {
                Request.ViewTemplateName = "Login";
            }

            return string.Empty;
        }

        [HttpMethod("POST")]
        public string Login()
        {
            if (Request.User.Roles.Any())
            {
                Request.ViewTemplateName = "Index";
                Index();
            }

            return "There was a problem with your login details. Please try again.";
        }
    }
}
