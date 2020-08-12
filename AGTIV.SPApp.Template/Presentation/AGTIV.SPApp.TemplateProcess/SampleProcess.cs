using AGTIV.SPApp.Template.Entities;
using AGTIV.SPApp.Template.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.SPApp.TemplateProcess
{
    public class SampleProcess
    {

        public SampleViewModel SamplePost(SampleViewModel vm)
        {
            var client = new RestClient(ConstantHelper.AppSetting.APIUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            RestRequest request = new RestRequest(ConstantHelper.APIRoute.Sample_DemoPost, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<SampleViewModel> response = client.Execute<SampleViewModel>(request);
            vm = response.Data;

            return vm;
        }

        public string GetSampleGetMyLogin(SampleTokenModel vm)
        {
           
            InsertError(vm.spHostURL + "-" + vm.accessToken);
            var client = new RestClient(ConstantHelper.AppSetting.APIUrl);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            RestRequest request = new RestRequest(ConstantHelper.APIRoute.Sample_GetMyLogin, Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(vm);

            IRestResponse<SampleTokenModel> response = client.Execute<SampleTokenModel>(request);
            SampleTokenModel name = response.Data;

            return name.Name;
        }

        private void InsertError(string msg)
        {
            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=error;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"); ;
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO ErrorLog (Error) VALUES ('" + msg + "')", conn);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
