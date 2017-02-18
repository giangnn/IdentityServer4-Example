using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceStackApi
{
    using Funq;
    using global::ServiceStack;
    using global::ServiceStack.Authentication.IdentityServer;
    using global::ServiceStack.Authentication.IdentityServer.Enums;

    public class AppHost : AppHostBase
    {
        private readonly string serviceUrl;

        public AppHost(string serviceUrl)
         : base("UserAuthProvider.ServiceStack.Core.SelfHost", typeof(AppHost).GetAssembly())
        {
            this.serviceUrl = serviceUrl;
        }

        public override void Configure(Container container)
        {
            SetConfig(new HostConfig
            {
#if DEBUG
                DebugMode = true,
#endif
                WebHostUrl = serviceUrl
            });

            this.Plugins.Add(new IdentityServerAuthFeature
            {
                AuthProviderType = IdentityServerAuthProviderType.ServiceProvider,
                AuthRealm = "http://localhost:5000/",
                ClientId = "oauthClient",
                ClientSecret = "superSecretPassword",
                Scopes = "customAPI.read"
            });
        }
    }
}
