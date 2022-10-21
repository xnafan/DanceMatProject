using static DanceMatClassLibrary.DanceMat;

namespace DanceMatClassLibrary
{
    public class DanceMatEventArgs : EventArgs
    {
        public DanceMatButton Button { get; set; }
        public DanceMatButtonAction Action { get; set; }

        public DanceMatEventArgs(DanceMatButton button, DanceMatButtonAction action)
        {
            Button = button;
            Action = action;
        }
        public override string ToString()
        {
            return $"{Button} {Action}";
        }
    }
}