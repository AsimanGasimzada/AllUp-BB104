using AllUp_BB104.ViewModels.CommentViewModels;

namespace AllUp_BB104.ViewModels;

public class ProductDetailVM
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int Rating { get; set; }
    public string CategoryName { get; set; } = null!;
    public string BrandName { get; set; } = null!;
    public List<string> ProductImages { get; set; } = [];
    public List<string> TagNames { get; set; } = [];
    public List<CommentGetVM> Comments { get; set; } = [];

}