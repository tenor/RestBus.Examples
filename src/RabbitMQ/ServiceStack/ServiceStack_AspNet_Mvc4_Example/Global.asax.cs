using RestBus.RabbitMQ;
using RestBus.RabbitMQ.Subscriber;
using RestBus.ServiceStack;

using ServiceStack.WebHost.Endpoints;
using ServiceStack_AspNet_Example.api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ServiceStack_AspNet_Mvc4_Example
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        RestBusHost restbusHost = null;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            new AppHost().Init();

            //*** Start RestBus subscriber/host **//

            var amqpUrl = "amqp://localhost:5672"; //AMQP URL for RabbitMQ installation
            var serviceName = "samba"; //Uniquely identifies this service

            var msgMapper = new BasicMessageMapper(amqpUrl, serviceName);
            var subscriber = new RestBusSubscriber(msgMapper);
            restbusHost = new RestBusHost(subscriber);
            restbusHost.Start();

            //****//
        }

        protected void Application_End()
        {
            if (restbusHost != null)
                restbusHost.Stop();
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