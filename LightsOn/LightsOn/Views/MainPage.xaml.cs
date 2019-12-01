using Prism.Navigation;
using Prism.Navigation.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LightsOn.Views
{
    public partial class MainPage : MasterDetailPage 
    {
        INavigationService _navigationService;
        public MainPage()
        {
            //_navigationService = navigationService;
            InitializeComponent();
            masterPage.listView.ItemSelected += OnItemSelected;


            if (Device.RuntimePlatform == Device.UWP)
            {
                MasterBehavior = MasterBehavior.Popover;
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));

                //_navigationService.NavigateAsync("NavigationPage/PartialViews/LightsPage").ConfigureAwait(false);
                masterPage.listView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}