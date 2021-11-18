using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingScore
{
    public class ScoreCalculationService
    {
        private readonly Score _score;
        private Frame _currentFrame;
        private bool _isFirstThrow;

        private List<int> _frames = new List<int>();
        private List<int> _throws = new List<int>();
        private int currentThrow = 0;
        
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
            var frameCount = 0;
            for (int i = 0; i < _throws.Count; i++)
            {
                Frame frame;
                switch (_throws[i])
                {
                    case 10:
                        frame = score.Frames.Count == 10 ? score.Frames[^1] : new Frame();
                        
                        if (i + 1 < _throws.Count && score.Frames.Count < 9)
                            frame.SetAdditionalPoints(_throws[i + 1]);
                        
                        if (i + 2 < _throws.Count && score.Frames.Count < 9)
                            frame.SetAdditionalPoints(_throws[i + 2]);

                        frame.Throws.Add(_throws[i]);
                        if (score.Frames.Count != 10)
                        {
                            score.Frames.Add(frame);
                            frameCount++;
                        }

                        isFirstThrow = !_isFirstThrow;
                        break;
                    default:
                        if (score.Frames.Count == 10)
                            isFirstThrow = false;
                        
                        frame = !isFirstThrow ? score.Frames[^1] : new Frame();
                        
                        frame.Throws.Add(_throws[i]);
                        if (isFirstThrow)
                        {
                            if (i + 2 < _throws.Count && _throws[i] + _throws[i + 1] == 10 && score.Frames.Count != 10)
                            {
                                frame.SetAdditionalPoints(_throws[i + 2]);
                            }
                            frameCount++;
                            score.Frames.Add(frame);
                        }
                        break;
                }

                isFirstThrow = !isFirstThrow;
            }

            return score;
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