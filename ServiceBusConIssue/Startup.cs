using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ServiceBusConIssue.Services;

[assembly: FunctionsStartup(typeof(ServiceBusConIssue.Startup))]

namespace ServiceBusConIssue
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<ServiceBusService>();
        }
    }
}