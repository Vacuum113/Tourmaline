namespace BowlingScore
{
    public class Throw
    {
        public Throw(int value, Frame frame)
        {
            Value = value;
            Frame = frame;
        }

        public int Value { get; }
        public Frame Frame { get; }
    }
}