using RestBus.ServiceStack;
using RestBus.RabbitMQ;
using RestBus.RabbitMQ.Subscriber;

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
        RestBusHost restbusHost = null;

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Initialize ServiceStack application
            new AppHost().Init();


            //*** Start RestBus subscriber/host **//

            var amqpUrl = "amqp:localhost:5672"; //AMQP URL for RabbitMQ installation
            var serviceName = "madagascar"; //Uniquely identifies this service

            var msgMapper = new BasicMessageMapper(amqpUrl, serviceName);
            var subscriber = new RestBusSubscriber(msgMapper);
            restbusHost = new RestBusHost(subscriber);
            restbusHost.Start();

            //****//
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

            if (restbusHost != null)
                restbusHost.Stop();

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
