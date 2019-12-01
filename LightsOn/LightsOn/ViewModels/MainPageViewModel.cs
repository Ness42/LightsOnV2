using LightsOn.ViewModels.PartialViewModel;
using LightsOnXamerin.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace LightsOn.ViewModels
{
    public class MainPageViewModel : MasterDetailPage
    {
        MasterPageViewModel masterPage;
        public MainPageViewModel()
        {
        //    masterPage = new MasterPageViewModel();
        //    Master = masterPage;
        //    Detail = new NavigationPage(new HomeComingPageViewModel());

        //    masterPage.ListView.ItemSelected += OnItemSelected;

        //    if (Device.RuntimePlatform == Device.UWP)
        //    {
        //        MasterBehavior = MasterBehavior.Popover;
        //    }
        //}

        //void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var item = e.SelectedItem as MasterPageItem;
        //    if (item != null)
        //    {
        //        Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
        //        masterPage.ListView.SelectedItem = null;
        //        IsPresented = false;
        //    }
        }
        //public DelegateCommand<object> ItemTappedCommand { get; set; }

    }
    //Test command
}
