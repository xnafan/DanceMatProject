using Dapplo.Windows.Input.Keyboard;

namespace DanceMatClassLibrary;

public class DanceMatKeyboardEmulator : DanceMatBase
{
    #region Constructor
    public DanceMatKeyboardEmulator() => KeyboardHook.KeyboardEvents.Subscribe(khea => HandleKeyboardInput(khea));

    private object HandleKeyboardInput(KeyboardHookEventArgs khea)
    {
        switch (khea.Key)
        {
            #region Up, down, left, right
            case Dapplo.Windows.Input.Enums.VirtualKeyCode.Up:
            case Dapplo.Windows.Input.Enums.VirtualKeyCode.Numpad8:
                OnButtonStateChanged(DanceMatButton.Up, khea.IsKeyDown ? DanceMatButtonAction.Pressed : DanceMatButtonAction.Released);
                break;
            case Dapplo.Windows.Input.Enums.VirtualKeyCode.Right:
            case Dapplo.Windows.Input.Enums.VirtualKeyCode.Numpad6:
                OnButtonStateChanged(DanceMatButton.Right, khea.IsKeyDown ? DanceMatButtonAction.Pressed : DanceMatButtonAction.Released);
                break;
            case Dapplo.Windows.Input.Enums.VirtualKeyCode.Down:
            case Dapplo.Windows.Input.Enums.VirtualKeyCode.Numpad2:
                OnButtonStateChanged(DanceMatButton.Down, khea.IsKeyDown ? DanceMatButtonAction.Pressed : DanceMatButtonAction.Released);
                break;
            case Dapplo.Windows.Input.Enums.VirtualKeyCode.Left:
            case Dapplo.Windows.Input.Enums.VirtualKeyCode.Numpad4:
                OnButtonStateChanged(DanceMatButton.Left, khea.IsKeyDown ? DanceMatButtonAction.Pressed : DanceMatButtonAction.Released);
                break;
            #endregion

            #region Diagonally
            case Dapplo.Windows.Input.Enums.VirtualKeyCode.Numpad7:
                OnButtonStateChanged(DanceMatButton.Cross, khea.IsKeyDown ? DanceMatButtonAction.Pressed : DanceMatButtonAction.Released);
                break;
            case Dapplo.Windows.Input.Enums.VirtualKeyCode.Numpad9:
                OnButtonStateChanged(DanceMatButton.Circle, khea.IsKeyDown ? DanceMatButtonAction.Pressed : DanceMatButtonAction.Released);
                break;
            case Dapplo.Windows.Input.Enums.VirtualKeyCode.Numpad1:
                OnButtonStateChanged(DanceMatButton.Triangle, khea.IsKeyDown ? DanceMatButtonAction.Pressed : DanceMatButtonAction.Released);
                break;
            case Dapplo.Windows.Input.Enums.VirtualKeyCode.Numpad3:
                OnButtonStateChanged(DanceMatButton.Square, khea.IsKeyDown ? DanceMatButtonAction.Pressed : DanceMatButtonAction.Released);
                break;
            #endregion

            #region Start/select
            case Dapplo.Windows.Input.Enums.VirtualKeyCode.Multiply:
                OnButtonStateChanged(DanceMatButton.Start, khea.IsKeyDown ? DanceMatButtonAction.Pressed : DanceMatButtonAction.Released);
                break;

            case Dapplo.Windows.Input.Enums.VirtualKeyCode.Divide:
                OnButtonStateChanged(DanceMatButton.Select, khea.IsKeyDown ? DanceMatButtonAction.Pressed : DanceMatButtonAction.Released);
                break;

                #endregion
        }
        return "";
    }
    #endregion
}