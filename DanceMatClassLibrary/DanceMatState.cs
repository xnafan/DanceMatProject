namespace DanceMatClassLibrary
{
    public struct DanceMatState
    {
        #region Properties
        public bool Start { get; set; }
        public bool Select { get; set; }
        public bool Circle { get; set; }
        public bool Cross { get; set; }
        public bool Square { get; set; }
        public bool Triangle { get; set; }
        public bool Up { get; set; }
        public bool Left { get; set; }
        public bool Down { get; set; }
        public bool Right { get; set; }

        public bool NothingPressed { get { return !(Start || Select || Circle || Cross || Square || Triangle || Up || Left || Down || Right); } }
        #endregion

        public override string ToString()
        {
            return $"DanceMatState: [Up:{Up},Down:{Down},Left:{Left},Right:{Right},Cross:{Cross},Circle:{Circle},Triangle:{Triangle},Square:{Square},Select:{Select},Start:{Start}]";
        }
    }
}