using System;
using System.Collections.Generic;
using System.Text;

namespace KIBERmobile.Models;

public class KiberonLog
{
    public int Id { get; set; }
    public string Date { get; set; }
    public string Sign { get; set; }
    public Color Color { get; set; }
    public int Amount { get; set; }
    public int Balance { get; set; }
    public string Comment { get; set; }
}