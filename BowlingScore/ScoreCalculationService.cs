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
        
        public void CalculateThrow(string s)
        {
            if (_isFirstThrow)
                _score.Frames.Add(_currentFrame);

            _isFirstThrow = !_isFirstThrow;
        }

        private enum ThrowResultType
        {
            Common
        }
    }
}