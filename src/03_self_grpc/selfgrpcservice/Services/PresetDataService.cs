using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Options;
using Presetdata;

namespace selfgrpcservice.Services;
public class PresetDataService : PresetDataS.PresetDataSBase
{
    private readonly List<MyChannelApp> _channelApps;

    public PresetDataService(IOptions<List<MyChannelApp>> channelApps)
    {
        _channelApps = channelApps.Value ?? throw new ArgumentNullException();
    }

    public override Task<ChannelAppRoot> GetChannelApps(Empty request, ServerCallContext context)
    {
        var channelApps = _channelApps.Select(c => new ChannelApp
        {
            Id = c.Id.ToString(),
            Code = c.Code,
            Description = c.Description,
            Keys = { c.Keys.Select(ck => new ChannelAppKey { Required = ck.Required, Key = ck.Key }) }
        });

        var channelAppRoot = new ChannelAppRoot
        {
            ChannelApps = { channelApps }
        };

        return Task.FromResult(channelAppRoot);
    }
}