using System;
using System.Collections.Generic;
using common;
using Funq;
using ServiceStack;
using ServiceStack.Discovery.Redis;
using ServiceStack.Redis;

namespace TestService1.Core
{
    public class AppHost : AppHostBase
    {
        public AppHost() : base("TestService1", typeof(AppHost).Assembly)
        { }

        public override void Configure(Container container)
        {
            container.Register<IRedisClientsManager>(new RedisManagerPool("localhost:6379", new RedisPoolConfig { MaxPoolSize = 100, }));
            SetConfig(new HostConfig
            {
                WebHostUrl = "http://localhost:5000",
            });
            LoadPlugin(new RedisServiceDiscoveryFeature()
            {
                ExcludedTypes = new HashSet<Type> { typeof(ExcludedServiceByHashset) },
                //SetServiceGateway = (baseUrl, requestType) => new JsonServiceClient(baseUrl) { UserAgent = "Custom User Agent" },
                NeverRunViaLocalGateway = new HashSet<Type> { typeof(Echo) }
            });

        }
    }
}