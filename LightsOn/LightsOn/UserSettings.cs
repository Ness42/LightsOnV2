using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace LightsOn
{
    class UserSettings
    {
        /// <summary>   
        /// This is the Settings static class that can be used in your Core solution or in any   
        /// of your client applications. All settings are laid out the same exact way with getters   
        /// and setters.     
        /// </summary>   
        public static class MyUserSettings
        {
            static ISettings AppSettings
            {
                get
                {
                    return CrossSettings.Current;
                }
            }
            public static string UserName
            {
                get => AppSettings.GetValueOrDefault(nameof(UserName), string.Empty);
                set => AppSettings.AddOrUpdateValue(nameof(UserName), value);
            }
            public static string State
            {
                get => AppSettings.GetValueOrDefault(nameof(State), string.Empty);
                set => AppSettings.AddOrUpdateValue(nameof(State), value);
            }
            public static string Email
            {
                get => AppSettings.GetValueOrDefault(nameof(Email), string.Empty);
                set => AppSettings.AddOrUpdateValue(nameof(Email), value);
            }
            public static string Password
            {
                get => AppSettings.GetValueOrDefault(nameof(Password), string.Empty);
                set => AppSettings.AddOrUpdateValue(nameof(Password), value);
            }
            public static void ClearAllData()
            {
                AppSettings.Clear();
            }
        }
    }
}
