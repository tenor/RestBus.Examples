using ServiceStack.ServiceHost;

namespace ServiceStack_AspNet_WebForms_Example.api.DTOs.Requests
{
    [Route("/hello")]
    [Route("/hello/{Name}")]
    public class Hello
    {
        public string Name { get; set; }
    }
}