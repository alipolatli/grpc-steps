using Google.Protobuf.WellKnownTypes;
using Groom;

namespace groomserver;

public class MessagesQueue  {
    private static Queue<ReceivedMessage> _queue;

    static MessagesQueue()  {
        _queue = new Queue<ReceivedMessage>();
    }

    public static void EnqueueQueue(NewsFlash news)  {
        var msg = new ReceivedMessage();
        msg.Contents = news.NewsItem;
        msg.User = "NewsBot";
        msg.NewsTime = Timestamp.FromDateTime(DateTime.UtcNow);
        _queue.Enqueue(msg);
    }

    public static ReceivedMessage DequeueQueue()  {
        return _queue.Dequeue();
    }  

    public static int QueueCount()  {
        return _queue.Count;
    }  
}