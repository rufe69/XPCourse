using System;
using System.Linq;
using System.Collections.Generic;

namespace Domain
{
    public class RollDiceGame
    {
        private List<Player> players = new List<Player>();

        public void AddPlayer(Player player)
        {
            if (players.Count == 6) throw new TooManyPlayersException();
            
            players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            players.Remove(player);
        }

        public void Play(IDice dice)
        {
            var luckyScore = getLuckyScore(dice);

            foreach (var player in players)
            {
                bool win = false;
                for (int i = 0; i < player.CurrentBets.Count; i++)
                {
                    if (player.CurrentBets[i].Chips.Amount % 5 != 0)
                        throw new InvalidOperationException();

                    if (player.CurrentBets[i].Score==luckyScore)
                    {
                        win = true;
                        player.Win(player.CurrentBets[i].Chips.Amount * 6);                        
                        break;
                    }
                }
                if (!win) player.Lose();
            }
        }
        
        public virtual int getLuckyScore(IDice dice)
        {
            return dice.GetLuckyScore();
        }
    }
}