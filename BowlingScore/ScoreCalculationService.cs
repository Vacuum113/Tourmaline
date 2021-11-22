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
            
            switch (numberPins)
            {
                case Strike:
                    _pointsInCurrentThrow = MaxPoints;
                    break;
                case Spare:
                    _pointsInCurrentThrow = MaxPoints - _throws[^1].Value;
                    break;
                default:
                    _pointsInCurrentThrow = int.Parse(numberPins);
                    break;
            }
        }

        private void ProcessingFirstThrowInFrame()
        {
            var frame = new Frame();
            _frames.Add(frame);
            _currentFrame = frame;
            
            var newThrow = new Throw(_pointsInCurrentThrow, frame);
            _throws.Add(newThrow);
            
            frame.Throws.Add(newThrow);

            if (_throws.Count > 1)
            {
                var prevThrow = _throws[^2];
                if (prevThrow.Frame.Spare)
                    prevThrow.Frame.SetAdditionalPoints(_pointsInCurrentThrow);
                
                if (prevThrow.Frame.Strike)
                    prevThrow.Frame.SetAdditionalPoints(_pointsInCurrentThrow);
            }

            if (_throws.Count > 2)
            {
                var prevPrevThrow = _throws[^3];
                if (prevPrevThrow.Frame.Strike)
                    prevPrevThrow.Frame.SetAdditionalPoints(_pointsInCurrentThrow);
            }

            if (!(_currentFrame.Strike || _currentFrame.Strike) || _frames.Count == 10)
                _frameState = FrameState.SecondThrow;
        }
        
        private void ProcessingSecondThrowInFrame()
        {
            var newThrow = new Throw(_pointsInCurrentThrow, _currentFrame);
            _currentFrame.Throws.Add(newThrow);
            _throws.Add(newThrow);

            if (_currentFrame.Total > 10 && (_frames.Count != 10 || _currentFrame.Throws.First().Value != MaxPoints))
                throw new Exception("Two throws cannot be more than 10");
                
            if (_throws.Count > 2)
            {
                var prevThrow = _throws[^3];
                if (prevThrow.Frame.Strike)
                    prevThrow.Frame.SetAdditionalPoints(_pointsInCurrentThrow);
            }
            
            _frameState = _frames.Count < 10 ? FrameState.FirstThrow : FrameState.ThirdThrow;
        }

        private void ProcessingThirdThrowInFrame()
        {
            if (_throws[^1].Value + _throws[^2].Value != MaxPoints && _throws[^2].Value != MaxPoints)
                throw new Exception("The third throw is allowed only if you have a strike or spare in the last frame.");
            
            _currentFrame.Throws.Add(new Throw(_pointsInCurrentThrow, _currentFrame));
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