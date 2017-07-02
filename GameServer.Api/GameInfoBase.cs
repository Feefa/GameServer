using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Api
{
    public abstract class GameInfoBase : IGameInfo
    {
        public GameInfoBase(int portNumber, string pathAndQuery, string title, string description, string role)
        {
            GetLocalIpAddress();

            Title = title;
            Description = description;
            Url = string.Format("http://{0}:{1}/{2}", ServerIpAddress, portNumber, pathAndQuery);
            Role = role;
        }

        private void GetLocalIpAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ipAddress in host.AddressList)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    ServerIpAddress = ipAddress.ToString();
                }
            }

            if (string.IsNullOrEmpty(ServerIpAddress))
            {
                throw new InvalidOperationException("Local IP Address Not Found!");
            }
        }

        public virtual string Title { get; protected set; }

        public virtual string Description { get; protected set; }

        public virtual string Url { get; protected set; }

        public virtual string Role { get; protected set; }

        protected string ServerIpAddress { get; private set; }

    }
}
