using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using ServiceBusConIssue.Services;
using System.Threading.Tasks;

namespace ServiceBusConIssue
{
    public class InboundFunction
    {
        private readonly ServiceBusService _serviceBusService;

        public InboundFunction(ServiceBusService serviceBusService)
        {
            _serviceBusService = serviceBusService;
        }

        [FunctionName("InboundFunction")]
        public async Task Run([ServiceBusTrigger("myqueue", Connection = "servicebus_con")] Message message, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message");

            await _serviceBusService.SendMessage(message).ConfigureAwait(false);
        }
    }
}