namespace AllUp_BB104.ViewModels;

public class ProductCreateVM
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public List<int> TagIds { get; set; } = [];
    public IFormFile MainImage { get; set; } = null!;
    public List<IFormFile> ProductImageFiles { get; set; } = [];
}
