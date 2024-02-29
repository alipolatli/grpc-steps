using System.Net.Http.Json;
using Google.Protobuf.WellKnownTypes;
using Groom;
using Grpc.Net.Client;

namespace newsbot;
class Program
{
    static async Task Main(string[] args)
    {
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5025");
        GroomS.GroomSClient client = new GroomS.GroomSClient(channel);
        using (HttpClient httpClient = new HttpClient())
        {
            var request = client.SendNewsFlash();
            for (int i = 1; i <= 10; i++)
            {
                var httpResponse = await httpClient.GetStringAsync($"https://jsonplaceholder.typicode.com/todos/{i}");
                await request.RequestStream.WriteAsync(new NewsFlash
                {
                    NewsItem = httpResponse,
                    NewsTime = Timestamp.FromDateTime(DateTime.UtcNow)
                });
                Console.WriteLine($"Send message: {i}");
            await Task.Delay(1000);
            }
            await request.RequestStream.CompleteAsync();
        }
        channel.Dispose();
    }
}
