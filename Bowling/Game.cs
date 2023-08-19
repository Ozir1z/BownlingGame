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
        private readonly List<Frame> _frames = new();
        private bool _OnStrikeStreak = false;
        private bool _isGameDone = false;
        private int extraThrows = 0;


        public Game()
        {
            for(int i = 0; i < 10 ; i++)
            {
                _frames.Add(new Frame()); // initialize 11 frames (10 base frames and a potential bonus frame)
            }
        }
        public void Roll(int pins)
        {
            if (_isGameDone) return;

            if(extraThrows > 0)
            {
                Score += pins;
                extraThrows--;
                if (extraThrows == 0) _isGameDone = true;
                return;
            }

            var currentFrame = _frames[CurrentFrame - 1];

            currentFrame.Roll(pins);
            Score += pins;

            if (CurrentFrame > 1)
            {
                var previousFrame = _frames[CurrentFrame - 2];

                if (previousFrame.IsSpare && (!currentFrame.IsFrameDone || currentFrame.IsStrike)) // previous frame was spare
                {
                    Score += pins;
                }
                if (previousFrame.IsStrike && currentFrame.IsFrameDone) // previous frame was strike
                {
                    if (currentFrame.IsStrike)
                        _OnStrikeStreak = true;
                    Score += currentFrame.GetFrameScore();

                }
                if (_OnStrikeStreak && !currentFrame.IsFrameDone) //been on a strike streak, but currentframe is not a strike we have to add that roll to the total score and end the streak
                {
                    Score += pins;
                    _OnStrikeStreak = false;
                }

            }

            if (CurrentFrame == 10) // last frame, check for extra shots
            {
                if (currentFrame.IsSpare) extraThrows = 1;
                else if (currentFrame.IsStrike) extraThrows = 2;
                else if(currentFrame.IsFrameDone) _isGameDone = true;
                return;
            }

            if (currentFrame.IsFrameDone)
                CurrentFrame++;

        }
    }

    public class Frame
    {
        public bool IsFrameDone { get; private set; } = false;
        public bool IsSpare { get; private set; } = false;
        public bool IsStrike { get; private set; } = false;
        private int _roll1 = -1;
        private int _roll2 = 0;

        public void Roll(int pins)
        {
            if (IsFrameDone) return; // throw OutOfFrownException

            if(_roll1 == -1)
            {
                _roll1 = pins;
                IsStrike = _roll1 == 10;
                if (IsStrike) IsFrameDone = true;
            }
            else
            {
                _roll2 = pins;
                IsFrameDone = true;
                IsSpare = _roll1 + _roll2 == 10;
            }
        }

        public int GetFrameScore()
        {
           return _roll1 + _roll2;
        }
    }
}
