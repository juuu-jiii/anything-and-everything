using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework
{
    public class ConstantHelper
    {
        public class AppSetting
        {
            public static string APIUrl = ConfigurationManager.AppSettings["APIUrl"];
        }

        public class APIRoute
        {
            #region Demo
            public const string Sample_DemoPost = "api/Sample/DemoPost";
            public const string Sample_GetMyLogin = "api/Sample/GetMyLogin";

            #endregion
        }

        public class ConnectionStrings
        {
            public static string db = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        }

        public class SharePoint
        {
            public class List
            {
                public const string SampleList = "SampleList";

                public class SampleListColumn
                {
                    public const string SampleColumn = "SampleColumn";
                }
            }
        }
    }
}
