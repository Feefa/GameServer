using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thingy.WebServerLite.Api;

namespace GameServer.WebSites.AvalonHelper
{
    public interface IAvalonGame
    {
        GameStatuses GetGameStatus();
            
        UserStatusModel GetUserStatus(IUser user);

        bool SelectRole(IUser user, int roleId);

        string StartGame();

        void EndGame();

        void ResetGame();

        PlayerStatusModel[] GetPlayerList();
    }
}
