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
    /// </summary>
    public class TestDecoratorAvalonGame : IAvalonGame
    {
        private readonly IAvalonGame baseGame;

        private IUser colin = new TestPlayer("Colin");
        private IUser tom = new TestPlayer("Tom");
        private IUser rupert = new TestPlayer("Rupert");
        private IUser katie = new TestPlayer("Katie");
        private IUser alexander = new TestPlayer("Alexander");

        public TestDecoratorAvalonGame(IAvalonGame game)
        {
            this.baseGame = game;
            // Colin picks Merlin
            game.SelectRole(colin, 1);
            // Tom picks Percival            
            game.SelectRole(tom, 2);
            // Rupert picks Loyal Servant of Arthur
            game.SelectRole(rupert, 5);
            // Katie picks Morgana
            game.SelectRole(katie, 9);
            // Alexander picks Assassin
            game.SelectRole(alexander, 8);
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
