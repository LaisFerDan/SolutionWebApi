using System;
using System.Text.Json.Serialization;

public class ChessPlayer
{
    [JsonPropertyName("id")]
    public int id { get; set; }
    [JsonPropertyName("url")]
    public string url { get; set; }
    [JsonPropertyName("name")]
    public string name { get; set; }
    [JsonPropertyName("username")]
    public string username { get; set; }

    [JsonPropertyName("followers")]
    public int followers { get; set; }
    [JsonPropertyName("country")]
    public string country { get; set; }
    [JsonPropertyName("last_online")]
    public DateTime last_online {get; set;}
    [JsonPropertyName("joined")]
    public DateTime joined { get; set; }
}


