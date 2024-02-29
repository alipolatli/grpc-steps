﻿using System.Text.Json.Serialization;

namespace selfgrpcservice;

public class MyChannelApp
{
    [JsonPropertyName(name: "id")]
    public Guid Id { get; set; }

    [JsonPropertyName("code")]
    public string Code { get; set; } = null!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = null!;

    [JsonPropertyName("keys")]
    public List<MyChannelAppKey>? Keys { get; set; } = null;
}

public class MyChannelAppKey
{
    [JsonPropertyName("required")]
    public bool Required { get; set; }

    [JsonPropertyName("key")]
    public string Key { get; set; } = null!;
}