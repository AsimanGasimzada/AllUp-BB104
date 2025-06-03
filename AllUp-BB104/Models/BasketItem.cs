using AllUp_BB104.Models.Common;

namespace AllUp_BB104.Models;

public class BasketItem : BaseEntity
{
    public int Count { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public string AppUserId { get; set; } = null!;
    public AppUser AppUser { get; set; }
}
