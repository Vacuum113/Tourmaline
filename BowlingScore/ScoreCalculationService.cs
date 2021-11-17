using System.Collections.Generic;

namespace BowlingScore
{
    public class ScoreCalculationService
    {
        private readonly Score _score;
        private Frame _currentFrame;
        private bool _isFirstThrow;
        
        public ScoreCalculationService(Score score)
        {
            _score = score;
            _currentFrame = new Frame();
            _isFirstThrow = true;
        }
        
        public void CalculateThrow(string numberPins)
        {
            if (_isFirstThrow)
                _score.Frames.Add(_currentFrame);

            if (_isFirstThrow)
                CalculateFirstThrow(numberPins);

        private void CalculateFirstThrow(string numberPins)
        {
            switch (numberPins)
            {
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
        }

        private enum ThrowResultType
        {
            Common
        }
    }
}