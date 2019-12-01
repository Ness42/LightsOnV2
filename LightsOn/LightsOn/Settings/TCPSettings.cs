using System;
using System.Collections.Generic;
using System.Text;

namespace LightsOn.Settings
{
    public class TcpSettings
    {

        //public static readonly  List<TcpFS20Device> TcpFS20DeviceList = new List<TcpFS20Device> {};
        //public static readonly List<TcpFS20Device> TcpFS20SutterDeviceList = new List<TcpFS20Device> { };
        public static readonly List<TcpCommand> TcpLedDeviceList = new List<TcpCommand> { };

        public static readonly Dictionary<string, TcpCommand> IrCommandDictionary = new Dictionary<string, TcpCommand>
        {
                {"On",          new TcpCommand{Name = "On", Ir= new TcpIrDevice{IrCommand       = 56 },Row = 0,Column = 0}},
                {"Off",         new TcpCommand{Name = "Off",Ir= new TcpIrDevice{IrCommand       = 12,},Row = 0,Column = 2}},
                {"Up",          new TcpCommand{Name = "↑",Ir= new TcpIrDevice{IrCommand         = 88,},Row = 1,Column = 1}},
                {"Ok",          new TcpCommand{Name = "Ok",Ir= new TcpIrDevice{IrCommand        = 92,},Row = 2,Column = 1}},
                {"Left",        new TcpCommand{Name = "←",Ir= new TcpIrDevice{IrCommand         = 90,},Row = 2,Column = 0}},
                {"Right",       new TcpCommand{Name = "→",Ir= new TcpIrDevice{IrCommand         = 91,},Row = 2,Column = 2}},
                {"Down",        new TcpCommand{Name = "↓",Ir= new TcpIrDevice{IrCommand         = 89,},Row = 3,Column = 1}},
                {"Source",      new TcpCommand{Name = "Source",Ir= new TcpIrDevice{IrCommand    = 56,},Row = 4,Column = 0}},
                {"Mute",        new TcpCommand{Name = "Mute",Ir= new TcpIrDevice{IrCommand      = 13,},Row = 4,Column = 2}},
                {"VolumeUp",    new TcpCommand{Name = "V↑",Ir= new TcpIrDevice{IrCommand        = 16,},Row = 1,Column = 2}},
                {"VolumeDown",  new TcpCommand{Name = "V↓",Ir= new TcpIrDevice{IrCommand        = 17,},Row = 3,Column = 2}},
                {"ProgUp",      new TcpCommand{Name = "P↑",Ir= new TcpIrDevice{IrCommand        = 76,},Row = 1,Column = 0}},
                {"ProgDwon",    new TcpCommand{Name = "P↓",Ir= new TcpIrDevice{IrCommand        = 77,},Row = 3,Column = 0}}
        };

        public static readonly Dictionary<string, TcpCommand> TcpFS20DeviceList = new Dictionary<string, TcpCommand>
        {
                {"Wohnzimmer Decke",        new TcpCommand{Name = "Wohnzimmer Decke",    Address="192.168.1.92" , Fs20 = new TcpFS20Device{Fs20Address = 0 }}},  //,Command="FS20;" + 0 + ";"+ TcpFS20CommandList.Toggle +"\n"}},
                {"Wohnzimmer TV",           new TcpCommand{Name = "Wohnzimmer TV",       Address="192.168.1.92" , Fs20 = new TcpFS20Device{Fs20Address = 2 },HomeComing=true}},  //,Command="FS20;" + 2 + ";"+ TcpFS20CommandList.Toggle +"\n"}},
                {"Küche Decke",             new TcpCommand{Name = "Küche Decke",         Address="192.168.1.92" , Fs20 = new TcpFS20Device{Fs20Address = 1 }}},  //,Command="FS20;" + 1 + ";"+ TcpFS20CommandList.Toggle +"\n"}},
                {"Küche Kühlschrank",       new TcpCommand{Name = "Küche Kühlschrank",   Address="192.168.1.92" , Fs20 = new TcpFS20Device{Fs20Address = 3 }}},  //,Command="FS20;" + 3 + ";"+ TcpFS20CommandList.Toggle +"\n"}}
        };

        public static readonly Dictionary<string, TcpCommand> TcpFS20SutterDeviceList = new Dictionary<string, TcpCommand>
        {
                {"Wohnzimmer Rollo",        new TcpCommand{Name = "Wohnzimmer",    Address="192.168.1.92" , Fs20 = new TcpFS20Device{Fs20Address = 21 }}},//,Command="FS20;" + 0 + ";"+ TcpFS20CommandList.Toggle +"\n"}},
                {"Küche Rollo",             new TcpCommand{Name = "Küche",         Address="192.168.1.92" , Fs20 = new TcpFS20Device{Fs20Address = 22 }}},//,Command="FS20;" + 1 + ";"+ TcpFS20CommandList.Toggle +"\n"}},
        };

        public enum TcpFS20CommandList { Toggle, ON ,OFF };

        public static List<string> IpAddress = new List<string>();

        public TcpSettings()
        {
            IpAddress.Add("192.168.1.92");
            IpAddress.Add("192.168.1.73");

            TcpLedDeviceList.Add(new TcpCommand
            {
                Address = IpAddress[0],
                Name = "Sofa",
                Command = "",
                HomeComing = true,
                Led = new TcpLedDevice { HomeComing = new TcpLedColors { Red = 0, Blue = 0, Green = 255 } },
                aktive = true
                
            });
            TcpLedDeviceList.Add(new TcpCommand
            {
                Address = IpAddress[1],
                Name = "Schrank",
                Command = "",
                HomeComing = true,
                Led = new TcpLedDevice { HomeComing = new TcpLedColors { Red = 0, Blue = 255, Green = 255 } },        
                aktive = false
            }) ;
        }

    }

    public class TcpCommand
    {
        public string Name { set; get; }
        public string Address { set; get; }
        public string Command { set; get; }
        public bool aktive { set; get; }
        public bool HomeComing { set; get; }
        public int Column { set; get; }
        public int Row { set; get; }
        public TcpFS20Device Fs20 { set; get; }
        public TcpLedDevice Led { set; get; }
        public TcpIrDevice Ir { set; get; }

    }

    public class TcpFS20Device
    {
        public int Fs20Address { set; get; }
    }

    public class TcpLedDevice
    {
        public TcpLedColors Current { set; get; }
        public TcpLedColors HomeComing { set; get; }
    }

    public class TcpLedColors
    {
        public int Red { set; get; }
        public int Green { set; get; }
        public int Blue { set; get; }
    }

    public class TcpIrDevice
    {
        public int IrCommand { set; get; }
    }

}
