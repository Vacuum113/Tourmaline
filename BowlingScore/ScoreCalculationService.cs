using System.Collections.Generic;
using System.Linq;

namespace BowlingScore
{
    public class ScoreCalculationService
    {
        private readonly Score _score;
        private Frame _currentFrame;
        private bool _isFirstThrow;

        private const string Strike = "X";
        private const int MaxPoints = 10;
        
        private const string Spare = "/";

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
            
            InternalCalculateThrow(numberPins);

            UpdateStateAfterCalculation();

            UpdateStateOnStrike();
        }

        private void UpdateStateOnStrike()
        {
            if (_currentFrame.Strike)
            {
                _currentFrame = new Frame();
                _isFirstThrow = true;
            }
        }

        private void UpdateStateAfterCalculation()
        {
            if (!_isFirstThrow)
                _currentFrame = new Frame();

            _isFirstThrow = !_isFirstThrow;
        }

        private void InternalCalculateThrow(string numberPins)
        {
            var points = numberPins switch
            {
                Strike => MaxPoints,
                Spare => MaxPoints - _currentFrame.Throws.First(),
                _ => CalculateCommonNumberPins(numberPins)
            };
            
            _currentFrame.Throws.Add(points);
            
            RecalculatePreviousFrame(points);
        }

        private void RecalculatePreviousFrame(int points)
        {
            var isPreviousFrameHasStrikeOrSpare = _score.Frames.Count > 1 && (_score.Frames[^2].Strike || _score.Frames[^2].Spare);
            
            if (isPreviousFrameHasStrikeOrSpare)
                _score.Frames[^2].AdditionalPoints += points;
        }
        

        private int CalculateCommonNumberPins(string numberPins)
        {
            return int.Parse(numberPins);
        }
    }
}