using LightsOn.Contorls;
using LightsOnXamerin.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightsOn.ViewModels.PartialViewModel
{
    public class RollerShutterPageViewModel : BindableBase
    {
        public TcpShutterControl TcpShutterControl { get; set; }

        public RollerShutterPageViewModel(ITCPClient tcpClient)
        {
            TcpShutterControl = new TcpShutterControl(tcpClient);
        }
    }
}
