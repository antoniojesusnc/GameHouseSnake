using GameHouse.Snake.Services;
using NUnit.Framework;

namespace GameHouse.Snake.Tests
{
    public class TestScoreService
    {
        private IScoreService _scoreService;
        
        [SetUp]
        public void SetUp()
        {
            _scoreService = new ScoreService();
        }

        [Test]
        public void Score_HasToAddScore()
        {
            var previousScore = _scoreService.GetScore();
            
            _scoreService.AddScore();
            
            Assert.That(_scoreService.GetScore(), Is.GreaterThan(previousScore));
        }
    }
}
