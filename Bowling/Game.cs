using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    public class Game
    {
        public int Score { get; set; }

        public void Roll(int pins)
        {
            Score += pins;
        }
    }
}
