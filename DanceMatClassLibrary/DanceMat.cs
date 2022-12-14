using HIDInterface;

namespace DanceMatClassLibrary
{
    //driver misidentification issue:
    //1. Plug in Dance Mat in a USB port
    //2. On Windows(10), go to Device Manager -> open USB Tab.There should be a one thats not working   (yellow triangle), called "Standard USB Hub".
    //3. Right click it -> Update Driver -> Browse my computer for driver software -> Let me pick from a list of device drivers on my computer -> Select "USB Input Device" -> Click OK.

    //NOTE: you may have to look under Human Interface Devices (HID) and look for 
    //"HID-Compliant game controller" -> copy VID and PID to the code below
    //and change the values of the constants DEVICE_VENDOR_ID and DEVICE_PRODUCT_ID
    public class DanceMat : DanceMatBase, IDisposable
    {

        #region variables and properties
        private byte[] _lastReadData = new byte[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        HIDDevice _device;

        //You must change these (VID/PID) to your type of Dance Mat,
        //in case you can't connect using this. Then your dance mat may be another brand.
        //Look in Device manager, find your "HID-Compliant game controller" and look in
        //properties > details > property: hardware ID
        private const int DEVICE_VENDOR_ID = 0x0079;
        private const int DEVICE_PRODUCT_ID = 0x0011;

        #endregion

        #region Constructor
        public DanceMat()
        {

            //Get the details of all connected USB HID devices
            HIDDevice.interfaceDetails[] devices = HIDDevice.getConnectedDevices();

            //Select a device from the available devices (uses the Vendor ID and Product ID of the Dance Mat controller).
            var dev = devices.Where(dev => dev.VID == DEVICE_VENDOR_ID
                && dev.PID == DEVICE_PRODUCT_ID).FirstOrDefault();

            if (dev.VID == 0) { throw new Exception("No Dance Mat controller detected. Do you need to change the driver to 'HID-Compliant game controller' in Device Manager?"); }

            //register device, and set it up for publishing events when new data comes in
            _device = new HIDDevice(dev.devicePath, true);

            //subscribe to data received event
            _device.dataReceived += _device_dataReceived;
        }
        #endregion

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
                if (message.Length != 9) { return; }

                //Now cancel out the first four bits of the seventh byte
                //as it is filled with 1's 
                message[6] = (byte)(message[6] & 240);

                ushort lastBitArray = BitConverter.ToUInt16(new byte[] { _lastReadData[6], _lastReadData[7] });
                ushort currentBitArray = BitConverter.ToUInt16(new byte[] { message[6], message[7] });

                ushort bitValue = 16;   //we start five bits from the right
                //and read ten bits in
                for (int bitPosition = 0; bitPosition < 10; bitPosition++)
                {
                    var action = GetActionFromBitChange((lastBitArray & bitValue) > 0, (currentBitArray & bitValue) > 0);
                    if (action != DanceMatButtonAction.Unchanged)
                    {
                        DanceMatButton button = (DanceMatButton)bitValue;
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

        private DanceMatButtonAction GetActionFromBitChange(bool previous, bool current)
        {
            if (previous == current) { return DanceMatButtonAction.Unchanged; }
            if (previous) { return DanceMatButtonAction.Released; }
            else { return DanceMatButtonAction.Pressed; }
        }
        #endregion
    }
}