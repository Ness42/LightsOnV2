using LightsOn.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LightsOn.Settings
{
    public class NanoLeafSettings
    {

        //public static readonly Dictionary<string, NanoLeafDevice> IrCommandDictionary = new Dictionary<string, IrCommand>
        //    {
        //        {"On",          new IrCommand{Name = "On",,Row = 0,Column = 0}},
        //        {"Off",         new IrCommand{Name = "Off",Command = 12,Row = 0,Column = 2}},
        //        {"Up",          new IrCommand{Name = "↑",Command = 88,Row = 1,Column = 1}},
        //        {"Ok",          new IrCommand{Name = "Ok",Command = 92,Row = 2,Column = 1}},
        //        {"Left",        new IrCommand{Name = "←",Command = 90,Row = 2,Column = 0}},
        //        {"Right",       new IrCommand{Name = "→",Command = 91,Row = 2,Column = 2}},
        //        {"Down",        new IrCommand{Name = "↓",Command = 89,Row = 3,Column = 1}},
        //        {"Source",      new IrCommand{Name = "Source",Command = 56,Row = 4,Column = 0}},
        //        {"Mute",        new IrCommand{Name = "Mute",Command = 13,Row = 4,Column = 2}},
        //        {"VolumeUp",    new IrCommand{Name = "V↑",Command = 16,Row = 1,Column = 2}},
        //        {"VolumeDown",  new IrCommand{Name = "V↓",Command = 17,Row = 3,Column = 2}},
        //        {"ProgUp",      new IrCommand{Name = "P↑",Command = 76,Row = 1,Column = 0}},
        //        {"ProgDwon",    new IrCommand{Name = "P↓",Command = 77,Row = 3,Column = 0}}
        //    };


        public static readonly List<NanoLeafDevice> NanoLeafDevices = new List<NanoLeafDevice>();


        private readonly INanoLeafClient _nanoLeafClient;

        public NanoLeafSettings(INanoLeafClient nanoLeafClient)
        {

            _nanoLeafClient = nanoLeafClient;

            NanoLeafDevices.Add(new NanoLeafDevice
            {
                Name = "NanoLeaf Wohnzimmer",
                Address = "192.168.1.136",
                HomeComing = true
            });
            NanoLeafDevices.Add(new NanoLeafDevice
            {
                Name = "NanoLeaf Küche",
                Address = "192.168.1.34",
                HomeComing = true
            });
            _nanoLeafClient.LoadAuhtTokenFromSettings();
        }


        public class NanoLeafDevice
        {
            public string Name { set; get; }
            public string Address { set; get; }
            public string Token { set; get; }    
            public bool HomeComing { set; get; }
        }
    }
}
