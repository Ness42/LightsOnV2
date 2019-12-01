using System;
using System.Collections.Generic;
using System.Text;

namespace LightsOn.Settings
{
    public class FritzSettings
    {

        public static readonly List<FritzDevice> FritzTempDeviceList = new List<FritzDevice> { };
        public static readonly List<FritzDevice> FritzSwitchDeviceList = new List<FritzDevice> { };

        public FritzSettings()
        {
            FritzTempDeviceList.Add(new FritzDevice
            {
                Name = "Heizung Wohnzimmer Schreibtisch",
                Ain = "09995 0307215"
            });
            FritzTempDeviceList.Add(new FritzDevice
            {
                Name = "Heizung Wohnzimmer Sofa",
                Ain = "09995 0308186"
            });
            FritzTempDeviceList.Add(new FritzDevice
            {
                Name = "Heizung Bad",
                Ain = "09995 0306720"
            });
            FritzTempDeviceList.Add(new FritzDevice
            {
                Name = "Gästezimmer",
                Ain = "09995 0307173"
            });
            FritzTempDeviceList.Add(new FritzDevice
            {
                Name = "Schlafzimmer",
                Ain = "09995 0308175"
            });
        }
        
    }

    public class FritzDevice
    {
        public string Name { set; get; }
        public string Ain { set; get; }
    }
}
