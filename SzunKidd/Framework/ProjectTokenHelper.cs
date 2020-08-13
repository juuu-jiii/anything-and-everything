using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public static class ProjectTokenHelper
    {
        public static bool CheckValidAccessToken(String spHostURL, String accessToken)
        {
            bool IsValid = false;
            try
            {
                using (ClientContext clientContext = TokenHelper.GetClientContextWithAccessToken(spHostURL, accessToken))
                {
                    clientContext.ExecuteQuery();

                    IsValid = true;
                }
            }
            catch (System.Net.WebException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Check valid access token error. ", ex);
            }
            return IsValid;
        }
    }
}
