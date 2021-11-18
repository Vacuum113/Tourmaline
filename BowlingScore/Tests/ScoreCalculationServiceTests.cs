using NUnit.Framework;

namespace BowlingScore.Tests
{
    [TestFixture]
    public class ScoreCalculationTests
    {
        private ScoreCalculationService _scoreCalculationService;
        
        [SetUp]
        public void Setup()
        {
            _scoreCalculationService = new ScoreCalculationService();
        }

        [Test]
        public void Calculation_One_Throw_Returns_One_Frame()
        {
            var throws = new [] {"9"};
            CalculationManyThrows(throws);

            Assert.AreEqual(1, _scoreCalculationService.Score().Frames.Count);
        }
        
        [Test]
        public void Calculation_Two_Throw_Returns_One_Frame()
        {
            var throws = new [] {"9", "2"};
            CalculationManyThrows(throws);

            Assert.AreEqual(1, _scoreCalculationService.Score().Frames.Count);
        }

        [Test]
        public void Calculation_Seven_Points_Returns_Seven_Points_In_Frame()
        {
            var throws = new [] {"7"};
            CalculationManyThrows(throws);

            Assert.AreEqual(7, _scoreCalculationService.Score().Frames[0].Total);
        }
        
        [Test]
        public void Calculation_Two_Throws_Returns_Sum_of_Pins_Knocked_Down_In_Frame()
        {
            var throws = new [] {"7", "2"};
            CalculationManyThrows(throws);

            Assert.AreEqual(9, _scoreCalculationService.Score().Frames[0].Total);
        }
        
        [Test]
        public void Calculation_Strike_Should_Returns_Ten_Points_In_Frame()
        {
            var throws = new [] {"X"};
            CalculationManyThrows(throws);

            Assert.AreEqual(10, _scoreCalculationService.Score().Frames[0].Total);
        }
        
        [Test]
        public void Calculation_Three_Strikes_Should_Returns_60_Points_In_Frame()
        {
            var throws = new [] {"X", "X", "X"};
            CalculationManyThrows(throws);

            Assert.AreEqual(60, _scoreCalculationService.Score().Total);
        }
        
        [Test]
        public void Strike_Should_Close_Scoring_In_Frame()
        {
            var throws = new [] {"X", "2"};
            CalculationManyThrows(throws);

            Assert.AreEqual(2, _scoreCalculationService.Score().Frames.Count);
        }
        
        [Test]
        public void Calculation_Spare_Should_Returns_Ten_Points_In_Frame()
        {
            var throws = new [] {"7", "/"};
            CalculationManyThrows(throws);

            Assert.AreEqual(10, _scoreCalculationService.Score().Frames[0].Total);
        }
        
        [Test]
        public void Calculation_Total_Score_In_Two_Frames()
        {
            var throws = new [] {"5", "2", "2", "2"};
            CalculationManyThrows(throws);

            Assert.AreEqual(11, _scoreCalculationService.Score().Total);
        }
        
        [Test]
        public void Points_For_Next_Two_Throws_After_Strike_Doubled()
        {
            var throws = new [] {"X", "2", "5"};
            CalculationManyThrows(throws);

            Assert.AreEqual(17, _scoreCalculationService.Score().Frames[0].Total);
            Assert.AreEqual(24, _scoreCalculationService.Score().Total);
        }
        
        [Test]
        public void Points_For_Next_One_Throws_After_Spare_Doubled()
        {
            var throws = new [] {"5", "/", "5", "2"};
            CalculationManyThrows(throws);

            Assert.AreEqual(15, _scoreCalculationService.Score().Frames[0].Total);
            Assert.AreEqual(22, _scoreCalculationService.Score().Total);
        }
        
        [Test]
        public void Calculation_Max_Possible_Result()
        {
            var throws = new [] {"X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X", "X"};
            CalculationManyThrows(throws);

            Assert.AreEqual(300, _scoreCalculationService.Score().Total);
        }
        
        [Test]
        public void General_Test()
        {
            var throws = new [] {"X", "7", "/", "7", "2", "9", "/", "X", "X", "X", "2", "3", "6", "/", "7", "/", "3"};
            CalculationManyThrows(throws);

            Assert.AreEqual(168, _scoreCalculationService.Score().Total);
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