using AllUp_BB104.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace AllUp_BB104.Models;

public class Category : BaseEntity
{
    //[Required,MaxLength(100),MinLength(21)]
    public string Name { get; set; } = null!;
    public ICollection<Product> Products { get; set; } = [];
}
