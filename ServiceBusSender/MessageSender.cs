using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusSender
{
    public static class MessageSender
    {
        private const string QueueName = "myqueue";

        private static readonly string ConStr;
        private static readonly IQueueClient Client;

        static MessageSender()
        {
            ConStr = Environment.GetEnvironmentVariable("servicebus_con");
            Client = new QueueClient(ConStr, QueueName);
        }

        public static async Task SendMessageAsync(string msg)
        {
            var message = new Message(Encoding.UTF8.GetBytes(msg))
            {
                Label = "Test",
                MessageId = Guid.NewGuid().ToString()
            };
            Console.WriteLine($"Sending message: {msg}");

            await Client.SendAsync(message).ConfigureAwait(false);
        }

        public static async Task Close()
        {
            await Client.CloseAsync().ConfigureAwait(false);
        }
    }
}