using Microsoft.CodeAnalysis.Operations;

namespace AllUp_BB104.ViewModels;

public class ProductGetVM
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string MainImagePath { get; set; } = null!;
}