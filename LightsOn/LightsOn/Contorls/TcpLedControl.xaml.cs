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
    public partial class TcpLedControl : ContentView
    {

        private readonly ITCPClient _tcpClient;

        public TcpLedControl(ITCPClient tcpClient)
        {
            //_Num = Num;

            _tcpClient = tcpClient;


            InitializeComponent();

            Label header = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            Picker picker = new Picker
            {
                Title = "Color",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            StackLayout switches = new StackLayout
            {
                Orientation = StackOrientation.Horizontal
            };
            int count = 0;
            foreach (var element in TcpSettings.TcpLedDeviceList)
            {

                var newLabel = new Label
                {
                    Text = element.Name
                };
                var newSwitch = new LedSwitch
                {
                    index = count
                };
                newSwitch.Toggled += HandlerSwitchToggeled;

                switches.Children.Add(newLabel);
                switches.Children.Add(newSwitch);
                count++;
            }


            foreach (string colorName in nameToColor.Keys)
            {
                picker.Items.Add(colorName);
            }

            // Create BoxView for displaying picked Color
            BoxView boxView = new BoxView
            {
                WidthRequest = 150,
                HeightRequest = 150,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            picker.SelectedIndexChanged += (sender, args) =>
            {
                if (picker.SelectedIndex == -1)
                {
                    boxView.Color = Color.Default;
                }
                else
                {
                    string colorName = picker.Items[picker.SelectedIndex];
                    boxView.Color = nameToColor[colorName];
                    foreach (var element in TcpSettings.TcpLedDeviceList)
                    {
                        if (element.aktive == true)
                            _tcpClient.SendTcpCommand(element.Address, boxView.Color.R * 255, boxView.Color.G * 255 , boxView.Color.B * 255 );
                    }
                }
            };

            GridTcp.Children.AddVertical(header);
            GridTcp.Children.AddVertical(picker);
            GridTcp.Children.AddVertical(boxView);
            GridTcp.Children.AddVertical(switches);
            //// Build the page.
            //this.Content = new StackLayout
            //{
            //    Children =
            //    {
            //        header,
            //        picker,
            //        boxView,
            //        switches
            //    }
            //};

        }

        private void HandlerSwitchToggeled(object sender, ToggledEventArgs e)
        {
            if(((LedSwitch)sender).IsToggled)
                TcpSettings.TcpLedDeviceList[((LedSwitch)sender).index].aktive=true;
            else
                TcpSettings.TcpLedDeviceList[((LedSwitch)sender).index].aktive = false;
        }


        // Dictionary to get Color from color name.
        Dictionary<string, Color> nameToColor = new Dictionary<string, Color>
        {
            { "Aqua", Color.Aqua }, { "Black", Color.Black },
            { "Blue", Color.Blue }, { "Fucshia", Color.Black },
            { "Gray", Color.Gray }, { "Green", Color.Green },
            { "Lime", Color.Lime }, { "Maroon", Color.Maroon },
            { "Navy", Color.Navy }, { "Olive", Color.Olive },
            { "Purple", Color.Purple }, { "Red", Color.Red },
            { "Silver", Color.Silver }, { "Teal", Color.Teal },
            { "White", Color.White }, { "Yellow", Color.Yellow }
        };

    }

    class LedSwitch : Switch
        {
            public int index { get; set; }
        } 


    //private void TcpControl_PropertyChanged(object sender, PropertyChangedEventArgs e)
    //{

    //}

    //public void TcpButtonControl(int NumOfButtons)
    //{
    //    for (int i = 0; i < NumOfButtons; i++)
    //    {
    //        var newbutton = new TcpButton
    //        {
    //            Text = "Licht " + i,
    //            Address = "192.168.1.92",
    //            Message = "FS20;" + i + ";Toggle\n",

    //        };
    //        newbutton.Clicked += HandlerButtonClicked;

    //        StackLayoutTcp.Children.Add(newbutton);
    //    }
    //}

    //private void HandlerButtonClicked(object sender, EventArgs e)
    //{
    //    _tcpClient.SendTcpCommand(((TcpButton)sender).Address, ((TcpButton)sender).Message);
    //}



    //private class TcpButton : Button
    //{
    //    public string Address { get; set; }
    //    public string NumOfButtons { get; set; }
    //    public string Message { get; set; }

    //}


}

