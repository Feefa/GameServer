using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Api
{
    public interface IGameInfo
    {
        string Title { get; }

        string Description { get; }

        string Url { get; }

        string Role { get; }
    }
}
