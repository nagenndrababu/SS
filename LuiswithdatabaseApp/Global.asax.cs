using LuiswithdatabaseApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace LuiswithdatabaseApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static string _iconnectConnection;
        public static string IConnect_Connection
        {
            //ConfigurationManager.ConnectionStrings ["HRMS"].ConnectionString
            get
            {
                if (string.IsNullOrEmpty(_iconnectConnection))
                {
                    _iconnectConnection = Utils.Utils.GetConnectionString(BotConstants.Iconnect);
                }
                return _iconnectConnection;
            }
            set
            {
                _iconnectConnection = value;
            }
        }
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
