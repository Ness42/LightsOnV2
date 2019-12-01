using LightsOn.Interfaces;
using LightsOn.Settings;
using LightsOnXamerin.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Xamarin.Forms;

namespace LightsOn.ViewModels.PartialViewModel
{


    public class HomeComingPageViewModel : ContentPage
    {
        private ImageSource _ImageButtonSource;
        public ImageSource ImageButtonSource
        {
            get { return _ImageButtonSource; }
            set
            {
                if (_ImageButtonSource == value)
                    return;
                _ImageButtonSource = value;
                //RaisePropertyChanged(nameof(ImageButtonSource));
            }
        }

        private ITCPClient _tcpClient;
        private INanoLeafClient _nanoLeafClient;

        public HomeComingPageViewModel(ITCPClient tcpClient, INanoLeafClient nanoLeafClient)
        //public HomeComingPageViewModel()
        {
            _tcpClient = tcpClient;
            _nanoLeafClient = nanoLeafClient;

            //  Create the command - calls Do...Commands.
            homeComingOnCommand = new Command(DoHomeComingOnCommand);
            homeComingOffCommand = new Command(DoHomeComingOffCommand);
        }



        /// <summary>
        /// The command objects.
        /// </summary>
        private Command homeComingOnCommand;
        private Command homeComingOffCommand;



        /// <summary>
        /// The Command functions.
        /// </summary>
        private void DoHomeComingOnCommand()
        {
            var time = DateTime.Now;
            foreach (var element in TcpSettings.TcpFS20DeviceList)
            {

                if (element.Value.HomeComing == true)
                    if (time.Hour >= 19)
                        _tcpClient.SendTcpCommand(element.Value.Address, element.Value.Fs20.Fs20Address,nameof(TcpSettings.TcpFS20CommandList.ON));
            }
            foreach (var element in TcpSettings.TcpLedDeviceList)
            {

                if (element.HomeComing == true)
                    _tcpClient.SendTcpCommand(element.Address,element.Led.HomeComing.Red, element.Led.HomeComing.Green, element.Led.HomeComing.Blue);
            }

            foreach (var element in NanoLeafSettings.NanoLeafDevices)
            {

                if (element.HomeComing == true)
                    _nanoLeafClient.SwitchNanoLeaf(true, element);
            }
        }
                //  Add a message.
                //Messages.Add("Calling 'DoSimpleCommand'.");
    

        private void DoHomeComingOffCommand()
        {
            foreach (var element in TcpSettings.TcpFS20DeviceList)
            {

                //if (element.Value.HomeComing == true)
                    _tcpClient.SendTcpOffCommand(element.Value.Address, element.Value.Fs20.Fs20Address);
            }
            foreach (var element in TcpSettings.TcpLedDeviceList)
            {

                //if (element.HomeComing == true)
                    _tcpClient.SendTcpOffCommand(element.Address);
            }

            foreach (var element in NanoLeafSettings.NanoLeafDevices)
            {

                //if (element.HomeComing == true)
                    _nanoLeafClient.SwitchNanoLeaf(false,element);
            }
        }



        /// <summary>
        /// commands.
        /// </summary>
        public Command HomeComingOffCommand
        {
            get { return homeComingOffCommand; }
        }
        public Command HomeComingOnCommand
        {
            get { return homeComingOnCommand; }
        }


    }
}
