namespace DanceMatClassLibrary
{
    public interface IDanceMat
    {
        event EventHandler<DanceMatEventArgs>? ButtonStateChanged;

        DanceMatState GetCurrentState();
    }
}