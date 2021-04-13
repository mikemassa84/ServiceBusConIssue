using Azure.Messaging.ServiceBus;
using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusConIssue.Services
{
    public class ServiceBusService
    {
        private readonly ServiceBusClient _serviceBusClient;

        public ServiceBusService()
        {
            _serviceBusClient = new ServiceBusClient(Environment.GetEnvironmentVariable("servicebus_con"));
        }

        public async Task SendMessage(Message message)
        {
            var sender = GetSender();

            await SendMessage(sender, message).ConfigureAwait(false);
        }

        private ServiceBusSender GetSender()
        {
            return _serviceBusClient.CreateSender("testdest");
        }

        private async Task SendMessage(ServiceBusSender sender, Message message)
        {
            await sender.SendMessageAsync(BuildServiceBusMessage(message)).ConfigureAwait(false);
        }

        private ServiceBusMessage BuildServiceBusMessage(Message message)
        {
            var msg = new ServiceBusMessage(Encoding.UTF8.GetString(message.Body));

            msg.Subject = message.Label ?? string.Empty;
            msg.CorrelationId = message.CorrelationId ?? string.Empty;
            msg.MessageId = message.MessageId ?? string.Empty;
            msg.SessionId = message.SessionId ?? string.Empty;

            foreach (var prop in message.UserProperties)
            {
                msg.ApplicationProperties.Add(prop.Key, prop.Value);
            }

            return msg;
        }
    }
}