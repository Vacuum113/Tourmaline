using NUnit.Framework;

namespace BowlingScore.Tests
{
    public class ScoreCalculationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Calculation_One_Throw_Returns_One_Frame()
        {
            var scoreCalculationService = new ScoreCalculationService();

            scoreCalculationService.CalculateThrow("9");

            Assert.AreEqual(1, scoreCalculationService.Score.Frames.Length);
        }
    }
}