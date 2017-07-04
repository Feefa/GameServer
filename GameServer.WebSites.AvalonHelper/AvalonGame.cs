using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thingy.WebServerLite.Api;

namespace GameServer.WebSites.AvalonHelper
{
    public class AvalonGame : IAvalonGame
    {
        private IDictionary<string, int> userRoles = new Dictionary<string, int>();

        private Mutex userRolesMutex = new Mutex();

        private GameStatuses gameStatus = GameStatuses.SettingUp;

        private Mutex gameStateMutex = new Mutex();

        public UserStatusModel GetUserStatus(IUser user)
        {
            UserStatusModel model = new UserStatusModel();
            model.RoleId = GetRoleIdForUser(user);

            throw new NotImplementedException();
        }

        private int GetRoleIdForUser(IUser user)
        {
            int roleId;
            userRolesMutex.WaitOne();

            try
            {
                if (userRoles.ContainsKey(user.UserId))
                {
                    roleId = userRoles[user.UserId];
                }
                else
                {
                    GameStatuses gameStatus = GetGameStatus();

                    roleId = gameStatus == GameStatuses.SettingUp ? 0 : -1;
                    {
                        userRoles[user.UserId] = roleId;
                    }
                }

                userRoles[user.UserId] = roleId;
            }
            finally
            {
                userRolesMutex.ReleaseMutex();
            }

            return roleId;
        }

        public bool SelectRole(IUser user, int roleId)
        {
            GameStatuses status = GetGameStatus();

            if (gameStatus != GameStatuses.SettingUp)
            {
                return false;
            }

            SetRoleIdForUser(user, roleId);

            return true;
        }

        private void SetRoleIdForUser(IUser user, int roleId)
        {
            userRolesMutex.WaitOne();

            try
            {
                userRoles[user.UserId] = roleId;
            }
            finally
            {
                userRolesMutex.ReleaseMutex();
            }
        }

        public GameStatuses GetGameStatus()
        {
            GameStatuses status;
            gameStateMutex.WaitOne();

            try
            {
                status = gameStatus;
            }
            finally
            {
                gameStateMutex.ReleaseMutex();
            }

            return status;
        }

        public void StartGame()
        {
            throw new NotImplementedException();
        }

        public void EndGame()
        {
            throw new NotImplementedException();
        }

        public PlayerStatusModel[] GetPlayerList()
        {
            PlayerStatusModel[] players;
            userRolesMutex.WaitOne();

            try
            {
                players = new PlayerStatusModel[userRoles.Keys.Count];
                int index = 0;

                foreach(string userId in userRoles.Keys)
                {
                    players[index++] = new PlayerStatusModel
                    {
                        UserId = userId,
                        RoleId = userRoles[userId] > 0 ? 1 : userRoles[userId]
                    };
                }
            }
            finally
            {
                userRolesMutex.ReleaseMutex();
            }

            return players;
        }
    }
}
