using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
using XamarinBlank.ViewModels;

namespace XamarinBlank
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listing : ContentPage
    {
        public ObservableCollection<Employee> Employees { get; private set; }
        private ListingViewModel vm;

        // ListView displays a collection as a vertical list. Why was there no need to declare this variable here?

        public Listing()
        {
            InitializeComponent();
            vm = new ListingViewModel();
        }

        // This method runs just before the page appears - overriding allows for calling of
        //      methods upon page display, for example.
        protected override async void OnAppearing()
        {
            base.OnAppearing(); // Inherit from base method to avoid missing essential code

            // The object created within a using statement will last until the block within the using finishes executing,
            //      after which it will be disposed of. Thus, the object must implement the interface IDisposable.
            using (UserDialogs.Instance.Loading("Loading", null, "Cancel", true, MaskType.Clear))
            {
                // The following must be awaited, else the line after it will execute before it finishes running.
                await vm.CallApi();
            }

            //// This is an alternative to the above block. ShowLoading() cannot be used within a using statement, because
            ////      it does not implement IDisposable.
            //UserDialogs.Instance.ShowLoading("Loading employee listing...", MaskType.Black);
            //await vm.CallApi();
            //UserDialogs.Instance.HideLoading();

            // This line of code gives context for the binding that will happen in the xaml file.
            // The source is bound to the target.
            // Here, the vm is bound to the view, so the vm is the source, and the view is the target.
            this.BindingContext = vm;
        }
    }
}