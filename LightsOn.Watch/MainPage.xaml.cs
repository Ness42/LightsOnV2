using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LightsOn;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;
using LightsOnXamerin.Interfaces;
using LightsOn.Contorls;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace LightsOn.Watch
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CirclePage
    {
        public MainPage(ITCPClient tcpClient)
        {
            InitializeComponent();
            //TcpLightControl = new Contorls.TcpSwitchControl(tcpClient);


        }
    }
 
}