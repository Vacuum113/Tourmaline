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

        [Test]
        public void Calculation_Seven_Points_Returns_Seven_Points_In_Frame()
        {
            var throws = new [] {"7"};
            CalculationManyThrows(throws);

            Assert.AreEqual(7, _score.Frames[0].Total);
        }
        
        [Test]
        public void Calculation_Two_Throws_Returns_Sum_of_Pins_Knocked_Down_In_Frame()
        {
            var throws = new [] {"7", "2"};
            CalculationManyThrows(throws);

            Assert.AreEqual(9, _score.Frames[0].Total);
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