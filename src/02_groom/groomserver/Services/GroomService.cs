using Google.Protobuf.WellKnownTypes;
using Groom;
using Grpc.Core;

namespace groomserver.Services;

public class GroomService : GroomS.GroomSBase
{
    private readonly ILogger<GroomService> _logger;
    public GroomService(ILogger<GroomService> logger)
    {
        _logger = logger;
    }

    public override Task<RoomRegistrationResponse> RegisterGroom(RoomRegistrationRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Service called...");
        Random rnd = new Random();
        int roomNum = rnd.Next(1, 100);
        _logger.LogInformation($"Room no. {roomNum}");
        var resp = new RoomRegistrationResponse { RoomId = roomNum };
        return Task.FromResult(resp);
    }

    public override async Task<NewsStreamStatus> SendNewsFlash(IAsyncStreamReader<NewsFlash> requestStream, ServerCallContext context)
    {
        while (await requestStream.MoveNext())
        {
            NewsFlash news = requestStream.Current;
            _logger.LogInformation($"News Flash: {news.NewsItem}");
            MessagesQueue.EnqueueQueue(news);
        }
        return new NewsStreamStatus { Success = true };
    }

    public override async Task StartMonitoring(Empty request, IServerStreamWriter<ReceivedMessage> responseStream, ServerCallContext context)
    {
        while (true)
            if (MessagesQueue.QueueCount() > 0) await responseStream.WriteAsync(MessagesQueue.DequeueQueue());
    }
}
