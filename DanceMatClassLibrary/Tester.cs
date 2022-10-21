using System.Collections;
using System.Diagnostics;

namespace DanceMatClassLibrary
{
    public class Tester
    {
        public void Test()
        {
            //Register the factory for creating Hid devices. 
            //HID\VID_0079&PID_0011&REV_0106

            //Get the details of all connected USB HID devices
            HIDInterface.HIDDevice.interfaceDetails[] devices = HIDInterface.HIDDevice.getConnectedDevices();

            //Select a device from the available devices (uses the Vendor ID and Product ID of the PS2 Buzz! controller).
            //var dev = devices.Where(dev => dev.VID == 0x054C && dev.PID == 0x0002).FirstOrDefault();
            var dev = devices.Where(dev => dev.VID == 0x0079 && dev.PID == 0x0011).FirstOrDefault();
            //var dev = devices.Where(dev => dev.VID == 0x8086 && dev.PID == 0x15EC).FirstOrDefault();
            if (dev.VID == 0) { throw new Exception("No dance mat detected"); }

            //register device, and set it up for publishing events when new data comes in
            //var _device = new HIDInterface.HIDDevice(dev.devicePath, true);
            var _mat = new DanceMat();
            //subscribe to data received event
            //_device.dataReceived += _device_dataReceived;
            _mat.ButtonStateChanged += _mat_ButtonStateChanged;

        }

        private void _mat_ButtonStateChanged(object? sender, DanceMatEventArgs e)
        {
            Console.WriteLine(e.ToString());
        }

        private void _device_dataReceived(byte[] message)
        {
            //Console.Write("length: " + message.Length + "  ");
            var arr = new BitArray(new byte[] { message[5], message[6], message[7] });
            //var arr = new BitArray(message);
            int counter = 0;
            foreach (var item in arr)
            {
                Console.Write((bool)item ? " 1":" 0");
                counter++;
                if (counter % 8 == 0) Console.Write(" ");
            }
            Console.WriteLine();
            //Console.WriteLine(arr);
            //foreach (var item in message)
            //{
            //    Console.Write(item + " ");
            //}
            //Console.WriteLine();
        }
    }
}