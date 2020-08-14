using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinBlank
{
    public partial class MainPage : ContentPage
    {
        // Constructor    
        public MainPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates an alert pop-up on-screen.
        /// </summary>
        /// await and async are used together in asynchronous methods. 
        /// An ansynchronous method must have the await keyword, and have a return type of void, Task, or Task<T>
        private async void DisplayAlert(object sender, EventArgs e)
        {
            await DisplayAlert("Alert", "This has been a friendly alert.", "OK");
        }

        // Event onClick method for top button
        // Navigation.PushModalAsync pushes the specified page onto the navigation stack.
        // Navigation.PopModalAsync pops the most recently pushed page off the navigation stack.
        private async void RedirectToListing(object sender, EventArgs e)
        {
            // The added page is a NavigationPage to allow for push/pop functionality.
            await Navigation.PushModalAsync(new NavigationPage(new Listing()));
        }
    }
}
