using System.Collections.Generic;

namespace BowlingScore
{
    public class ScoreCalculationService
    {
        private readonly Score _score;
        private Frame _currentFrame;
        private bool _isFirstThrow;

        private const string Strike = "X";
        private const int MaxPoints = 10;
        
        private const string Spare = "/";

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

            if (_currentFrame.FirstThrow != MaxPoints)
                _isFirstThrow = !_isFirstThrow;
        }

        private void CalculateSecondThrow(string numberPins)
        {
            if (numberPins == Spare)
                _currentFrame.SecondThrow = MaxPoints - _currentFrame.FirstThrow;
            else
                CalculateCommonNumberPins(numberPins);
        }

        private void CalculateFirstThrow(string numberPins)
        {
            switch (numberPins)
            {
                case Strike:
                    _currentFrame.FirstThrow = MaxPoints;
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