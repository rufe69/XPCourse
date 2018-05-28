using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace UnitTest
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        [ExpectedException(typeof(TooManyPlayersException))]
        public void NoMoreThanSixPlayer_Game_AddPlayer()//1.5
        {
            var game = Create.Game().WithPlayers(7).Please();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void OnlyX5Bets_Game_Bet()//2.5
        {
            var game = Create.Game().FakeGameWithScore(5).Please();
            var player = Create.Player().In(game).WithChips(4).WithBet(5).Please();

            game.Play(new Dice());
        }
    }
}
