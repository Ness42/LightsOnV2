using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using static LightsOn.UserSettings;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using LightsOn.Settings;

namespace LightsOn.ViewModels.PartialViewModel
{
 

    public class SettingsPageViewModel : INotifyPropertyChanged
    {
        

        private int _SettingNumOfLights;
        public int SettingNumOfLights
        {
            get { return _SettingNumOfLights; }
            set
            {
                if (_SettingNumOfLights == value)
                    return;
                _SettingNumOfLights = value;
                RaisePropertyChanged(nameof(SettingNumOfLights));
            }
        }


        private bool _SettingServerActive;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool SettingServerActive
        {
            get { return _SettingServerActive; }
            set
            {
                if (_SettingServerActive == value)
                    return;
                _SettingServerActive = value;
                RaisePropertyChanged(nameof(SettingServerActive));
            }
        }




        private bool _testState;
        public bool TestState
        {
            set
            {
                if (_testState != value)
                {
                    _testState = value;
                    RaisePropertyChanged(nameof(TestState));
                }
            }
            get
            {
                return _testState;
            }
        }






        bool TestFunktion(bool yes)
        {
            //if (yes == true)
            // //;
            return yes;
        }



        public ObservableCollection<SettingsPageItem> Settings { get; private set; }
        public List<FritzDevice> SettingsFritzDevices { get; private set; }


        public SettingsPageViewModel()
        {

            //SettingsFritzDevices = new ObservableCollection<SettingsPageItem>();
            SettingsFritzDevices = FritzSettings.FritzTempDeviceList;

            SaveCommand = new Command(() => { SavePreferences(); });

            TestCommand = new Command(() => { TestFunktion(TestState); });

            ReadPreferences();
            Settings = new ObservableCollection<SettingsPageItem>();
            Settings.Add(new SettingsPageItem
            {
                Name = "NumOfLights",
                Value = SettingNumOfLights
            });

            Settings.Add(new SettingsPageItem
            {
                Name = "ServerActive",
                State = SettingServerActive
            });

            Settings.CollectionChanged += item_PropertyChanged;

        }

        private void item_PropertyChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ReadPreferences()
        {
            SettingNumOfLights = Preferences.Get("NumOfLights", 5);
           // SettingServerActive = Preferences.Get("ServerActive", false);

        }

        private void SavePreferences()
        {
            //Preferences.Set("NumOfLights", SettingNumOfLights);
            //Preferences.Set("ServerActive", SettingServerActive);
            int count = 0;
            foreach (var element in Settings)
            {
                count++;
                Preferences.Set(element.Name, element.Value);
            }

        }

        public ICommand SaveCommand { private set; get; }
        public ICommand TestCommand { private set; get; }


        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
        
}
