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
            if (CurrentFrame == null)
                CurrentFrame = new Frame();
            
            Score.Frames.Add(CurrentFrame);
        }
    }
}