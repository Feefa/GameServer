using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thingy.WebServerLite.Api;

namespace GameServer.WebSites.AvalonHelper
{
    /// <summary>
    /// Simple extension of WebSite which ignores all calls to file URLs and allows them to fall to the default web site
    /// </summary>
    public class AvalonHelperWebSite : WebSite
    {
        public AvalonHelperWebSite(
            IViewProvider viewProvider,
            IControllerProviderFactory controllerProviderFactory,
            IController[] controllers,
            string name,
            int portNumber,
            string path) : base(viewProvider, controllerProviderFactory, controllers, name, portNumber, path)
        { }

        public override bool CanHandle(IWebServerRequest request)
        {
            return base.CanHandle(request) && !request.IsFile;
        }
    }
}

