namespace DanceMatMazeGame.components
{
    public class DanceMoveSuccesRate
    {
        public enum Precision {Perfect = 5, Close = 3, Acceptable = 1, Bad = -3, Lost = -5}
        public DanceMove DanceMove { get; set; }
        public Precision TimingPrecision { get; set; }

        public DanceMoveSuccesRate(DanceMove danceMove, Precision timingPrecision)
        {
            DanceMove = danceMove;
            TimingPrecision = timingPrecision;
        }
    }
}