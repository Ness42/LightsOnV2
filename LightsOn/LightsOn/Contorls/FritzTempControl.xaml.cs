using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LightsOn.Interfaces;
using LightsOn.Settings;
using System.Globalization;

namespace LightsOn.Contorls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FritzTempControl : ContentView
    {
        private readonly IFritzCommand _fritzCommand;
        string Sid;


        public FritzTempControl(IFritzCommand fritzCommand)
        {
            InitializeComponent();

            if (fritzCommand != null)
            {
                _fritzCommand = fritzCommand;
                Sid = _fritzCommand.GetSessionID("marc", "kaliwerra");
                FritzTempLabel();
            }

        }


        public void FritzTempLabel()
        {
            int count = 0;
            foreach (var element in FritzSettings.FritzTempDeviceList)
            {
                string temp = _fritzCommand.GetDectTemp(element.Ain, Sid).ToString(new CultureInfo("de-DE"));

                var newLabel = new Label
                {
                    //Text = _fritzCommand.GetDectTemp(element.Ain, nameof(FritzCommands.getbasicdevicestats), Sid).ToString()
                    Text =  temp
                };
            
                count++;
                StackLayoutFritz.Children.Add(newLabel);
            }
        }

    }



}

