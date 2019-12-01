using System;
using System.Collections.Generic;
using System.Text;

namespace LightsOn.Settings
{
    class HomeComingSettings
    {
        public static List<object> HomeComingDeviceList = new List<object> { };

        public HomeComingSettings()
        {
            foreach (var element in TcpSettings.TcpFS20DeviceList)
            {
                HomeComingDeviceList.Add(element);
            }
            foreach (var element in TcpSettings.TcpLedDeviceList)
            {
                HomeComingDeviceList.Add(element);
            }
            foreach (var element in NanoLeafSettings.NanoLeafDevices)
            {
                HomeComingDeviceList.Add(element);
            }
        }
    }


}
