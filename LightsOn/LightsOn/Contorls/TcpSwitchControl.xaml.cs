using LightsOn.Settings;
using LightsOnXamerin.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LightsOn.Contorls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TcpSwitchControl : ContentView
    {

        private readonly ITCPClient _tcpClient;

        public TcpSwitchControl(ITCPClient tcpClient)
        {
            _tcpClient = tcpClient;

            InitializeComponent();

            TcpButtonControl();
        }


        public void TcpButtonControl()
        {
            foreach (var element in TcpSettings.TcpFS20DeviceList)
            {
                var newbutton = new Fs20Button
                {
                    Text = element.Value.Name,
                    Address = element.Value.Address,
                    Fs20Address = element.Value.Fs20.Fs20Address,
                    Fs20Command = nameof(TcpSettings.TcpFS20CommandList.Toggle)
                    

                };
                newbutton.Clicked += HandlerButtonClicked;

                GridLayoutTcp.Children.AddVertical(newbutton);
            }
        }

        private void HandlerButtonClicked(object sender, EventArgs e)
        {
            _tcpClient.SendTcpCommand(((Fs20Button)sender).Address, ((Fs20Button)sender).Fs20Address, ((Fs20Button)sender).Fs20Command);
        }



        private class Fs20Button : Button
        {
            public string Address { get; set; }
            public int Fs20Address { get; set; }
            public string Fs20Command { get; set; }
        }


    }
}
