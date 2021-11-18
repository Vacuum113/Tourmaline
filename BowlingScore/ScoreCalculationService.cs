using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingScore
{
    public class ScoreCalculationService
    {
        private List<int> _throws = new List<int>();

        private const string Strike = "X";
        private const string Spare = "/";

        private const int MaxPoints = 10;

        public void CalculateThrow(string numberPins)
        {
            var points = numberPins switch
            {
                Strike => MaxPoints,
                Spare => MaxPoints - _throws[^1],
                _ => CalculateCommonNumberPins(numberPins)
            };

            _throws.Add(points);
        }

        public Score Score()
        {
            var score = new Score();
            var isFirstThrow = true;
            
            for (var i = 0; i < _throws.Count; i++)
            {
                switch (_throws[i])
                {
                    case MaxPoints:
                        CalculationStrike(score, i);
                        isFirstThrow = true;
                        break;
                    
                    default:
                        CalculationCommonThrow(score, isFirstThrow, i);
                        isFirstThrow = !isFirstThrow;
                        break;
                }
            }

            return score;
        }

        private void CalculationStrike(Score score, int numberThrow)
        {
            var isLastFrame = score.Frames.Count == 10;
            var frame = isLastFrame ? score.Frames[^1] : new Frame();
            
            frame.Throws.Add(_throws[numberThrow]);
            
            if (!isLastFrame)
                score.Frames.Add(frame);

            if (score.Frames.Count == 10) 
                return;
            
            if (numberThrow + 1 < _throws.Count)
                frame.SetAdditionalPoints(_throws[numberThrow + 1]);
                            
            if (numberThrow + 2 < _throws.Count)
                frame.SetAdditionalPoints(_throws[numberThrow + 2]);
        }
        
        private void CalculationCommonThrow(Score score, bool isFirstThrow, int numberThrow)
        {
            var frame = isFirstThrow ? new Frame() : score.Frames[^1];
                        
            frame.Throws.Add(_throws[numberThrow]);
            if (!isFirstThrow) 
                return;
            
            var isSpareAndNotLastFrame = numberThrow + 2 < _throws.Count && 
                                         _throws[numberThrow] + _throws[numberThrow + 1] == MaxPoints && 
                                         score.Frames.Count < 9;
                            
            if (isSpareAndNotLastFrame)
                frame.SetAdditionalPoints(_throws[numberThrow + 2]);

            score.Frames.Add(frame);
        }
        

        private int CalculateCommonNumberPins(string numberPins)
        {
            return int.Parse(numberPins);
        }
    }
}