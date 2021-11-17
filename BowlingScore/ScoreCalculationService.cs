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

            switch (numberPins)
            {
                default:
                    var num = int.Parse(numberPins);
                    _currentFrame.FirstThrow = num;
                    break;
            }

            _isFirstThrow = !_isFirstThrow;
        }

        private enum ThrowResultType
        {
            Common
        }
    }
}