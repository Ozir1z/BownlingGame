
namespace Bowling
{
    public class Game
    {
        public int CurrentFrameNumber { get; set; } = 1;
        public int Score => _frames.Sum(x => x.FrameScore);

        private bool _isGameDone = false;
        private readonly List<Frame> _frames = new();

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

            if (currentFrame.IsFrameComplete && CurrentFrameNumber == 10) // last frame
            {
                currentFrame.BonusRoll(pins);

                if (_isGameDone = currentFrame.IsFrameCompleteWithBonusScores)
                    return;//game is done
            }

            currentFrame.Roll(pins);
            AddCurrentRollToPreviousSparesAndOrStrikes(pins);

            if (currentFrame.IsFrameComplete && CurrentFrameNumber != 10)
                CurrentFrameNumber++;

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
