namespace BowlingScore
{
    public class Frame
    {
        public int FirstThrow { get; set; }
        public int Total => FirstThrow + SecondThrow;
        public int SecondThrow { get; set; }
    }
}