using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Infrastructure.TokenServıces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Infrastructure.Registrations
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler, Implementation.Servıces.TokenServıces.TokenHandler>();
            
        }
    }
}
