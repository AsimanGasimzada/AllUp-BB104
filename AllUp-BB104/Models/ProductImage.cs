using AllUp_BB104.Models.Common;

namespace AllUp_BB104.Models;

public class ProductImage : BaseEntity
{
    public string Path { get; set; } = null!;
    public Product Product { get; set; }
    public int ProductId { get; set; }
    public bool IsMain { get; set; } = false;
}
