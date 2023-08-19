using Bowling;

namespace BowlingTests
{
    [TestClass]
    public class GameTests
    {
        private Game _game;

        [TestInitialize]
        public void SetupGame()
        {
            _game = new Game();

        }

        [TestMethod]
        public void RollingBallResultsIntoScore()
        {
            _game.Roll(1);
            _game.Roll(3);

            Assert.AreEqual(4, _game.Score);
        }

        [TestMethod]
        public void TwoRollsPerOpenFrame()
        {
            _game.Roll(1);
            _game.Roll(3);
            _game.Roll(4);

            Assert.AreEqual(2, _game.CurrentFrame);
        }

        [TestMethod]
        public void ThrowAfterSpareGetsAddedToPreviousFrame()
        {
            _game.Roll(4);
            _game.Roll(6);
            _game.Roll(2);
            _game.Roll(0);

            Assert.AreEqual(14, _game.Score);
        }
    }
}