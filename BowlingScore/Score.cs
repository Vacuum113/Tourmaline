using System.Collections.Generic;
using System.Linq;

namespace BowlingScore
{
    public class Score
    {
        public Score(List<Frame> frames)
        {
            Frames = frames;
        }

        public List<Frame> Frames { get; }
        public int Total => Frames.Sum(f => f.Total);
    }
}