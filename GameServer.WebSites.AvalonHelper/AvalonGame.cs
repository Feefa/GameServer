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

        private Mutex userRolesMutex = new Mutex(false, "bf801b8a-5cf2-4875-997a-c98c385c2ef3");

        private IDictionary<string, string[]> userSecrets = new Dictionary<string, string[]>();

        private Mutex userSecretsMutex = new Mutex(false, "807e37bc-35f2-4adc-b036-28b3c6875595");

        private GameStatuses gameStatus = GameStatuses.SettingUp;

        private Mutex gameStateMutex = new Mutex(false, "d1c47b61-4af3-4f45-a10e-b7bedc11a002");

        public UserStatusModel GetUserStatus(IUser user)
        {
            return new UserStatusModel
            {
                RoleId = GetRoleIdForUser(user),
                Secrets = GetSecretsForUser(user)
            };
        }

        private int GetRoleIdForUser(IUser user)
        {
            if (!user.Roles.Contains("Player"))
            {
                return -1;
            }

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

        private string[] GetSecretsForUser(IUser user)
        {
            if (!user.Roles.Contains("Player"))
            {
                return new string[] { "n/a" };
            }

            string[] secrets;
            userSecretsMutex.WaitOne();

            try
            {
                if (userSecrets.ContainsKey(user.UserId))
                {
                    secrets = new string[userSecrets[user.UserId].Length];
                    userSecrets[user.UserId].CopyTo(secrets, 0);
                }
                else
                {
                    secrets = new string[] { "None" };
                }
            }
            finally
            {
                userSecretsMutex.ReleaseMutex();
            }

            return secrets;
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

        public string CanStartGame()
        {
            GameStatuses gameStatus = GetGameStatus();

            if (gameStatus != GameStatuses.SettingUp)
            {
                return "The game has already been started";
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

            return string.Empty;
        }

        public string StartGame()
        {
            string message = CanStartGame();

            if (string.IsNullOrEmpty(message))
            {
                SetGameStatus(GameStatuses.InProgress);
                RefreshSecrets();
            }

            return message;
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

            if (gameStatus != GameStatuses.EndScreen)
            {
                throw new AvalonHelperException("Attempted to reset game when GameStatus != EndScreen.");
            }

            SetGameStatus(GameStatuses.SettingUp);
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

                foreach (string userId in userRoles.Keys)
                {
                    int roleId = userRoles[userId] > 0 ? 1 : userRoles[userId];

                    if (roleId != -1)
                    {
                        players[index] = new PlayerStatusModel
                        {
                            UserId = userId,
                            RoleId = roleId
                        };
                    }

                    index++;
                }
            }
            finally
            {
                userRolesMutex.ReleaseMutex();
            }

            return players;
        }

        public void RefreshSecrets()
        {
            userRolesMutex.WaitOne();

            try
            {
                userSecretsMutex.WaitOne();

                try
                {
                    userSecrets.Clear();

                    IList<string> seenByMerlin = new List<string>();
                    IList<string> seenByPercival = new List<string>();
                    IList<string> seenByMinions = new List<string>();

                    foreach (string userId in userRoles.Keys)
                    {
                        if (userRoles[userId] > 7 && userRoles[userId] != 11)
                        {
                            if (userRoles[userId] != 8)
                            {
                                seenByMerlin.Add(string.Format("{0} is a minion of Mordred", userId));
                            }
                            seenByMinions.Add(string.Format("{0} is a minion of Mordred", userId));
                        }

                        if (userRoles[userId] == 1 || userRoles[userId] == 9)
                        {
                            if (userRoles.Values.Contains(9))
                            {
                                seenByPercival.Add(string.Format("{0} might be Merlin or Morgana", userId));
                            }
                            else
                            {
                                seenByPercival.Add(string.Format("{0} is Merlin", userId));
                            }
                        }

                    }

                    foreach (string userId in userRoles.Keys)
                    {
                        switch (userRoles[userId])
                        {
                            case (1): userSecrets[userId] = seenByMerlin.ToArray(); break;
                            case (2): userSecrets[userId] = seenByPercival.ToArray(); break;
                            default:

                                if (userRoles[userId] > 7 && userRoles[userId] != 11)
                                {
                                    userSecrets[userId] = seenByMinions.ToArray();
                                }

                                break;
                        }
                    }
                }
                finally
                {
                    userSecretsMutex.ReleaseMutex();
                }
            }
            finally
            {
                userRolesMutex.ReleaseMutex();
            }
        }

        public RoleStatusModel[] GetRevealedRoles()
        {
            GameStatuses gameStatus = GetGameStatus();

            if (gameStatus != GameStatuses.EndScreen)
            {
                return null;
            }

            IList<RoleStatusModel> roles = new List<RoleStatusModel>();
            userRolesMutex.WaitOne();

            try
            {
                foreach(string userId in userRoles.Keys)
                {
                    RoleStatusModel model = new RoleStatusModel { PlayerName = userId };

                    switch(userRoles[userId])
                    {
                        case (1):
                            model.RoleName = "Merlin";
                            model.RoleImageFileName = "Images/merlin_small.jpg";
                            break;
                        case (2):
                            model.RoleName = "Percival";
                            model.RoleImageFileName = "Images/percival_small.jpg";
                            break;
                        case (3):
                            model.RoleName = "Loyal Servant of Arthur";
                            model.RoleImageFileName = "Images/servant1_small.jpg";
                            break;
                        case (4):
                            model.RoleName = "Loyal Servant of Arthur";
                            model.RoleImageFileName = "Images/servant2_small.jpg";
                            break;
                        case (5):
                            model.RoleName = "Loyal Servant of Arthur";
                            model.RoleImageFileName = "Images/servant3_small.jpg";
                            break;
                        case (6):
                            model.RoleName = "Loyal Servant of Arthur";
                            model.RoleImageFileName = "Images/servant4_small.jpg";
                            break;
                        case (7):
                            model.RoleName = "Loyal Servant of Arthur";
                            model.RoleImageFileName = "Images/servant5_small.jpg";
                            break;
                        case (8):
                            model.RoleName = "Mordred";
                            model.RoleImageFileName = "Images/mordred_small.jpg";
                            break;
                        case (9):
                            model.RoleName = "Morgana";
                            model.RoleImageFileName = "Images/morgana_small.jpg";
                            break;
                        case (10):
                            model.RoleName = "Assassin";
                            model.RoleImageFileName = "Images/assassin_small.jpg";
                            break;
                        case (11):
                            model.RoleName = "Oberon";
                            model.RoleImageFileName = "Images/oberon_small.jpg";
                            break;
                        case (12):
                            model.RoleName = "Minion of Mordred";
                            model.RoleImageFileName = "Images/minion1_small.jpg";
                            break;
                        case (13):
                            model.RoleName = "Minion of Mordred";
                            model.RoleImageFileName = "Images/minion2_small.jpg";
                            break;
                        case (14):
                            model.RoleName = "Minion of Mordred";
                            model.RoleImageFileName = "Images/minion3_small.jpg";
                            break;
                    }

                    roles.Add(model);
                }
            }
            finally
            {
                userRolesMutex.ReleaseMutex();
            }

            return roles.ToArray();
        }
    }
}
