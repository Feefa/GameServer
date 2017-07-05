using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.WebSites.AvalonHelper
{
    public class AvalonHelperModel
    {
        public GameStatuses GameStatus { get; set; }

        public UserStatusModel UserStatus { get; set; }

        public PlayerStatusModel[] Players { get; set; }

        public bool CanSelectRole()
        {
            return UserStatus.RoleId == 0;
        }

        public bool RoleSelected()
        {
            return UserStatus.RoleId > 0;
        }

        public bool CanShowSecrets()
        {
            return RoleSelected() && GameStatus != GameStatuses.SettingUp;
        }

        public bool IsWaiting()
        {
            return UserStatus.RoleId == -1 || (UserStatus.RoleId > 0 && GameStatus == GameStatuses.SettingUp);
        }
    }
}
