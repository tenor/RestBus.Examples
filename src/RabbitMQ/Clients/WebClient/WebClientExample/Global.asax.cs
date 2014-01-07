using RestBus.RabbitMQ;
using RestBus.RabbitMQ.Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebClientExample;

namespace WebClientExample
{
    public class Global : HttpApplication
    {
        public static RestBusClient HelloServiceClient = null;

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Init RestBus client
            var amqpUrl = "amqp:localhost:5672"; //AMQP URL for RabbitMQ installation
            var serviceName = "madagascar"; //The unique identifier for the target service

            var msgMapper = new BasicMessageMapper(amqpUrl, serviceName);
            HelloServiceClient = new RestBusClient(msgMapper);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

            //Dispose restbus client
            if(HelloServiceClient != null)
                HelloServiceClient.Dispose();

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }
    }
}
