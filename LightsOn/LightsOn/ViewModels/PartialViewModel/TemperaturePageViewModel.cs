using LightsOn.Contorls;
using LightsOn.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightsOn.ViewModels.PartialViewModel
{
    public class TemperaturePageViewModel : BindableBase
    {
        public FritzTempControl FritzTempControl { get; set; }
        public TemperaturePageViewModel(IFritzCommand fritzCommand)
        {
            
            FritzTempControl = new FritzTempControl(fritzCommand);
        }
    }
}
