using AllUp_BB104.Models.Common;

namespace AllUp_BB104.Models;

public class ProductTag : BaseEntity
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public Tag Tag { get; set; }
    public int TagId { get; set; }
}