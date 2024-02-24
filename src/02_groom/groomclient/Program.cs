using Groom;
using Grpc.Net.Client;

namespace groomclient;
class Program
{
    static void Main(string[] args)
    {

        GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5025");
        GroomS.GroomSClient client = new GroomS.GroomSClient(channel);
        while (true)
        {
            Console.WriteLine("Enter room name for register:");
            string roomName = Console.ReadLine();

            var response = client.RegisterGroom(new RoomRegistrationRequest { RoomName = roomName });

            Console.WriteLine($"Room Id: {response.RoomId}");

            Console.WriteLine("Do you want to register another room? (Y/N)");
            if (Console.ReadKey().Key != ConsoleKey.Y){
                break;
            }
            Console.WriteLine();
        }

        Console.WriteLine("Program finished.");
        Console.Read();

    }
}