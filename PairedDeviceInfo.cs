using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Networking.Proximity;
using Windows.Storage.Streams;

namespace MyTeletouch
{
    /// <summary>
    ///  Class to hold all paired device information
    /// </summary>
    public class PairedDeviceInfo
    {
        /// <summary>
        /// Currently selected Characteristic
        /// </summary>
        private GattCharacteristic currentCharacteristic;

        //private GattDeviceService service;

        internal PairedDeviceInfo(PeerInformation peerInformation)
        {
            this.PeerInfo = peerInformation;
            this.DisplayName = this.PeerInfo.DisplayName;
            this.HostName = this.PeerInfo.HostName.DisplayName;
            this.ServiceName = this.PeerInfo.ServiceName;
        }

        internal PairedDeviceInfo(BluetoothLEDevice bluetoothLEDevice)
        {
            this.BluetoothLEDevice = bluetoothLEDevice;
            this.DisplayName = this.BluetoothLEDevice.Name;
        }

        public string DisplayName { get; private set; }
        public string HostName { get; private set; }
        public string ServiceName { get; private set; }
        public PeerInformation PeerInfo { get; private set; }
        public BluetoothLEDevice BluetoothLEDevice { get; private set; }
        public int LastCommandId { get; set; }
        public int LastProcessedCommandId { get; private set; }

        /// <summary>
        /// Loads GATT services information in list and attaches to CapSense and RGB led custom services if available.
        /// </summary>
        public async Task<bool> LoadGattServices()
        {
            if (currentCharacteristic != null)
                currentCharacteristic.ValueChanged -= currentCharacteristic_ValueChanged;
            currentCharacteristic = null;

            foreach (var service in BluetoothLEDevice.GattServices)
            {
                foreach (var characteristic in service.GetAllCharacteristics())
                {
                    if (characteristic.Uuid == Guid.Parse(BLEDeviceConstants.HM_RX_TX))
                        return await AttachCharacteristic(characteristic);
                }
            }

            //service = await GattDeviceService.FromIdAsync(BluetoothLEDevice.DeviceId);
            //currentCharacteristic = service.GetCharacteristics(Guid.Parse(BLEDeviceConstants.HM_RX_TX)).FirstOrDefault();
            //if(currentCharacteristic != null)
            //    return await AttachCharacteristic(currentCharacteristic);

            return false;
        }

        /// <summary>
        /// Attaches valuechanged to capsense characteristic
        /// </summary>
        /// <param name="characteristic"></param>
        private async Task<bool> AttachCharacteristic(GattCharacteristic characteristic)
        {
            currentCharacteristic = characteristic;
            currentCharacteristic.ValueChanged += currentCharacteristic_ValueChanged;

            try
            {
                GattCommunicationStatus status = await characteristic.WriteClientCharacteristicConfigurationDescriptorAsync(GattClientCharacteristicConfigurationDescriptorValue.Notify);
                return status != GattCommunicationStatus.Unreachable;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return false;
        }

        /// <summary>
        /// Handles value changed of capsense slider
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void currentCharacteristic_ValueChanged(GattCharacteristic sender, GattValueChangedEventArgs args)
        {
            if (args.CharacteristicValue.Length > 1)
            {
                var data = new byte[args.CharacteristicValue.Length];
                using (var reader = DataReader.FromBuffer(args.CharacteristicValue))
                {
                    string[] command = reader.ReadString(args.CharacteristicValue.Length).Split('|', ']');
                    if (command.Length > 1)
                    {
                        if (command[1] == LastCommandId.ToString() && command[2] == "1")
                            LastProcessedCommandId = LastCommandId;
                    }
                }
            }
        }

        internal async Task<bool> SendBLEData(StringBuilder sbCmd)
        {
            try
            {
                if (currentCharacteristic != null)
                {
                    using (var writer = new Windows.Storage.Streams.DataWriter())
                    {
                        writer.WriteString(sbCmd.ToString());
                        GattCommunicationStatus status = await currentCharacteristic.WriteValueAsync(writer.DetachBuffer(), GattWriteOption.WriteWithoutResponse);
                        return status == GattCommunicationStatus.Success;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return false;
        }
    }
}
