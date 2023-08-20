namespace Bowling
{
    public class Frame
    {
        public bool IsSpare => _roll1 + _roll2 == _totalPins;
        public bool IsStrike => _roll1 == _totalPins;
        public bool IsFrameDone { get; private set; } = false;
        public bool IsFrameCompleteWithBonusScores { get; private set; } = false;
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
                else // no spare or strike
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
