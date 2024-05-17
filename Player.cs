﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace carpet_of_winners.git
{
    internal class Player
    {
        public int Number { get; private set; }
        public int Col { get; private set; }
        public int Row { get; private set; }
        public Player(int col,int row,int number)
        {
            Number = number;
            Col = col;
            Row = row;

        }
        public void move(int newCol,int newRow)
        {
            Row = newRow;
            Col = newCol;
        }
    }
}
