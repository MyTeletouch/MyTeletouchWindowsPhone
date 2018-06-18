using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTeletouch
{
    public static class BLEDeviceConstants
    {
        // Sample Characteristics.
        public const string HM_RX_TX = "0000ffe1-0000-1000-8000-00805f9b34fb";//HM Serial
        public const string CLIENT_CHARACTERISTIC_CONFIG0 = "00002901-0000-1000-8000-00805f9b34fb";
        public const string CLIENT_CHARACTERISTIC_CONFIG = "00002902-0000-1000-8000-00805f9b34fb";

        // Sample Services.
        public const string HM_10_CONF = "0000ffe0-0000-1000-8000-00805f9b34fb";//HM 10 Serial
        public const string HM_DeviceInformation = "00001800-0000-1000-8000-00805f9b34fb";//Device Information Service

    }
}
