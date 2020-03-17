using Xamarin.Forms;


namespace LightsOn.Views.PartialViews
{
    public partial class HomeComingPage : ContentPage
    {
        public HomeComingPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            DisplayAlert("Awesome", "Gesture recognizers in play!", "Great!");
        }
    }
}
