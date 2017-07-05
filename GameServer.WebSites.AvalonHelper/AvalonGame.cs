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
            return new UserStatusModel
            {
                RoleId = GetRoleIdForUser(user)
            };
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

        public void SetGameStatus(GameStatuses status)
        {
            gameStateMutex.WaitOne();

            try
            {
                gameStatus = status;
            }
            finally
            {
                gameStateMutex.ReleaseMutex();
            }
        }

        public string StartGame()
        {
            GameStatuses gameStatus = GetGameStatus();

            if (gameStatus != GameStatuses.SettingUp)
            {
                throw new AvalonHelperException("Attempted to start game when GameStatus != SettingUp.");
            }

            int playerCount;
            int settingUpCount;

            userRolesMutex.WaitOne();

            try
            {
                playerCount = userRoles.Keys.Count(k => userRoles[k] > 0);
                settingUpCount = userRoles.Keys.Count(k => userRoles[k] == 0);
            }
            finally
            {
                userRolesMutex.ReleaseMutex();
            }

            if (playerCount < 5)
            {
                return "There are not enough players to start the game";
            }

            if (settingUpCount > 0)
            {
                return "One or more players has not confirmed their role on the role section screen";
            }

            SetGameStatus(GameStatuses.InProgress);

            return string.Empty;
        }

        public void EndGame()
        {
            GameStatuses gameStatus = GetGameStatus();

            if (gameStatus != GameStatuses.InProgress)
            {
                throw new AvalonHelperException("Attempted to end game when GameStatus != SettingUp.");
            }

            SetGameStatus(GameStatuses.EndScreen);
        }

        public void ResetGame()
        {
            GameStatuses gameStatus = GetGameStatus();

            if (gameStatus == GameStatuses.InProgress)
            {
                throw new AvalonHelperException("Attempted to end game when GameStatus = InProgress.");
            }

            SetGameStatus(GameStatuses.EndScreen);
            ClearUserRoles();
        }

        private void ClearUserRoles()
        {
            userRolesMutex.WaitOne();

            try
            {
                userRoles.Clear();
            }
            finally
            {
                userRolesMutex.ReleaseMutex();
            }
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
