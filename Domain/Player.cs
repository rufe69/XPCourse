using System;
using System.Linq;
using System.Collections.Generic;

namespace Domain
{
    public class Player
    {
        private RollDiceGame currentGame;
        public bool IsInGame => currentGame != null;
        public Chip availableChips { get; set; }
        public List<Bet> CurrentBets;

        public Player()
        {
            CurrentBets = new List<Domain.Bet>();
            availableChips = new Chip(0);
        }

        public void Join(RollDiceGame game)
        {
            if (IsInGame) throw new InvalidOperationException();

            currentGame = game;
            currentGame.AddPlayer(this);
        }

        public void LeaveGame()
        {
            if (!IsInGame) throw new InvalidOperationException();
            
            currentGame.RemovePlayer(this);
            currentGame = null;
        }

        public void Buy(Chip chips)
        {
            availableChips = chips;
        }
        
        public bool Has(Chip chips)
        {
            return availableChips >= chips;
        }

        public int Bet(Bet bet)
        {
            if (bet.Score < 1 || bet.Score > 6) throw new InvalidOperationException();
            if (bet.Chips.Amount > availableChips.Amount) throw new InvalidOperationException();

            CurrentBets.Add(bet);
            return availableChips.Amount;
        }
        
        public void Win(int chipsAmount)
        {
            availableChips = new Chip(availableChips.Amount + chipsAmount);
            CurrentBets = null;
        }

        public void Lose()
        {
            CurrentBets = null;
        }
    }
}