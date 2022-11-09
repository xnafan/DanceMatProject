namespace DanceMatClassLibrary;

public abstract class DanceMatBase : IDanceMat
{
    #region variables and properties
    protected Dictionary<DanceMatButton, bool> _buttonStates = new();
    public event EventHandler<DanceMatEventArgs>? ButtonStateChanged;
    #endregion


    public DanceMatBase()
    {
        //initialize the button states dictionary with all buttons
        Enum.GetValues<DanceMatButton>().ToList().ForEach(button => _buttonStates.Add(button, false));
    }

    #region Public methods
    public DanceMatState GetCurrentState()
    {
        return new DanceMatState()
        {
            Circle = _buttonStates[DanceMatButton.Circle],
            Square = _buttonStates[DanceMatButton.Square],
            Triangle = _buttonStates[DanceMatButton.Triangle],
            Cross = _buttonStates[DanceMatButton.Cross],
            Start = _buttonStates[DanceMatButton.Start],
            Select = _buttonStates[DanceMatButton.Select],
            Up = _buttonStates[DanceMatButton.Up],
            Down = _buttonStates[DanceMatButton.Down],
            Left = _buttonStates[DanceMatButton.Left],
            Right = _buttonStates[DanceMatButton.Right]
        };
    }
    #endregion

    #region Events
    protected void OnButtonStateChanged(DanceMatButton button, DanceMatButtonAction action)
    {
        _buttonStates[button] = action == DanceMatButtonAction.Pressed;
        ButtonStateChanged?.Invoke(this, new DanceMatEventArgs(button, action));
    } 
    #endregion

}