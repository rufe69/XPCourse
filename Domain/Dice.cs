﻿using System;

namespace Domain
{
    public class Dice : IDice
    {
        public int GetLuckyScore()
        {
            return new Random(DateTime.Now.Millisecond).Next(1, 6);
        }
    }  
}