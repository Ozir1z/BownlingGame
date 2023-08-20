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
        public void RollingBallShouldResultIntoScor()
        {
            _game.Roll(1);
            _game.Roll(3);

            Assert.AreEqual(4, _game.Score);
            Assert.AreEqual(false, _game.IsGameDone);
        }

        [TestMethod]
        public void AfterTwoRollsShouldBeAtTheSecondFrame()
        {
            _game.Roll(1);
            _game.Roll(3);

            _game.Roll(4);

            Assert.AreEqual(2, _game.CurrentFrameNumber);
            Assert.AreEqual(false, _game.IsGameDone);
        }

        [TestMethod]
        public void ThrowAfterSpareGetsAddedToPreviousFrame()
        {
            _game.Roll(7);
            _game.Roll(3);

            _game.Roll(4);
            _game.Roll(2);

            Assert.AreEqual(20, _game.Score);
            Assert.AreEqual(false, _game.IsGameDone);
        }

        [TestMethod]
        public void TwoThrowsAfterStrikeGetsAddedToPreviousFrame()
        {
            _game.Roll(10);

            _game.Roll(10);

            _game.Roll(4);
            _game.Roll(2);

            Assert.AreEqual(46, _game.Score);
            Assert.AreEqual(false, _game.IsGameDone);
        }

        [TestMethod]
        public void TwoThrowsAfterStrikeGetsAddedToPreviousFrame2()
        {
            _game.Roll(10);

            _game.Roll(10); 

            _game.Roll(2);
            _game.Roll(2); 
                            
            _game.Roll(10);

            _game.Roll(10);

            _game.Roll(4);
            _game.Roll(2);

            Assert.AreEqual(86, _game.Score);
            Assert.AreEqual(false, _game.IsGameDone);
        }


        [TestMethod]
        public void OneExtraThrowsAfter10thFrameIsSpare()
        {
            _game.Roll(10);

            _game.Roll(1);
            _game.Roll(7);

            _game.Roll(3);
            _game.Roll(4);

            _game.Roll(10);

            _game.Roll(1);
            _game.Roll(0);

            _game.Roll(0);
            _game.Roll(9);

            _game.Roll(2);
            _game.Roll(0);

            _game.Roll(0);
            _game.Roll(0);

            _game.Roll(6);
            _game.Roll(4);

            _game.Roll(1);
            _game.Roll(9);

            //10th frame bonus rolls roll
            _game.Roll(4);

            Assert.AreEqual(81, _game.Score);
            Assert.AreEqual(true, _game.IsGameDone);
        }

        [TestMethod]
        public void TwoExtraThrowsAfter10thFrameIsStrike()
        {
            _game.Roll(10);

            _game.Roll(1);
            _game.Roll(7);

            _game.Roll(3);
            _game.Roll(4);

            _game.Roll(10);

            _game.Roll(1);
            _game.Roll(0);

            _game.Roll(0);
            _game.Roll(9);

            _game.Roll(2);
            _game.Roll(0);

            _game.Roll(0);
            _game.Roll(0);

            _game.Roll(6);
            _game.Roll(4);

            _game.Roll(10);

            //10th frame bonus rolls rolls
            _game.Roll(4);
            _game.Roll(2);

            Assert.AreEqual(92, _game.Score);
            Assert.AreEqual(true, _game.IsGameDone);
        }

        [TestMethod]
        public void GutterBallGameScoreShouldBe0()
        {
            _game.Roll(0);
            _game.Roll(0);

            _game.Roll(0);
            _game.Roll(0);

            _game.Roll(0);
            _game.Roll(0);

            _game.Roll(0);
            _game.Roll(0);

            _game.Roll(0);
            _game.Roll(0);

            _game.Roll(0);
            _game.Roll(0);

            _game.Roll(0);
            _game.Roll(0);

            _game.Roll(0);
            _game.Roll(0);

            _game.Roll(0);
            _game.Roll(0);

            _game.Roll(0);
            _game.Roll(0);

            Assert.AreEqual(0, _game.Score);
            Assert.AreEqual(true, _game.IsGameDone);
        }

        [TestMethod]
        public void PerfectGameShouldGive300()
        {
            _game.Roll(10);

            _game.Roll(10); 

            _game.Roll(10);

            _game.Roll(10);

            _game.Roll(10);

            _game.Roll(10);

            _game.Roll(10);

            _game.Roll(10);

            _game.Roll(10);

            _game.Roll(10);

            //10th frame bonus rolls rolls
            _game.Roll(10);
            _game.Roll(10);

            Assert.AreEqual(300, _game.Score);
            Assert.AreEqual(true, _game.IsGameDone);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFrameException))]
        public void MoreThan10PinsOnFirstRollOfFramShouldThrowException()
        {
            _game.Roll(11);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFrameException))]
        public void MoreThan10PinsCombinedOnBothRollsOfFramShouldThrowException()
        {
            _game.Roll(9);
            _game.Roll(2);
        }
    }
}