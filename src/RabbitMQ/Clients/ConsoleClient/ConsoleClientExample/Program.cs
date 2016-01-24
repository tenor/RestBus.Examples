using RestBus.Client;
using RestBus.RabbitMQ;
using RestBus.RabbitMQ.Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClientExample
{
    class Program
    {
        static void Main(string[] args)
        {

            var amqpUrl = "amqp://localhost:5672"; //AMQP URL for RabbitMQ server
            var serviceName = "samba"; //The unique identifier for the target service

            var msgMapper = new BasicMessageMapper(amqpUrl, serviceName);

            RestBusClient client = new RestBusClient(msgMapper);

            RequestOptions requestOptions = null;
            /* 
             * //Uncomment this section to get a response in JSON format (necessary when calling ServiceStack servers)
             * 
            requestOptions = new RequestOptions();
            requestOptions.Headers.Add("Accept", "application/json");
             */

            var response = SendMessage(client, requestOptions).Result;

            //Display response
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            client.Dispose();
            Console.ReadKey();

        }

        private async static Task<System.Net.Http.HttpResponseMessage> SendMessage(RestBusClient client, RequestOptions requestOptions)
        {
            //Send Request
            var uri = "api/values"; //Substitute "hello/random" for the ServiceStack self-hosted example and "api/hello/random" for the ServiceStack ASP.Net hosted example
            return await client.GetAsync(uri, requestOptions);
        }
    }
}
