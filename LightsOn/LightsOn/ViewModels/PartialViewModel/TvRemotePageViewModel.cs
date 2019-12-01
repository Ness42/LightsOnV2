using LightsOn.Contorls;
using LightsOnXamerin.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightsOn.ViewModels.PartialViewModel
{
    public class TvRemotePageViewModel : BindableBase
    {
        public TvRemoteControl TcpTvControl { get; set; }
        public TvRemotePageViewModel(ITCPClient tcpClient)
        {
            TcpTvControl = new TvRemoteControl(tcpClient);

        }
    }
}
