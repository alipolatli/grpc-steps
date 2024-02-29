using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Presetdata;

var channel = GrpcChannel.ForAddress("http://localhost:5009");
var presetDataClient = new PresetDataS.PresetDataSClient(channel);
var response = await presetDataClient.GetChannelAppsAsync(new Empty());
response.ChannelApps.ToList().ForEach(channelApp =>
{
    Console.WriteLine($"ChannelApp ID: {channelApp.Id}, Code: {channelApp.Code}, Description: {channelApp.Description}");
    
    if (channelApp.Keys?.Any() == true)
    {
        Console.WriteLine("Keys:");
        channelApp.Keys.ToList().ForEach(key =>
            Console.WriteLine($"  Required: {key.Required}, Key: {key.Key}")
        );
    }
    else
    {
        Console.WriteLine("No keys available.");
    }

    Console.WriteLine();
});
