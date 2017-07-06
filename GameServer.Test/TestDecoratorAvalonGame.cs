using GameServer.WebSites.AvalonHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Thingy.WebServerLite.Api;

namespace GameServer.Test
{
    /// <summary>
    /// This class uses the Decorator pattern to extend the functionality of the
    /// IAvalonGame implementation.
    /// It adds players to the game so you can test functionality without
    /// having to have five machines on the go. 
    /// 
    /// THIS CLASS IS INTENDED FOR LOCAL TESTING. It's not massively thread-safe.
    /// 
    /// </summary>
    public class TestDecoratorAvalonGame : IAvalonGame
    {
        private readonly IAvalonGame baseGame;

        private IUser colin = new TestPlayer("Colin");
        private IUser tom = new TestPlayer("Tom");
        private IUser rupert = new TestPlayer("Rupert");
        private IUser angel = new TestPlayer("Angel");
        private IUser katie = new TestPlayer("Katie");
        private IUser alexander = new TestPlayer("Alexander");
        private IUser cal = new TestPlayer("Cal");
        private bool addedPlayers = false;

        public TestDecoratorAvalonGame(IAvalonGame game)
        {
            this.baseGame = game;
        }

        public string CanStartGame()
        {
            return baseGame.CanStartGame();
        }

        public void EndGame()
        {
            baseGame.EndGame();
        }

        public GameStatuses GetGameStatus()
        {
            return baseGame.GetGameStatus();
        }

        public PlayerStatusModel[] GetPlayerList()
        {
            return baseGame.GetPlayerList();
        }

        public UserStatusModel GetUserStatus(IUser user)
        {
            return baseGame.GetUserStatus(user);
        }

        public void ResetGame()
        {
            baseGame.ResetGame();
        }

        public bool SelectRole(IUser user, int roleId)
        {
            if (!addedPlayers)
            {
                baseGame.SelectRole(colin, roleId == 1 ? 3 : 1); // Colin will pick Merlin if the human player doesn't and servant 1 if he does
                baseGame.SelectRole(tom, roleId == 2 ? 7 : 2); // Tom will pick Percival if the human player doesn't and servant 2 if he does
                baseGame.SelectRole(rupert, roleId == 5 ? 7 : 5); // Rupert will pick servant 3 if the human player doesn't and a servant 5 if he does
                baseGame.SelectRole(angel, roleId == 6 ? 4 : 6); // Angel will pick servant 4 if the human player doesn't and a servant 2 if he does
                baseGame.SelectRole(katie, roleId == 9 ? 12 : 9); // Katie will pick Morgana if the human player doesn't and minion 1 if he does
                baseGame.SelectRole(alexander, roleId == 8 ? 13 : 8); // Alexander will pick Mordred if the human player doesn't and minion 2 if he does
                baseGame.SelectRole(cal, roleId == 10 ? 14 : 10); // Cal will pick the Assassin if the human player doesn't and minion 3 if he does
                addedPlayers = true;
            }

            return baseGame.SelectRole(user, roleId);
        }

        public string StartGame()
        {
            return baseGame.StartGame();
        }

        public void RefreshSecrets()
        {
            baseGame.RefreshSecrets();
        }
    }
}
