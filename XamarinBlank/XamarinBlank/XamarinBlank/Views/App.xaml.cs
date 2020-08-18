using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinBlank
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Sets the main page to allow for navigation.
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
