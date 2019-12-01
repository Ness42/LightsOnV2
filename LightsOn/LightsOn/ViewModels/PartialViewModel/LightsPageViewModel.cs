using LightsOn.Contorls;
using LightsOn.Interfaces;
using LightsOnXamerin.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace LightsOn.ViewModels.PartialViewModel
{
    public class LightsPageViewModel : BindableBase
    {
        public Grid LightsPageGrid { get; set; }
        public TcpSwitchControl TcpLightControl { get; set; }
        public TcpLedControl TcpLedControl { get; set; }
        public NanoLeafControl NanoLeafControl { get; set; }

        public LightsPageViewModel(ITCPClient tcpClient, INanoLeafClient nanoLeafClient)
        {

            TcpLightControl = new Contorls.TcpSwitchControl(tcpClient);
            TcpLedControl = new Contorls.TcpLedControl(tcpClient);
            NanoLeafControl = new Contorls.NanoLeafControl(nanoLeafClient);

        }
    }
}
