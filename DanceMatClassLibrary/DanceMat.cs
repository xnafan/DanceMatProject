using HIDInterface;

namespace DanceMatClassLibrary
{
    //driver misidentification issue:
    //1. Plug in Dance Mat in a USB port
    //2. On Windows(10), go to Device Manager -> open USB Tab.There should be a one thats not working   (yellow triangle), called "Standard USB Hub".
    //3. Right click it -> Update Driver -> Browse my computer for driver software -> Let me pick from a list of device drivers on my computer -> Select "USB Input Device" -> Click OK.

    //NOTE: you may have to look under Human Interface Devices (HID) and look for 
    //"HID-Compliant game controller" -> copy VID and PID to the code below.
    public class DanceMat : IDisposable
    {
        #region Enumerations
        public enum DanceMatButtonAction { Unchanged, Pressed, Released };
        public enum DanceMatButton {
            //Here I have added the bits in the 7th and 8th bytes
            //in the 9 bytes that are sent from the dance map
            //for easy reference

            Start = 8192,      //0 0 1 0 0 0 0 0  0 0 0 0 0 0 0 0
            Select = 4096,     //0 0 0 1 0 0 0 0  0 0 0 0 0 0 0 0
            Circle =2048,      //0 0 0 0 1 0 0 0  0 0 0 0 0 0 0 0
            Cross = 1024,      //0 0 0 0 0 1 0 0  0 0 0 0 0 0 0 0
            Square = 512,      //0 0 0 0 0 0 1 0  0 0 0 0 0 0 0 0
            Triangle = 256,    //0 0 0 0 0 0 0 1  0 0 0 0 0 0 0 0    
            Right = 128,       //0 0 0 0 0 0 0 0  1 0 0 0 0 0 0 0
            Up =64,            //0 0 0 0 0 0 0 0  0 1 0 0 0 0 0 0
            Down = 32,         //0 0 0 0 0 0 0 0  0 0 1 0 0 0 0 0
            Left = 16,         //0 0 0 0 0 0 0 0  0 0 0 1 0 0 0 0
        };
        #endregion

        #region variables and properties
        public event EventHandler<DanceMatEventArgs>? ButtonStateChanged;
        private byte[] _lastReadData = new byte[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private byte[] _lastWriteData = new byte[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        
        HIDDevice _device;
        //You must change these (VID/PID) to your type of Dance Mat,
        //in case you can't connect using this. Then your dance mat may be another brand.
        //Look in Device manager, find your "HID-Compliant game controller" and look in
        //properties > details > property: hardware ID
        private const int DEVICE_VENDOR_ID = 0x0079;
        private const int DEVICE_PRODUCT_ID = 0x0011;

        private Dictionary<DanceMatButton, bool> _buttonStates = new();

        #endregion

        #region Constructor
        public DanceMat()
        {
            //initialize the button states dictionary with all buttons
            Enum.GetValues<DanceMatButton>().ToList().ForEach(button => _buttonStates.Add(button, false));

            //Get the details of all connected USB HID devices
            HIDDevice.interfaceDetails[] devices = HIDDevice.getConnectedDevices();

            //Select a device from the available devices (uses the Vendor ID and Product ID of the Dance Mat controller).
            var dev = devices.Where(dev =>dev.VID == DEVICE_VENDOR_ID 
                && dev.PID == DEVICE_PRODUCT_ID).FirstOrDefault();
            
            if (dev.VID == 0) { throw new Exception("No Dance Mat controller detected. Do you need to change the driver to 'HID-Compliant game controller' in Device Manager?"); }

            //register device, and set it up for publishing events when new data comes in
            _device = new HIDDevice(dev.devicePath, true);

            //subscribe to data received event
            _device.dataReceived += _device_dataReceived;
        }
        #endregion


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
        #region Internal functionality

        /// <summary>
        /// This message subscribes to data received from the HID (Human Interface Device)
        /// using the HIDDevice class
        /// </summary>
        private void _device_dataReceived(byte[] message)
        {
            try
            {
                //Only look for the "button action" messages (step or release on the mat tiles),
                // which are 9 bytes in length
                if(message.Length != 9) { return; }

                //Now cancel out the first four bits of the seventh byte
                //as it is filled with 1's 
                message[6] = (byte)(message[6] & 240);

                ushort lastBitArray = BitConverter.ToUInt16( new byte[] {_lastReadData[6], _lastReadData[7] });
                ushort currentBitArray = BitConverter.ToUInt16(new byte[] {message[6], message[7] });
                
                ushort bitValue = 16;   //we start five bits from the right
                //and read ten bits in
                for (int bitPosition = 0; bitPosition < 10; bitPosition++)
                {
                    var action = GetActionFromBitChange((lastBitArray & bitValue) > 0, (currentBitArray & bitValue) > 0);
                    if (action != DanceMatButtonAction.Unchanged)
                    {
                        DanceMatButton button = (DanceMatButton)bitValue;
                        _buttonStates[button] = action == DanceMatButtonAction.Pressed;
                        OnButtonStateChanged(button, action);
                    }
                    bitValue = (ushort)(bitValue << 1); //multiply by 2 (go to next bit to the left)
                }
                _lastReadData = message.ToArray();

            }
            catch (Exception ex) { throw new Exception($"Error while receiving data from controller. Maybe it was disconnected?. Error was: '{ex.Message}'", ex); }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) => _device.close();

        protected void OnButtonStateChanged(DanceMatButton button, DanceMatButtonAction action)
        {
            ButtonStateChanged?.Invoke(this, new DanceMatEventArgs(button, action));
        }

        private DanceMatButtonAction GetActionFromBitChange(bool previous, bool current)
        {
            if (previous == current) { return DanceMatButtonAction.Unchanged; }
            if (previous) { return DanceMatButtonAction.Released; }
            else { return DanceMatButtonAction.Pressed; }
        }
        #endregion
    }
}