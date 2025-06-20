namespace AllUp_BB104.ViewModels;

public class ProductGetVM
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int Rating { get; set; }
    public string MainImagePath { get; set; } = null!;
}
