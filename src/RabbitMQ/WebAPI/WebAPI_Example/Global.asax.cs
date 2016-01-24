using RestBus.RabbitMQ;
using RestBus.RabbitMQ.Subscription;
using RestBus.WebApi;

using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebAPI_Example
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        RestBusHost restbusHost = null;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //*** Start RestBus subscriber/host **//

            var amqpUrl = "amqp://localhost:5672"; //AMQP URL for RabbitMQ installation
            var serviceName = "samba"; //Uniquely identifies this service

            var msgMapper = new BasicMessageMapper(amqpUrl, serviceName);
            var subscriber = new RestBusSubscriber(msgMapper);
            restbusHost = new RestBusHost(subscriber, GlobalConfiguration.Configuration);
            restbusHost.Start();

            //****//

        }

        protected void Application_End()
        {
            if (restbusHost != null)
                restbusHost.Dispose();

        }
    }
}