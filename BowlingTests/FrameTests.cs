using Bowling;

namespace BowlingTests
{
    [TestClass]
    public class FrameTests
    {
        private Frame _frame;

        [TestInitialize]
        public void SetupGame()
        {
            _frame = new Frame();
        }

        [TestMethod]
        public void RollingOnceLessThan10ShouldntCompleteFrame()
        {
            _frame.Roll(1);

            Assert.AreEqual(false, _frame.IsSpare);
            Assert.AreEqual(false, _frame.IsStrike);
            Assert.AreEqual(false, _frame.IsFrameComplete);
            Assert.AreEqual(false, _frame.IsFrameCompleteWithBonusScores);
            Assert.AreEqual(1, _frame.FrameScore);
        }


        [TestMethod]
        public void OpenFrameResultsIntoScore()
        {
            _frame.Roll(3);
            _frame.Roll(1);

            Assert.AreEqual(false, _frame.IsSpare);
            Assert.AreEqual(false, _frame.IsStrike);
            Assert.AreEqual(true, _frame.IsFrameComplete);
            Assert.AreEqual(true, _frame.IsFrameCompleteWithBonusScores);
            Assert.AreEqual(4, _frame.FrameScore);
        }

        [TestMethod]
        public void BothRollsOFframeCombine10ShouldBeASpare() 
        {
            _frame.Roll(3);
            _frame.Roll(7);

            Assert.AreEqual(true, _frame.IsSpare);
            Assert.AreEqual(false, _frame.IsStrike);
            Assert.AreEqual(true, _frame.IsFrameComplete);
            Assert.AreEqual(false, _frame.IsFrameCompleteWithBonusScores);
            Assert.AreEqual(10, _frame.FrameScore);
        }

        [TestMethod]
        public void FirstRollOFframeIs10ShouldBeAStrike()
        {
            _frame.Roll(10);

            Assert.AreEqual(true, _frame.IsStrike);
            Assert.AreEqual(false, _frame.IsSpare);
            Assert.AreEqual(true, _frame.IsFrameComplete);
            Assert.AreEqual(false, _frame.IsFrameCompleteWithBonusScores);
            Assert.AreEqual(10, _frame.FrameScore);
        }

        [TestMethod]
        public void RollingMoreThan2TimesShouldntIncreaseScore()
        {
            _frame.Roll(2);
            _frame.Roll(2);
            _frame.Roll(2);

            Assert.AreEqual(4, _frame.FrameScore);
        }

        [TestMethod]
        public void ASpareShouldGiveOneBonusRoll()
        {
            _frame.Roll(2);
            _frame.Roll(8);
            _frame.BonusRoll(3);

            Assert.AreEqual(true, _frame.IsFrameComplete);
            Assert.AreEqual(true, _frame.IsFrameCompleteWithBonusScores);
            Assert.AreEqual(13, _frame.FrameScore);
        }

        [TestMethod]
        public void AStrikeShouldGiveTwoBonusRolls()
        {
            _frame.Roll(10);
            _frame.BonusRoll(3);
            _frame.BonusRoll(4);

            Assert.AreEqual(true, _frame.IsFrameComplete);
            Assert.AreEqual(true, _frame.IsFrameCompleteWithBonusScores);
            Assert.AreEqual(17, _frame.FrameScore);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFrameException))]
        public void MoreThan10PinsOnFirstRollOfFramShouldThrowException()
        {
            _frame.Roll(11);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidFrameException))]
        public void MoreThan10PinsCombinedOnBothRollsOfFramShouldThrowException()
        {
            _frame.Roll(9);
            _frame.Roll(2);
        }
    }
}