using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace UnitTest
{
    //Класс-заглушка для кубика
    public class DiceFake : IDice
    {
        public int Score { get; set; }
        public virtual int GetLuckyScore()
        {
            return Score;
        }
    }

    //Класс-заглушка для игры
    class RollDiceGameFake : RollDiceGame
    {
        public RollDiceGameFake() { }

        public RollDiceGameFake(int score)
        {
            _score = score;
        }

        public override int getLuckyScore(IDice dice)
        {
            return _score;
        }

        public int _score { get; set; }
    }
}
