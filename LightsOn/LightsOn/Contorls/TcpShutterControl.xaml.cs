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
    public partial class TcpShutterControl : ContentView
    {

        private readonly ITCPClient _tcpClient;

        public TcpShutterControl(ITCPClient tcpClient)
        {
            _tcpClient = tcpClient;

            InitializeComponent();

            TcpButtonControl();
        }


        public void TcpButtonControl()
        {
            int count = 0;
            foreach (var element in TcpSettings.TcpFS20SutterDeviceList)
            {
                var newbuttonOpen = new Fs20Button
                {
                    Address = element.Value.Address,
                    Fs20Address = element.Value.Fs20.Fs20Address,
                    Text = element.Value.Name + " öffnen",
                    Fs20Command = nameof(TcpSettings.TcpFS20CommandList.ON),
                };
                GridSutterTcp.Children.Add(newbuttonOpen,count,0);
                newbuttonOpen.Clicked += HandlerButtonClicked;

                var newbuttonClose = new Fs20Button
                {
                    Address = element.Value.Address,
                    Fs20Address = element.Value.Fs20.Fs20Address,
                    Text = element.Value.Name + " schließen",
                    Fs20Command = nameof(TcpSettings.TcpFS20CommandList.OFF),
                };
                GridSutterTcp.Children.Add(newbuttonClose, count,1);
                newbuttonClose.Clicked += HandlerButtonClicked;

                count++;

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
