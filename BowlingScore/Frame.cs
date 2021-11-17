using System.Collections.Generic;
using System.Linq;

namespace BowlingScore
{
    public class Frame
    {
        public List<int> Throws = new();
        public int Total => Throws.Sum(t => t) + AdditionalPoints;

        public bool Strike => Throws.Any() && Throws.First() == 10;
        public bool Spare => Throws.Count == 2 && Throws.Sum(t => t) == 10;
        private int AdditionalPoints { get; set; }

        public void SetAdditionalPoints(int points) => AdditionalPoints += points;

    }
}