using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AGTIV.SPApp.Template.Entities;
using AGTIV.SPApp.Template.Framework;
using Microsoft.SharePoint.Client;

namespace AGTIV.SPApp.Template.BusinessComponent
{
    public class SampleBL
    {
        
        public SampleViewModel DemoPost(SampleViewModel vm)
        {
            try
            {
                ProjectTokenHelper.CheckValidAccessToken(vm.spHostURL, vm.accessToken);
                using (ClientContext clientContext = TokenHelper.GetClientContextWithAccessToken(vm.spHostURL, vm.accessToken))
                {
                    vm.Title = "API Response: " + vm.Title;
                    vm.Description = "API Response: " + vm.Description;
                }
            }
            catch(Exception)
            {

            }
            
            return (vm);
        }

        public SampleTokenModel GetMyLogin(SampleTokenModel vm)
        {           
            SampleTokenModel result = new SampleTokenModel();
            try
            {
                ProjectTokenHelper.CheckValidAccessToken(vm.spHostURL, vm.accessToken);

                using (ClientContext clientContext = TokenHelper.GetClientContextWithAccessToken(vm.spHostURL, vm.accessToken))
                {
                    User spUser = clientContext.Web.CurrentUser;

                    clientContext.Load(spUser, user => user.Title);

                    clientContext.ExecuteQuery();

                    result.Name = spUser.Title;
                }

            }
            catch (Exception)
            {
                
            }
            return result;
        }
    }
}
