//using static DanceMatClassLibrary.DanceMatKeyboardEmulator;

namespace DanceMatClassLibrary
{

    /// <summary>
    /// This class represents an event from the dance mat.
    /// It stores which "Button" was involved, and the action (Pressed/Released).
    /// </summary>
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