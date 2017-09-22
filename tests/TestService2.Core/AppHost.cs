using System;
using common;
using Funq;
using ServiceStack;
using ServiceStack.Discovery.Redis;
using ServiceStack.Redis;
using TestService1.Core;

namespace TestService2.Core
{
    public class AppHost : AppHostBase
    {
        public AppHost() : base("TestService2", typeof(AppHost).Assembly)
        { }

        public override void Configure(Container container)
        {
            container.Register<IRedisClientsManager>(new RedisManagerPool("localhost:6379", new RedisPoolConfig { MaxPoolSize = 100, }));
            SetConfig(new HostConfig
            {
                WebHostUrl = "http://localhost:5002"
            });
            LoadPlugin(new RedisServiceDiscoveryFeature());
        }
    }

    public class TestService : Service
    {

        /// <summary>
        /// Makes outbound call to Service2
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public object Any(Service2CallsService1 req)
        {
            return Gateway.Send(req.ConvertTo<Service1External>());
        }

        /// <summary>
        /// Receives inbound call from Service 2
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public object Any(Service2External req)
        {
            return $"Service2 Received from: {req.From}";
        }

       

          public string Any(Echo req) => $"{HostContext.ServiceName} is echoing {req.Input}";


    }    
}