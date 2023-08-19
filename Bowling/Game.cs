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
        public int CurrentFrame { get; set; } = 1;
        private readonly List<Frame> _frames = new(10);

        public Game()
        {
            for(int i = 0; i < 10 ; i++)
            {
                _frames.Add(new Frame());
            }
        }
        public void Roll(int pins)
        {
            var currentFrame = _frames[CurrentFrame - 1];
            Score += currentFrame.Roll(pins);

            if (currentFrame.IsFrameDone())
                CurrentFrame++;
        }
    }

    public class Frame
    {
        public int CurrentRole = 1;

        private int _roll1 = 0;
        private int _roll2 = 0;

        public int Roll(int pins)
        {
            if (CurrentRole > 2) return -1; // make custom OutOfFrameException

            if(CurrentRole == 1)
                _roll1 = pins;
            else if(CurrentRole == 2)
                _roll2 = pins;

            CurrentRole++;
            return pins;
        }

        public int GetFrameScore()
        {
           return _roll1 + _roll2;
        }

        public bool IsFrameDone() => CurrentRole > 2;
    }
}
