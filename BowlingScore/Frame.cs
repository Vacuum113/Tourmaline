using System.Collections.Generic;
using System.Linq;

namespace BowlingScore
{
    public class Frame
    {
        public readonly List<Throw> Throws = new();
        public int Total => Throws.Sum(t => t.Value) + AdditionalPoints;
        private int AdditionalPoints { get; set; }

        public bool Strike => Throws.Any() && Throws.First().Value == 10;
        public bool Spare => Throws.Count == 2 && Throws.Sum(t => t.Value) == 10;
        
        public void SetAdditionalPoints(int points) => AdditionalPoints += points;

    }
}