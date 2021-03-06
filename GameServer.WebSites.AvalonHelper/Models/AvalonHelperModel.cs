﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thingy.WebServerLite.Api;

namespace GameServer.WebSites.AvalonHelper
{
    public class AvalonHelperModel
    {
        public AvalonHelperModel(IAvalonGame game, IWebServerRequest request)
        {
            GameStatus = game.GetGameStatus();
            UserStatus = game.GetUserStatus(request.User);
            Players = game.GetPlayerList();            
            CanStartGameMessage = game.CanStartGame();
            Roles = game.GetRevealedRoles();
        }

        public GameStatuses GameStatus { get; set; }

        public UserStatusModel UserStatus { get; set; }

        public PlayerStatusModel[] Players { get; set; }

        public RoleStatusModel[] Roles { get; set; }

        public string CanStartGameMessage { get; set; }

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

        public bool CanStartGame()
        {
            return GameStatus == GameStatuses.SettingUp && string.IsNullOrEmpty(CanStartGameMessage);
        }

        public bool CanEndGame()
        {
            return GameStatus == GameStatuses.InProgress;
        }

        public bool CanResetGame()
        {
            return GameStatus == GameStatuses.EndScreen;
        }

        public bool CanShowRoleList()
        {
            return GameStatus == GameStatuses.EndScreen && (Roles != null);
        }

        public bool CanShowPlayerList()
        {
            return GameStatus != GameStatuses.EndScreen || (Roles == null);
        }
    }
}
