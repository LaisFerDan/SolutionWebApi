using System;
using System.Text.Json.Serialization;

public class ChessPlayer
{
    public int id { get; set; }
    public string url { get; set; }
    public string name { get; set; }
    public string username { get; set; }
    public int best_rating { get; set; }
    public int wins { get; set; }
    public int loses { get; set; }
    public int number_of_games { get; set; }
    public int followers { get; set; }
    public string country { get; set; }
    public DateTime last_online {get; set;}
    public DateTime joined { get; set; }
}


