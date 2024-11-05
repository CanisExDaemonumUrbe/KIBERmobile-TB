using System;
using System.Collections.Generic;
using System.Text;

namespace KIBERmobile.HttpApi.JModels;

public class JNews
{
    public int id { get; set; }
    public int city_id { get; set; }
    public int section_id { get; set; }
    public int active { get; set; }
    public string title { get; set; }
    public string anons { get; set; }
    public string description { get; set; }
    public string image { get; set; }
    public string date { get; set; }
}