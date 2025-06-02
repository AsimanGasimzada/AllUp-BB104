namespace AllUp_BB104.ViewModels;

public class ProductUpdateVM
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
    public List<int> TagIds { get; set; } = [];
    public IFormFile? MainImage { get; set; }
    public string? MainImagePath { get; set; }
    public List<IFormFile> ProductImageFiles { get; set; } = [];
    public List<string> ProductImagePaths { get; set; } = [];
    public List<int> ProductImageIds { get; set; } = [];
}