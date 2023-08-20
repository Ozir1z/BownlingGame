using System.Net.NetworkInformation;

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

            if (currentFrame.IsFrameDone && CurrentFrameNumber == 10) // last frame
            {
                currentFrame.TryAndAddBonusScore(pins);

                if (_isGameDone = currentFrame.IsFrameCompleteWithBonusScores)
                    return;//game is done
            }

            currentFrame.Roll(pins);
            AddCurrentRollToPreviousSparesAndOrStrikes(pins);

            if (currentFrame.IsFrameDone && CurrentFrameNumber != 10)
                CurrentFrameNumber++;

        }

        private void AddCurrentRollToPreviousSparesAndOrStrikes(int pins)
        {
            if (CurrentFrameNumber > 1)
            {
                var previousFrame = _frames[CurrentFrameNumber - 2];
                previousFrame.TryAndAddBonusScore(pins);
            }
            if (CurrentFrameNumber > 2)
            {
                var twoRollsAgoFrame = _frames[CurrentFrameNumber - 3];
                twoRollsAgoFrame.TryAndAddBonusScore(pins);
            }
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
}
