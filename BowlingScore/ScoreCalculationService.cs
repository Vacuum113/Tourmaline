using System.Collections.Generic;

namespace BowlingScore
{
    public class ScoreCalculationService
    {
        public Score Score { get; }

        private Frame CurrentFrame { get; set; }

        public ScoreCalculationService()
        {
            Score = new Score();
        }
        
        public void CalculateThrow(string s)
        {
            if (CurrentFrame == null)
                CurrentFrame = new Frame();
            
            Score.Frames.Add(CurrentFrame);
        }
    }
}