using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace UnitTest
{
    //Класс для создания игрока
    class PlayerBuilder
    {
        private Player _player = new Player();
        private Chip _chips;
        private int _score = 0;
        private int[] _scores;

        public Player Please()
        {
            if (_chips != null)
                _player.Buy(_chips);

            if (_score != 0)
                _player.Bet(new Bet(_chips, _score));

            if(_scores!=null)
            {
                int equalChipsAmount = _chips.Amount / _scores.Length;
                for (int i = 0; i < _scores.Length; i++)
                {
                    _player.Bet(new Bet(new Chip(equalChipsAmount), _scores[i]));
                }
            }

            return _player;
        }

        public PlayerBuilder In(RollDiceGame game)
        {
            _player.Join(game);
            return this;
        }

        public PlayerBuilder WithChips(int amount)
        {
            _chips = new Chip(amount);
            return this;
        }

        public PlayerBuilder WithBet(int score)
        {
            _score = score;
            return this;
        }

        public PlayerBuilder WithBets(int[] scores)
        {
            _scores = new int[scores.Length];
            for (int i = 0; i < scores.Length; i++)
                _scores[i] = scores[i];
            return this;
        }
    }

    //Класс для создания игры
    class GameBuilder
    {
        private RollDiceGame _game = new RollDiceGame();

        public RollDiceGame Please()
        {
            return _game;
        }

        public GameBuilder FakeGameWithScore(int score)
        {
            _game = new RollDiceGameFake(score);
            return this;
        }

        public GameBuilder WithPlayers(int count)
        {
            for (int i = 0; i < count; i++)
                _game.AddPlayer(new Player());
            
            return this;
        }
    }

    //Класс для создания объектов "игрок" или "игра"
    static class Create
    {
        public static PlayerBuilder Player()
        {
            return new PlayerBuilder();
        }

        public static GameBuilder Game()
        {
            return new GameBuilder();
        }
    }

    
}
