using Prism;
using Prism.Ioc;
using LightsOn.ViewModels;
using LightsOn.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LightsOn.Views.PartialViews;
using LightsOn.ViewModels.PartialViewModel;
using LightsOn.Settings;
using LightsOnXamerin.Interfaces;
using LightsOn.Interfaces;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LightsOn
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            FritzSettings FritzDeviceSettings= new FritzSettings();
            TcpSettings TcpDeviceSettings = new TcpSettings();

            await NavigationService.NavigateAsync("MainPage").ConfigureAwait(false);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            TCPClient _tcpClient = new TCPClient();
            FritzCommand _fritzCommand = new FritzCommand();
            NanoLeafClient _nanoLeaf = new NanoLeafClient();
            containerRegistry.RegisterInstance<ITCPClient>(_tcpClient);
            containerRegistry.RegisterInstance<IFritzCommand>(_fritzCommand);
            containerRegistry.RegisterInstance<INanoLeafClient>(_nanoLeaf);


            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<MasterPage, MasterPageViewModel>();
            containerRegistry.RegisterForNavigation<LightsPage, LightsPageViewModel>();
            containerRegistry.RegisterForNavigation<HomeComingPage, HomeComingPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<TemperaturePage, TemperaturePageViewModel>();
            containerRegistry.RegisterForNavigation<TvRemotePage, TvRemotePageViewModel>();
            containerRegistry.RegisterForNavigation<RollerShutterPage, RollerShutterPageViewModel>();


            NanoLeafSettings NanoLeafDeviceSettings = new NanoLeafSettings(_nanoLeaf);
        }
    }
}
