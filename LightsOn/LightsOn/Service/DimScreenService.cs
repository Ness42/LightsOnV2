using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Windows.Graphics.Display;

namespace LightsOn.Service
{
    class DimScreenService : IDimScreenService
    {
        private static BrightnessOverride brightnessOverride = null;
        private static System.Timers.Timer aTimer;

        public DimScreenService()
        {
            InitializeBrightnessOverride();
            SetTimer();
        }

        private static void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(3000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += SetBrightnessOverride;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void InitializeBrightnessOverride()
        {
            brightnessOverride = BrightnessOverride.GetForCurrentView();
        }

        public static void SetBrightnessOverride(Object source, System.Timers.ElapsedEventArgs e)
        {

            brightnessOverride.SetBrightnessLevel(0.00, DisplayBrightnessOverrideOptions.None);

            brightnessOverride.StartOverride();

        }

        public void ResetBrightnessOverride()
        {

            brightnessOverride.SetBrightnessLevel(0.80, DisplayBrightnessOverrideOptions.None);

            brightnessOverride.StartOverride();

        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }


    }
}
