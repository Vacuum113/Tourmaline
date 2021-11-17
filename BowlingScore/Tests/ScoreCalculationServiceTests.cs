using NUnit.Framework;

namespace BowlingScore.Tests
{
    [TestFixture]
    public class ScoreCalculationTests
    {
        private Score _score;
        private ScoreCalculationService _scoreCalculationService;
        
        [SetUp]
        public void Setup()
        {
            _score = new Score();
            _scoreCalculationService = new ScoreCalculationService(_score);
        }

        [Test]
        public void Calculation_One_Throw_Returns_One_Frame()
        {
            var throws = new [] {"9"};
            CalculationManyThrows(throws);

            Assert.AreEqual(1, _score.Frames.Count);
        }
        
        [Test]
        public void Calculation_Two_Throw_Returns_One_Frame()
        {
            var throws = new [] {"9", "2"};
            CalculationManyThrows(throws);

            Assert.AreEqual(1, _score.Frames.Count);
        }

        private void CalculationManyThrows(string[] throws)
        {
            foreach (var thr in throws)
            {
                _scoreCalculationService.CalculateThrow(thr);
            }
        }
    }
}