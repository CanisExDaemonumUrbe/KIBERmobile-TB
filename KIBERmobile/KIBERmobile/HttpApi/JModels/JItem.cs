using System;
using System.Collections.Generic;
using System.Text;

namespace KIBERmobile.HttpApi.JModels;

public class JItem
{
    public int id { get; set; }
    public int city_id { get; set; }
    public int active { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string image { get; set; }
    public int price { get; set; }
}