using ChessTournament.Test.Exceptions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebAppFac = Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<Program>;

namespace ChessTournament.Test.Helpers
{
    public static class WebAppFactoryExtensions
    {
        public static WebAppFac WithService<TService>(this WebAppFac factory, TService service, Type serviceType)
            where TService : class
        {
            if (service is null)
            {
                throw new ArgumentNullException("service");
            }

            if (serviceType is null)
            {
                throw new ArgumentNullException("serviceType");
            }

            if (!serviceType.IsAssignableFrom(typeof(TService)))
            {
                throw new NotAssignableServiceTypeException(serviceType, typeof(TService));
            }

            return factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var currentlyRegistered = services.SingleOrDefault(s => s.ServiceType == serviceType);

                    if (currentlyRegistered != null)
                    {
                        services.Remove(currentlyRegistered);
                    }

                    services.AddSingleton(service);
                });
            });
        }

        public static WebAppFac WithService<TService>(this WebAppFac factory, TService service)
            where TService : class
        {
            return WithService(factory, service, typeof(TService));
        }
    }
}
