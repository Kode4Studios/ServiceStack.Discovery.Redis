using ServiceStack;
using ServiceStack.DataAnnotations;

namespace TestService1.Core
{
    public class Service1CallsService2 : IReturn<string>
    {
        public string From { get; set; }
    }

    [Restrict(RequestAttributes.Jsv)]
    public class Service1External : IReturn<string>
    {
        public string From { get; set; }
    }

    public class ExcludedServiceByHashset : IReturnVoid
    { }  

    [Restrict(AccessTo = RequestAttributes.InProcess)]
    public class CallEcho
    {
        public string Input { get; set; }
    }
}

namespace common
{
    [Exclude(Feature.ServiceDiscovery)]
    public class Echo : IReturn<string>
    {
        public string Input { get; set; }
    }
}