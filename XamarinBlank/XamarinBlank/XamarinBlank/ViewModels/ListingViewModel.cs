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

namespace XamarinBlank.ViewModels
{
    class ListingViewModel
    {
        // This must be set to static to be used within the Uri constructor.
        private static string webApiUrl = "https://sebdevoffshoreapi.azurewebsites.net/api/Employee/List";
        private Uri uri;
        private HttpClient httpClient;
        public ObservableCollection<Employee> Employees { get; private set; }

        public ListingViewModel()
        {
            uri = new Uri(webApiUrl);
            httpClient = new HttpClient();            
        }

        // Store values in local list??

        // Task represents an async operation that returns a value.
        // ObservableCollection: essentially a list that notifies the program when updated/refreshed. If a ListView is
        //      sourcing from an array/list, it does not update along with the data structure. With an ObservableCollection,
        //      though, the ListView will update along with the data structure.
        public async /*Task<ObservableCollection<Employee>>*/ Task CallApi()
        {
            try
            {
                // Use await here because GetAsync is defined as an awaitable/async method.
                // This line sends a get request to the specified URI as an asynchronous operation.
                HttpResponseMessage response = await httpClient.GetAsync(uri);

                // This line serialises the HTTP content within response to a string asynchronously, hence the use of await.
                // To determine the format of the data being passed into content, set a breakpoint and check the value of
                //      content during runtime.
                string content = await response.Content.ReadAsStringAsync();

                // Json formatting uses [] to denote the start and end of data. Individual entries are surrounded by {}.
                // Because the API only understands JSON, the string in content must be deserialised.
                // Here the data format matches that of the Employee class constructor, and so entries are converted into
                //      Employee objects stored within an ObservableCollection when the following line runs.
                Employees = JsonConvert.DeserializeObject<ObservableCollection<Employee>>(content);

                // A return statement here will result in the "not all code paths return a value" error message.
                // If an exception is hit here, nothing will be returned.
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            // For any return type Task<T>, call return T.
            // Therefore, for a return type Task, merely call return.

            // Returning here is ok, because if an exception is hit, a null object will be returned.
            return;
        }
    }
}
