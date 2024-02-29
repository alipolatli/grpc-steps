using Google.Protobuf.WellKnownTypes;
using Groom;
using Grpc.Net.Client;

namespace groomadmin;
class Program
{
    static async Task Main(string[] args)
    {
        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5025");
        GroomS.GroomSClient client = new GroomS.GroomSClient(channel);
        var streamingCall = client.StartMonitoring(new Empty());
        CancellationTokenSource cancellationTokenSource = new();
        System.Console.WriteLine("Starting admin, welcome!");
        while(await streamingCall.ResponseStream.MoveNext(cancellationTokenSource.Token)){
            var response= streamingCall.ResponseStream.Current;
            Console.WriteLine($"News message : Time \n {response.NewsTime} User: \n {response.User} Contents: \n {response.Contents}");
        }
        System.Console.WriteLine("?");
        channel.Dispose();
    }
}
