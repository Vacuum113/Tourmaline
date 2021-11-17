using System.Collections.Generic;
using System.Linq;

namespace BowlingScore
{
    public class Score
    {
        public List<Frame> Frames { get; } = new ();
        public int Total => Frames.Sum(f => f.Total);
    }
}