namespace DanceMatMazeGame.components
{
    public class DanceMoveSuccesRate
    {
        public enum Precision {Exact = 5, Close = 3, Acceptable = 1, Unacceptable = -3}
        public DanceMove DanceMove { get; set; }
        public Precision TimingPrecision { get; set; }

        public DanceMoveSuccesRate(DanceMove danceMove, Precision timingPrecision)
        {
            DanceMove = danceMove;
            TimingPrecision = timingPrecision;
        }
    }
}