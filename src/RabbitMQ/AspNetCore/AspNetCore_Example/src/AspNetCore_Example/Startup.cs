using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RestBus.RabbitMQ;
using RestBus.RabbitMQ.Subscription;
using RestBus.AspNet.Server;
using RestBus.AspNet;

namespace AspNetCore_Example
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc();

            // Create RestBus Subscriber
            var amqpUrl = "amqp://localhost:5672"; //AMQP URI for RabbitMQ server
            var serviceName = "samba"; //Uniquely identifies this service

            var msgMapper = new BasicMessageMapper(amqpUrl, serviceName);
            var subscriber = new RestBusSubscriber(msgMapper);

            bool standAlone = false;
            /* 
               This service listens for requests through both HTTP and the message broker. 
               If you desire a standalone service that only listens to the message broker:
               1. Set standAlone to true
               2. Update the hosting.json file with the instructions in the file.
            */

            if (standAlone)
            {
                // Configures the rest bus server -- needed if running standalone server, ignored otherwise.
                app.ConfigureRestBusServer(subscriber);
            }
            else
            {
                app.RunRestBusHost(subscriber);
            }
        }

        // Entry point for the application.
        public static void Main(string[] args)
        {
            var config = WebApplicationConfiguration.GetDefault(args);

            var application = new WebApplicationBuilder()
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .Build();

            application.Run();

        }
    }
}
