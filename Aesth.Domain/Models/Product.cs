using System;
using System.Collections.Generic;

namespace Aesth.Domain.Models;

public class Product
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Type { get; set; } = null!;
    public double Price { get; set; }
    public string Color { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime Release { get; set; }
    public bool Availability { get; set; }
    public long Views { get; set; }

    public List<string> Images { get; set; } = new();
    public List<string> Details { get; set; } = new();
    public List<string> Sizes { get; set; } = new();
}