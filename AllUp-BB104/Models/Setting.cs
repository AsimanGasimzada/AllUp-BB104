using AllUp_BB104.Models.Common;

namespace AllUp_BB104.Models;

public class Setting : BaseEntity
{
    public string Value { get; set; } = null!;
    public string Key { get; set; } = null!;
}
