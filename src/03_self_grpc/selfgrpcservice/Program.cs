using selfgrpcservice;
using selfgrpcservice.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Configuration.AddJsonFile("CHANNELAPPS.json", false, true);
builder.Services.Configure<List<MyChannelApp>>(builder.Configuration.GetSection("CHANNELAPPS"));
var channelAppsConfig = builder.Configuration.GetSection("CHANNELAPPS").Get<List<MyChannelApp>>();
builder.Services.AddSingleton(_ => builder.Configuration);


var app = builder.Build();
app.MapGrpcService<PresetDataService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
