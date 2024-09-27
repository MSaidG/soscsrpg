using Engine.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEngine.ViewModels
{
    [TestClass]
    public class TestGameSession
    {
        [TestMethod]
        public void TestCreateGameSession()
        {
            GameSession gameSession = new GameSession();
            Assert.IsNotNull(gameSession.Player);
            Assert.AreEqual("Home",  gameSession.Location.Name);
        }
        [TestMethod]
        public void TestPlayerMovesHomeAndIsCompletelyHealedOnKilled()
        {
            GameSession gameSession = new GameSession();
            gameSession.Player.TakeDamage(999);
            Assert.AreEqual("Home", gameSession.Location.Name);
            Assert.AreEqual(gameSession.Player.Level * 10, gameSession.Player.CurrentHitPoints);
        }
    }
}
