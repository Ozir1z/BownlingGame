
using System.Linq;
using System.Text;

namespace Bowling
{
    public class Game
    {
        public int CurrentFrameNumber { get; private set; } = 1;
        public bool IsGameDone { get; private set; } = false;
        public int Score => _frames.Sum(x => x.FrameScore);

        public int TotalRolls = 1;
        private readonly int _totalFrames = 10;
        private readonly List<Frame> _frames = new();

        public Game()
        {
            for(int i = 0; i < _totalFrames; i++)
            {
                _frames.Add(new Frame()); // initialize 11 frames (10 base frames and a potential bonus frame)
            }
        }

        public void Roll(int pins)
        {
            if (IsGameDone) return;
            var curerntFrame = _frames[CurrentFrameNumber - 1];

            if (curerntFrame.IsFrameComplete && CurrentFrameNumber == _totalFrames) // 10th frame bonus rolls
            {
                curerntFrame.BonusRoll(pins);

                if (IsGameDone = curerntFrame.IsFrameCompleteWithBonusScores)
                    return;//game is done
            }

            curerntFrame.Roll(pins);
            AddTotalRolls(curerntFrame);
            AddCurrentRollToPreviousSparesAndOrStrikes(pins);

            if (CurrentFrameNumber == _totalFrames)
                IsGameDone = curerntFrame.IsFrameCompleteWithBonusScores;

            if (curerntFrame.IsFrameComplete && CurrentFrameNumber != _totalFrames)
                CurrentFrameNumber++;
        }

        private void AddTotalRolls(Frame curerntFrame)
        {
            TotalRolls++;
            if (curerntFrame.IsStrike)
                TotalRolls++;
        }

        public override string ToString()
        {
            var gameScoreString = new StringBuilder();

            //frame number
            gameScoreString.AppendLine("");
            gameScoreString.Append(string.Format("{0, 5} ", "frame |"));
            for (int i = 0; i < _frames.Count; i++)
                gameScoreString.Append(string.Format("{0, 5}   |", i + 1));

            //score
            gameScoreString.AppendLine("");
            gameScoreString.Append(string.Format("{0, 5} ", "score |"));
            for (int i = 0; i < _frames.Count; i++)
                gameScoreString.Append(string.Format("{0, 7} |", _frames[i]));

            return gameScoreString.ToString();
        }

        private void AddCurrentRollToPreviousSparesAndOrStrikes(int pins)
        {
            if (CurrentFrameNumber > 1)
            {
                var previousFrame = _frames[CurrentFrameNumber - 2];
                previousFrame.BonusRoll(pins);
            }
            if (CurrentFrameNumber > 2)
            {
                var twoRollsAgoFrame = _frames[CurrentFrameNumber - 3];
                twoRollsAgoFrame.BonusRoll(pins);
            }
        }
    }
}
