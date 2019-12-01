using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using static LightsOn.UserSettings;

namespace LightsOn
{

    public class SettingsPageItem
    {        
        public string Name { set; get; }

        public bool State { set; get; }

        public int Value { set; get; }


        
    }
}
