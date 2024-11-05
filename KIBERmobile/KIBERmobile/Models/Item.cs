using System;
using System.Collections.Generic;
using System.Text;

namespace KIBERmobile.Models;

public class Item
{
    public int Id { get; set; }
    public int CityId { get; set; }
    public int Active { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? BigImages { get; set; }
    public string? IconImages { get; set; }
    public int Price { get; set; }
}