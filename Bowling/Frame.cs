using System.Runtime.Serialization;

namespace Bowling
{
    public class Frame
    {
        public bool IsSpare => !IsStrike && _roll1 + _roll2 == _totalPins;
        public bool IsStrike => _roll1 == _totalPins;
        public bool IsFrameComplete { get; private set; } = false;
        public bool IsFrameCompleteWithBonusScores { get; private set; } = false;
        public int FrameScore { get; private set; }

        private readonly int _totalPins = 10;
        private int _roll1 = -1;
        private int _roll2 = -1;
        private int _extraRolls = 0;
        private bool IsInvalid(int pins) => pins > _totalPins || _roll1 + pins > _totalPins;
        private int InvalidPinsAmount(int pins) => pins > _totalPins ? pins : _roll1 + pins;

        public void Roll(int pins)
        {
            if (IsFrameComplete) 
                return;
            if (IsInvalid(pins)) 
                throw new InvalidFrameException($"Trying to knock over {InvalidPinsAmount(pins)} pins." +
                    $" There are only {_totalPins} pins total."); // throw InvalidFrameException

            if (_roll1 == -1)
                Roll1(pins);
            else
                Roll2(pins);
        }

        public void BonusRoll(int pins)
        {
            if (_extraRolls > 0)
            {
                FrameScore += pins;
                _extraRolls--;
            }
            if (_extraRolls == 0)
                IsFrameCompleteWithBonusScores = true;
        }

        public override string ToString()
        {
            var stringRoll1 = _roll1 == -1 ? " " : _roll1.ToString();
            var stringRoll2 = _roll2 == -1 ? " " : _roll2.ToString();
            return $"{stringRoll1} / {stringRoll2}";
        }

        private void Roll1(int pins)
        {
            _roll1 = pins;
            FrameScore += _roll1;
            if (IsStrike)
            {
                IsFrameComplete = true;
                _extraRolls = 2;
            }
        }

        private void Roll2(int pins)
        {
            _roll2 = pins;
            FrameScore += _roll2;
            IsFrameComplete = true;
            if (IsSpare)
                _extraRolls = 1;
            else // open frame
                IsFrameCompleteWithBonusScores = true;
        }
    }

    [Serializable]
    public class InvalidFrameException : Exception
    {
        public InvalidFrameException()
        {
        }

        public InvalidFrameException(string? message) : base(message)
        {
        }

        public InvalidFrameException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidFrameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
