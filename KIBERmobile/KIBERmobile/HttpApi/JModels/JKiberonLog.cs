using System;
using System.Collections.Generic;
using System.Text;

namespace KIBERmobile.HttpApi.JModels;

public class JKiberonLog
{
    public int id { get; set; }
    public string date { get; set; }
    public int sign { get; set; }
    public int amount { get; set; }
    public int balance { get; set; }
    public string comment { get; set; }
}