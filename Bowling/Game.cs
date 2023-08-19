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

            if (currentFrame.IsFrameDone)
                CurrentFrame++;
        }
    }

    public class Frame
    {
        public bool IsFrameDone { get; private set; } = false;
        private int _roll1 = -1;
        private int _roll2 = 0;

        public int Roll(int pins)
        {
            if (IsFrameDone) return -1; // make custom OutOfFrameException

            if(_roll1 == -1)
                _roll1 = pins;
            else
            {
                _roll2 = pins;
                IsFrameDone = true;
            }

            return pins;
        }

        public int GetFrameScore()
        {
           return _roll1 + _roll2;
        }
    }
}
