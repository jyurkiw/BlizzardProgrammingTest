﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using BlizzardProgrammingTest.Backend;

namespace BlizzardProgrammingTest
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        public void Application_End()
        {
            DBObject.Instance.Dispose();
        }
    }
}
