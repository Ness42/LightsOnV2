using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LightsOn.Interfaces;
using LightsOn.Settings;

namespace LightsOn.Contorls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FritzSwitchControl : ContentView
    {
        private readonly IFritzCommand _fritzCommand;
        string Sid;

        private int _numOfButtons;
        public int NumOfButtons
        {
            set
            {
                if (_numOfButtons != value)
                {
                    _numOfButtons = value;
                    OnPropertyChanged(nameof(NumOfButtons));
                    FritzButtonControl();
                }
            }
            get
            {
                return _numOfButtons;
            }
        }

        public FritzSwitchControl()
        {
            InitializeComponent();

            _fritzCommand = new FritzCommand();
            Sid=_fritzCommand.GetSessionID("marc","kaliwerra");
        }

        //TODO repalce NumOfControls with num of FritzSwitchDevices 

        public void FritzButtonControl()
        {
            foreach (var element in FritzSettings.FritzSwitchDeviceList)
            {
                var newbutton = new FritzButton
                {
                    Text = element.Name,
                    Ain = element.Ain,
                };
                newbutton.Clicked += HandlerButtonClicked;

                StackLayoutFritz.Children.Add(newbutton);
            }
        }
       private void HandlerButtonClicked(object sender, EventArgs e)
        {
            _fritzCommand.DectCommand(((FritzButton)sender).Ain, nameof(FritzCommands.getbasicdevicestats), Sid);
        }



        private class FritzButton : Button
        {
            public string Ain { get; set; }
            public string NumOfButtons { get; set; }
        }

    }



}

