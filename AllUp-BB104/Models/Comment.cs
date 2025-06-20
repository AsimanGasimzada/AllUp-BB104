using AllUp_BB104.Models.Common;

namespace AllUp_BB104.Models;

public class Comment : BaseEntity
{
    public string Text { get; set; } = null!;
    public int Rating { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public string AppUserId { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
