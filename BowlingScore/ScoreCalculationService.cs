using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BowlingScore
{
    public class ScoreCalculationService
    {
        private const string Strike = "X";
        private const string Spare = "/";
        private const int MaxPoints = 10;

        private readonly List<Frame> _frames = new();
        private readonly List<Throw> _throws = new();

        private FrameState _frameState = FrameState.FirstThrow;

        private Frame _currentFrame;
        private int _pointsInCurrentThrow;
            
        public void CalculateThrow(string numberPins)
        {
            ParseCurrentPoints(numberPins);
            
            switch (_frameState)
            {
                case FrameState.FirstThrow:
                    ProcessingFirstThrowInFrame();
                    break;
                case FrameState.SecondThrow:
                    ProcessingSecondThrowInFrame();
                    break;
                case FrameState.ThirdThrow:
                    ProcessingThirdThrowInFrame();
                    break;
            }
        }
        
        public Score GetCurrentScore() => new(_frames);
        
        

        private void ParseCurrentPoints(string numberPins)
        {
            if (!CheckForLegalThrowInput(numberPins))
                throw new Exception($"Input is invalid, input: {numberPins}");

            _pointsInCurrentThrow = numberPins switch
            {
                Strike => MaxPoints,
                Spare => MaxPoints - _throws[^1].Value,
                _ => int.Parse(numberPins)
            };
        }

        private void ProcessingFirstThrowInFrame()
        {
            _currentFrame = new Frame();
            _frames.Add(_currentFrame);

            AddNewThrowInScore();
            
            if (_throws.Count > 1)
            {
                var prevThrow = _throws[^2];
                
                CheckSpare(prevThrow);
                CheckStrike(prevThrow);
            }

            if (_throws.Count > 2)
            {
                var prevPrevThrow = _throws[^3];
                CheckStrike(prevPrevThrow);
            }

            if (!_currentFrame.Strike || _frames.Count == 10)
                _frameState = FrameState.SecondThrow;
        }
        
        private void ProcessingSecondThrowInFrame()
        {
            AddNewThrowInScore();

            if (_currentFrame.Total > 10 && _frames.Count != 10 && !_currentFrame.Strike)
                throw new Exception("Two throws cannot be more than 10");
                
            if (_throws.Count > 2)
            {
                var prevPrevThrow = _throws[^3];
                CheckStrike(prevPrevThrow);
            }
            
            _frameState = _frames.Count < 10 ? FrameState.FirstThrow : FrameState.ThirdThrow;
        }

        private void ProcessingThirdThrowInFrame()
        {
            if (!_currentFrame.Strike && !_currentFrame.Spare)
                throw new Exception("The third throw is allowed only if you have a strike or spare in the last frame.");

            AddNewThrowInScore();
        }
        
        private void AddNewThrowInScore()
        {
            var newThrow = new Throw(_pointsInCurrentThrow, _currentFrame);
            
            _throws.Add(newThrow);
            _currentFrame.Throws.Add(newThrow);
        }

        private void CheckStrike(Throw @throw)
        {
            if (@throw.Frame.Strike)
                @throw.Frame.SetAdditionalPoints(_pointsInCurrentThrow);
        }
        
        private void CheckSpare(Throw @throw)
        {
            if (@throw.Frame.Spare)
                @throw.Frame.SetAdditionalPoints(_pointsInCurrentThrow);
        }

        private static bool CheckForLegalThrowInput(string input) =>
            Regex.IsMatch(input, @"^([0-9]|X|/)$");
    }

    public enum FrameState
    {
        FirstThrow,
        SecondThrow,
        ThirdThrow
    }
}