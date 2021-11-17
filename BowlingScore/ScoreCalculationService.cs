using System;
using System.Linq;

namespace BowlingScore
{
    public class ScoreCalculationService
    {
        private readonly Score _score;
        private Frame _currentFrame;
        private bool _isFirstThrow;
        
        private bool IsLastFrame => _score.Frames.Count == 10;
        
        private const string Strike = "X";
        private const string Spare = "/";

        private const int MaxPoints = 10;
        

        public ScoreCalculationService(Score score)
        {
            _score = score;
            _currentFrame = new Frame();
            _isFirstThrow = true;
        }
        
        public void CalculateThrow(string numberPins)
        {
            if (_isFirstThrow && !IsLastFrame)
                _score.Frames.Add(_currentFrame);
            
            InternalCalculateThrow(numberPins);

            UpdateStateAfterCalculation();

            UpdateStateOnStrike();
        }

        private void UpdateStateOnStrike()
        {
            if (_currentFrame.Strike && !IsLastFrame)
            {
                _currentFrame = new Frame();
                _isFirstThrow = true;
            }
        }

        private void UpdateStateAfterCalculation()
        {
            if (!_isFirstThrow && !IsLastFrame)
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
            
            RecalculatePreviousFrames(points);
        }

        private void RecalculatePreviousFrames(int points)
        {
            var isPreviousFrameHasStrike = _score.Frames.Count > 1 && _score.Frames[^2].Strike;
            var isPreviousFrameHasSpare = _score.Frames.Count > 1 && _score.Frames[^2].Spare;

            if (isPreviousFrameHasStrike)
            {
                if (_score.Frames[^1].Throws.Count != 3)
                    _score.Frames[^2].SetAdditionalPoints(points);
                
                if (_score.Frames.Count > 2 && _score.Frames[^3].Strike && _currentFrame.Throws.Count == 1)
                    _score.Frames[^3].SetAdditionalPoints(points);
            }

            if (isPreviousFrameHasSpare && _isFirstThrow && _score.Frames[^1].Throws.Count != 3)
                _score.Frames[^2].SetAdditionalPoints(points);
        }
        

        private int CalculateCommonNumberPins(string numberPins)
        {
            return int.Parse(numberPins);
        }
    }
}