using Skoruba.IdentityServer4.Shared.Configuration.Configuration.Identity;
using AdminIdentityserver.STS.Identity.Configuration.Interfaces;

namespace AdminIdentityserver.STS.Identity.Configuration
{
    public class RootConfiguration : IRootConfiguration
    {      
        public AdminConfiguration AdminConfiguration { get; } = new AdminConfiguration();
        public RegisterConfiguration RegisterConfiguration { get; } = new RegisterConfiguration();
    }
}







