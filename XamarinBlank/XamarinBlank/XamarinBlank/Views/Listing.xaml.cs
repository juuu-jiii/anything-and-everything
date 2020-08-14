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

namespace XamarinBlank
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Listing : ContentPage
    {
        // This must be set to static to be used within the Uri constructor.
        static string webApiUrl = "https://sebdevoffshoreapi.azurewebsites.net/api/Employee/List";
        Uri uri;
        HttpClient httpClient;
        
        public Listing()
        {
            InitializeComponent();
            uri = new Uri(webApiUrl);
            httpClient = new HttpClient();
        }

        // This method runs just before the page appears - overriding allows for calling of
        //      methods upon page display, for example.
        protected override void OnAppearing()
        {
            base.OnAppearing(); // Inherit from base method to avoid missing essential code
            CallApi();
        }

        // Store values in local list??

        // Task represents an async operation that returns a value.
        // ObservableCollection: essentially a list that notifies the program when updated/refreshed.
        public async /*Task<ObservableCollection<Employee>>*/ void CallApi()
        {
            try
            {
                // Use await here because GetAsync is defined as an awaitable/async method.
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                string content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(content);
            }
            catch
            {

            }
        }

    }
}