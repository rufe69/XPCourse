using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain;

namespace UnitTest
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void Join_Player_IsInGame()//1.1
        {
            var game = Create.Game().Please();
            var player = Create.Player().In(game).Please();

            Assert.AreEqual(true, player.IsInGame);
        }

        [TestMethod]
        public void Leave_Player_LeaveGame()//1.2
        {
            var game = Create.Game().Please();
            var player = Create.Player().In(game).Please();

            player.LeaveGame();

            Assert.AreEqual(false, player.IsInGame);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanNotLeaveIfDidNotJoin_Player_LeaveGame()//1.3
        {
            var player = Create.Player().Please();

            player.LeaveGame();
            player.LeaveGame();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayJustOneGame_Player_Join()//1.4
        {
            var game = Create.Game().Please();
            var player = Create.Player().In(game).Please();

            player.Join(game);
        }

        
        [TestMethod]
        public void CanBuyChipsToBet_Player_Bet() // 2.1
        {
            var player = Create.Player().WithChips(500).WithBet(1).Please();

            Assert.IsNotNull(player.CurrentBets[0]);
        }

        [TestMethod]
        public void Play_Winner_ShouldGetMoreChips() // 2.2
        {
            var game = Create.Game().FakeGameWithScore(5).Please();
            var player = Create.Player().In(game).WithChips(500).WithBet(5).Please();
            
            game.Play(new Dice());

            Assert.AreEqual(true, player.Has(new Chip(3000)));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanNotPutMoreChipsThanBought_Player_Buy() // 2.3
        {
            var player = Create.Player().WithChips(500).Please();
            var bet = new Bet(new Chip(1000), 1);

            player.Bet(bet);
        }

        [TestMethod]
        public void CanBetMoreThanOne_Player_Bet() // 2.4
        {
            var game = new RollDiceGame();
            var player = Create.Player().In(game).WithChips(600).WithBets(new int[] { 1, 2, 3, 4, 5, 6 }).Please();
            
            int playerChips = player.availableChips.Amount;
            game.Play(new Dice());

            Assert.AreEqual(6 * 100 + playerChips, player.availableChips.Amount);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanBetOnlyFromOneToSix_Player_Bet() // 2.6
        {
            var player = Create.Player().WithChips(100).WithBet(8).Please();
        }
    }


    

    
}
