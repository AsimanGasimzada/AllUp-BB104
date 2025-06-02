using AllUp_BB104.Models.Common;

namespace AllUp_BB104.Models;

public class Tag : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<ProductTag> ProductTags { get; set; } = [];

}