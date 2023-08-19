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
            var game = new Game();
            game.Roll(1);
            game.Roll(3);

            Assert.AreEqual(4, game.Score);
        }

        [TestMethod]
        public void TwoRollsPerOpenFrame()
        {
            var game = new Game();
            game.Roll(1);
            game.Roll(3);
            game.Roll(4);

            Assert.AreEqual(2, game.CurrentFrame);
        }
    }
}