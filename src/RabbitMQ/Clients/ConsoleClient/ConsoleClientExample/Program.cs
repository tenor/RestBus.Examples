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

            var amqpUrl = "amqp:localhost:5672"; //AMQP URL for RabbitMQ installation
            var serviceName = "madagascar"; //The unique identifier for the target service

            var msgMapper = new BasicMessageMapper(amqpUrl, serviceName);

            RestBusClient client = new RestBusClient(msgMapper);

            RequestOptions requestOptions = null;
            /* 
             * //Uncomment this section to get a response in JSON format
             * 
            requestOptions = new RequestOptions();
            requestOptions.Headers.Add("Accept", "application/json");
             */

            //Send Request
            var uri = "api/values"; //Substitute "hello/random" for the ServiceStack example
            var response = client.GetAsync(uri, requestOptions).Result;

            //Display reponse;
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            client.Dispose();
            Console.ReadKey();

        }
    }
}
