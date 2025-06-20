namespace AllUp_BB104.Models.Common;

public class BaseAuditableEntity : BaseEntity
{
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? CreatedTime { get; set; }
    public DateTime? UpdatedTime { get; set; }
    public bool IsDeleted { get; set; } = false;
}