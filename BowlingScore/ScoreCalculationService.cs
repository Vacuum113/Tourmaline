using System.Collections.Generic;

namespace BowlingScore
{
    public class ScoreCalculationService
    {
        private readonly Score _score;
        private Frame _currentFrame;
        private bool _isFirstThrow;

        private const string Strike = "X";
        private const int PointsForStrike = 10;
        
        private const string Spare = "/";
        private const int PointsForSpare = 10;

        public ScoreCalculationService(Score score)
        {
            _score = score;
            _currentFrame = new Frame();
            _isFirstThrow = true;
        }
        
        public void CalculateThrow(string numberPins)
        {
            if (_isFirstThrow)
            {
                _score.Frames.Add(_currentFrame);
                CalculateFirstThrow(numberPins);
            }
            else
                CalculateSecondThrow(numberPins);

            if (_currentFrame.FirstThrow != PointsForStrike)
                _isFirstThrow = !_isFirstThrow;
        }

        private void CalculateSecondThrow(string numberPins)
        {
            if (numberPins == Spare)
            {
                    
            }
            else
                CalculateCommonNumberPins(numberPins);
        }

        private void CalculateFirstThrow(string numberPins)
        {
            switch (numberPins)
            {
                case Strike:
                    _currentFrame.FirstThrow = PointsForStrike;
                    break;
                default:
                    CalculateCommonNumberPins(numberPins);
                    break;
            }
        }

        private void CalculateCommonNumberPins(string numberPins)
        {
            var num = int.Parse(numberPins);
            
            if (_isFirstThrow)
                _currentFrame.FirstThrow = num;
            else
                _currentFrame.SecondThrow = num;
        }

        private enum ThrowResultType
        {
            Common
        }
    }
}