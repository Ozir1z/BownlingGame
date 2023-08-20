using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    public class Game
    {
        public int Score => CalucalteScore();

        public int CurrentFrameNumber { get; set; } = 1;
        private readonly List<Frame> _frames = new();
        private bool _isGameDone = false;

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

            var currentFrame = _frames[CurrentFrameNumber - 1];

            //add bonus score to 10th frame
            if (currentFrame.IsFrameDone && CurrentFrameNumber == 10)
            {
                currentFrame.TryAndAddBonusScore(pins);
                if (currentFrame.IsFrameCompleteWithBonusScores)
                {
                    _isGameDone = true;
                    return; // game done
                }
            }

            //roll in current frame
            currentFrame.Roll(pins);

            //check and add bonus score for previous strike/spare
            if (CurrentFrameNumber > 1)
            {
                var previousFrame = _frames[CurrentFrameNumber - 2];
                previousFrame.TryAndAddBonusScore(pins);
            }
            //check and add bonus score for strike 2 frames ago
            if (CurrentFrameNumber > 2)
            {
                var twoRollsAgoFrame = _frames[CurrentFrameNumber - 3];
                twoRollsAgoFrame.TryAndAddBonusScore(pins);
            }

            // go to next frame unless we're on the last frame
            if (currentFrame.IsFrameDone && CurrentFrameNumber != 10)
                CurrentFrameNumber++;

        }

        public int CalucalteScore()
        {
            var score = 0;
            foreach( var frame in _frames)
            {
                score += frame.FrameScore;
            }
            return score;
        }

    }

    public class Frame
    {
        public bool IsFrameDone { get; private set; } = false;
        public bool IsFrameCompleteWithBonusScores { get; private set; } = false;
        public bool IsSpare => _roll1 + _roll2 == _totalPins;
        public bool IsStrike => _roll1 == _totalPins;
        public int FrameScore { get; private set; }

        private readonly int _totalPins = 10;
        private int _roll1 = -1;
        private int _roll2 = 0;
        private int _extraRolls = 0;

        public void Roll(int pins)
        {
            if (IsFrameDone) return; // throw OutOfFrameException

            if (_roll1 == -1)
            {
                _roll1 = pins;
                FrameScore += _roll1;
                if (IsStrike)
                {
                    IsFrameDone = true;
                    _extraRolls = 2;
                }
            }
            else
            {
                _roll2 = pins;
                FrameScore += _roll2;
                IsFrameDone = true;
                if (IsSpare)
                    _extraRolls = 1;
                else
                    IsFrameCompleteWithBonusScores = true;
            }
        }

        public void TryAndAddBonusScore(int pins)
        {
            if(_extraRolls > 0)
            {
                FrameScore += pins;
                _extraRolls--;
            }
            if (_extraRolls == 0) 
                IsFrameCompleteWithBonusScores = true;
        }
    }
}
