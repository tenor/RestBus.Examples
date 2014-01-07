using ServiceStack.ServiceInterface;
using ServiceStack_AspNet_WebForms_Example.api.DTOs.Requests;
using ServiceStack_AspNet_WebForms_Example.api.DTOs.Responses;

namespace ServiceStack_AspNet_WebForms_Example.api.Services
{
    public class HelloService : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = "Hello, " + request.Name };
        }
    }
}