using System.Threading.Tasks;

namespace ServiceBusSender
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            for (int i = 0; i < 25000; i++)
            {
                await Task.Run(() => MessageSender.SendMessageAsync($"Message {i} sent!").ConfigureAwait(false));
            }

            await MessageSender.Close().ConfigureAwait(false);
        }
    }
}