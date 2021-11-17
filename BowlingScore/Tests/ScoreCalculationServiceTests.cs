using NUnit.Framework;

namespace BowlingScore.Tests
{
    public class ScoreCalculationTests
    {
        private Score _score = new Score();
        private ScoreCalculationService _scoreCalculationService;
        
        [SetUp]
        public void Setup()
        {
            _scoreCalculationService = new ScoreCalculationService(_score);
        }

        [Test]
        public void Calculation_One_Throw_Returns_One_Frame()
        {
            _scoreCalculationService.CalculateThrow("9");

            Assert.AreEqual(1, _score.Frames.Count);
        }
        
        [Test]
        public void Calculation_Two_Throw_Returns_One_Frame()
        {
            _scoreCalculationService.CalculateThrow("9");
            _scoreCalculationService.CalculateThrow("2");

            Assert.AreEqual(1, _score.Frames.Count);
        }
    }
}