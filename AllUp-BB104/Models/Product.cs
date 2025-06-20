﻿using AllUp_BB104.Models.Common;

namespace AllUp_BB104.Models;

public class Product : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string Description { get; set; } = null!;
    public int Rating { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; }

    public ICollection<ProductTag> ProductTags { get; set; } = [];
    public ICollection<ProductImage> ProductImages { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
}