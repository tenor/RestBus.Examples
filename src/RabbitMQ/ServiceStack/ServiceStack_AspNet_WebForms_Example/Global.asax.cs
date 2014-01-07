using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using ServiceStack_AspNet_WebForms_Example;
using ServiceStack.WebHost.Endpoints;
using ServiceStack_AspNet_WebForms_Example.api.Services;

namespace ServiceStack_AspNet_WebForms_Example
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Initialize ServiceStack application
            new AppHost().Init();
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        public class AppHost : AppHostBase
        {
            //Tell Service Stack the name of your application and where to find your web services
            public AppHost() : base("Hello Web Services", typeof(HelloService).Assembly) { }

            public override void Configure(Funq.Container container)
            {
                //register any dependencies your services use, e.g:
                //container.Register<ICacheClient>(new MemoryCacheClient());

                //Set the servicestack app root
                SetConfig(new EndpointHostConfig { ServiceStackHandlerFactoryPath = "api" });
            }
        }
    }
}
