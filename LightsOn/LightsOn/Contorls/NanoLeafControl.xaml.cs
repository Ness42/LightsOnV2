using LightsOn.Interfaces;
using LightsOn.Settings;
using LightsOnXamerin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LightsOn.Contorls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NanoLeafControl : ContentView
    {
        private readonly INanoLeafClient _nanoLeafClient;
        public NanoLeafControl(INanoLeafClient nanoLeafClient)
        {
            InitializeComponent();
            _nanoLeafClient = nanoLeafClient;
            int count = 0;
            foreach (var element in NanoLeafSettings.NanoLeafDevices)
            {
                var newbuttonOff = new NanoLeafButton
                {
                    Text = element.Name + " On",
                    Command = true,
                    Device = element
                };
                newbuttonOff.Clicked += HandlerButtonClicked;
                GridNanoLeaf.Children.Add(newbuttonOff, count, 1);

                var newbuttonOn = new NanoLeafButton
                {
                    Text = element.Name + " Off",
                    Command = false,
                    Device = element
                };
                newbuttonOn.Clicked += HandlerButtonClicked;
                GridNanoLeaf.Children.Add(newbuttonOn, count, 2);

                count++;

            }

        }

        private void HandlerButtonClicked(object sender, EventArgs e)
        {
            if ((((NanoLeafButton)sender).Device.Token) == null)
                _nanoLeafClient.getAuthorizationToken(((NanoLeafButton)sender).Device.Name);
            _nanoLeafClient.SwitchNanoLeaf((((NanoLeafButton)sender).Command),((NanoLeafButton)sender).Device);
        }




        private class NanoLeafButton : Button
        {
            public string Name { get; set; }
            public bool Command { get; set; }

            public NanoLeafSettings.NanoLeafDevice Device { get; set; }

        }
    }
}