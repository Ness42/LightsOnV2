using LightsOn.Settings;
using LightsOnXamerin.Interfaces;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LightsOn.Contorls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TvRemoteControl : ContentView
    {
        private readonly ITCPClient _tcpClient;
        public TvRemoteControl(ITCPClient tcpClient)
        {
            _tcpClient = tcpClient;

            InitializeComponent();
            foreach (var element in TcpSettings.IrCommandDictionary)
            {
                var newbutton = new TcpButton
                {
                    Text = element.Value.Name,
                    Address = TcpSettings.IpAddress[0],
                    IrCommand = element.Value.Ir.IrCommand,
                    Row = element.Value.Row,
                    Column = element.Value.Column
                }; 
                newbutton.Clicked += HandlerButtonClicked;
                TvButtonGrid.Children.Add(newbutton, newbutton.Column,newbutton.Row);
            }
        }

        private void HandlerButtonClicked(object sender, EventArgs e)
        {
            _tcpClient.SendTcpCommand(((TcpButton)sender).Address, ((TcpButton)sender).IrCommand);
        }

        private class TcpButton : Button
        {
            public string Address { get; set; }
            public string NumOfButtons { get; set; }
            public int IrCommand { get; set; }
            public int Row { get; set; }
            public int Column { get; set; }
        }
    }
}