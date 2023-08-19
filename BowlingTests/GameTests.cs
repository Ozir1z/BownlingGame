using Bowling;

namespace BowlingTests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void RollBall()
        {
            var game = new Game();
            game.Roll(1);
        }
    }
}